using System;
using System.Collections.Generic;

namespace NaiQiu.Framework.View
{
    public class MixedAdapter : Adapter<MixedData>
    {
        public MixedAdapter(RecyclerView recyclerView) : base(recyclerView)
        {
        }

        public MixedAdapter(RecyclerView recyclerView, List<MixedData> list) : base(recyclerView, list)
        {
        }

        public MixedAdapter(RecyclerView recyclerView, List<MixedData> list, Action<MixedData> onItemClick) : base(recyclerView, list, onItemClick)
        {
        }

        public override string GetViewName(int index)
        {
            return list[index].viewName;
        }
    }

    public class MixedData
    {
        public string viewName;
        public string name;
        public string icon;
        public int number;
        public int percent;

        public MixedData(string viewName, string name, string icon, int number, int percent)
        {
            this.viewName = viewName;
            this.name = name;
            this.icon = icon;
            this.number = number;
            this.percent = percent;
        }
    }
}
