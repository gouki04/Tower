using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Component
{
    [DialogueCategoryAttribute(Path = "仙子/神秘宝物对话")]
    public class Dialogue_3 : Dialogue
    {
        public override void OnEnter()
        {
            base.OnEnter();

            DialogueDataList = new List<DialogueData>()
            {
                new DialogueData(null, "仙子", @"嗯？！你手里的那个东西是什么？"),
                new DialogueData(null, "勇士", @"这个？这是一个老人交给我的，是他叫我带它来找你的。他说你知道它的来历和作用。"),
                new DialogueData(null, "仙子", @"这个东西是仙界的圣物“灵之杖”，是很久以前的一个圣者留下的。它们一共有三个，分别镶着红、绿、蓝三种颜色的宝石。你现在拿着的是一颗镶有蓝宝石的“冰之神杖”，应该还有一个镶有绿宝石的“心之神杖”和镶有红宝石的“炎之神杖”。在这座塔的下面，封印着一只魔界的世兽，名叫“血影”，这三把“灵之杖”就是封印的钥匙。"),
                new DialogueData(null, "勇士", @"封印钥匙？"),
                new DialogueData(null, "仙子", @"每一个“灵之杖”里面都有着很强的魔法力量，如果被恶魔得到了将会使它的力量倍增。如果被恶魔将它们找齐的话。那么“血影”的封印便会解除！勇士，你还是快去把我要的东西找来吧，等我恢复了魔力，我就可以帮你将“灵之杖”中的魔力都开放出来！"),
            };
        }

        public override bool CheckCanTrigger(Player player)
        {
            return player.CheckSwitch("神秘宝物");
        }

        protected override void OnDialogueFinished(Player player)
        {
            player.OpenSwitch("隐藏关卡");
        }
    }
}
