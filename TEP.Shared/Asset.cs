using System;
using TEP.Shared.ValueObjects;

namespace TEP.Shared
{
    public class Asset
    {
        public Asset(Path path, Description description)
        {
            Path = path;
            Description = description;
        }

        public Path Path { get; private set; }
        public Description Description { get; private set; }
    }
}
