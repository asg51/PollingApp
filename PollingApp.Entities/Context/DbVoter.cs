namespace PollingApp.Entities.Context
{
    public class DbVoter : ContextBase<Voter>
    {
        public int GetIndex(int index)
        {
            return Get(index).Index;
        }

        public Voter this[int index]
        {
            get
            {
                foreach (Voter voter in GetList())
                {
                    if (voter.Index == index)
                        return voter;
                }
                return null;
            }
        }
    }
}
