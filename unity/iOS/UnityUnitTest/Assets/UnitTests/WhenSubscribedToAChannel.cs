﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.ComponentModel;
using System.Threading;
using System.Collections;
using PubNubMessaging.Core;

namespace PubNubMessaging.Tests
{
    public class WhenSubscribedToAChannel//: UUnitTestCase
    {
        ManualResetEvent meSubscribeNoConnect = new ManualResetEvent(false);
        ManualResetEvent meSubscribeYesConnect = new ManualResetEvent(false);
        ManualResetEvent mePublish = new ManualResetEvent(false);
        ManualResetEvent meUnsubscribe = new ManualResetEvent(false);
        ManualResetEvent meAlreadySubscribed = new ManualResetEvent(false);
        ManualResetEvent meChannel1SubscribeConnect = new ManualResetEvent(false);
        ManualResetEvent meChannel2SubscribeConnect = new ManualResetEvent(false);
        ManualResetEvent meSubscriberManyMessages = new ManualResetEvent(false);

        bool receivedMessage = false;
        bool receivedConnectMessage = false;
        bool receivedAlreadySubscribedMessage = false;
        bool receivedChannel1ConnectMessage = false;
        bool receivedChannel2ConnectMessage = false;
        bool receivedManyMessages = false;

        int numberOfReceivedMessages = 0;

        [UUnitTest]
        public void ThenSubscribeShouldReturnReceivedMessage()
        {
			Debug.Log("Running ThenSubscribeShouldReturnReceivedMessage()");
            receivedMessage = false;
            Pubnub pubnub = new Pubnub("demo","demo","","",false);

            PubnubUnitTest unitTest = new PubnubUnitTest();
            unitTest.TestClassName = "WhenSubscribedToAChannel";
            unitTest.TestCaseName = "ThenSubscribeShouldReturnReceivedMessage";
            pubnub.PubnubUnitTest = unitTest;

            string channel = "hello_my_channel";

            pubnub.Subscribe<string>(channel, ReceivedMessageCallbackWhenSubscribed, SubscribeDummyMethodForConnectCallback, DummyErrorCallback);

            pubnub.Publish<string>(channel, "Test for WhenSubscribedToAChannel ThenItShouldReturnReceivedMessage", dummyPublishCallback, DummyErrorCallback);
            mePublish.WaitOne(310 * 1000);

            meSubscribeNoConnect.WaitOne(310 * 1000);
            pubnub.Unsubscribe<string>(channel, dummyUnsubscribeCallback, SubscribeDummyMethodForConnectCallback, UnsubscribeDummyMethodForDisconnectCallback, DummyErrorCallback);
            
            meUnsubscribe.WaitOne(310 * 1000);
            
            pubnub.EndPendingRequests();

            UUnitAssert.True(receivedMessage,"WhenSubscribedToAChannel --> ThenItShouldReturnReceivedMessage Failed");
        }

        [UUnitTest]
        public void ThenSubscribeShouldReturnConnectStatus()
        {
			Debug.Log("Running ThenSubscribeShouldReturnConnectStatus()");
            receivedConnectMessage = false;
            Pubnub pubnub = new Pubnub("demo", "demo", "", "", false);

            PubnubUnitTest unitTest = new PubnubUnitTest();
            unitTest.TestClassName = "WhenSubscribedToAChannel";
            unitTest.TestCaseName = "ThenSubscribeShouldReturnConnectStatus";
            pubnub.PubnubUnitTest = unitTest;

            string channel = "hello_my_channel";

            pubnub.Subscribe<string>(channel, ReceivedMessageCallbackYesConnect, ConnectStatusCallback, DummyErrorCallback);
            meSubscribeYesConnect.WaitOne(310 * 1000);

            pubnub.EndPendingRequests();

            UUnitAssert.True(receivedConnectMessage, "WhenSubscribedToAChannel --> ThenSubscribeShouldReturnConnectStatus Failed");
        }

        [UUnitTest]
        public void ThenMultiSubscribeShouldReturnConnectStatus()
        {
			Debug.Log("Running ThenMultiSubscribeShouldReturnConnectStatus()");
            receivedChannel1ConnectMessage = false;
            receivedChannel2ConnectMessage = false;
            Pubnub pubnub = new Pubnub("demo", "demo", "", "", false);

            PubnubUnitTest unitTest = new PubnubUnitTest();
            unitTest.TestClassName = "WhenSubscribedToAChannel";
            unitTest.TestCaseName = "ThenMultiSubscribeShouldReturnConnectStatus";
            pubnub.PubnubUnitTest = unitTest;


            string channel1 = "hello_my_channel1";
            pubnub.Subscribe<string>(channel1, ReceivedChannelUserCallback, ReceivedChannel1ConnectCallback, DummyErrorCallback);
            meChannel1SubscribeConnect.WaitOne(310 * 1000);

            string channel2 = "hello_my_channel2";
            pubnub.Subscribe<string>(channel2, ReceivedChannelUserCallback, ReceivedChannel2ConnectCallback, DummyErrorCallback);
            meChannel2SubscribeConnect.WaitOne(310 * 1000);

            pubnub.EndPendingRequests();

            UUnitAssert.True(receivedChannel1ConnectMessage && receivedChannel2ConnectMessage, "WhenSubscribedToAChannel --> ThenSubscribeShouldReturnConnectStatus Failed");
        }

        [UUnitTest]
        public void ThenDuplicateChannelShouldReturnAlreadySubscribed()
        {
			Debug.Log("Running ThenDuplicateChannelShouldReturnAlreadySubscribed()");
            receivedAlreadySubscribedMessage = false;
            Pubnub pubnub = new Pubnub("demo", "demo", "", "", false);

            PubnubUnitTest unitTest = new PubnubUnitTest();
            unitTest.TestClassName = "WhenSubscribedToAChannel";
            unitTest.TestCaseName = "ThenDuplicateChannelShouldReturnAlreadySubscribed";
            pubnub.PubnubUnitTest = unitTest;

            string channel = "hello_my_channel";

            pubnub.Subscribe<string>(channel, DummyMethodDuplicateChannelUserCallback1, DummyMethodDuplicateChannelConnectCallback, DummyErrorCallback);
            Thread.Sleep(100);
            
            pubnub.Subscribe<string>(channel, DummyMethodDuplicateChannelUserCallback2, DummyMethodDuplicateChannelConnectCallback, DuplicateChannelErrorCallback);
            meAlreadySubscribed.WaitOne();

            pubnub.EndPendingRequests();

            UUnitAssert.True(receivedAlreadySubscribedMessage, "WhenSubscribedToAChannel --> ThenDuplicateChannelShouldReturnAlreadySubscribed Failed");
        }

        private void ReceivedChannelUserCallback(string result)
        {
        }

        private void ReceivedChannel1ConnectCallback(string result)
        {
            if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(result.Trim()))
            {
                object[] deserializedMessage = new JsonFXDotNet().DeserializeToListOfObject(result).ToArray();
                if (deserializedMessage is object[])
                {
                    long statusCode = Int64.Parse(deserializedMessage[0].ToString());
                    string statusMessage = (string)deserializedMessage[1];
                    if (statusCode == 1 && statusMessage.ToLower() == "connected")
                    {
                        receivedChannel1ConnectMessage = true;
                    }
                }
            }
            meChannel1SubscribeConnect.Set();
        }

        private void ReceivedChannel2ConnectCallback(string result)
        {
            if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(result.Trim()))
            {
                object[] deserializedMessage = new JsonFXDotNet().DeserializeToListOfObject(result).ToArray();
                if (deserializedMessage is object[])
                {
                    long statusCode = Int64.Parse(deserializedMessage[0].ToString());
                    string statusMessage = (string)deserializedMessage[1];
                    if (statusCode == 1 && statusMessage.ToLower() == "connected")
                    {
                        receivedChannel2ConnectMessage = true;
                    }
                }
            }
            meChannel2SubscribeConnect.Set();
        }

        private void DummyMethodDuplicateChannelUserCallback1(string result)
        {
        }

        private void DummyMethodDuplicateChannelUserCallback2(string result)
        {
            if (result.Contains("already subscribed"))
            {
                receivedAlreadySubscribedMessage = true;
            }
            meAlreadySubscribed.Set();
        }

        private void DummyMethodDuplicateChannelConnectCallback(string result)
        {
        }

        private void ReceivedMessageCallbackWhenSubscribed(string result)
        {
            if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(result.Trim()))
            {
                object[] deserializedMessage = new JsonFXDotNet().DeserializeToListOfObject(result).ToArray();
                if (deserializedMessage is object[])
                {
                    object subscribedObject = (object)deserializedMessage[0];
                    if (subscribedObject != null)
                    {
                        receivedMessage = true;
                    }
                }
            }
            meSubscribeNoConnect.Set();
        }

        private void ReceivedMessageCallbackYesConnect(string result)
        {
            //dummy method provided as part of subscribe connect status check.
        }

        private void ConnectStatusCallback(string result)
        {
            if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(result.Trim()))
            {
                object[] deserializedMessage = new JsonFXDotNet().DeserializeToListOfObject(result).ToArray();
                if (deserializedMessage is object[])
                {
                    long statusCode = Int64.Parse(deserializedMessage[0].ToString());
                    string statusMessage = (string)deserializedMessage[1];
                    if (statusCode == 1 && statusMessage.ToLower() == "connected")
                    {
                        receivedConnectMessage = true;
                    }
                }
            }
            meSubscribeYesConnect.Set();
        }

        private void dummyPublishCallback(string result)
        {
            mePublish.Set();
        }

        private void dummyUnsubscribeCallback(string result)
        {
            
        }

        void SubscribeDummyMethodForConnectCallback(string receivedMessage)
        {
        }

        void UnsubscribeDummyMethodForDisconnectCallback(string receivedMessage)
        {
            meUnsubscribe.Set();
        }

        void DummyErrorCallback(PubnubClientError result)
        {
			//Debug.Log("WhenSubscribedToAChannel ErrorCallback" + result);
        }
		
        private void DuplicateChannelErrorCallback(PubnubClientError result)
        {
            if (result != null && result.Message.ToLower().Contains("already subscribed"))
            {
                receivedAlreadySubscribedMessage = true;
            }
            meAlreadySubscribed.Set();
        }
		
    }
}
