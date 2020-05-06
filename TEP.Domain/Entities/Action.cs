using System;
using System.Collections.Generic;
using System.Text;
using TEP.Shared;

namespace TEP.Domain.Entities
{
    class Action : EntityBase
    {
        public Action(Asset source, Asset target, Interaction myProperty)
        {
            Source = source;
            Target = target;
            MyProperty = myProperty;
        }

        public Asset Source { get; private set; }
        public Asset Target { get; private set; }
        public Interaction MyProperty { get; private set; }
    }
}
