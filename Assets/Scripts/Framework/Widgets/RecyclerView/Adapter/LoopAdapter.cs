using System;
using System.Collections.Generic;

namespace NaiQiu.Framework.View
{
    public class LoopAdapter<T> : Adapter<T>
    {
        public LoopAdapter(RecyclerView recyclerView) : base(recyclerView)
        {
        }

        public LoopAdapter(RecyclerView recyclerView, List<T> list) : base(recyclerView, list)
        {
        }

        public LoopAdapter(RecyclerView recyclerView, List<T> list, Action<T> onItemClick) : base(recyclerView, list, onItemClick)
        {
        }

        public override int GetItemCount()
        {
            return int.MaxValue;
        }

        public override int GetRealCount()
        {
            return list == null ? 0 : list.Count;
        }

        public override void OnBindViewHolder(ViewHolder viewHolder, int index)
        {
            index %= list.Count;
            base.OnBindViewHolder(viewHolder, index);
        }
    }
}
