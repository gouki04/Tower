namespace Tower
{
    [System.Serializable]
    public class ObjectCondition : Condition
    {
        public UnityEngine.Object Obj;
        public string MethodName;

        public override bool Execute(Player player)
        {
            if (Obj == null || MethodName == string.Empty) {
                return true;
            } else {
                return (bool)Obj.GetType().GetMethod(MethodName).Invoke(Obj, new object[] { player });
            }
        }
    }
}
