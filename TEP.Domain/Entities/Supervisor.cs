using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Domain.Entities
{
    public class Supervisor : Worker
    {
        public int SystemPassword { get; private set; }
    }
}
