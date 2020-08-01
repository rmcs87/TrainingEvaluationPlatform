using System.Collections.Generic;
using TEP.Domain.Common;

namespace TEP.Domain.ValueObjects
{
    public class Duration : ValueObject
    {
        //Manages the duration of an event in Seconds, to a max of 596.52 Hour.
        //Convert to something like this-> https://medium.com/swlh/value-objects-to-the-rescue-28c563ad97c6 (exemplo do Distance)
        public Duration(double seconds)
        {
            Seconds = seconds;
        }

        public double Seconds { get; set; }
        public void Increment(double amount)
        {
            Seconds += amount;
        }
        public void Decrement(double amount)
        {
            Seconds -= amount;
        }
        public void Increment(Duration increment)
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
        protected override IEnumerable<object> GetAtomicValues()
        {
            //https://medium.com/swlh/value-objects-to-the-rescue-28c563ad97c6 (exemplo do Distance)
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

    }
}
