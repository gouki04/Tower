using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Component
{
    [DialogueCategoryAttribute(Path = "2层/商人（红色）")]
    public class Dialogue_5 : Dialogue
    {
        public override void OnEnter()
        {
            base.OnEnter();

            DialogueDataList = new List<DialogueData>()
            {
                new DialogueData(null, "勇士", @"您已经得救了！"),
                new DialogueData(null, "神秘老人", @"哦，是嘛！真是太感谢你了！
我是个商人，不知道为什么被抓到这里来了。"),
                new DialogueData(null, "勇士", @"快走吧，现在您已经自由了。"),
                new DialogueData(null, "神秘老人", @"哦，对对对，我已经自由了。
那这个东西就给你吧，本来我是准备卖钱的。
相信它对你一定很有帮助！"),
            };
        }

        protected override void OnDialogueFinished(Player player)
        {
            player.Attrs.Def += 30;
            Owner.RemoveSelfInRoutine();
        }
    }
}
