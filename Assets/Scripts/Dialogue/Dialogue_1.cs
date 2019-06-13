using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Component
{
    [DialogueCategoryAttribute(Path = "仙子/第一次对话")]
    public class Dialogue_1 : Dialogue
    {
        public override void OnEnter()
        {
            base.OnEnter();

            DialogueDataList = new List<DialogueData>()
            {
                new DialogueData(null, "勇士", "......"),
                new DialogueData(null, "仙子", "你醒了！"),
                new DialogueData(null, "勇士", "......你是谁？我在哪里？"),
                new DialogueData(null, "仙子", "我是这里的仙子，刚才你被这里的小怪打昏了。"),
                new DialogueData(null, "勇士", "......剑，剑，我的剑呢？"),
                new DialogueData(null, "仙子", "你的剑被他们抢走了，我只来得及将你救出来。"),
                new DialogueData(null, "勇士", "那，公主呢？我是来救公主的。"),
                new DialogueData(null, "仙子", "公主还在里面，你这样进去是打不过里面的小怪的。"),
                new DialogueData(null, "勇士", "那我怎么办，我答应了国王一定要把公主救出来的，那我现在应该怎么办呢？"),
                new DialogueData(null, "仙子", "放心吧，我把我的力量借给你，你就可以打赢那些小怪了。不过，你的先帮我去找一样东西，找到了再来这里找我。"),
                new DialogueData(null, "勇士", "找东西？找什么东西？"),
                new DialogueData(null, "仙子", "是一个十字架，中间有一颗红色的宝石。"),
                new DialogueData(null, "勇士", "那个东西有什么用吗？"),
                new DialogueData(null, "仙子", "我本是这座塔守护者，可不久前，从北方来了一批恶魔，并将我的魔力封在了这个十字架里面，如果你能将它带出塔来，那我的魔力便会慢慢地恢复，到那时我便可以把力量借给你去救出公主了。"),
                new DialogueData(null, "仙子", "要记住：只有用我的魔力才能打开二十一层的门。"),
                new DialogueData(null, "勇士", "......好吧，我试试看。"),
                new DialogueData(null, "仙子", "刚才我去看过了，你的剑被放在三楼，你的盾在五楼上，而那个十字架被放在七楼。要到七楼，你的先取回你的剑和盾。"),
                new DialogueData(null, "仙子", "另外，在塔里的其它楼层上，还有一些存放了好几百年的宝剑和宝物，如果得到它们，对于你对付这里面的怪物将有很大的帮助。"),
                new DialogueData(null, "仙子", "我这里有三把钥匙，你先拿去，在塔里面还有很多这样的钥匙，你一定要珍惜使用。勇敢的去吧，勇士！")
            };
        }

        protected override void OnDialogueFinished(Player player)
        {
            player.Attrs.Key_Yellow += 1;
            player.Attrs.Key_Blue += 1;
            player.Attrs.Key_Red += 1;

            Owner.ParentTileMap.MoveTile(Owner, new TileIndex(Owner.Row, Owner.Column - 1));
        }
    }
}
