using System;
using System.Collections.Generic;
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

namespace StreamLibrary.Encoders.GridCoder
{
    public class GridEncoder : IEncoder
    {
        public override ulong CachedSize
        {
            get;
            internal set;
        }

        public override event IVideoCodec.VideoDebugScanningDelegate onCodeDebugScan;
        public override event IVideoCodec.VideoDebugScanningDelegate onDecodeDebugScan;

        public override int BufferCount
        {
            get { return 1; }
        }

        public override CodecOption CodecOptions
        {
            get { return CodecOption.AutoDispose | CodecOption.RequireSameSize; }
        }



        public override unsafe void CodeImage(IntPtr Scan0, System.Drawing.Rectangle ScanArea, System.Drawing.Size ImageSize, System.Drawing.Imaging.PixelFormat Format, System.IO.Stream outStream)
        {



        }

        public override unsafe System.Drawing.Bitmap DecodeData(System.IO.Stream inStream)
        {
            return null;
        }

        public override unsafe System.Drawing.Bitmap DecodeData(IntPtr CodecBuffer, uint Length)
        {
            return null;
        }
    }
}