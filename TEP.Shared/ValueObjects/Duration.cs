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

        public float Milis { get; private set; }
    }
}
