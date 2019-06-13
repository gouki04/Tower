using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Component
{
    [DialogueCategoryAttribute(Path = "小偷/星光神锒对话")]
    public class Dialogue_7 : Dialogue
    {
        public List<Tile> RoadTiles = new List<Tile>();

        public override void OnEnter()
        {
            base.OnEnter();

            DialogueDataList = new List<DialogueData>()
            {
                new DialogueData(null, "勇士", @"哈，快看，我找到了什么！"),
                new DialogueData(null, "小偷", @"太好了，这个东西果然是在这里。
好吧，我这就去帮你修好第十八层的路面。"),
            };
        }

        public override bool CheckCanTrigger(Player player)
        {
            return player.CheckSwitch("星光神锒");
        }

        protected override void OnDialogueFinished(Player player)
        {
            foreach (var tile in RoadTiles) {
                tile.ParentTileMap.DeleteTile(tile);
            }
            RoadTiles.Clear();

            Owner.RemoveSelfInRoutine();
        }
    }
}
