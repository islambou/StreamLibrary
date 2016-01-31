# StreamLibrary
A library with codecs for Videos, Remote desktop, Animation and more

Features
---
* [Driver supported] The Unsafe Codecs do support drivers by heart, use pointers to archieve this
* [Unsafe Codec support] Use Pointers to gain more performance!
* [Managed Codec support] use the managed Bitmap/Image classes
* [Multiple Codecs to chose from] There are 5 Unsafe and 6 Managed Codecs


Managed codecs
---
* DirectDriver Codec:
I barely used this as it was a long time ago, I would have to look at the code/debug again but I guess it does what the name says

* MJPG Codec:
This is a very simple MJPG Codec which can even be used for real life situations, webbrowsers etc, It is using the exact same method

* QuickCachedStream Codec:
This will cache all the image(s) and trying to quickly process it to keep the performance up, this codec will use cache in the hope to drop ALOT of data to gain higher performance over the internet
Keep in mind that this codec could be unstable or not working correctly, needs more testing

* QuickStream Codec:
The name says what it will do, it will quickly stream your images (remote desktop, animations) so fast as possible with plain simple Motion Detection

* SmallCachedStream Codec:
This codec is a modified version of the "QuickStreamCodec" but with caching in the hope it will gain performance over the internet

* SmallStream Codec:
I don't know exactly what the SmallStream did, I have to look at it again

Unsafe Codecs
---
* UnsafeCache Codec:
I would need to look up what it does again but I'm pretty sure the name explains it all

* UnsafeCachedStream Codec:
I would need to look up what it does again but I'm pretty sure the name explains it all

* UnsafeOptimized Codec:
It could be stable or not need to test this completely,
But the idea behind this codec is to really optimize the performance for if there are any video's running for example in Remote Desktop
Or specific animations running in the video, it is able to detect multiple video's running in image(s) and to optimize and focus performance at those animation/video's in the image
It sounds complex perhaps, but it will speedup video a little bit if there is any running

* UnsafeQuick Stream:
This is an unsafe codec version as the "QuickStreamCodec", I think

* UnsafeStream Codec:
This is a very good choice and even RECOMMENDED to use for everything as this is the most stable codec and the most reliable codec I made which supports Drivers and more
It does not contain any caching but yet again, It is really recommended to use over any of the other codecs that are in the library


How to use any codec
---
* Encoding
```C#
IUnsafeCodec UnsafeCodec = new UnsafeStreamCodec(80); //initialize our Codec
Bitmap bmp = (Bitmap)Bitmap.FromFile("Some file"); // It's just an image, could be of RemoteDesktop, video, anything, it's a image
int width = bmp.Width;
int height = bmp.Height;
BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat); //since it's a unsafe codec we need the LockBits to grab the pointer

using (MemoryStream stream = new MemoryStream())
{
    UnsafeCodec.CodeImage(bmpData.Scan0,
                          new Rectangle(0, 0, width, height), //the area of the image to use
                          new Size(width, height), //give the total size of the image
                          bmp.PixelFormat, //the format of the image
                          stream); //where the bytes will be stored in
} 
```

* Decoding
```C#
IUnsafeCodec UnsafeCodec = new UnsafeStreamCodec(80); //initialize our Codec
Bitmap DecodedImage = UnsafeCodec.DecodeData(new MemoryStream(YOUR_BYTES_OR_STREAM)); //where is "YOUR_BYTES_OR_STREAM" should be what it says
```
