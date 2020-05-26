using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Shared.ValueObjects
{
    public class Description
    {
        public Description(string text)
        {
            this.Text = text;
        }

        public string Text { get; private set; }
    }
}
