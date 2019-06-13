using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Component
{
    [DialogueCategoryAttribute(Path = "2层/神秘老人（蓝色）")]
    public class Dialogue_4 : Dialogue
    {
        public override void OnEnter()
        {
            base.OnEnter();

            DialogueDataList = new List<DialogueData>()
            {
                new DialogueData(null, "勇士", @"您已经得救了！"),
                new DialogueData(null, "神秘老人", @"哦，我的孩子，真是太感谢你了！
这个地方又脏又坏，我真的是快呆不下去了。"),
                new DialogueData(null, "勇士", @"快走吧，我还得拯救被关在这里的公主。"),
                new DialogueData(null, "神秘老人", @"哦，你是来救公主的，为了表示对你的感谢，这
个东西就送给你吧，这还是我年青的时候用过的。
拿着它去解救公主吧！"),
            };
        }

        protected override void OnDialogueFinished(Player player)
        {
            player.Attrs.Atk += 70;
            Owner.RemoveSelfInRoutine();
        }
    }
}
