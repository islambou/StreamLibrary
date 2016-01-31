using StreamLibrary.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

/*
    The MIT License (MIT)

    Copyright (c) 2016 AnguisCaptor

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

namespace StreamLibrary.Codecs
{
    /// <summary>
    /// The M-JPG codec is not very efficient for networking as it is just a very simple codec
    /// </summary>
    public class MJPGCodec : IVideoCodec
    {
        public override event IVideoCodec.VideoCodeProgress onVideoStreamCoding;
        public override event IVideoCodec.VideoDecodeProgress onVideoStreamDecoding;
        public override event IVideoCodec.VideoDebugScanningDelegate onCodeDebugScan;
        public override event IVideoCodec.VideoDebugScanningDelegate onDecodeDebugScan;

        public override ulong CachedSize
        {
            get { return 0; }
            internal set { }
        }

        public override int BufferCount
        {
            get { return 0; }
        }

        public override CodecOption CodecOptions
        {
            get { return CodecOption.None; }
        }

        public MJPGCodec(int ImageQuality = 100)
            : base(ImageQuality)
        {

        }

        public override void CodeImage(Bitmap bitmap, Stream outStream)
        {
            lock (base.jpgCompression)
            {
                byte[] data = base.jpgCompression.Compress(bitmap);
                outStream.Write(BitConverter.GetBytes(data.Length), 0, 4);
                outStream.Write(data, 0, data.Length);
            }
        }

        public override Bitmap DecodeData(Stream inStream)
        {
            lock (base.jpgCompression)
            {
                if (!inStream.CanRead)
                    throw new Exception("Must have access to Read in the Stream");

                byte[] temp = new byte[4];
                inStream.Read(temp, 0, temp.Length);
                int DataLength = BitConverter.ToInt32(temp, 0);
                temp = new byte[DataLength];
                inStream.Read(temp, 0, temp.Length);
                return (Bitmap)Bitmap.FromStream(new MemoryStream(temp));
            }
        }
    }
}