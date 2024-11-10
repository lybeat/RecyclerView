using UnityEngine;

namespace NaiQiu.Framework.View
{
    public class LinearLayoutManager : LayoutManager
    {
        protected float lineHeight;

        public LinearLayoutManager() { }

        public override Vector2 CalculateContentSize()
        {
            Vector2 size = viewProvider.CalculateViewSize(0);
            lineHeight = direction == Direction.Vertical ? size.y : size.x;

            int index = adapter.GetItemCount();
            float position;
            if (direction == Direction.Vertical)
            {
                position = index * (lineHeight + spacing.y) - spacing.y;
                return new Vector2(contentSize.x, position + padding.y * 2);
            }
            position = index * (lineHeight + spacing.x) - spacing.x;
            return new Vector2(position + padding.x * 2, contentSize.y);
        }

        public override Vector2 CalculatePosition(int index)
        {
            float position;
            if (direction == Direction.Vertical)
            {
                position = index * (lineHeight + spacing.y) - ScrollPosition;
                return new Vector2(0, position + padding.y);
            }
            position = index * (lineHeight + spacing.x) - ScrollPosition;
            return new Vector2(position + padding.x, 0);
        }

        public override Vector2 CalculateContentOffset()
        {
            float len = GetFitContentSize();
            if (direction == Direction.Vertical)
            {
                return new Vector2(0, (len - lineHeight) / 2);
            }
            return new Vector2((len - lineHeight) / 2, 0);
        }

        public override Vector2 CalculateViewportOffset()
        {
            if (direction == Direction.Vertical)
            {
                return new Vector2(0, (viewportSize.y - lineHeight) / 2);
            }
            return new Vector2((viewportSize.x - lineHeight) / 2, 0);
        }

        public override int GetStartIndex()
        {
            float len = direction == Direction.Vertical ? lineHeight + spacing.y : lineHeight + spacing.x;
            int index = Mathf.FloorToInt(ScrollPosition / len);
            return Mathf.Max(0, index);
        }

        public override int GetEndIndex()
        {
            float viewLength = direction == Direction.Vertical ? viewportSize.y : viewportSize.x;
            float len = direction == Direction.Vertical ? lineHeight + spacing.y : lineHeight + spacing.x;
            int index = Mathf.FloorToInt((ScrollPosition + viewLength) / len);
            return Mathf.Min(index, adapter.GetItemCount() - 1);
        }

        public override float IndexToPosition(int index)
        {
            if (index < 0 || index >= adapter.GetItemCount()) return 0;

            float len, viewLength, position;
            if (direction == Direction.Vertical)
            {
                len = index * (lineHeight + spacing.y);
                viewLength = viewportSize.y;
                position = len + viewLength > contentSize.y ? contentSize.y - viewportSize.y : len;
            }
            else
            {
                len = index * (lineHeight + spacing.x);
                viewLength = viewportSize.x;
                position = len + viewLength > contentSize.x ? contentSize.x - viewportSize.x : len;
            }

            return position;
        }

        public override int PositionToIndex(float position)
        {
            float len = direction == Direction.Vertical ? lineHeight + spacing.y : lineHeight + spacing.x;
            int index = Mathf.RoundToInt(position / len);

            return index;
        }
    }
}
