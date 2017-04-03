using System;

namespace Tower
{
    public class Talk : Obj
    {
        public DialoguerDialogues Dialog;
        public bool StayAfterTalk = true;

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

            return !StayAfterTalk;
        }

        #region basic call back
        public virtual void AddHp(Game game, string msg)
        {
            game.AddHp(int.Parse(msg));
        }

        public virtual void AddAtk(Game game, string msg)
        {
            game.AddAtk(int.Parse(msg));
        }

        public virtual void AddDef(Game game, string msg)
        {
            game.AddDef(int.Parse(msg));
        }

        public virtual void AddKey(Game game, string msg)
        {
            game.AddKey((Tower.EKey)Enum.Parse(typeof(Tower.EKey), msg, true));
        }

        public virtual void AddGold(Game game, string msg)
        {
            game.AddGold(int.Parse(msg));
        }

        public virtual bool CheckGold(Game game, string msg)
        {
            return game.Gold >= int.Parse(msg);
        }
        #endregion
    }
}
