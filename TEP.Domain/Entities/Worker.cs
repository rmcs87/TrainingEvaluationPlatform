using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Domain.Entities
{
    public class Worker:EntityBase
    {
        public string Name { get; private set; }
        public int Registry { get; private set; }
    }
}
