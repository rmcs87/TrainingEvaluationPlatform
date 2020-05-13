using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Shared.ValueObjects
{
    public class Duration
    {
        public Duration(float milis)
        {
            Milis = milis;
        }

        public float Milis { get; set; }
        public void Increment(float increment)
        {
            Milis += increment;
        }
        public void Decrement(float decrement)
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
            Milis = 0.0f;
        }
    }
}
