namespace TEP.Domain.ValueObjects
{
    public class Duration
    {
        public Duration(double seconds)
        {
            Seconds = seconds;
        }        
        public double Seconds { get; set; }
        public void Increment(double amount)
        {
            Seconds += amount;
        }
        public void Increment(Duration duration)
        {
            Seconds += duration.Seconds;
        }
        public void Decrement(double amount)
        {
            Seconds -= amount;
        }        
        public void Decrement(Duration duration)
        {
            Seconds -= duration.Seconds;
        }
        public void Reset()
        {
            Seconds = 0;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = obj as Duration;
            return Seconds.Equals(other.Seconds);    
        }
        public override int GetHashCode()
        {
            return Seconds.GetHashCode();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

    }
}
