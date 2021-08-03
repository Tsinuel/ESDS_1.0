using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Slab
{
    public class eASBeam
    {

        private List<eASBMember> members;

        public List<eASBMember> Members
        {
            get
            {
                return members;
            }

            set
            {
                members = value;
            }
        }    
    }
}
