using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Shared.ValueObjects
{
    public class Description
    {
        public Description(string text)
        {
            this.text = text;
        }

        public string text { get; private set; }
    }
}
