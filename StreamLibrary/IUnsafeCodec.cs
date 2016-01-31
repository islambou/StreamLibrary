using StreamLibrary.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

namespace StreamLibrary
{
    public abstract class IUnsafeCodec
    {
        protected JpgCompression jpgCompression;
        protected LzwCompression lzwCompression;
        public abstract ulong CachedSize { get; internal set; }
        protected object ImageProcessLock { get; private set; }

        private int _imageQuality;
        public int ImageQuality
        {
            get { return _imageQuality; }
            set
            {
                _imageQuality = value;
                jpgCompression = new JpgCompression(value);
                lzwCompression = new LzwCompression(value);
            }
        }


        public abstract event IVideoCodec.VideoDebugScanningDelegate onCodeDebugScan;
        public abstract event IVideoCodec.VideoDebugScanningDelegate onDecodeDebugScan;

        public IUnsafeCodec(int ImageQuality = 100)
        {
            this.ImageQuality = ImageQuality;
            this.ImageProcessLock = new object();
        }

        public abstract int BufferCount { get; }
        public abstract CodecOption CodecOptions { get; }
        public abstract unsafe void CodeImage(IntPtr Scan0, Rectangle ScanArea, Size ImageSize, PixelFormat Format, Stream outStream);
        public abstract unsafe Bitmap DecodeData(Stream inStream);
        public abstract unsafe Bitmap DecodeData(IntPtr CodecBuffer, uint Length);
    }
}