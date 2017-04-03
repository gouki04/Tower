using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tower
{
    public class Dialogue : TileComponent
    {
        public delegate bool Condition(Player player);

        public Condition Conditions;

        public override bool CheckCanTrigger(Player player)
        {
            if (Conditions != null) {
                return Conditions(player);
            }

            return true;
        }

        public override IEnumerator OnTrigger(Player player)
        {
            return null;
        }

        public override IEnumerator OnAfterTrigger(Player player)
        {
            return null;
        }
    }
}
