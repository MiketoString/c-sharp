﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubNubMessaging.Core;
using System.Net;
using System.IO;

namespace PubNubMessaging.Tests
{
    public class PubnubUnitTest : IPubnubUnitTest
    {
        private bool _enableStubTest = false;
        private string _testClassName = "";
        private string _testCaseName = "";

        public bool EnableStubTest
        {
            get
            {
                return _enableStubTest;
            }
            set
            {
                _enableStubTest = value;
            }
        }

        Dictionary<string, string> LoadWhenUnsubscribedToAChannelThenNonExistentChannelShouldReturnNotSubscribed()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
    #if ((!__MonoCS__) && (!SILVERLIGHT) && (!WINDOWS_PHONE))
          data.Add("/subscribe/demo/my%2Fchannel/0/0", "[[],\"13559006802662768\"]");
          data.Add("/subscribe/demo/my%2Fchannel/0/13559006802662768", "[[],\"13559006802662768\"]");
    #else
          data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
          data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[],\"13559006802662768\"]");
    #endif
          return data;
        }

        private Dictionary<string, string> LoadWhenUnsubscribedToAChannelThenShouldReturnUnsubscribedMessage()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
    #if ((!__MonoCS__) && (!SILVERLIGHT) && (!WINDOWS_PHONE))
          data.Add("/subscribe/demo/my%2Fchannel/0/0", "[[],\"13559006802662768\"]");
          data.Add("/subscribe/demo/my%2Fchannel/0/13559006802662768", "[[],\"13559006802662768\"]");
    #else
          data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
          data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[],\"13559006802662768\"]");
    #endif
          return data;
        }

        private Dictionary<string, string> LoadWhenSubscribedToAChannelThenSubscriberShouldBeAbleToReceiveManyMessages()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
          #if ((!__MonoCS__) && (!SILVERLIGHT) && (!WINDOWS_PHONE))
                data.Add("/subscribe/demo/my%2Fchannel/0/0", "[[],\"13602645380839594\"]");
                data.Add("/subscribe/demo/my%2Fchannel/0/13602645380839594", "[[742730406,1853970548,1899616327,1043229779,1270838952,788288787,627599385,1517373321,1202317119,184893837],\"13602645382888692\"]");
                data.Add("/subscribe/demo/my%2Fchannel/0/13602645382888692", "[[],\"13602645382888692\"]");
                data.Add("/v2/presence/sub_key/demo/channel/my%2Fchannel/leave", "{\"action\": \"leave\"}");
          #else
                data.Add("/subscribe/demo/testChannel/0/0", "[[],\"13602645380839594\"]");
                data.Add("/subscribe/demo/testChannel/0/13602645380839594", "[[742730406,1853970548,1899616327,1043229779,1270838952,788288787,627599385,1517373321,1202317119,184893837],\"13602645382888692\"]");
                data.Add("/subscribe/demo/testChannel/0/13602645382888692", "[[],\"13602645382888692\"]");
                data.Add("/v2/presence/sub_key/demo/channel/testChannel/leave", "{\"action\": \"leave\"}");
          #endif
          return data;
        }


        private Dictionary<string, string> LoadWhenSubscribedToAChannelThenDuplicateChannelShouldReturnAlreadySubscribed()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
          #if ((!__MonoCS__) && (!SILVERLIGHT) && (!WINDOWS_PHONE))
                data.Add("/subscribe/demo/my%2Fchannel/0/0", "[[],\"13559006802662768\"]");
                data.Add("/subscribe/demo/my%2Fchannel/0/13559006802662768", "[[],\"13559006802662768\"]");
          #else
                data.Add("/subscribe/demo/testChannel/0/0", "[[],\"13559006802662768\"]");
                data.Add("/subscribe/demo/testChannel/0/13559006802662768", "[[],\"13559006802662768\"]");
          #endif
          return data;
        }

        private Dictionary<string, string> LoadWhenSubscribedToAChannelThenMultiSubscribeShouldReturnConnectStatus()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
          #if ((!__MonoCS__) && (!SILVERLIGHT) && (!WINDOWS_PHONE))
                data.Add("/subscribe/demo/my%2Fchannel1/0/0", "[[],\"13559006802662768\"]");
                data.Add("/subscribe/demo/my%2Fchannel1/0/13559006802662768", "[[],\"13559006802662768\"]");
                
                data.Add("/subscribe/demo/my%2Fchannel1,my%2Fchannel2/0/0", "[[],\"13559006802662768\"]");
                data.Add("/subscribe/demo/my%2Fchannel1,my%2Fchannel2/0/13559006802662768", "[[],\"13559006802662768\"]");
          #else
                data.Add("/subscribe/demo/testChannel1/0/0", "[[],\"13559006802662768\"]");
                data.Add("/subscribe/demo/testChannel1/0/13559006802662768", "[[],\"13559006802662768\"]");
                    
                data.Add("/subscribe/demo/testChannel1,testChannel2/0/0", "[[],\"13559006802662768\"]");
                data.Add("/subscribe/demo/testChannel1,testChannel2/0/13559006802662768", "[[],\"13559006802662768\"]");
          #endif
          return data;
        }

        private Dictionary<string, string> LoadWhenAClientIsPresentedThenPresenceShouldReturnReceivedMessageCipher()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
          #if ((!__MonoCS__) && (!SILVERLIGHT) && (!WINDOWS_PHONE))
                data.Add("/subscribe/demo/hello_world-pnpres/0/0", "[[],\"13559007117760880\"]");
                data.Add("/subscribe/demo/hello_world-pnpres/0/13559007117760880", "[[{\"action\": \"join\", \"timestamp\": 1355929955, \"uuid\": \"eb4c1645-1319-4425-865f-008563009d67\", \"occupancy\": 1}],\"13559011560379628\"]");
                data.Add("/subscribe/demo/hello_world-pnpres/0/13559011560379628", "[[],\"13559011560379628\"]");
                data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
                data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[\"f7wNXpx8Ys8pVJNR5ZHT9g==\"],\"13559014566792816\"]");
                data.Add("/subscribe/demo/hello_world/0/13559014566792816", "[[],\"13559014566792816\"]");
          #else
                data.Add("/subscribe/demo/hello_world-pnpres/0/0", "[[],\"13559007117760880\"]");
                data.Add("/subscribe/demo/hello_world-pnpres/0/13559007117760880", "[[{\"action\": \"join\", \"timestamp\": 1355929955, \"uuid\": \"eb4c1645-1319-4425-865f-008563009d67\", \"occupancy\": 1}],\"13559011560379628\"]");
                data.Add("/subscribe/demo/hello_world-pnpres/0/13559011560379628", "[[],\"13559011560379628\"]");
                data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
                data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[\"f7wNXpx8Ys8pVJNR5ZHT9g==\"],\"13559014566792816\"]");      
                data.Add("/subscribe/demo/hello_world/0/13559014566792816", "[[],\"13559014566792816\"]");

          #endif
                    data.Add("/v2/presence/sub_key/demo/channel/hello_world/leave", "{\"action\": \"leave\"}");
          return data;
        }

        private Dictionary<string, string> LoadWhenAClientIsPresentedThenPresenceShouldReturnReceivedMessage()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
#if ((!__MonoCS__) && (!SILVERLIGHT) && (!WINDOWS_PHONE))
            data.Add("/subscribe/demo/hello_world-pnpres/0/0", "[[],\"13559007117760880\"]");
            data.Add("/subscribe/demo/hello_world-pnpres/0/13559007117760880", "[[{\"action\": \"join\", \"timestamp\": 1355929955, \"uuid\": \"eb4c1645-1319-4425-865f-008563009d67\", \"occupancy\": 1}],\"13559011560379628\"]");
            data.Add("/subscribe/demo/hello_world-pnpres/0/13559011560379628", "[[],\"13559011560379628\"]");
            data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
            data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[\"demo test for stubs\"],\"13559014566792816\"]");
            data.Add("/subscribe/demo/hello_world/0/13559014566792816", "[[],\"13559014566792816\"]");
#else
          data.Add("/subscribe/demo/hello_world-pnpres/0/0", "[[],\"13559007117760880\"]");
          data.Add("/subscribe/demo/hello_world-pnpres,hello_world/0/0", "[[{\"action\": \"join\", \"timestamp\": 1355929955, \"uuid\": \"eb4c1645-1319-4425-865f-008563009d67\", \"occupancy\": 1}],\"13559011560379628\"]");
          data.Add("/subscribe/demo/hello_world-pnpres/0/13559011560379628", "[[],\"13559011560379628\"]");
          data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
          data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[\"demo test for stubs\"],\"13559014566792816\"]");
          data.Add("/subscribe/demo/hello_world/0/13559014566792816", "[[],\"13559014566792816\"]");
#endif
            data.Add("/v2/presence/sub_key/demo/channel/hello_world/leave", "{\"action\": \"leave\"}");
            return data;
        }

        private Dictionary<string, string> LoadWhenAClientIsPresentedIfHereNowIsCalledThenItShouldReturnInfo()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/presence/sub_key/demo/channel/hello_world", "{\"uuids\":[\"eb4c1645-1319-4425-865f-008563009d67\"],\"occupancy\":1}");
            return data;
        }

        private Dictionary<string, string> LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfo()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/0/hello_world/0/%22Pubnub%20API%20Usage%20Example%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"Pubnub API Usage Example\"],13557486057035336,13559006802662769]");
            return data;
        }

        private Dictionary<string, string> LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoWhenEncrypted()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/0/hello_world/0/%225c69IWbJmfgAF18380MRmWe%2B1V3gHYH4Wxnlzm4l0RM%3D%22", "[1,\"Sent\",\"13559014566792817\"]");
            return data;
        }

        Dictionary<string, string> LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoWhenEncryptedAndSecretKeyed()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/73b3782110165adcecf3712fb382a2f8/hello_world/0/%225c69IWbJmfgAF18380MRmWe%2B1V3gHYH4Wxnlzm4l0RM%3D%22", "[1,\"Sent\",\"13559014566792817\"]");
            return data;
        }

        Dictionary<string, string> LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoForComplexMessage()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            //data.Add("/publish/demo/demo/0/hello_world/0/{\"foo\":\"hi!\",\"bar\":[1,2,3,4,5]}", "[1,\"Sent\",\"13609434515497075\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%7B%22foo%22%3A%22hi%21%22%2C%22bar%22%3A%5B1%2C2%2C3%2C4%2C5%5D%7D", "[1,\"Sent\",\"13609434515497075\"]");
            return data;    
        }

        Dictionary<string, string> LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoForComplexMessage2()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
          data.Add("/publish/demo/demo/73b3782110165adcecf3712fb382a2f8/hello_world/0/\"5c69IWbJmfgAF18380MRmWe+1V3gHYH4Wxnlzm4l0RM=\"", "[1,\"Sent\",\"13559014566792817\"]");
          return data;    
        }

        Dictionary<string, string> LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoForComplexMessage2WithSsl()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
          data.Add("/publish/demo/demo/73b3782110165adcecf3712fb382a2f8/hello_world/0/\"5c69IWbJmfgAF18380MRmWe+1V3gHYH4Wxnlzm4l0RM=\"", "[1,\"Sent\",\"13559014566792817\"]");
          return data;    
        }

        Dictionary<string, string> LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoForEncryptedComplexMessage2()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/0/hello_world/0/\"aIO7Y59pCGpQQVBc00QMwCQjxLLMr1CSz96f2meVch8Q+FVA/mBqbD4xE3cOt1hO4wZlhQwJimpbwlZiMH0NwhCCC5RAu3SDP46oLzkxXXlIj3KsgNUpZlP5ouuYkvo68p1CGc23AeYXK6PZNVjBbPvbzfiy93os6D3HGQcgMBIALie/PYFvJMb8/AuGGbiqaxTcf3YZu9jcp4IzntdH3jHY7P7o8FHYbxmmra99CCxOnDbV+8p7PPtT6b7LKsGcAXJcTIpg6WYduuv6T6Cy1uhsUzE+ejO+UsnqwcEm71s2jW0kh4uiywed46QUh2aO/C3NRHgeg7+MJjHmYJOURhmrZyntVeKobM4U3Ipiqfm3PxNX/iiv5GSzPUUQOYDW7UZn0rve8Uv929d4C4bl90v2w/NwzcTKXvGDMLidinrhsksa5HXPtYAQCOTDeXjqs3JxNZu+4LcsrBdTSgXyInovXOw93VTXWIGuJlblOyplmW+1hagg2aSkcGWYXQgn7xy0mQpxZEKO6PmtJxbX5rJeZwVjmOpYMj4WtsM9AUQHEloui031zgmGoy/s87GHuKYyjM22uPfPw0l3NRSfPZ9lFyBb6JIK0ydOlej/wGJBKiocv2RoYgPnid04pZRFYUqTuhBaHWcbOi7iHxjjJNYBTcTXL3jW/mRhnUncQrcYTXHRHpuzJ2IPkLEbAyz+/XdLXSB5soL+H+Dc4mYiWM6sQkon/BB6vjmex2JIOctMVCZPXmU+LFNKpqBJ6faNARUMQ+VqRpJqwT+VMcOQEjrBsbR3oKF7/dGmaxyq60o3fSrIyg4e+V/qvpkG4SM/zsIMGKja6MczE1NIz9mD+kDIqQyNPVjE5QLGOP4/RrPjooRV4Y+hQYTNPOzvDFyY\"", "[1,\"Sent\",\"13559014566792817\"]");
            return data;
        }

        private Dictionary<string, string> LoadWhenAMessageIsPublishedThenUnencryptPublishShouldReturnSuccessCodeAndInfo()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/0/hello_world/0/\"Pubnub Messaging API 1\"", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"Pubnub Messaging API 1\"],13557486057035336,13559006802662769]");
            return data;
        }

        private Dictionary<string, string> LoadWhenAMessageIsPublishedThenUnencryptObjectPublishShouldReturnSuccessCodeAndInfo()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/0/hello_world/0/%7B%22foo%22%3A%22hi%21%22%2C%22bar%22%3A%5B1%2C2%2C3%2C4%2C5%5D%7D", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[{\"foo\":\"hi!\",\"bar\":[1,2,3,4,5]}],13557486057035336,13559006802662769]");
            return data;
        }

        private Dictionary<string, string> LoadWhenAMessageIsPublishedThenEncryptObjectPublishShouldReturnSuccessCodeAndInfo()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/0/hello_world/0/%22nQTUCOeyWWgWh5NRLhSlhIingu92WIQ6RFloD9rOZsTUjAhD7AkMaZJVgU7l28e2%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"nQTUCOeyWWgWh5NRLhSlhIingu92WIQ6RFloD9rOZsTUjAhD7AkMaZJVgU7l28e2\"],13559215464464812,13559215464464812]");
            return data;
        }

        
        private Dictionary<string, string> LoadWhenAMessageIsPublishedThenEncryptPublishShouldReturnSuccessCodeAndInfo()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/0/hello_world/0/%22%2BBY5%2FmiAA8aeuhVl4d13Kg%3D%3D%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"+BY5/miAA8aeuhVl4d13Kg==\"],13557486057035336,13559006802662769]");
            return data;
        }

        private Dictionary<string, string> LoadWhenAMessageIsPublishedThenSecretKeyWithEncryptPublishShouldReturnSuccessCodeAndInfo()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/3f75435fcd800f5d0476fc0fb5b572d1/hello_world/0/%22f42pIQcWZ9zbTbH8cyLwB%2FtdvRxjFLOYcBNMVKeHS54%3D%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"f42pIQcWZ9zbTbH8cyLwB/tdvRxjFLOYcBNMVKeHS54=\"],13559191494674157,13559191494674157]");
            return data;
        }

        private Dictionary<string, string> LoadWhenAMessageIsPublishedThenOptionalSecretKeyShouldBeProvidedInConstructor()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/d759c756abbd45a9864adc7f2b91393e/hello_world/0/%22Pubnub%20API%20Usage%20Example%22", "[1,\"Sent\",\"13559014566792817\"]");
            return data;
        }

        private Dictionary<string, string> LoadWhenAMessageIsPublishedIfSSLNotProvidedThenDefaultShouldBeFalse()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/0/hello_world/0/%22Pubnub%20API%20Usage%20Example%22", "[1,\"Sent\",\"13559014566792817\"]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestEncryptedDetailedHistory()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"EyKRq4Bzi1V44Lwz9i/cZw==\",\"BIWan5yOqIz3a0hAinJJ9Q==\",\"2x9t3rbm6Jr6YcCCuWCRdQ==\",\"NmPPeaSVnChejVR44rFJ5Q==\",\"y4FSc7Y9IEEoEmtDAJO3FQ==\",\"QByvge9lb/3H008RfX+VRA==\",\"tEJ1HKlGhYklpZqZLUDQjA==\",\"XZGNx138XpiwS5aVESXuYg==\",\"ayWFXhv+qv09Gj+I/ooNQQ==\",\"4N2LhvhnPG3v3bvWuqEb0g==\"],13561926677985130,13561926705714509]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestUnencryptedDetailedHistory()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"0\",\"1\",\"2\",\"3\",\"4\",\"5\",\"6\",\"7\",\"8\",\"9\"],13561931614319981,13561931641037537]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedDetailedHistoryDecryptedExample()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"f7wNXpx8Ys8pVJNR5ZHT9g==\"],13561993102217562,13561993102217562]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedItShouldReturnDetailedHistory()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"Test Message\"],13561916644576302,13561916644576302]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestEncryptedSecretDetailedHistoryParams1()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"EyKRq4Bzi1V44Lwz9i/cZw==\",\"BIWan5yOqIz3a0hAinJJ9Q==\",\"2x9t3rbm6Jr6YcCCuWCRdQ==\",\"NmPPeaSVnChejVR44rFJ5Q==\",\"y4FSc7Y9IEEoEmtDAJO3FQ==\"],13561997459447496,13561997470537187]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestEncryptedSecretDetailedHistoryParams2()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"QByvge9lb/3H008RfX+VRA==\",\"tEJ1HKlGhYklpZqZLUDQjA==\",\"XZGNx138XpiwS5aVESXuYg==\",\"ayWFXhv+qv09Gj+I/ooNQQ==\",\"4N2LhvhnPG3v3bvWuqEb0g==\"],13561997475925030,13561997486798712]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestEncryptedSecretDetailedHistoryParams3()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"EyKRq4Bzi1V44Lwz9i/cZw==\",\"BIWan5yOqIz3a0hAinJJ9Q==\",\"2x9t3rbm6Jr6YcCCuWCRdQ==\",\"NmPPeaSVnChejVR44rFJ5Q==\",\"y4FSc7Y9IEEoEmtDAJO3FQ==\"],13561997459447496,13561997470537187]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestUnencryptedSecretDetailedHistoryParams1()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"0\",\"1\",\"2\",\"3\",\"4\"],13561998607085158,13561998618677990]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestUnencryptedSecretDetailedHistoryParams2()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"5\",\"6\",\"7\",\"8\",\"9\"],13561998626205890,13561998636560986]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestUnencryptedSecretDetailedHistoryParams3()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"0\",\"1\",\"2\",\"3\",\"4\"],13561998607085158,13561998618677990]");
            return data;        
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestEncryptedDetailedHistoryParams1()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"EyKRq4Bzi1V44Lwz9i/cZw==\",\"BIWan5yOqIz3a0hAinJJ9Q==\",\"2x9t3rbm6Jr6YcCCuWCRdQ==\",\"NmPPeaSVnChejVR44rFJ5Q==\",\"y4FSc7Y9IEEoEmtDAJO3FQ==\"],13561885846793689,13561885857459163]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestEncryptedDetailedHistoryParams2()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"QByvge9lb/3H008RfX+VRA==\",\"tEJ1HKlGhYklpZqZLUDQjA==\",\"XZGNx138XpiwS5aVESXuYg==\",\"ayWFXhv+qv09Gj+I/ooNQQ==\",\"4N2LhvhnPG3v3bvWuqEb0g==\"],13561885862589838,13561885872731649]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestEncryptedDetailedHistoryParams3()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"EyKRq4Bzi1V44Lwz9i/cZw==\",\"BIWan5yOqIz3a0hAinJJ9Q==\",\"2x9t3rbm6Jr6YcCCuWCRdQ==\",\"NmPPeaSVnChejVR44rFJ5Q==\",\"y4FSc7Y9IEEoEmtDAJO3FQ==\"],13561885846793689,13561885857459163]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestUnencryptedDetailedHistoryParams1()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"0\",\"1\",\"2\",\"3\",\"4\"],13561969547888925,13561969560429174]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestUnencryptedDetailedHistoryParams2()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"5\",\"6\",\"7\",\"8\",\"9\"],13561969565962377,13561969576984085]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedTestUnencryptedDetailedHistoryParams3()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"0\",\"1\",\"2\",\"3\",\"4\"],13561969547888925,13561969560429174]");
            return data;        
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedDetailHistoryCount10ReturnsRecords()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"f42pIQcWZ9zbTbH8cyLwB/tdvRxjFLOYcBNMVKeHS54=\",\"Pubnub API Usage Example\",\"nQTUCOeyWWgWh5NRLhSlhIingu92WIQ6RFloD9rOZsTUjAhD7AkMaZJVgU7l28e2\",\"Pubnub API Usage Example\",\"nQTUCOeyWWgWh5NRLhSlhIingu92WIQ6RFloD9rOZsTUjAhD7AkMaZJVgU7l28e2\",\"+BY5/miAA8aeuhVl4d13Kg==\",\"Pubnub API Usage Example\",\"f42pIQcWZ9zbTbH8cyLwB/tdvRxjFLOYcBNMVKeHS54=\",{\"foo\":\"hi!\",\"bar\":[1,2,3,4,5]},\"Pubnub Messaging API 1\"],13559191494674157,13559319777162196]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedDetailHistoryCount10ReverseTrueReturnsRecords()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"Pubnub API Usage Example\",\"nQTUCOeyWWgWh5NRLhSlhIingu92WIQ6RFloD9rOZsTUjAhD7AkMaZJVgU7l28e2\",\"+BY5/miAA8aeuhVl4d13Kg==\",\"Pubnub API Usage Example\",\"f42pIQcWZ9zbTbH8cyLwB/tdvRxjFLOYcBNMVKeHS54=\",{\"foo\":\"hi!\",\"bar\":[1,2,3,4,5]},\"Pubnub Messaging API 1\",\"DetailedHistoryStartTimeWithReverseTrue 13557486100000000 0\",\"DetailedHistoryStartTimeWithReverseTrue 13557486100000000 1\",\"DetailedHistoryStartTimeWithReverseTrue 13557486100000000 3\"],13557486057035336,13557486128690220]");
            return data;
        }

        private Dictionary<string, string> LoadWhenDetailedHistoryIsRequestedDetailedHistoryStartWithReverseTrue()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/v2/history/sub-key/demo/channel/hello_world", "[[\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 0\",\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 1\",\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 2\",\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 3\",\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 4\",\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 4\",\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 6\",\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 7\",\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 8\",\"DetailedHistoryStartTimeWithReverseTrue 13559326410000000 9\"],13559326456056557,13559327017296315]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%200%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%201%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%202%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%203%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%204%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%205%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%206%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%207%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%208%22", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/%22DetailedHistoryStartTimeWithReverseTrue%209%22", "[1,\"Sent\",\"13559014566792817\"]");
            return data;
        }

        private Dictionary<string, string> LoadWhenGetRequestServerTimeThenItShouldReturnTimeStamp()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/time/0", "[13559011090230537]");
            return data;
        }

        private Dictionary<string, string> LoadWhenSubscribedToAChannelThenItShouldReturnReceivedMessageCipherForComplexMessage()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
          data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
          data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[\"Zbr7pEF/GFGKj1rOstp0tWzA4nwJXEfj+ezLtAr8qqE=\"],\"13608494042542696\"]");
          data.Add("/subscribe/demo/hello_world/0/13608494042542696", "[[],\"13608494042542696\"]");
          data.Add("/publish/demo/demo/0/hello_world/0/\"%22Zbr7pEF%2FGFGKj1rOstp0tWzA4nwJXEfj%2BezLtAr8qqE%3D%22\"", "[1,\"Sent\",\"13559014566792817\"]");
          data.Add("/v2/presence/sub_key/demo/channel/hello_world/leave", "{\"action\": \"leave\"}");
          return data;
        }
        
        private Dictionary<string, string> LoadWhenSubscribedToAChannelThenItShouldReturnReceivedMessageForComplexMessage()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
          data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
          data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[{\"foo\":\"hi!\",\"bar\":[1,2,3,4,5]}],\"13608497080247044\"]");
          data.Add("/subscribe/demo/hello_world/0/13608497080247044", "[[],\"13608497080247044\"]");
          data.Add("/publish/demo/demo/0/hello_world/0/\"%7B%22foo%22%3A%22hi%21%22%2C%22bar%22%3A%5B1%2C2%2C3%2C4%2C5%5D%7D\"", "[1,\"Sent\",\"13559014566792817\"]");
          data.Add("/v2/presence/sub_key/demo/channel/hello_world/leave", "{\"action\": \"leave\"}");
          return data;
        }

        private Dictionary<string, string> LoadWhenSubscribedToAChannelThenSubscribeShouldReturnReceivedMessageCipher()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
            data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[\"f7wNXpx8Ys8pVJNR5ZHT9g==\"],\"13559014566792816\"]");
            data.Add("/subscribe/demo/hello_world/0/13559014566792816", "[[],\"13559014566792816\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/\"f7wNXpx8Ys8pVJNR5ZHT9g==\"", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/v2/presence/sub_key/demo/channel/hello_world/leave", "{\"action\": \"leave\"}");
            return data;
        }

        private Dictionary<string, string> LoadWhenSubscribedToAChannelThenSubscribeShouldReturnReceivedMessage()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
            data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[\"Test Message\"],\"13559014566792816\"]");
            data.Add("/subscribe/demo/hello_world/0/13559014566792816", "[[],\"13559014566792816\"]");
            data.Add("/publish/demo/demo/0/hello_world/0/\"Test Message\"", "[1,\"Sent\",\"13559014566792817\"]");
            data.Add("/v2/presence/sub_key/demo/channel/hello_world/leave", "{\"action\": \"leave\"}");
            return data;
        }

        private Dictionary<string, string> LoadWhenSubscribedToAChannelThenSubscribeShouldReturnConnectStatus()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
            data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[],\"13559006802662768\"]");
            data.Add("/v2/presence/sub_key/demo/channel/hello_world/leave", "{\"action\": \"leave\"}");
            return data;
        }

        private Dictionary<string, string> LoadWhenAClientIsPresentedThenPresenceShouldReturnCustomUUID()
        {
          Dictionary<string, string> data = new Dictionary<string, string>();
          #if ((!__MonoCS__) && (!SILVERLIGHT) && (!WINDOWS_PHONE))
            data.Add("/subscribe/demo/hello_world-pnpres/0/0", "[[],\"13559007117760880\"]");
            data.Add("/subscribe/demo/testChannel/0/13559007117760880", "[[{\"action\": \"join\", \"timestamp\": 1355929955, \"uuid\": \"CustomSessionUUIDTest\", \"occupancy\": 1}],\"13559011560379628\"]");
            data.Add("/subscribe/demo/hello_world-pnpres/0/13559011560379628", "[[],\"13559011560379628\"]");
            data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559006802662768\"]");
            data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[\"f7wNXpx8Ys8pVJNR5ZHT9g==\"],\"13559014566792816\"]");
            data.Add("/subscribe/demo/hello_world/0/13559014566792816", "[[],\"13559014566792816\"]");              
          #else
                data.Add("/subscribe/demo/hello_world-pnpres/0/0", "[[],\"13559007117760880\"]");
                data.Add("/v2/presence/sub_key/demo/channel/hello_world", "{\"uuids\":[\"1b31e299-0c55-4e0b-b1da-04243dd1b4aa\",\"CustomSessionUUIDTest\"],\"occupancy\":2}");
                data.Add("/subscribe/demo/hello_world/0/0", "[[],\"13559011560379628\"]");
                data.Add("/subscribe/demo/hello_world/0/13559011560379628", "[[],\"13559014566792816\"]");              
                data.Add("/subscribe/demo/hello_world/0/13559007117760880", "[[{\"action\": \"join\", \"timestamp\": 1355929955, \"uuid\": \"CustomSessionUUIDTest\", \"occupancy\": 1}],\"13559011560379628\"]");
                data.Add("/subscribe/demo/hello_world/0/13559006802662768", "[[\"f7wNXpx8Ys8pVJNR5ZHT9g==\"],\"13559014566792816\"]");
                data.Add("/subscribe/demo/hello_world/0/13559014566792816", "[[],\"13559014566792816\"]");              

          #endif
              data.Add("/v2/presence/sub_key/demo/channel/hello_world/leave", "{\"action\": \"leave\"}");
          return data;
        }

        Dictionary<string, string> LoadWhenAMessageIsPublishedThenLargeMessageShoudFailWithMessageTooLargeInfo()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("/publish/demo/demo/0/my/channel/0/%22This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20This%20is%20a%20large%20message%20test%20which%20will%20return%20an%20error%20message.%20%22", "[0,\"Message Too Large\",\"13559014566792817\"]");
            return data;
        }
        
        public string GetStubResponse(HttpWebRequest request)
        {
            Uri requestUri = request.RequestUri;

            Dictionary<string,string> responseDictionary = null;
            string stubResponse = "!! Stub Response Not Assigned !!";
            //string lookUpString = request.PathAndQuery;

            switch (_testClassName)
            {
                case "WhenAClientIsPresented":
                    switch (_testCaseName)
                    {
                        case "ThenPresenceShouldReturnReceivedMessage":
                          responseDictionary = LoadWhenAClientIsPresentedThenPresenceShouldReturnReceivedMessage();
                          break;
                        case "ThenPresenceShouldReturnReceivedMessageCipher":
                          responseDictionary = LoadWhenAClientIsPresentedThenPresenceShouldReturnReceivedMessageCipher();
                          break;
                        case "IfHereNowIsCalledThenItShouldReturnInfo":
                          responseDictionary = LoadWhenAClientIsPresentedIfHereNowIsCalledThenItShouldReturnInfo();
                          break;
                        case "ThenPresenceShouldReturnCustomUUID":
                          responseDictionary = LoadWhenAClientIsPresentedThenPresenceShouldReturnCustomUUID();
                          break;
                        default:
                            break;
                    }
                    break;
                case "WhenAMessageIsPublished":
                    switch (_testCaseName)
                    {

                        case "ThenItShouldReturnSuccessCodeAndInfoForEncryptedComplexMessage2":
                            responseDictionary = LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoForEncryptedComplexMessage2();
                            break;
                        case "ThenItShouldReturnSuccessCodeAndInfoForComplexMessage2":
                            responseDictionary = LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoForComplexMessage2();
                            break;
                        case "ThenItShouldReturnSuccessCodeAndInfoForComplexMessage2WithSsl":
                            responseDictionary = LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoForComplexMessage2WithSsl();
                             break;
                        case "ThenItShouldReturnSuccessCodeAndInfoForComplexMessage":
                            responseDictionary = LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoForComplexMessage();
                            break;
                        case "ThenItShouldReturnSuccessCodeAndInfoWhenEncryptedAndSecretKeyed":
                            responseDictionary = LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoWhenEncryptedAndSecretKeyed();
                            break;
                        case "ThenItShouldReturnSuccessCodeAndInfoWhenEncrypted":
                            responseDictionary = LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfoWhenEncrypted();
                            break;
                        case "ThenItShouldReturnSuccessCodeAndInfo":
                            responseDictionary = LoadWhenAMessageIsPublishedThenItShouldReturnSuccessCodeAndInfo();
                            break;
                        case "ThenUnencryptPublishShouldReturnSuccessCodeAndInfo":
                            responseDictionary = LoadWhenAMessageIsPublishedThenUnencryptPublishShouldReturnSuccessCodeAndInfo();
                            break;
                        case "ThenUnencryptObjectPublishShouldReturnSuccessCodeAndInfo":
                            responseDictionary = LoadWhenAMessageIsPublishedThenUnencryptObjectPublishShouldReturnSuccessCodeAndInfo();
                            break;
                        case "ThenEncryptObjectPublishShouldReturnSuccessCodeAndInfo":
                            responseDictionary = LoadWhenAMessageIsPublishedThenEncryptObjectPublishShouldReturnSuccessCodeAndInfo();
                            break;
                        case "ThenEncryptPublishShouldReturnSuccessCodeAndInfo":
                            responseDictionary = LoadWhenAMessageIsPublishedThenEncryptPublishShouldReturnSuccessCodeAndInfo();
                            break;
                        case "ThenSecretKeyWithEncryptPublishShouldReturnSuccessCodeAndInfo":
                            responseDictionary = LoadWhenAMessageIsPublishedThenSecretKeyWithEncryptPublishShouldReturnSuccessCodeAndInfo();
                            break;
                        case "ThenOptionalSecretKeyShouldBeProvidedInConstructor":
                            responseDictionary = LoadWhenAMessageIsPublishedThenOptionalSecretKeyShouldBeProvidedInConstructor();
                            break;
                        case "IfSSLNotProvidedThenDefaultShouldBeFalse":
                            responseDictionary = LoadWhenAMessageIsPublishedIfSSLNotProvidedThenDefaultShouldBeFalse();
                            break;
                        case "ThenLargeMessageShoudFailWithMessageTooLargeInfo":
                            responseDictionary = LoadWhenAMessageIsPublishedThenLargeMessageShoudFailWithMessageTooLargeInfo();    
                            break;
                        default:
                            break;
                    }
                    break;
                case "WhenDetailedHistoryIsRequested":
                    switch (_testCaseName)
                    {

                        case "DetailedHistoryDecryptedExample":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedDetailedHistoryDecryptedExample();
                            break;
                        case "TestUnencryptedSecretDetailedHistoryParams1":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestUnencryptedSecretDetailedHistoryParams1();
                            break;
                        case "TestUnencryptedSecretDetailedHistoryParams2":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestUnencryptedSecretDetailedHistoryParams2();
                            break;
                        case "TestUnencryptedSecretDetailedHistoryParams3":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestUnencryptedSecretDetailedHistoryParams3();
                            break;
                        case "TestEncryptedSecretDetailedHistoryParams1":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestEncryptedSecretDetailedHistoryParams1();
                            break;
                        case "TestEncryptedSecretDetailedHistoryParams2":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestEncryptedSecretDetailedHistoryParams2();
                            break;
                        case "TestEncryptedSecretDetailedHistoryParams3":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestEncryptedSecretDetailedHistoryParams3();
                            break;
                        case "TestUnencryptedDetailedHistoryParams1":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestUnencryptedDetailedHistoryParams1();
                            break;
                        case "TestUnencryptedDetailedHistoryParams2":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestUnencryptedDetailedHistoryParams2();
                            break;
                        case "TestUnencryptedDetailedHistoryParams3":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestUnencryptedDetailedHistoryParams3();
                            break;
                        case "TestEncryptedDetailedHistory":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestEncryptedDetailedHistory();
                            break;
                        case "TestUnencryptedDetailedHistory":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestUnencryptedDetailedHistory();
                            break;
                        case "ItShouldReturnDetailedHistory":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedItShouldReturnDetailedHistory();
                            break;
                        case "TestEncryptedDetailedHistoryParams1":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestEncryptedDetailedHistoryParams1();
                            break;
                        case "TestEncryptedDetailedHistoryParams2":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestEncryptedDetailedHistoryParams2();
                            break;
                        case "TestEncryptedDetailedHistoryParams3":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedTestEncryptedDetailedHistoryParams3();
                            break;
                        case "DetailHistoryCount10ReturnsRecords":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedDetailHistoryCount10ReturnsRecords();
                            break;
                        case "DetailHistoryCount10ReverseTrueReturnsRecords":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedDetailHistoryCount10ReverseTrueReturnsRecords();
                            break;
                        case "DetailedHistoryStartWithReverseTrue":
                            responseDictionary = LoadWhenDetailedHistoryIsRequestedDetailedHistoryStartWithReverseTrue();
                            break;
                        default:
                            break;
                    }
                    break;
                case "WhenGetRequestServerTime":
                    switch (_testCaseName)
                    {
                        case "ThenItShouldReturnTimeStamp":
                            responseDictionary = LoadWhenGetRequestServerTimeThenItShouldReturnTimeStamp();
                            break;
                        default:
                            break;
                    }
                    break;
                case "WhenSubscribedToAChannel":
                    switch (_testCaseName)
                    {
                        case "ThenSubscriberShouldBeAbleToReceiveManyMessages":
                          responseDictionary = LoadWhenSubscribedToAChannelThenSubscriberShouldBeAbleToReceiveManyMessages();
                          break;
                        case "ThenDuplicateChannelShouldReturnAlreadySubscribed":
                          responseDictionary = LoadWhenSubscribedToAChannelThenDuplicateChannelShouldReturnAlreadySubscribed();
                          break;
                        case "ThenMultiSubscribeShouldReturnConnectStatus":
                          responseDictionary = LoadWhenSubscribedToAChannelThenMultiSubscribeShouldReturnConnectStatus();
                          break;
                        case "ThenItShouldReturnReceivedMessageForComplexMessage":
                            responseDictionary = LoadWhenSubscribedToAChannelThenItShouldReturnReceivedMessageForComplexMessage();
                            break;    
                        case "ThenItShouldReturnReceivedMessageCipherForComplexMessage":
                          responseDictionary = LoadWhenSubscribedToAChannelThenItShouldReturnReceivedMessageCipherForComplexMessage();
                          break;
                        case "ThenSubscribeShouldReturnReceivedMessageCipher":
                            responseDictionary = LoadWhenSubscribedToAChannelThenSubscribeShouldReturnReceivedMessageCipher();
                            break;    
                        case "ThenSubscribeShouldReturnReceivedMessage":
                            responseDictionary = LoadWhenSubscribedToAChannelThenSubscribeShouldReturnReceivedMessage();
                            break;
                        case "ThenSubscribeShouldReturnConnectStatus":
                            responseDictionary = LoadWhenSubscribedToAChannelThenSubscribeShouldReturnConnectStatus();
                            break;
                        default:
                            break;
                    }
                    break;
                case "WhenUnsubscribedToAChannel":
                  switch (_testCaseName)
                  {
                    case "ThenShouldReturnUnsubscribedMessage":
                      responseDictionary = LoadWhenUnsubscribedToAChannelThenShouldReturnUnsubscribedMessage();
                      break;
                    case "ThenNonExistentChannelShouldReturnNotSubscribed":
                      responseDictionary = LoadWhenUnsubscribedToAChannelThenNonExistentChannelShouldReturnNotSubscribed();
                      break;
                    default:
                      break;
                  }
                  break;
                default:
                    break;
            }
            if (responseDictionary != null && responseDictionary.ContainsKey(requestUri.AbsolutePath))
            {
              stubResponse = responseDictionary[requestUri.AbsolutePath];
              if (_testClassName == "WhenAMessageIsPublished" && _testCaseName == "ThenLargeMessageShoudFailWithMessageTooLargeInfo")
              {
                PubnubWebResponse stubWebResponse = new PubnubWebResponse(new MemoryStream(Encoding.UTF8.GetBytes(stubResponse)), HttpStatusCode.BadRequest);
                WebException largeMessageException = new WebException("The remote server returned an error: (400) Bad Request", null, WebExceptionStatus.ProtocolError, stubWebResponse);
                throw largeMessageException;
              }
            }
            else
            {
              stubResponse = "!! Stub Response Not Assigned !!";
            }

      return stubResponse;
        }

        public string TestCaseName
        {
            get
            {
                return _testCaseName;
            }
            set
            {
                _testCaseName = value;
            }
        }


        public string TestClassName
        {
            get
            {
                return _testClassName;
            }
            set
            {
                _testClassName = value;
            }
        }
    }
}
