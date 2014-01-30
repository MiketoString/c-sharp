using System;
using PubNubMessaging.Core;
using NUnit.Framework;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PubNubMessaging.Tests
{
  [TestFixture]
  public class EncryptionTests
  {
    /// <summary>
    /// Tests the yay decryption.
    /// Assumes that the input message is deserialized  
    /// Decrypted string should match yay!
    /// </summary>
    [Test]
    public void TestYayDecryptionBasic ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      string message= "q/xJqqN6qbiZMXYmiQC1Fw==";
      //decrypt
      string decrypted = pubnubCrypto.Decrypt(message);

      Assert.True(("yay!").Equals(decrypted));
    }
    /// <summary>
    /// Tests the yay encryption.
    /// The output is not serialized
    /// Encrypted string should match q/xJqqN6qbiZMXYmiQC1Fw==
    /// </summary>
    [Test]
    public void TestYayEncryptionBasic ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //deserialized string
      string message= "yay!";
      //Encrypt
      string encrypted= pubnubCrypto.Encrypt(message);
	  Assert.True(("q/xJqqN6qbiZMXYmiQC1Fw==").Equals(encrypted));
    }
    /// <summary>
    /// Tests the yay decryption.
    /// Assumes that the input message is not deserialized  
    /// Decrypted and Deserialized string should match yay!
    /// </summary>
    [Test]
    public void TestYayDecryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //Non deserialized string
      string message= "\"Wi24KS4pcTzvyuGOHubiXg==\"";

      //Deserialize 
      message = Common.Deserialize<string>(message);

      //decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      //deserialize again
      message= Common.Deserialize<string>(decrypted);
	  Assert.True(("yay!").Equals(message));
    }
    /// <summary>
    /// Tests the yay encryption.
    /// The output is not serialized
    /// Encrypted string should match Wi24KS4pcTzvyuGOHubiXg==
    /// </summary>
    [Test]
    public void TestYayEncryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //deserialized string
      string message= "yay!";
      //serialize the string
      message = Common.Serialize(message);
      Console.WriteLine(message);
      //Encrypt
      string encrypted= pubnubCrypto.Encrypt(message);
	  Assert.True(("Wi24KS4pcTzvyuGOHubiXg==").Equals(encrypted));
    }
    
    /// <summary>
    /// Tests the array encryption.
    /// The output is not serialized
    /// Encrypted string should match the serialized object
    /// </summary>
    [Test]
    public void TestArrayEncryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //create an empty array object
      object [] objArr = {};
      string strArr = Common.Serialize(objArr);
      //Encrypt
      string encrypted= pubnubCrypto.Encrypt(strArr);
      
	  Assert.True(("Ns4TB41JjT2NCXaGLWSPAQ==").Equals(encrypted));
    }
    
    /// <summary>
    /// Tests the array decryption.
    /// Assumes that the input message is deserialized
    /// And the output message has to been deserialized.
    /// Decrypted string should match the serialized object
    /// </summary>
    [Test]
    public void TestArrayDecryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //Input the deserialized string
      string message= "Ns4TB41JjT2NCXaGLWSPAQ==";
      //decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      //create a serialized object
      object [] objArr = {};
      string result= Common.Serialize(objArr);
      //compare the serialized object and the return of the Decrypt method
	  Assert.True((result).Equals(decrypted));
    }
    
    /// <summary>
    /// Tests the object encryption.
    /// The output is not serialized
    /// Encrypted string should match the serialized object
    /// </summary>
    [Test]
    public void TestObjectEncryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //create an object
      Object obj = new Object();
      //serialize
      string strObj = Common.Serialize(obj);
      //encrypt
      string encrypted= pubnubCrypto.Encrypt(strObj);
      
	  Assert.True (("IDjZE9BHSjcX67RddfCYYg==").Equals(encrypted));
    }
    /// <summary>
    /// Tests the object decryption.
    /// Assumes that the input message is deserialized
    /// And the output message has to be deserialized.
    /// Decrypted string should match the serialized object
    /// </summary>
    [Test]
    public void TestObjectDecryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //Deserialized
      string message= "IDjZE9BHSjcX67RddfCYYg==";
      //Decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      //create an object
      Object obj = new Object();
      //Serialize the object
      string result= Common.Serialize(obj);
      
	  Assert.True((decrypted).Equals(result));
    }
    /// <summary>
    /// Tests my object encryption.
    /// The output is not serialized 
    /// Encrypted string should match the serialized object
    /// </summary>
    [Test]
    public void TestMyObjectEncryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //create an object of the custom class
      CustomClass cc = new CustomClass();
      //serialize it
      string result= Common.Serialize(cc);
      //encrypt it
      string encrypted= pubnubCrypto.Encrypt(result);
      
	  Assert.True(("Zbr7pEF/GFGKj1rOstp0tWzA4nwJXEfj+ezLtAr8qqE=").Equals(encrypted));
    }
    /// <summary>
    /// Tests my object decryption.
    /// The output is not deserialized
    /// Decrypted string should match the serialized object
    /// </summary>
    [Test]
    public void TestMyObjectDecryption()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //Deserialized
      string message= "Zbr7pEF/GFGKj1rOstp0tWzA4nwJXEfj+ezLtAr8qqE=";
      //Decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      //create an object of the custom class
      CustomClass cc = new CustomClass();
      //Serialize it
      string result= Common.Serialize(cc);
      
	  Assert.True ((decrypted).Equals(result));
    }
    
    /// <summary>
    /// Tests the pub nub encryption2.
    /// The output is not serialized
    /// Encrypted string should match f42pIQcWZ9zbTbH8cyLwB/tdvRxjFLOYcBNMVKeHS54=
    /// </summary>
    [Test]
    public void TestPubNubEncryption2 ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //Deserialized
      string message= "Pubnub Messaging API 2";
      //serialize the message
      message= Common.Serialize(message);
      //encrypt
      string encrypted= pubnubCrypto.Encrypt(message);
      
	  Assert.True(("f42pIQcWZ9zbTbH8cyLwB/tdvRxjFLOYcBNMVKeHS54=").Equals(encrypted));
    }
    
    /// <summary>
    /// Tests the pub nub decryption2.
    /// Assumes that the input message is deserialized  
    /// Decrypted and Deserialized string should match Pubnub Messaging API 2
    /// </summary>
    [Test]
    public void TestPubNubDecryption2 ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //Deserialized string    
      string message= "f42pIQcWZ9zbTbH8cyLwB/tdvRxjFLOYcBNMVKeHS54=";
      //Decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      //Deserialize
      message = Common.Deserialize<string>(decrypted);
	  Assert.True(("Pubnub Messaging API 2").Equals(message));
    }
    
    /// <summary>
    /// Tests the pub nub encryption1.
    /// The input is not serialized
    /// Encrypted string should match f42pIQcWZ9zbTbH8cyLwByD/GsviOE0vcREIEVPARR0=
    /// </summary>
    [Test]
    public void TestPubNubEncryption1 ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //non serialized string
      string message= "Pubnub Messaging API 1";
      //serialize
      message= Common.Serialize(message);
      //encrypt
      string encrypted= pubnubCrypto.Encrypt(message);
      
	  Assert.True(("f42pIQcWZ9zbTbH8cyLwByD/GsviOE0vcREIEVPARR0=").Equals(encrypted));
    }
    
    /// <summary>
    /// Tests the pub nub decryption1.
    /// Assumes that the input message is  deserialized  
    /// Decrypted and Deserialized string should match Pubnub Messaging API 1        
    /// </summary>
    [Test]
    public void TestPubNubDecryption1 ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //deserialized string
      string message= "f42pIQcWZ9zbTbH8cyLwByD/GsviOE0vcREIEVPARR0=";
      //decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      //deserialize
      message= Common.Deserialize<string>(decrypted);
	  Assert.True(("Pubnub Messaging API 1").Equals(message));
    }
    
    /// <summary>
    /// Tests the stuff can encryption.
    /// The input is serialized
    /// Encrypted string should match zMqH/RTPlC8yrAZ2UhpEgLKUVzkMI2cikiaVg30AyUu7B6J0FLqCazRzDOmrsFsF
    /// </summary>
    [Test]
    public void TestStuffCanEncryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //input serialized string
      string message= "{\"this stuff\":{\"can get\":\"complicated!\"}}";
      //encrypt
      string encrypted= pubnubCrypto.Encrypt(message);
      
	  Assert.True(("zMqH/RTPlC8yrAZ2UhpEgLKUVzkMI2cikiaVg30AyUu7B6J0FLqCazRzDOmrsFsF").Equals(encrypted));
    }
    
    /// <summary>
    /// Tests the stuffcan decryption.
    /// Assumes that the input message is  deserialized  
    /// </summary>
    [Test]
    public void TestStuffcanDecryption()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //deserialized string
      string message= "zMqH/RTPlC8yrAZ2UhpEgLKUVzkMI2cikiaVg30AyUu7B6J0FLqCazRzDOmrsFsF";
      //decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      
	  Assert.True(("{\"this stuff\":{\"can get\":\"complicated!\"}}").Equals(decrypted));
    }
    
    /// <summary>
    /// Tests the hash encryption.
    /// The input is serialized
    /// Encrypted string should match GsvkCYZoYylL5a7/DKhysDjNbwn+BtBtHj2CvzC4Y4g=
    /// </summary>
    [Test]
    public void TestHashEncryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //serialized string
      string message= "{\"foo\":{\"bar\":\"foobar\"}}";
      //encrypt
      string encrypted= pubnubCrypto.Encrypt(message);
      
	  Assert.True(("GsvkCYZoYylL5a7/DKhysDjNbwn+BtBtHj2CvzC4Y4g=").Equals(encrypted));
    }
    
    /// <summary>
    /// Tests the hash decryption.
    /// Assumes that the input message is  deserialized  
    /// </summary>        
    [Test]
    public void TestHashDecryption()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //deserialized string
      string message= "GsvkCYZoYylL5a7/DKhysDjNbwn+BtBtHj2CvzC4Y4g=";
      //decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      
	  Assert.True(("{\"foo\":{\"bar\":\"foobar\"}}").Equals(decrypted));
    }
    
    /// <summary>
    /// Tests the null encryption.
    /// The input is serialized
    /// </summary>
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestNullEncryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //serialized string
      string message= null;
      //encrypt
      string encrypted= pubnubCrypto.Encrypt(message);
      
	  Assert.True(("").Equals(encrypted));
    }
    
    /// <summary>
    /// Tests the null decryption.
    /// Assumes that the input message is  deserialized  
    /// </summary>        
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestNullDecryption()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      //deserialized string
      string message= null;
      //decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      
	  Assert.True(("").Equals(decrypted));
    }
    /// <summary>
    /// Tests the unicode chars encryption.
    /// The input is not serialized
    /// </summary>
    [Test]
    public void TestUnicodeCharsEncryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      string message= "漢語";
      message= Common.Serialize(message);
      Console.WriteLine(message);
      string encrypted= pubnubCrypto.Encrypt(message);
      Console.WriteLine(encrypted);
	  Assert.True(("+BY5/miAA8aeuhVl4d13Kg==").Equals(encrypted));
    }
    /// <summary>
    /// Tests the unicode decryption.
    /// Assumes that the input message is  deserialized  
    /// Decrypted and Deserialized string should match the unicode chars       
    /// </summary>
    [Test]
    public void TestUnicodeCharsDecryption()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      string message= "+BY5/miAA8aeuhVl4d13Kg==";
      //decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      //deserialize
      message= Common.Deserialize<string>(decrypted);
      
	  Assert.True(("漢語").Equals(message));
    }
    /// <summary>
    /// Tests the german chars decryption.
    /// Assumes that the input message is  deserialized  
    /// Decrypted and Deserialized string should match the unicode chars  
    /// </summary>
    [Test]
    public void TestGermanCharsDecryption()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      string message= "stpgsG1DZZxb44J7mFNSzg==";
      //decrypt
      string decrypted= pubnubCrypto.Decrypt(message);
      //deserialize
      message= Common.Deserialize<string>(decrypted);
      
	  Assert.True(("ÜÖ").Equals(message));
    }
    /// <summary>
    /// Tests the german encryption.
    /// The input is not serialized
    /// </summary>
    [Test]
    public void TestGermanCharsEncryption ()
    {
      PubnubCrypto pubnubCrypto = new PubnubCrypto("enigma");
      string message= "ÜÖ";
      message= Common.Serialize(message);
      Console.WriteLine(message);
      string encrypted= pubnubCrypto.Encrypt(message);
      Console.WriteLine(encrypted);

	  Assert.True(("stpgsG1DZZxb44J7mFNSzg==").Equals(encrypted));
    }
    
    static string EncodeNonAsciiCharacters( string value ) {
      StringBuilder encodedString = new StringBuilder();
      foreach( char c in value ) {
        if( c > 127 ) {
          // This character is too big for ASCII
          string encodedValue = "\\u" + ((int) c).ToString( "x4" );
          encodedString.Append( encodedValue );
        }
        else {
          encodedString.Append( c );
        }
      }
      return encodedString.ToString();
    }
    
    static string DecodeEncodedNonAsciiCharacters( string value ) {
      return Regex.Replace(
        value,
        @"\\u(?<Value>[a-zA-Z0-9]{4})",
        m => {
        return ((char) int.Parse( m.Groups["Value"].Value, NumberStyles.HexNumber )).ToString();
      } );
    }
  }
}


