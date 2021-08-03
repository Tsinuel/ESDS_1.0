using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Mechanics.Analysis.Beam;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents all methods used used to calculate the ordinate for a given absisa.
    /// </summary>
    /// <param name="X">The absisa used to computed the ordinate.</param>
    /// <param name="sectionAt">The value indicating whether the section is imediatly from the left or from the right.</param>
    public delegate double eDiagramFunction(double X, eSectionAt sectionAt = eSectionAt.FromLeft);
}
