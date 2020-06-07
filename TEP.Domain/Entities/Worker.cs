using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Domain.Entities
{
    public abstract class Worker:EntityBase
    {
        public string Name { get; private set; }
        public int Registry { get; private set; }
    }
}
