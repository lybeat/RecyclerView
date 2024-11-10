using System;

namespace NaiQiu.Framework.View
{
    public sealed class SimpleViewProvider : ViewProvider
    {
        private readonly IObjectPool<ViewHolder> objectPool;

        public SimpleViewProvider(RecyclerView recyclerView, ViewHolder[] templates) : base(recyclerView, templates)
        {
            UnityComponentFactory<ViewHolder> factory = new(GetTemplate(), recyclerView.Content);
            objectPool = new ObjectPool<ViewHolder>(factory, 100);
        }

        public override ViewHolder GetTemplate(string viewName = "")
        {
            if (templates == null || templates.Length == 0)
            {
                throw new NullReferenceException("ViewProvider templates can not null or empty.");
            }
            return templates[0];
        }

        public override ViewHolder[] GetTemplates()
        {
            if (templates == null || templates.Length == 0)
            {
                throw new NullReferenceException("ViewProvider templates can not null or empty.");
            }
            return templates;
        }

        public override ViewHolder Allocate(string viewName)
        {
            var viewHolder = objectPool.Allocate();
            viewHolder.gameObject.SetActive(true);
            return viewHolder;
        }

        public override void Free(string viewName, ViewHolder viewHolder)
        {
            objectPool.Free(viewHolder);
        }

        public override void Reset()
        {
            Clear();
            objectPool.Dispose();
        }
    }
}
