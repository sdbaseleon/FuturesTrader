using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace FuturesTrader
{
    // Candlestick class holds the open, high, low, and close data for a stream of price data over a certain time period (two minutes).
    class Candlestick
    {
        public double open = 0, close = 0, low = 1000000, high = 0, volume = 0;
        public Candlestick()
        {
        }

        // Returns if candlestick is green (close is higher than open).
        public bool isGreen()
        {
            return (open<=close?true:false);
        }

        // Returns if candlestick is red (close is lower than open).
        public bool isRed()
        {
            return (open>=close?true:false);
        }
    }
}
