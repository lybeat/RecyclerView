using System;
using System.Collections.Generic;

namespace NaiQiu.Framework.View
{
    public class GroupAdapter : Adapter<GroupData>
    {
        private readonly List<GroupData> showList = new();
        private string groupViewName;

        public GroupAdapter(RecyclerView recyclerView, string groupViewName) : base(recyclerView)
        {
            this.groupViewName = groupViewName;
        }

        public GroupAdapter(RecyclerView recyclerView, List<GroupData> list) : base(recyclerView, list)
        {
        }

        public GroupAdapter(RecyclerView recyclerView, List<GroupData> list, Action<GroupData> onItemClick) : base(recyclerView, list, onItemClick)
        {
        }

        public override int GetItemCount()
        {
            return showList.Count;
        }

        public override string GetViewName(int index)
        {
            return showList[index].viewName;
        }

        public override void OnBindViewHolder(ViewHolder viewHolder, int index)
        {
            if (index < 0 || index >= GetItemCount()) return;

            GroupData data = showList[index];

            viewHolder.BindViewData(data);
            viewHolder.BindItemClick(data, t =>
            {
                if (data.viewName == groupViewName)
                {
                    data.bExpand = !data.bExpand;
                    NotifyDataChanged();
                }
                else
                {
                    SetChoiceIndex(index);
                    onItemClick?.Invoke(data);
                }
            });
        }

        public override void NotifyDataChanged()
        {
            foreach (var data in list)
            {
                CreateGroup(data.type);
            }

            var groupList = showList.FindAll(data => data.viewName == groupViewName);
            for (int i = 0; i < groupList.Count; i++)
            {
                int index = showList.IndexOf(groupList[i]);
                Collapse(index);
                if (groupList[i].bExpand)
                {
                    Expand(index);
                }
            }

            foreach (var group in groupList)
            {
                if (list.FindAll(data => data.type == group.type).Count == 0)
                {
                    showList.Remove(group);
                }
            }

            base.NotifyDataChanged();
        }

        public override void SetList(List<GroupData> list)
        {
            showList.Clear();
            base.SetList(list);
        }

        private void CreateGroup(int type)
        {
            var groupData = showList.Find(data => data.type == type && data.viewName == groupViewName);
            if (groupData == null)
            {
                groupData = new GroupData(type, groupViewName, type.ToString());
                showList.Add(groupData);
            }
        }

        public void Expand(int index)
        {
            var expandList = list.FindAll(data => data.type == showList[index].type);
            showList.InsertRange(index + 1, expandList);
        }

        public void Collapse(int index)
        {
            var collapseList = showList.FindAll(data => data.type == showList[index].type && data.viewName != groupViewName);
            showList.RemoveRange(index + 1, collapseList.Count);
        }
    }

    public class GroupData
    {
        public bool bExpand;
        public int type;
        public string viewName;
        public string name;

        public GroupData(int type, string viewName, string name)
        {
            this.type = type;
            this.viewName = viewName;
            this.name = name;
        }
    }
}
