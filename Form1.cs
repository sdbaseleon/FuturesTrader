using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuturesTrader
{
    public partial class Form1 : Form
    {
        int counter = 0;
        List<Candlestick> candlesticksNQ = new List<Candlestick>();
        List<Candlestick> candlesticksCL = new List<Candlestick>();
        List<Candlestick> candlesticks6E = new List<Candlestick>();
        List<Candlestick> candlesticks6J = new List<Candlestick>();
        List<Candlestick> candlesticksGC = new List<Candlestick>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;

            Candlestick temp = new Candlestick();

            candlesticksCL.Add(temp);
            temp.high = 9291;
            temp.low = 9287;
            candlesticksCL.Add(temp);
            temp.high = 9291;
            temp.low = 9287;
            candlesticksCL.Add(temp);
            temp.high = 9291;
            temp.low = 9287;
            candlesticksCL.Add(temp);
            temp.high = 9291;
            temp.low = 9287;
            candlesticksCL.Add(temp);
            temp.high = 9291;
            temp.low = 9287;
            candlesticksCL.Add(temp);
            temp.high = 9291;
            temp.low = 9287;
            candlesticksCL.Add(temp);
            temp.high = 9291;
            temp.low = 9287;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10500;
            temp.low = 10494;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10497;
            temp.low = 10485;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10494;
            temp.low = 10487;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10499;
            temp.low = 10485;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10498;
            temp.low = 10494;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10498;
            temp.low = 10490;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10496;
            temp.low = 10490;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10495;
            temp.low = 10491;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10497;
            temp.low = 10493;
            candlesticksCL.Add(temp);
            temp = new Candlestick();
            temp.high = 10497;
            temp.low = 10500;
            candlesticksCL.Add(temp);

            //MessageBox.Show(isWedgeCL().ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (counter % 120 == 0)
            {
                addCandlesticks();
                if (candlesticksNQ.Count > 13)
                {
                    if (isWedgeNQ())
                    {
                        string path = @"E:\Steve\Documents\Market Analysis\NQoutput.txt";
                        using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
                        {
                            sw.WriteLine("NQ " + System.DateTime.Now.ToString());
                        }
                        MessageBox.Show(System.DateTime.Now.ToString());
                    }
                    if (isWedgeCL())
                    {
                        string path = @"E:\Steve\Documents\Market Analysis\NQoutput.txt";
                        using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
                        {
                            sw.WriteLine("CL " + System.DateTime.Now.ToString());
                        }
                        MessageBox.Show(System.DateTime.Now.ToString());
                    }
                    if (isWedge6E())
                    {
                        string path = @"E:\Steve\Documents\Market Analysis\NQoutput.txt";
                        using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
                        {
                            sw.WriteLine("6E " + System.DateTime.Now.ToString());
                        }
                        MessageBox.Show(System.DateTime.Now.ToString());
                    }
                    if (isWedge6J())
                    {
                        string path = @"E:\Steve\Documents\Market Analysis\NQoutput.txt";
                        using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
                        {
                            sw.WriteLine("6J " + System.DateTime.Now.ToString());
                        }
                        MessageBox.Show(System.DateTime.Now.ToString());
                    }
                    if (isWedgeGC())
                    {
                        string path = @"E:\Steve\Documents\Market Analysis\NQoutput.txt";
                        using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
                        {
                            sw.WriteLine("GC " + System.DateTime.Now.ToString());
                        }
                        MessageBox.Show(System.DateTime.Now.ToString());
                    }
                }
            }
            updateCandlesticks();

            // do check upon buying that breakout bar isnt part of wedge (hasnt already occured)

            

            counter++;
        }

        public bool isWedgeNQ()
        {
            for (int i = candlesticksNQ.Count - 2; i > candlesticksNQ.Count - 12; i--)
            {
                double negY2 = candlesticksNQ[i].high;
                double X1 = 0;
                double posY2 = candlesticksNQ[i].low;
                for (int j = i - 1; j > candlesticksNQ.Count - 12; j--)
                {
                    X1++;
                    double negY1 = candlesticksNQ[j].high;
                    double negSlope = (negY2 - negY1) / X1;
                    if (negSlope <= 0)
                    {
                        List<int> negNotTouches = isNegLineTouchingNQ(negSlope, j);
                        List<int> posNotTouches;
                        double X2 = 0;
                        for (int k = i - 1; k > candlesticksNQ.Count - 12; k--)
                        {
                            X2++;
                            double posY1 = candlesticksNQ[k].low;
                            double posSlope = (posY2 - posY1) / X2;
                            if (posSlope >= 0)
                            {
                                posNotTouches = isPosLineTouchingNQ(posSlope, k);
                                int numIslands = 0;
                                for (int x = 0; x < negNotTouches.Count; x++)
                                {
                                    for (int y = 0; y < posNotTouches.Count; y++)
                                    {
                                        int farthestFromEnd = 0;
                                        if (k < j)
                                        {
                                            farthestFromEnd = k;
                                        }
                                        else
                                        {
                                            farthestFromEnd = j;
                                        }
                                        if (negNotTouches[x] == posNotTouches[y] && negNotTouches[x] >= farthestFromEnd && posNotTouches[y] >= farthestFromEnd)
                                        {
                                            numIslands++;
                                        }
                                    }
                                }
                                bool isTallAtLeft = false;
                                if (i - j <= 3)
                                {
                                    int x = 0;
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] <= j)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] != negNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                } else if (i - k <= 3)
                                {
                                    int x = 0;
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] <= k)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] != posNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                } else {
                                    isTallAtLeft = true;
                                }
                                if (numIslands == 0 && ((candlesticksNQ.Count - j) >= 5 || (candlesticksNQ.Count - k) >= 5)
                                    && (candlesticksNQ.Count - j) >= 4 && (candlesticksNQ.Count - k) >= 4
                                    && ((candlesticksNQ.Count - j) <= 6 || (candlesticksNQ.Count - k) <= 6)
                                    && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 1 && ((candlesticksNQ.Count - j) >= 7 || (candlesticksNQ.Count - k) >= 7)
                                  && (candlesticksNQ.Count - j) >= 6 && (candlesticksNQ.Count - k) >= 6
                                  && ((candlesticksNQ.Count - j) <= 8 || (candlesticksNQ.Count - k) <= 8)
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 3 && ((candlesticksNQ.Count - j) >= 9 || (candlesticksNQ.Count - k) >= 9)
                                  && (candlesticksNQ.Count - j) >= 8 && (candlesticksNQ.Count - k) >= 8
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

            }
            return false;
        }

        public List<int> isNegLineTouchingNQ(double negSlope, int pos)
        {
            double currPos = candlesticksNQ[pos].high;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticksNQ.Count - 1; i++)
            {
                currPos += negSlope;
                if (!(candlesticksNQ[i].high >= roundNQ(currPos - 25) && candlesticksNQ[i].high <= roundNQ(currPos + 25)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticksNQ[pos].high;
            for (int i = pos - 1; i > candlesticksNQ.Count - 12; i--)
            {
                currPos -= negSlope;
                if (!(candlesticksNQ[i].high >= roundNQ(currPos - 25) && candlesticksNQ[i].high <= roundNQ(currPos + 25)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public List<int> isPosLineTouchingNQ(double posSlope, int pos)
        {
            double currPos = candlesticksNQ[pos].low;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticksNQ.Count - 1; i++)
            {
                currPos += posSlope;
                if (!(candlesticksNQ[i].low >= roundNQ(currPos - 25) && candlesticksNQ[i].low <= roundNQ(currPos + 25)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticksNQ[pos].low;
            for (int i = pos - 1; i > candlesticksNQ.Count - 12; i--)
            {
                currPos -= posSlope;
                if (!(candlesticksNQ[i].low >= roundNQ(currPos - 25) && candlesticksNQ[i].low <= roundNQ(currPos + 25)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public double roundNQ(double num)
        {
            if ((int)num % 25 >= 13)
            {
                return (double)((int)num + (25 - (int)num % 25));
            }
            return (double)((int)num - ((int)num % 25));
        }

        public bool isWedgeCL()
        {
            for (int i = candlesticksCL.Count - 2; i > candlesticksCL.Count - 12; i--)
            {
                double negY2 = candlesticksCL[i].high;
                double X1 = 0;
                double posY2 = candlesticksCL[i].low;
                for (int j = i - 1; j > candlesticksCL.Count - 12; j--)
                {
                    X1++;
                    double negY1 = candlesticksCL[j].high;
                    double negSlope = (negY2 - negY1) / X1;
                    if (negSlope <= 0)
                    {
                        List<int> negNotTouches = isNegLineTouchingCL(negSlope, j);
                        List<int> posNotTouches;
                        double X2 = 0;
                        for (int k = i - 1; k > candlesticksCL.Count - 12; k--)
                        {
                            X2++;
                            double posY1 = candlesticksCL[k].low;
                            double posSlope = (posY2 - posY1) / X2;
                            if (posSlope >= 0)
                            {
                                posNotTouches = isPosLineTouchingCL(posSlope, k);
                                int numIslands = 0;
                                int farthestFromEnd = 0;
                                if (k < j)
                                {
                                    farthestFromEnd = k;
                                }
                                else
                                {
                                    farthestFromEnd = j;
                                }
                                for (int x = 0; x < negNotTouches.Count; x++)
                                {
                                    for (int y = 0; y < posNotTouches.Count; y++)
                                    {
                                        if (negNotTouches[x] == posNotTouches[y] && negNotTouches[x] >= farthestFromEnd && posNotTouches[y] >= farthestFromEnd)
                                        {
                                            numIslands++;
                                        }
                                    }
                                }
                                bool isTallAtLeft = false;
                                if (i - j <= 3)
                                {
                                    int x = 0;
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] <= j)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] != negNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                }
                                else if (i - k <= 3)
                                {
                                    int x = 0;
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] <= k)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] != posNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    isTallAtLeft = true;
                                }
                                if (numIslands == 0 && ((candlesticksCL.Count - j) >= 5 || (candlesticksCL.Count - k) >= 5)
                                    && (candlesticksCL.Count - j) >= 4 && (candlesticksCL.Count - k) >= 4
                                    && ((candlesticksCL.Count - j) <= 6 || (candlesticksCL.Count - k) <= 6)
                                    && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 1 && ((candlesticksCL.Count - j) >= 7 || (candlesticksCL.Count - k) >= 7)
                                  && (candlesticksCL.Count - j) >= 6 && (candlesticksCL.Count - k) >= 6
                                  && ((candlesticksCL.Count - j) <= 8 || (candlesticksCL.Count - k) <= 8)
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 3 && ((candlesticksCL.Count - j) >= 9 || (candlesticksCL.Count - k) >= 9)
                                  && (candlesticksCL.Count - j) >= 8 && (candlesticksCL.Count - k) >= 8
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

            }
            return false;
        }

        public List<int> isNegLineTouchingCL(double negSlope, int pos)
        {
            double currPos = candlesticksCL[pos].high;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticksCL.Count - 1; i++)
            {
                currPos += negSlope;
                if (!(candlesticksCL[i].high >= Math.Round(currPos - 1, 0) && candlesticksCL[i].high <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticksCL[pos].high;
            for (int i = pos - 1; i > candlesticksCL.Count - 12; i--)
            {
                currPos -= negSlope;
                if (!(candlesticksCL[i].high >= Math.Round(currPos - 1, 0) && candlesticksCL[i].high <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public List<int> isPosLineTouchingCL(double posSlope, int pos)
        {
            double currPos = candlesticksCL[pos].low;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticksCL.Count - 1; i++)
            {
                currPos += posSlope;
                if (!(candlesticksCL[i].low >= Math.Round(currPos - 1, 0) && candlesticksCL[i].low <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticksCL[pos].low;
            for (int i = pos - 1; i > candlesticksCL.Count - 12; i--)
            {
                currPos -= posSlope;
                if (!(candlesticksCL[i].low >= Math.Round(currPos - 1, 0) && candlesticksCL[i].low <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public bool isWedge6E()
        {
            for (int i = candlesticks6E.Count - 2; i > candlesticks6E.Count - 12; i--)
            {
                double negY2 = candlesticks6E[i].high;
                double X1 = 0;
                double posY2 = candlesticks6E[i].low;
                for (int j = i - 1; j > candlesticks6E.Count - 12; j--)
                {
                    X1++;
                    double negY1 = candlesticks6E[j].high;
                    double negSlope = (negY2 - negY1) / X1;
                    if (negSlope <= 0)
                    {
                        List<int> negNotTouches = isNegLineTouching6E(negSlope, j);
                        List<int> posNotTouches;
                        double X2 = 0;
                        for (int k = i - 1; k > candlesticks6E.Count - 12; k--)
                        {
                            X2++;
                            double posY1 = candlesticks6E[k].low;
                            double posSlope = (posY2 - posY1) / X2;
                            if (posSlope >= 0)
                            {
                                posNotTouches = isPosLineTouching6E(posSlope, k);
                                int numIslands = 0;
                                for (int x = 0; x < negNotTouches.Count; x++)
                                {
                                    for (int y = 0; y < posNotTouches.Count; y++)
                                    {
                                        int farthestFromEnd = 0;
                                        if (k < j)
                                        {
                                            farthestFromEnd = k;
                                        }
                                        else
                                        {
                                            farthestFromEnd = j;
                                        }
                                        if (negNotTouches[x] == posNotTouches[y] && negNotTouches[x] >= farthestFromEnd && posNotTouches[y] >= farthestFromEnd)
                                        {
                                            numIslands++;
                                        }
                                    }
                                }
                                bool isTallAtLeft = false;
                                if (i - j <= 3)
                                {
                                    int x = 0;
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] <= j)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] != negNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                }
                                else if (i - k <= 3)
                                {
                                    int x = 0;
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] <= k)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] != posNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    isTallAtLeft = true;
                                }
                                if (numIslands == 0 && ((candlesticks6E.Count - j) >= 5 || (candlesticks6E.Count - k) >= 5)
                                    && (candlesticks6E.Count - j) >= 4 && (candlesticks6E.Count - k) >= 4
                                    && ((candlesticks6E.Count - j) <= 6 || (candlesticks6E.Count - k) <= 6)
                                    && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 1 && ((candlesticks6E.Count - j) >= 7 || (candlesticks6E.Count - k) >= 7)
                                  && (candlesticks6E.Count - j) >= 6 && (candlesticks6E.Count - k) >= 6
                                  && ((candlesticks6E.Count - j) <= 8 || (candlesticks6E.Count - k) <= 8)
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 3 && ((candlesticks6E.Count - j) >= 9 || (candlesticks6E.Count - k) >= 9)
                                  && (candlesticks6E.Count - j) >= 8 && (candlesticks6E.Count - k) >= 8
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

            }
            return false;
        }

        public List<int> isNegLineTouching6E(double negSlope, int pos)
        {
            double currPos = candlesticks6E[pos].high;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticks6E.Count - 1; i++)
            {
                currPos += negSlope;
                if (!(candlesticks6E[i].high >= Math.Round(currPos - 1, 0) && candlesticks6E[i].high <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticks6E[pos].high;
            for (int i = pos - 1; i > candlesticks6E.Count - 12; i--)
            {
                currPos -= negSlope;
                if (!(candlesticks6E[i].high >= Math.Round(currPos - 1, 0) && candlesticks6E[i].high <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public List<int> isPosLineTouching6E(double posSlope, int pos)
        {
            double currPos = candlesticks6E[pos].low;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticks6E.Count - 1; i++)
            {
                currPos += posSlope;
                if (!(candlesticks6E[i].low >= Math.Round(currPos - 1, 0) && candlesticks6E[i].low <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticks6E[pos].low;
            for (int i = pos - 1; i > candlesticks6E.Count - 12; i--)
            {
                currPos -= posSlope;
                if (!(candlesticks6E[i].low >= Math.Round(currPos - 1, 0) && candlesticks6E[i].low <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public bool isWedge6J()
        {
            for (int i = candlesticks6J.Count - 2; i > candlesticks6J.Count - 12; i--)
            {
                double negY2 = candlesticks6J[i].high;
                double X1 = 0;
                double posY2 = candlesticks6J[i].low;
                for (int j = i - 1; j > candlesticks6J.Count - 12; j--)
                {
                    X1++;
                    double negY1 = candlesticks6J[j].high;
                    double negSlope = (negY2 - negY1) / X1;
                    if (negSlope <= 0)
                    {
                        List<int> negNotTouches = isNegLineTouching6J(negSlope, j);
                        List<int> posNotTouches;
                        double X2 = 0;
                        for (int k = i - 1; k > candlesticks6J.Count - 12; k--)
                        {
                            X2++;
                            double posY1 = candlesticks6J[k].low;
                            double posSlope = (posY2 - posY1) / X2;
                            if (posSlope >= 0)
                            {
                                posNotTouches = isPosLineTouching6J(posSlope, k);
                                int numIslands = 0;
                                for (int x = 0; x < negNotTouches.Count; x++)
                                {
                                    for (int y = 0; y < posNotTouches.Count; y++)
                                    {
                                        int farthestFromEnd = 0;
                                        if (k < j)
                                        {
                                            farthestFromEnd = k;
                                        }
                                        else
                                        {
                                            farthestFromEnd = j;
                                        }
                                        if (negNotTouches[x] == posNotTouches[y] && negNotTouches[x] >= farthestFromEnd && posNotTouches[y] >= farthestFromEnd)
                                        {
                                            numIslands++;
                                        }
                                    }
                                }
                                bool isTallAtLeft = false;
                                if (i - j <= 3)
                                {
                                    int x = 0;
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] <= j)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] != negNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                }
                                else if (i - k <= 3)
                                {
                                    int x = 0;
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] <= k)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] != posNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    isTallAtLeft = true;
                                }
                                if (numIslands == 0 && ((candlesticks6E.Count - j) >= 5 || (candlesticks6E.Count - k) >= 5)
                                    && (candlesticks6E.Count - j) >= 4 && (candlesticks6E.Count - k) >= 4
                                    && ((candlesticks6E.Count - j) <= 6 || (candlesticks6E.Count - k) <= 6)
                                    && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 1 && ((candlesticks6E.Count - j) >= 7 || (candlesticks6E.Count - k) >= 7)
                                  && (candlesticks6E.Count - j) >= 6 && (candlesticks6E.Count - k) >= 6
                                  && ((candlesticks6E.Count - j) <= 8 || (candlesticks6E.Count - k) <= 8)
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 3 && ((candlesticks6E.Count - j) >= 9 || (candlesticks6E.Count - k) >= 9)
                                  && (candlesticks6E.Count - j) >= 8 && (candlesticks6E.Count - k) >= 8
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

            }
            return false;
        }

        public List<int> isNegLineTouching6J(double negSlope, int pos)
        {
            double currPos = candlesticks6J[pos].high;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticks6J.Count - 1; i++)
            {
                currPos += negSlope;
                if (!(candlesticks6J[i].high >= Math.Round(currPos - 1, 0) && candlesticks6J[i].high <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticks6J[pos].high;
            for (int i = pos - 1; i > candlesticks6J.Count - 12; i--)
            {
                currPos -= negSlope;
                if (!(candlesticks6J[i].high >= Math.Round(currPos - 1, 0) && candlesticks6J[i].high <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public List<int> isPosLineTouching6J(double posSlope, int pos)
        {
            double currPos = candlesticks6J[pos].low;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticks6J.Count - 1; i++)
            {
                currPos += posSlope;
                if (!(candlesticks6J[i].low >= Math.Round(currPos - 1, 0) && candlesticks6J[i].low <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticks6J[pos].low;
            for (int i = pos - 1; i > candlesticks6J.Count - 12; i--)
            {
                currPos -= posSlope;
                if (!(candlesticks6J[i].low >= Math.Round(currPos - 1, 0) && candlesticks6J[i].low <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public bool isWedgeGC()
        {
            for (int i = candlesticksGC.Count - 2; i > candlesticksGC.Count - 12; i--)
            {
                double negY2 = candlesticksGC[i].high;
                double X1 = 0;
                double posY2 = candlesticksGC[i].low;
                for (int j = i - 1; j > candlesticksGC.Count - 12; j--)
                {
                    X1++;
                    double negY1 = candlesticksGC[j].high;
                    double negSlope = (negY2 - negY1) / X1;
                    if (negSlope <= 0)
                    {
                        List<int> negNotTouches = isNegLineTouchingGC(negSlope, j);
                        List<int> posNotTouches;
                        double X2 = 0;
                        for (int k = i - 1; k > candlesticksGC.Count - 12; k--)
                        {
                            X2++;
                            double posY1 = candlesticksGC[k].low;
                            double posSlope = (posY2 - posY1) / X2;
                            if (posSlope >= 0)
                            {
                                posNotTouches = isPosLineTouchingGC(posSlope, k);
                                int numIslands = 0;
                                for (int x = 0; x < negNotTouches.Count; x++)
                                {
                                    for (int y = 0; y < posNotTouches.Count; y++)
                                    {
                                        int farthestFromEnd = 0;
                                        if (k < j)
                                        {
                                            farthestFromEnd = k;
                                        }
                                        else
                                        {
                                            farthestFromEnd = j;
                                        }
                                        if (negNotTouches[x] == posNotTouches[y] && negNotTouches[x] >= farthestFromEnd && posNotTouches[y] >= farthestFromEnd)
                                        {
                                            numIslands++;
                                        }
                                    }
                                }
                                bool isTallAtLeft = false;
                                if (i - j <= 3)
                                {
                                    int x = 0;
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] <= j)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < negNotTouches.Count - 1; x++)
                                    {
                                        if (negNotTouches[x] != negNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                }
                                else if (i - k <= 3)
                                {
                                    int x = 0;
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] <= k)
                                        {
                                            break;
                                        }
                                    }
                                    for (; x < posNotTouches.Count - 1; x++)
                                    {
                                        if (posNotTouches[x] != posNotTouches[x + 1] - 1)
                                        {
                                            isTallAtLeft = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    isTallAtLeft = true;
                                }
                                if (numIslands == 0 && ((candlesticksGC.Count - j) >= 5 || (candlesticksGC.Count - k) >= 5)
                                    && (candlesticksGC.Count - j) >= 4 && (candlesticksGC.Count - k) >= 4
                                    && ((candlesticksGC.Count - j) <= 6 || (candlesticksGC.Count - k) <= 6)
                                    && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 1 && ((candlesticksGC.Count - j) >= 7 || (candlesticksGC.Count - k) >= 7)
                                  && (candlesticksGC.Count - j) >= 6 && (candlesticksGC.Count - k) >= 6
                                  && ((candlesticksGC.Count - j) <= 8 || (candlesticksGC.Count - k) <= 8)
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                                else if (numIslands <= 3 && ((candlesticksGC.Count - j) >= 9 || (candlesticksGC.Count - k) >= 9)
                                  && (candlesticksGC.Count - j) >= 8 && (candlesticksGC.Count - k) >= 8
                                  && negNotTouches.Count < 8 && posNotTouches.Count < 8 && isTallAtLeft)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

            }
            return false;
        }

        public List<int> isNegLineTouchingGC(double negSlope, int pos)
        {
            double currPos = candlesticksGC[pos].high;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticksGC.Count - 1; i++)
            {
                currPos += negSlope;
                if (!(candlesticksGC[i].high >= Math.Round(currPos - 1, 0) && candlesticksGC[i].high <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticksGC[pos].high;
            for (int i = pos - 1; i > candlesticksGC.Count - 12; i--)
            {
                currPos -= negSlope;
                if (!(candlesticksGC[i].high >= Math.Round(currPos - 1, 0) && candlesticksGC[i].high <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public List<int> isPosLineTouchingGC(double posSlope, int pos)
        {
            double currPos = candlesticksGC[pos].low;
            List<int> retList = new List<int>();
            for (int i = pos + 1; i < candlesticksGC.Count - 1; i++)
            {
                currPos += posSlope;
                if (!(candlesticksGC[i].low >= Math.Round(currPos - 1, 0) && candlesticksGC[i].low <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            currPos = candlesticksGC[pos].low;
            for (int i = pos - 1; i > candlesticksGC.Count - 12; i--)
            {
                currPos -= posSlope;
                if (!(candlesticksGC[i].low >= Math.Round(currPos - 1, 0) && candlesticksGC[i].low <= Math.Round(currPos + 1, 0)))
                {
                    retList.Add(i);
                }
            }
            return retList;
        }

        public void addCandlesticks()
        {
            try
            {
                System.IO.StreamReader myFile = new System.IO.StreamReader("C:\\Users\\Steve\\Documents\\1.txt");
                string myString = myFile.ReadToEnd();
                myFile.Close();
                string[] lines = myString.Split(null);
                double[] prices = { Double.Parse(lines[0]), Double.Parse(lines[2]), Double.Parse(lines[4]), Double.Parse(lines[6]), Double.Parse(lines[8]) };

                candlesticksNQ.Add(new Candlestick());
                candlesticksCL.Add(new Candlestick());
                candlesticks6E.Add(new Candlestick());
                candlesticks6J.Add(new Candlestick());
                candlesticksGC.Add(new Candlestick());

                candlesticksNQ[candlesticksNQ.Count - 1].open = prices[0];
                candlesticksCL[candlesticksCL.Count - 1].open = prices[1];
                candlesticks6E[candlesticks6E.Count - 1].open = prices[2];
                candlesticks6J[candlesticks6J.Count - 1].open = prices[3];
                candlesticksGC[candlesticksGC.Count - 1].open = prices[4];
            }
            catch (Exception ex)
            {
                addCandlesticks();
            }
        }

        public void updateCandlesticks()
        {
            try
            {
                System.IO.StreamReader myFile = new System.IO.StreamReader("C:\\Users\\Steve\\Documents\\1.txt");
                string myString = myFile.ReadToEnd();
                myFile.Close();
                string[] lines = myString.Split(null);
                double[] prices = { Double.Parse(lines[0]), Double.Parse(lines[2]), Double.Parse(lines[4]), Double.Parse(lines[6]), Double.Parse(lines[8]) };

                if (prices[0] < candlesticksNQ[candlesticksNQ.Count - 1].low)
                {
                    candlesticksNQ[candlesticksNQ.Count - 1].low = prices[0];
                }
                if (prices[0] > candlesticksNQ[candlesticksNQ.Count - 1].high)
                {
                    candlesticksNQ[candlesticksNQ.Count - 1].high = prices[0];
                }
                if (prices[1] < candlesticksCL[candlesticksCL.Count - 1].low)
                {
                    candlesticksCL[candlesticksCL.Count - 1].low = prices[1];
                }
                if (prices[1] > candlesticksCL[candlesticksCL.Count - 1].high)
                {
                    candlesticksCL[candlesticksCL.Count - 1].high = prices[1];
                }
                if (prices[2] < candlesticks6E[candlesticks6E.Count - 1].low)
                {
                    candlesticks6E[candlesticks6E.Count - 1].low = prices[2];
                }
                if (prices[2] > candlesticks6E[candlesticks6E.Count - 1].high)
                {
                    candlesticks6E[candlesticks6E.Count - 1].high = prices[2];
                }
                if (prices[3] < candlesticks6J[candlesticks6J.Count - 1].low)
                {
                    candlesticks6J[candlesticks6J.Count - 1].low = prices[3];
                }
                if (prices[3] > candlesticks6J[candlesticks6J.Count - 1].high)
                {
                    candlesticks6J[candlesticks6J.Count - 1].high = prices[3];
                }
                if (prices[4] < candlesticksGC[candlesticksGC.Count - 1].low)
                {
                    candlesticksGC[candlesticksGC.Count - 1].low = prices[4];
                }
                if (prices[4] > candlesticksGC[candlesticksGC.Count - 1].high)
                {
                    candlesticksGC[candlesticksGC.Count - 1].high = prices[4];
                }

                candlesticksNQ[candlesticksNQ.Count - 1].close = prices[0];
                candlesticksCL[candlesticksCL.Count - 1].close = prices[1];
                candlesticks6E[candlesticks6E.Count - 1].close = prices[2];
                candlesticks6J[candlesticks6J.Count - 1].close = prices[3];
                candlesticksGC[candlesticksGC.Count - 1].close = prices[4];
            }
            catch (Exception ex)
            {

            }
        }
    }
}
