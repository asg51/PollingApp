namespace PollingApp.Entities.Context
{
    public class DbAdmins : ContextBase<Admin>
    {
        public int GetIndex(int index)
        {
            return Get(index).Index;
        }

        public Admin this[int index]
        {
            get
            {
                foreach (Admin admin in GetList())
                {
                    if (admin.Index == index)
                        return admin;
                }
                return null;
            }
        }
    }
}
