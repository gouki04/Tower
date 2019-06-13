namespace Tower
{
    [System.Serializable]
    public class Condition
    {
        public virtual bool Execute(Player player)
        {
            return false;
        }
    }
}
