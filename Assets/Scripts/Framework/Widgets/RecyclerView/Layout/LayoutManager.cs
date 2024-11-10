using UnityEngine;

namespace NaiQiu.Framework.View
{
    public abstract class LayoutManager : ILayoutManager
    {
        protected Vector2 viewportSize;
        public Vector2 ViewportSize
        {
            get => viewportSize;
            private set => viewportSize = value;
        }

        protected Vector2 contentSize;
        public Vector2 ContentSize
        {
            get => contentSize;
            private set => contentSize = value;
        }

        protected Vector2 contentOffset;
        public Vector2 ContentOffset
        {
            get => contentOffset;
            private set => contentOffset = value;
        }

        protected Vector2 viewportOffset;
        public Vector2 ViewportOffset
        {
            get => viewportOffset;
            private set => viewportOffset = value;
        }

        protected IAdapter adapter;
        public IAdapter Adapter
        {
            get => adapter;
            set => adapter = value;
        }

        protected ViewProvider viewProvider;
        public ViewProvider ViewProvider
        {
            get => viewProvider;
            set => viewProvider = value;
        }

        protected RecyclerView recyclerView;
        public virtual RecyclerView RecyclerView
        {
            get => recyclerView;
            set => recyclerView = value;
        }

        protected Direction direction;
        public Direction Direction
        {
            get => direction;
            set => direction = value;
        }

        protected Alignment alignment;
        public Alignment Alignment
        {
            get => alignment;
            set => alignment = value;
        }

        protected Vector2 spacing;
        public Vector2 Spacing
        {
            get => spacing;
            set => spacing = value;
        }

        protected Vector2 padding;
        public Vector2 Padding
        {
            get => padding;
            set => padding = value;
        }

        protected int unit = 1;
        public int Unit
        {
            get => unit;
            set => unit = value;
        }

        protected bool canScroll;
        public bool CanScroll
        {
            get => canScroll;
            set => canScroll = value;
        }

        public float ScrollPosition => recyclerView.GetScrollPosition();

        public LayoutManager() { }

        public void SetContentSize()
        {
            viewportSize = recyclerView.GetComponent<RectTransform>().rect.size;
            contentSize = CalculateContentSize();
            contentOffset = CalculateContentOffset();
            viewportOffset = CalculateViewportOffset();
        }

        public void UpdateLayout()
        {
            foreach (var viewHolder in viewProvider.ViewHolders)
            {
                Layout(viewHolder, viewHolder.Index);
            }
        }

        public virtual void Layout(ViewHolder viewHolder, int index)
        {
            Vector2 pos = CalculatePosition(index);
            Vector3 position = direction == Direction.Vertical ?
                                new Vector3(pos.x - contentOffset.x, -pos.y + contentOffset.y, 0) :
                                new Vector3(pos.x - contentOffset.x, -pos.y + contentOffset.y, 0);
            viewHolder.RectTransform.anchoredPosition3D = position;
        }

        public abstract Vector2 CalculateContentSize();

        public abstract Vector2 CalculatePosition(int index);

        public abstract Vector2 CalculateContentOffset();

        public abstract Vector2 CalculateViewportOffset();

        public abstract int GetStartIndex();

        public abstract int GetEndIndex();

        public abstract float IndexToPosition(int index);

        public abstract int PositionToIndex(float position);

        public virtual void DoItemAnimation() { }

        public virtual bool IsFullVisibleStart(int index)
        {
            Vector2 vector2 = CalculatePosition(index);
            float position = direction == Direction.Vertical ? vector2.y : vector2.x;
            return position + GetOffset() >= 0;
        }

        public virtual bool IsFullInvisibleStart(int index)
        {
            Vector2 vector2 = CalculatePosition(index + unit);
            float position = direction == Direction.Vertical ? vector2.y : vector2.x;
            return position + GetOffset() < 0;
        }

        public virtual bool IsFullVisibleEnd(int index)
        {
            Vector2 vector2 = CalculatePosition(index + unit);
            float position = direction == Direction.Vertical ? vector2.y : vector2.x;
            float viewLength = direction == Direction.Vertical ? viewportSize.y : viewportSize.x;
            return position + GetOffset() <= viewLength;
        }

        public virtual bool IsFullInvisibleEnd(int index)
        {
            Vector2 vector2 = CalculatePosition(index);
            float position = direction == Direction.Vertical ? vector2.y : vector2.x;
            float viewLength = direction == Direction.Vertical ? viewportSize.y : viewportSize.x;
            return position + GetOffset() > viewLength;
        }

        public virtual bool IsVisible(int index)
        {
            float position, viewLength;
            viewLength = direction == Direction.Vertical ? viewportSize.y : viewportSize.x;

            Vector2 vector2 = CalculatePosition(index);
            position = direction == Direction.Vertical ? vector2.y : vector2.x;
            if (position + GetOffset() > 0 && position + GetOffset() <= viewLength)
            {
                return true;
            }

            vector2 = CalculatePosition(index + unit);
            position = direction == Direction.Vertical ? vector2.y : vector2.x;
            if (position + GetOffset() > 0 && position + GetOffset() <= viewLength)
            {
                return true;
            }

            return false;
        }

        protected virtual float GetFitContentSize()
        {
            float len;
            if (direction == Direction.Vertical)
            {
                len = alignment == Alignment.Center ? Mathf.Min(contentSize.y, viewportSize.y) : viewportSize.y;
            }
            else
            {
                len = alignment == Alignment.Center ? Mathf.Min(contentSize.x, viewportSize.x) : viewportSize.x;
            }
            return len;
        }

        protected virtual float GetOffset()
        {
            return direction == Direction.Vertical ? -contentOffset.y + viewportOffset.y : -contentOffset.x + viewportOffset.x;
        }
    }

    public enum Direction
    {
        Vertical = 1,
        Horizontal = 2,
        Custom = 10
    }

    public enum Alignment
    {
        Left,
        Center,
        Top
    }
}
