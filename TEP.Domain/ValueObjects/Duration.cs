using System.Collections.Generic;
using TEP.Domain.Common;

namespace TEP.Domain.ValueObjects
{
    public class Duration : ValueObject
    {
        //Manages the duration of an event in Seconds, to a max of 596.52 Hour.
        //https://medium.com/swlh/value-objects-to-the-rescue-28c563ad97c6 (exemplo do Distance)
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

        protected override IEnumerable<object> GetAtomicValues()
        {
            //https://medium.com/swlh/value-objects-to-the-rescue-28c563ad97c6 (exemplo do Distance)
            throw new System.NotImplementedException();
        }

        /*public override string ToString()
        {
            var apt = !string.IsNullOrWhiteSpace(Street2) ? " " + Street2 : "";
            return $"{Street1}{apt}, {City}, {State} {Zip}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street1;
            yield return Street2;
            yield return City;
            yield return State;
            yield return Zip;
        }*/
    }
}
