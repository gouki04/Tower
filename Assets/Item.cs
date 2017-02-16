using System;

namespace Tower
{
    public class Item : Obj
    {
        public int Hp = 0;
        public int Atk = 0;
        public int Def = 0;
        public int Gold = 0;
        public int Exp = 0;

        public override bool Trigger(Game game)
        {
            if (Hp != 0) {
                game.AddHp(Hp);
            }

            if (Atk != 0) {
                game.AddAtk(Atk);
            }

            if (Def != 0) {
                game.AddDef(Def);
            }

            if (Gold != 0) {
                game.AddGold(Gold);
            }

            if (Exp != 0) {
                game.AddExp(Exp);
            }

            return true;
        }
    }
}
