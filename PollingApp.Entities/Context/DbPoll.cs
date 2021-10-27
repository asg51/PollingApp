
namespace PollingApp.Entities.Context
{
    public delegate void Control();
    public class DbPoll : ContextBase<Poll>
    {
        public event Control ControlEvent;

        public int GetIndex(int index)
        {
            return Get(index).Index;
        }

        public Poll this[int index]
        {
            get
            {
                foreach (Poll poll in GetList())
                {
                    if (poll.Index == index)
                        return poll;
                }
                return null;
            }
        }
        public int GetLastIndex()
        {
            if (GetList().Count == 0)
                return 0;
            return GetList()[GetList().Count - 1].Index;
        }
        public override void Add(Poll name)
        {
            base.Add(name);
            if (ControlEvent != null)
                ControlEvent();
        }
        public override void Delete(int index)
        {
            base.Delete(index);
            if (ControlEvent != null)
                ControlEvent();
        }
        public override void Delete(Poll t)
        {
            base.Delete(t);
            if (ControlEvent != null)
                ControlEvent();
        }
        public Poll Search(string name)
        {
            foreach (Poll poll in GetList())
                if (poll.PollingName == name)
                    return poll;
            return null;
        }
    }
}
