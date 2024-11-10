using System;
using System.Collections.Generic;

namespace NaiQiu.Framework.View
{
    public class Adapter<T> : IAdapter
    {
        protected RecyclerView recyclerView;

        protected List<T> list;
        protected Action<T> onItemClick;

        protected int choiceIndex = -1;
        public int ChoiceIndex
        {
            get => choiceIndex;
            set
            {
                SetChoiceIndex(value);
            }
        }

        public Adapter(RecyclerView recyclerView) : this(recyclerView, new List<T>(), null)
        {
        }

        public Adapter(RecyclerView recyclerView, List<T> list) : this(recyclerView, list, null)
        {
        }

        public Adapter(RecyclerView recyclerView, List<T> list, Action<T> onItemClick)
        {
            this.recyclerView = recyclerView;
            this.list = list;
            this.onItemClick = onItemClick;

            this.recyclerView.Adapter = this;
        }

        public virtual int GetItemCount()
        {
            return list == null ? 0 : list.Count;
        }

        public virtual int GetRealCount()
        {
            return GetItemCount();
        }

        public virtual string GetViewName(int index)
        {
            return "";
        }

        public virtual void OnBindViewHolder(ViewHolder viewHolder, int index)
        {
            if (index < 0 || index >= GetItemCount()) return;

            T data = list[index];

            viewHolder.BindViewData(data);
            viewHolder.BindItemClick(data, t =>
            {
                SetChoiceIndex(index);
                onItemClick?.Invoke(data);
            });
            viewHolder.BindChoiceState(index == choiceIndex);
        }

        public virtual void NotifyDataChanged()
        {
            recyclerView.RequestLayout();
            recyclerView.Refresh();
        }

        public virtual void SetList(List<T> list)
        {
            this.list = list;
            recyclerView.Reset();
            NotifyDataChanged();
        }

        public T GetData(int index)
        {
            if (index < 0 || index >= GetItemCount()) return default;

            return list[index];
        }

        public void Add(T item)
        {
            list.Add(item);
            NotifyDataChanged();
        }

        public void AddRange(IEnumerable<T> collection)
        {
            list.AddRange(collection);
            NotifyDataChanged();
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
            NotifyDataChanged();
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            list.InsertRange(index, collection);
            NotifyDataChanged();
        }

        public void Remove(T item)
        {
            int index = list.IndexOf(item);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= GetItemCount()) return;

            list.RemoveAt(index);
            NotifyDataChanged();
        }

        public void RemoveRange(int index, int count)
        {
            list.RemoveRange(index, count);
            NotifyDataChanged();
        }

        public void RemoveAll(Predicate<T> match)
        {
            list.RemoveAll(match);
            NotifyDataChanged();
        }

        public void Clear()
        {
            list.Clear();
            NotifyDataChanged();
        }

        public void Reverse(int index, int count)
        {
            list.Reverse(index, count);
            NotifyDataChanged();
        }

        public void Reverse()
        {
            list.Reverse();
            NotifyDataChanged();
        }

        public void Sort(Comparison<T> comparison)
        {
            list.Sort(comparison);
            NotifyDataChanged();
        }

        public void SetOnItemClick(Action<T> onItemClick)
        {
            this.onItemClick = onItemClick;
        }

        protected void SetChoiceIndex(int index)
        {
            if (index == choiceIndex) return;

            if (choiceIndex != -1)
            {
                if (TryGetViewHolder(choiceIndex, out var viewHolder))
                {
                    viewHolder.BindChoiceState(false);
                }
            }

            choiceIndex = index;

            if (choiceIndex != -1)
            {
                if (TryGetViewHolder(choiceIndex, out var viewHolder))
                {
                    viewHolder.BindChoiceState(true);
                }
            }
        }

        private bool TryGetViewHolder(int index, out ViewHolder viewHolder)
        {
            viewHolder = recyclerView.ViewProvider.GetViewHolder(index);
            return viewHolder != null;
        }
    }
}
