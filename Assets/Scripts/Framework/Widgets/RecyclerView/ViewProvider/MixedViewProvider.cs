using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NaiQiu.Framework.View
{
    public class MixedViewProvider : ViewProvider
    {
        [SerializeField] private ViewHolder chatLeftViewHolder;
        [SerializeField] private ViewHolder chatRightViewHolder;

        private IMixedObjectPool<ViewHolder> objectPool;
        private Dictionary<string, ViewHolder> dict = new();

        public MixedViewProvider(RecyclerView recyclerView, ViewHolder[] templates) : base(recyclerView, templates)
        {
            foreach (var template in templates)
            {
                dict[template.name] = template;
            }

            UnityMixedComponentFactory<ViewHolder> factory = new(dict, recyclerView.Content);
            objectPool = new MixedObjectPool<ViewHolder>(factory);
        }

        public override ViewHolder GetTemplate(string viewName)
        {
            if (templates == null || templates.Length == 0)
            {
                throw new NullReferenceException("ViewProvider templates can not null or empty.");
            }
            return dict[viewName];
        }

        public override ViewHolder[] GetTemplates()
        {
            if (templates == null || templates.Length == 0)
            {
                throw new NullReferenceException("ViewProvider templates can not null or empty.");
            }
            return dict.Values.ToArray();
        }

        public override ViewHolder Allocate(string viewName)
        {
            var viewHolder = objectPool.Allocate(viewName);
            viewHolder.gameObject.SetActive(true);
            return viewHolder;
        }

        public override void Free(string viewName, ViewHolder viewHolder)
        {
            objectPool.Free(viewName, viewHolder);
        }

        public override void Reset()
        {
            Clear();
            objectPool.Dispose();
        }
    }
}
