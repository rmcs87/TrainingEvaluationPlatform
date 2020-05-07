using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Domain.Entities
{
    class Worker:EntityBase
    {
        public string Name { get; private set; }
        public int registry { get; private set; }
    }
}
