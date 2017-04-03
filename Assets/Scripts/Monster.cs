using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tower
{
    public class Monster : TileComponent
    {
        public string Name;
        public int Hp = 0;
        public int Atk = 0;
        public int Def = 0;
        public int ExtraDamage = 0;
        public float ExtraDamagePercent = 0.0f;

        public Attributes Attrs = new Attributes();

        protected int m_damage = 0;

        public int CalcCostHp(Player player)
        {
            if (player.Attrs.Atk <= this.Def) {
                return int.MaxValue;
            }

            var damage = this.ExtraDamage + (int)Math.Floor(player.Attrs.Hp * this.ExtraDamagePercent);
            if (player.Attrs.Def < this.Atk) {
                var hit = player.Attrs.Atk - this.Def;
                var round = (int)Math.Floor((float)this.Hp / hit);
                damage += round * (this.Atk - player.Attrs.Def);
            }

            return damage;
        }

        public override bool CheckCanTrigger(Player player)
        {
            m_damage = CalcCostHp(player);
            return player.Attrs.Hp >= m_damage;
        }

        public override IEnumerator OnTrigger(Player player)
        {
            player.Attrs.Hp -= m_damage;
            player.Attrs.Combine(this.Attrs);
            return null;
        }

        public override IEnumerator OnAfterTrigger(Player player)
        {
            yield return null;
            Owner.DestroySelf();
        }
    }
}
