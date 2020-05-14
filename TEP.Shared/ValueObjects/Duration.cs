using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Shared.ValueObjects
{
    public class Duration
    {
        //Manages the duration of an event in miliseconds, to a max of 596.52 Hour.
        public Duration(int milis)
        {
            Milis = milis;
        }

        public int Milis { get; set; }
        public void Increment(int increment)
        {
            Milis += increment;
        }
        public void Decrement(int decrement)
        {
            Milis -= decrement;
        }public void Increment(Duration increment)
        {
            Milis += increment.Milis;
        }
        public void Decrement(Duration decrement)
        {
            Milis -= decrement.Milis;
        }
        public void Reset()
        {
            Milis = 0;
        }
    }
}
