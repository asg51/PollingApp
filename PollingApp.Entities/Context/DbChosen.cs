namespace PollingApp.Entities.Context
{
    public class DbChosen : ContextBase<Chosen>
    {
        public int GetIndex(int index)
        {
            return Get(index).Index;
        }

        public Chosen this[int index]
        {
            get
            {
                foreach (Chosen chosen in GetList())
                {
                    if (chosen.Index == index)
                        return chosen;
                }
                return null;
            }
        }
    }
}
