using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Component
{
    [DialogueCategoryAttribute(Path = "仙子/幸运十字架对话")]
    public class Dialogue_2 : Dialogue
    {
        public override void OnEnter()
        {
            base.OnEnter();

            DialogueDataList = new List<DialogueData>()
            {
                new DialogueData(null, "勇士", @"仙子，我已经将那个十字架找到了。"),
                new DialogueData(null, "仙子", @"你做得很好。
那么，现在我就开始授与你更强的力量！
咪啦哆咪哗......
好了，我已经将你现在的力量提升了！"),
                new DialogueData(null, "仙子", @"记住：如果你没有足够的实力的话，不要去第二十一层！在那一层里，你所有宝物的法力都会失去作用！")
            };
        }

        public override bool CheckCanTrigger(Player player)
        {
            return player.CheckSwitch("幸运十字架");
        }

        protected override void OnDialogueStart(Player player)
        {
            if (player.CheckSwitch("隐藏关卡")) {
                DialogueDataList.Add(new DialogueData(null, "仙子", @"快走吧，杀死魔王后，来第二十二层上找我！"));
            }
        }

        protected override void OnDialogueFinished(Player player)
        {
            player.Attrs.Hp += Mathf.FloorToInt(player.Attrs.Hp / 3.0f);
            player.Attrs.Atk += Mathf.FloorToInt(player.Attrs.Atk / 3.0f);
            player.Attrs.Def += Mathf.FloorToInt(player.Attrs.Def / 3.0f);
        }
    }
}
