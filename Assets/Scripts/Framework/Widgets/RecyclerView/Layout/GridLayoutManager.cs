using UnityEngine;

namespace NaiQiu.Framework.View
{
    public class GridLayoutManager : LayoutManager
    {
        private Vector2 cellSize;

        public GridLayoutManager(int unit = 1)
        {
            this.unit = unit;
        }

        public override Vector2 CalculateContentSize()
        {
            cellSize = viewProvider.CalculateViewSize(0);

            int row = Mathf.CeilToInt(adapter.GetItemCount() / (float)unit);
            float len;
            if (direction == Direction.Vertical)
            {
                len = row * (cellSize.y + spacing.y) - spacing.y;
                return new Vector2(contentSize.x, len + padding.y * 2);
            }

            len = row * (cellSize.x + spacing.x) - spacing.x;
            return new Vector2(len, contentSize.y + padding.x * 2);
        }

        public override Vector2 CalculatePosition(int index)
        {
            int row = index / unit;
            int column = index % unit;
            float x, y;
            if (direction == Direction.Vertical)
            {
                x = column * (cellSize.x + spacing.x);
                y = row * (cellSize.y + spacing.y) - ScrollPosition;
            }
            else
            {
                x = row * (cellSize.x + spacing.x) - ScrollPosition;
                y = column * (cellSize.y + spacing.y);
            }

            return new Vector2(x + padding.x, y + padding.y);
        }

        public override Vector2 CalculateContentOffset()
        {
            float width, height;
            if (alignment == Alignment.Center)
            {
                width = Mathf.Min(contentSize.x, viewportSize.x);
                height = Mathf.Min(contentSize.y, viewportSize.y);
            }
            else
            {
                width = viewportSize.x;
                height = viewportSize.y;
            }
            return new Vector2((width - cellSize.x) / 2, (height - cellSize.y) / 2);
        }

        public override Vector2 CalculateViewportOffset()
        {
            float width, height;
            if (alignment == Alignment.Center)
            {
                width = Mathf.Min(contentSize.x, viewportSize.x);
                height = Mathf.Min(contentSize.y, viewportSize.y);
            }
            else
            {
                width = viewportSize.x;
                height = viewportSize.y;
            }
            return new Vector2((width - cellSize.x) / 2, (height - cellSize.y) / 2);
        }

        public override int GetStartIndex()
        {
            float len = direction == Direction.Vertical ? cellSize.y + spacing.y : cellSize.x + spacing.x;
            int index = Mathf.FloorToInt(ScrollPosition / len) * unit;
            return Mathf.Max(0, index);
        }

        public override int GetEndIndex()
        {
            float viewLength = direction == Direction.Vertical ? viewportSize.y : viewportSize.x;
            float len = direction == Direction.Vertical ? cellSize.y + spacing.y : cellSize.x + spacing.x;
            int index = Mathf.FloorToInt((ScrollPosition + viewLength) / len) * unit;
            return Mathf.Min(index, adapter.GetItemCount() - 1);
        }

        public override float IndexToPosition(int index)
        {
            int row = index / unit;
            float len, viewLength, position;
            if (direction == Direction.Vertical)
            {
                len = row * (cellSize.y + spacing.y);
                viewLength = viewportSize.y;
                position = len + viewLength > contentSize.y ? contentSize.y - viewportSize.y : len;
            }
            else
            {
                len = row * (cellSize.x + spacing.x);
                viewLength = viewportSize.x;
                position = len + viewLength > contentSize.x ? contentSize.x - viewportSize.x : len;
            }

            return position;
        }

        public override int PositionToIndex(float position)
        {
            float len = direction == Direction.Vertical ? cellSize.y + spacing.y : cellSize.x + spacing.x;
            int index = Mathf.RoundToInt(position / len);

            return index * unit;
        }
    }
}
