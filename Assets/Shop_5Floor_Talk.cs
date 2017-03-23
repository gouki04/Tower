using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    public class Shop_5Floor_Talk : Talk
    {
        public bool CanBuyAtk(Game game, string msg)
        {
            return game.Gold > int.Parse(msg);
        }

        public void BuyAtk(Game game, string msg)
        {
            game.AddAtk(int.Parse(msg));
        }

        public void BuyDef(Game game, string msg)
        {

        }

        public void BuyLv(Game game, string msg)
        {

        }
    }
}