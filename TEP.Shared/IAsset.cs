using System;
using TEP.Shared.ValueObjects;

namespace TEP.Shared
{
    /// <summary>
    /// An Interface to Represent objects in a Interaction.
    /// </summary>
    public interface IAsset
    {
        public string Path { get;}
        public string Name { get;}
    }
}
