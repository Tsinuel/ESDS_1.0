using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design
{
    public interface eIDModel
    {
        /// <summary>
        /// Gets or sets the steel material used in the design.
        /// </summary>
        eSteel Steel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the concrete material used in the design.
        /// </summary>
        eConcrete Concrete
        {
            get;
            set;
        }

        /// <summary>
        /// Gets ors sets the concrete conver used in the design.
        /// </summary>
        double Cover
        {
            get;
            set;
        }

        void Design();
    }
}
