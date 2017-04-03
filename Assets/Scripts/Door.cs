using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tower
{
    public class Door : TileComponent
    {
        public Attributes Cost = new Attributes();

        public override bool CheckCanTrigger(Player player)
        {
            return player.Attrs.Include(Cost);
        }

        public override IEnumerator OnTrigger(Player player)
        {
            player.Attrs.Cost(this.Cost);
            return null;
        }

        public override IEnumerator OnAfterTrigger(Player player)
        {
            Owner.DestroySelf();
            return null;
        }
    }
}
