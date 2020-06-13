using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Domain.ValueObjects
{
    /// <summary>
    /// Categories which an Interaction can be classified.
    /// </summary>
    public class Category
    {
        public Category(string name)
        {
            this.name = name;
        }

        public string name { get; private set; }
    }
}
