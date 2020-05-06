using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Domain.ValueObjects
{
    /// <summary>
    /// The classification for a step, which implies how it will be hadled in treinning.
    /// </summary>
    public enum Standard
    {
        Mandatory,
        Optional,
        Error
    }
}
