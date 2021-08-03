using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS
{
    /// <summary>
    /// Represents methods which handle event which occure when the unit system is changed.
    /// </summary>
    /// <param name="sender">Object sending the event.</param>
    /// <param name="e">Event argument which contain all the neccessary data realated to the event.</param>
    public delegate void eUnitChangedEventHandler(object sender, eUnitChangedEventArgs e);
}
