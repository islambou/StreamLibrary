using StreamLibrary;
using StreamLibrary.UnsafeCodecs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

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

namespace StreamServerSample
{
    public partial class Form1 : Form
    {
        private delegate void Invoky();
        private ChartLine FpsLine = new ChartLine()
        {
            AverageComment = "Avg FPS ",
            ChartLinePen = new ChartPen()
            {
                 Color = Color.Green,
                 DashStyle = System.Drawing.Drawing2D.DashStyle.Solid,
                 Width = 3
            },
             ShowAverageLine = false,
             Fill = true,
             PeakComment = "Peak FPS ",
             ValueSpacing = 10,
        };

        private Socket Server;
        public Form1()
        {
            InitializeComponent();

            this.performanceChart1.ChartLines.Add(FpsLine);

            Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Server.Bind(new IPEndPoint(0, 4432));
            Server.Listen(10);
            Server.BeginAccept(BeginAccept_Callback, null);
        }

        private void BeginAccept_Callback(IAsyncResult ar)
        {
            try
            {
                Socket sock = Server.EndAccept(ar);
                IUnsafeCodec decoder = new UnsafeStreamCodec(80);
                int FPS = 0;
                Stopwatch sw = Stopwatch.StartNew();
                Stopwatch RenderSW = Stopwatch.StartNew();

                while (sock.Connected)
                {
                    //keep receiving data
                    byte[] Header = ReceiveData(4, sock);
                    if (Header.Length != 4)
                        break;

                    int length = BitConverter.ToInt32(Header, 0);

                    byte[] Payload = ReceiveData(length, sock);
                    if (Payload.Length != length)
                        break;

                    Bitmap decoded = decoder.DecodeData(new MemoryStream(Payload));

                    if (RenderSW.ElapsedMilliseconds >= (1000 / 20))
                    {
                        this.pictureBox1.Image = (Bitmap)decoded.Clone();
                        RenderSW = Stopwatch.StartNew();
                    }

                    FPS++;
                    if (sw.ElapsedMilliseconds >= 1000)
                    {
                        this.Invoke(new Invoky(() =>
                        {
                            this.Fpslbl.Text = "FPS: " + FPS;
                            this.ScreenSizelbl.Text = "Screen Size: " + decoded.Width + " x " + decoded.Height;
                        }));

                        performanceChart1.AddValue(FpsLine, FPS);
                        FPS = 0;
                        sw = Stopwatch.StartNew();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            Server.BeginAccept(BeginAccept_Callback, null);
        }

        private byte[] ReceiveData(int Length, Socket socket)
        {
            byte[] data = new byte[Length];
            int offset = 0;

            while (Length > 0)
            {
                int recv = socket.Receive(data, offset, Length, SocketFlags.None);

                if (recv <= 0)
                    return new byte[0];

                offset += recv;
                Length -= recv;
            }
            return data;
        }
    }
}