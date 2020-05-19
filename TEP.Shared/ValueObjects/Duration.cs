using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Shared.ValueObjects
{
    public class Duration
    {
        //Manages the duration of an event in Seconds, to a max of 596.52 Hour.
        public Duration(double seconds)
        {
            Seconds = seconds;
        }

        public double Seconds { get; set; }
        public void Increment(double increment)
        {
            Seconds += increment;
        }
        public void Decrement(double decrement)
        {
            Seconds -= decrement;
        }public void Increment(Duration increment)
        {
            Seconds += increment.Seconds;
        }
        public void Decrement(Duration decrement)
        {
            Seconds -= decrement.Seconds;
        }
        public void Reset()
        {
            Seconds = 0;
        }
    }
}
