using System;

namespace Tower
{
    public class Talk : Obj
    {
        public DialoguerDialogues Dialog;

        public override bool Trigger(Game game)
        {
            Dialoguer.StartDialogue(Dialog);

            return false;
        }
    }
}
