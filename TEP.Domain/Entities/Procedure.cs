using System;
using System.Collections.Generic;
using System.Text;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
{
    class Procedure : EntityBase
    {
        public Procedure(string name, Description description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public Description Description { get; private set; }
        public void ToJson()
        {
            //2do
            //https://docs.microsoft.com/pt-br/dotnet/standard/serialization/system-text-json-how-to
            //https://www.newtonsoft.com/json
            throw new NotImplementedException();
        }

        public void Print()
        {
            //2do
            throw new NotImplementedException();
        }
    }
}
