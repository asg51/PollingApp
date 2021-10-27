using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PollingApp.Entities.Context
{
    public class ContextBase<T>
    {
        private IList<T> List;
        public ContextBase()
        {
            List = new List<T>();
        }
        public ContextBase(T t)
        {
            List = new List<T>
            {
                t
            };
        }
        public virtual void Add(T name) => List.Add(name);
        public IList<T> GetList() => List;
        public T Get(int index) => List[index];
        public virtual void Delete(int index) => List.RemoveAt(index);
        public virtual void Delete(T t) => List.Remove(t);
        public int Count { get => List.Count; }
        public T Search(T t) => List.FirstOrDefault<T>();
        public void SetList(IList<T> t) => List = t;
    }
}
