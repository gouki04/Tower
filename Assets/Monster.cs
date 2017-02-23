using System;

namespace Tower
{
    public class Monster : Obj
    {
        public string Name;
        public int Hp = 0;
        public int Atk = 0;
        public int Def = 0;
        public int ExtraDamage = 0;
        public float ExtraDamagePercent = 0.0f;
        public int Gold = 0;
        public int Exp = 0;

        public override bool Trigger(Game game)
        {
            if (game.ATK <= Def) {
                return false;
            }

            var damage = ExtraDamage + (int)Math.Floor(game.HP * ExtraDamagePercent);
            if (damage >= game.HP) {
                return false;
            }

            if (game.DEF >= Atk) {
                game.AddHp(-damage);
                game.AddGold(Gold);
                game.AddExp(Exp);
                return true;
            }

            var hit = game.ATK - Def;
            var round = (int)Math.Floor((float)Hp / hit);
            damage += round * (Atk - game.DEF);

            if (damage >= game.HP) {
                return false;
            }

            game.AddHp(-damage);
            game.AddGold(Gold);
            game.AddExp(Exp);
            return true;
        }
    }
}
