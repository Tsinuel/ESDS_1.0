using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Footing
{
    public enum eDesignCompletionState
    {
        ReinforcementCongested,
        OverReinforced,
        UnderReinforced,
        InsufficeintDepth,
        InsufficeintAnchorageLength,
        NoBarBetweenSpacingLimit,
    }
}
