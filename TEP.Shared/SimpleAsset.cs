using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Shared
{
    public class SimpleAsset : IAsset
    {
        public SimpleAsset(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public string Path { get; }
        public string Name { get; }
    }
}
