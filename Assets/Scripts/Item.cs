using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tower
{
    public class Item : TileComponent
    {
        public Attributes Attrs = new Attributes();

        public override IEnumerator OnTrigger(Player player)
        {
            player.Attrs.Combine(this.Attrs);
            return null;
        }

        public override IEnumerator OnAfterTrigger(Player player)
        {
            yield return 0;
            Owner.DestroySelf();
        }
    }
}
