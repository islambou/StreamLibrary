using System;
using System.Collections.Generic;
using System.Drawing;
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
    public static unsafe class Extensions
    {
        public static SortedList<int, SortedList<int, Rectangle>> RectanglesTo2D(this Rectangle[] rects)
        {
            SortedList<int, SortedList<int, Rectangle>> Rects = new SortedList<int, SortedList<int, Rectangle>>();
            for (int i = 0; i < rects.Length; i++)
            {
                if (!Rects.ContainsKey(rects[i].Y))
                    Rects.Add(rects[i].Y, new SortedList<int, Rectangle>());

                if (!Rects[rects[i].Y].ContainsKey(rects[i].X))
                    Rects[rects[i].Y].Add(rects[i].X, rects[i]);
            }
            return Rects;
        }

        public static SortedList<int, SortedList<int, Rectangle>> Rectangle2DToRows(this SortedList<int, SortedList<int, Rectangle>> Rects)
        {
            SortedList<int, SortedList<int, Rectangle>> RectRows = new SortedList<int, SortedList<int, Rectangle>>();

            for (int i = 0; i < Rects.Values.Count; i++)
            {
                if (!RectRows.ContainsKey(Rects.Values[i].Values[0].Y))
                {
                    RectRows.Add(Rects.Values[i].Values[0].Y, new SortedList<int, Rectangle>());
                }
                if (!RectRows[Rects.Values[i].Values[0].Y].ContainsKey(Rects.Values[i].Values[0].X))
                {
                    RectRows[Rects.Values[i].Values[0].Y].Add(Rects.Values[i].Values[0].X, Rects.Values[i].Values[0]);
                }

                Rectangle EndRect = Rects.Values[i].Values[0];
                for (int x = 1; x < Rects.Values[i].Values.Count; x++)
                {
                    Rectangle CurRect = Rects.Values[i].Values[x];
                    Rectangle tmpRect = RectRows[EndRect.Y].Values[RectRows[EndRect.Y].Count - 1];
                    if (tmpRect.IntersectsWith(new Rectangle(CurRect.X - 1, CurRect.Y, CurRect.Width, CurRect.Height)))
                    {
                        RectRows[EndRect.Y][tmpRect.X] = new Rectangle(tmpRect.X, tmpRect.Y, tmpRect.Width + EndRect.Width, tmpRect.Height);
                        EndRect = Rects.Values[i].Values[x];
                    }
                    else
                    {
                        EndRect = Rects.Values[i].Values[x];
                        RectRows[Rects.Values[i].Values[0].Y].Add(EndRect.X, EndRect);
                    }
                }
            }
            return RectRows;
        }
    }
}
