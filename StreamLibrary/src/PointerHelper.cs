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

namespace StreamLibrary.src
{
    /// <summary>
    /// A helper class for pointers
    /// </summary>
    public class PointerHelper
    {
        private int _offset;

        public IntPtr Pointer
        {
            get;
            private set;
        }

        public int TotalLength { get; private set; }

        public int Offset
        {
            get { return _offset; }
            set
            {
                if (value < 0)
                    throw new Exception("Offset must be >= 1");

                if (value >= TotalLength)
                    throw new Exception("Offset cannot go outside of the reserved buffer space");

                _offset = value;
            }
        }

        public PointerHelper(IntPtr pointer, int Length)
        {
            this.TotalLength = Length;
            this.Pointer = pointer;
        }

        /// <summary>
        /// Copies data from Source to the current Pointer Offset
        /// </summary>
        public void Copy(IntPtr Source, int SourceOffset, int SourceLength)
        {
            if (CheckBoundries(this.Offset, SourceLength))
                throw new AccessViolationException("Cannot write outside of the buffer space");
            NativeMethods.memcpy(new IntPtr(this.Pointer.ToInt64() + Offset), new IntPtr(Source.ToInt64() + SourceOffset), (uint)SourceLength);
        }

        private bool CheckBoundries(int offset, int length)
        {
            return offset + length > TotalLength;
        }
    }
}