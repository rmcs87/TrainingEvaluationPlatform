namespace TEP.Shared.ValueObjects

{
    public class Path
    {
        public Path(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}