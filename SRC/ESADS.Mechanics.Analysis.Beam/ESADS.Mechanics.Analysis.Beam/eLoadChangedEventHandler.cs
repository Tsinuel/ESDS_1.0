using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents all methods which handle ESADS.Mechanics.eLoad.Changed event.
    /// </summary>
    /// <param name="sender">The object sending this event.</param>
    /// <param name="e">Event argument containg all the necessary information related to the event.</param>
    public delegate void eLoadChangedEventHandler(object sender, eLoadChangedEventArgs e);
}
