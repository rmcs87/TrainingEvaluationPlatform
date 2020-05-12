using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Domain.ValueObjects
{
    /// <summary>
    /// The types of interactions between assets, or within the action itself.
    /// </summary>
    public enum Act
    {
        Insert,
        Grab,
        Hold,
        Press,
        Timer,
        Bubble  //Special case where the operator is between two steps.
    }
}
