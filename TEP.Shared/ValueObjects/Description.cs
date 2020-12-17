namespace TEP.Shared.ValueObjects
{
    public class Description
    {
        public Description(string text)
        {
            this.Text = text;
        }

        public string Text { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = obj as Description;
            return Text.Equals(other.Text);
        }
        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}
