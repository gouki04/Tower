using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Component
{
    [DialogueCategoryAttribute(Path = "小偷/第一次对话")]
    public class Dialogue_6 : Dialogue
    {
        public Tile DoorTile;

        public override void OnEnter()
        {
            base.OnEnter();

            DialogueDataList = new List<DialogueData>()
            {
                new DialogueData(null, "勇士", @"你已经得救了！"),
                new DialogueData(null, "小偷", @"啊，那真是太好了，我又可以在这里面寻宝了！
哦，还没有自我介绍，我叫杰克，是这附近有名的小偷，什么金银财宝我样样都偷过。"),
                new DialogueData(null, "小偷", @"不过这次运气可不是太好，刚进来就被抓了。
现在你帮我打开了门，那我就帮你做一件事吧。"),
                new DialogueData(null, "勇士", @"快走吧，外面还有很多怪物，我可能顾不上你。"),
                new DialogueData(null, "小偷", @"不，不，不会有事的。快说吧，叫我做什么？"),
                new DialogueData(null, "勇士", @"......
你会开门吗？"),
                new DialogueData(null, "小偷", @"那当然。"),
                new DialogueData(null, "勇士", @"那就请你帮我打开第二层的门吧！"),
                new DialogueData(null, "小偷", @"那个简单，不过，如果你能帮我找到一把嵌了红宝石的铁锒头的话，我还帮你打通第十八层的路。"),
                new DialogueData(null, "勇士", @"嵌了红宝石的铁锒头？
好吧，我帮你找找。"),
                new DialogueData(null, "小偷", @"非常地感谢。一会我便会把第二层的门打开。
如果你找到那个铁锒头的话，还是来这里找我！"),
            };
        }

        protected override void OnDialogueFinished(Player player)
        {
            if (DoorTile != null) {
                DoorTile.ParentTileMap.DeleteTile(DoorTile);
                DoorTile = null;
            }
        }
    }
}
