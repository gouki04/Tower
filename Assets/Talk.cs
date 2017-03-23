using System;

namespace Tower
{
    public class Talk : Obj
    {
        public DialoguerDialogues Dialog;

        protected Game ResveredGame = null;

        protected void OnDialoguerEvent(string message, string metadata)
        {
            var func = GetType().GetMethod(message);
            if (func != null) {
                func.Invoke(this, new object[] { ResveredGame, metadata });
            }
        }

        protected bool OnDialoguerCondition(string message, string metadata)
        {
            var func = GetType().GetMethod(message);
            if (func != null) {
                return (bool)func.Invoke(this, new object[] { ResveredGame, metadata });
            }

            return false;
        }

        protected void OnDialoguerEnd()
        {
            Dialoguer.events.onEnded -= OnDialoguerEnd;
            Dialoguer.events.onMessageEvent -= OnDialoguerEvent;
            Dialoguer.events.onCondition -= OnDialoguerCondition;
        }

        public override bool Trigger(Game game)
        {
            Dialoguer.events.onEnded += OnDialoguerEnd;
            Dialoguer.events.onMessageEvent += OnDialoguerEvent;
            Dialoguer.events.onCondition += OnDialoguerCondition;

            ResveredGame = game;
            Dialoguer.StartDialogue(Dialog);

            return false;
        }

        #region basic call back
        public void AddHp(Game game, string msg)
        {

        }

        public void AddKey(Game game, string msg)
        {
            game.AddKey((Tower.EKey)Enum.Parse(typeof(Tower.EKey), msg, true));
        }
        #endregion
    }
}
