using UnityEngine;

namespace NaiQiu.Framework.View
{
    public class MixedLayoutManager : LayoutManager
    {
        public MixedLayoutManager() { }

        public override Vector2 CalculateContentSize()
        {
            int index = adapter.GetItemCount();
            float position = 0;
            for (int i = 0; i < index; i++)
            {
                position += GetLength(i);
            }

            return direction == Direction.Vertical ?
                        new Vector2(contentSize.x, position - spacing.y + padding.y * 2) :
                        new Vector2(position - spacing.x + padding.x * 2, contentSize.y);
        }

        public override Vector2 CalculatePosition(int index)
        {
            // TODO 优化点，将 position 定义成全局变量
            float position = 0;
            for (int i = 0; i < index; i++)
            {
                position += GetLength(i);
            }
            position -= ScrollPosition;
            return direction == Direction.Vertical ? new Vector2(0, position + padding.y) : new Vector2(position + padding.x, 0);
        }

        public override Vector2 CalculateContentOffset()
        {
            Vector2 size = viewProvider.CalculateViewSize(0);
            float len = GetFitContentSize();
            if (direction == Direction.Vertical)
            {
                return new Vector2(0, (len - size.y) / 2);
            }
            return new Vector2((len - size.x) / 2, 0);
        }

        public override Vector2 CalculateViewportOffset()
        {
            Vector2 size = viewProvider.CalculateViewSize(0);
            if (direction == Direction.Vertical)
            {
                return new Vector2(0, (viewportSize.y - size.y) / 2);
            }
            return new Vector2((viewportSize.x - size.x) / 2, 0);
        }

        public override int GetStartIndex()
        {
            float position = 0;
            float contentPosition = ScrollPosition;
            int itemCount = adapter.GetItemCount();
            for (int i = 0; i < itemCount; i++)
            {
                position += GetLength(i);

                if (position > contentPosition)
                {
                    return Mathf.Max(0, i);
                }
            }
            return 0;
        }

        public override int GetEndIndex()
        {
            float position = 0;
            float viewLength = direction == Direction.Vertical ? viewportSize.y : viewportSize.x;
            int itemCount = adapter.GetItemCount();
            for (int i = 0; i < itemCount; i++)
            {
                position += GetLength(i);

                if (position > ScrollPosition + viewLength)
                {
                    return Mathf.Min(i, adapter.GetItemCount() - 1); ;
                }
            }
            return itemCount - 1;
        }

        private float GetLength(int index)
        {
            Vector2 size = viewProvider.CalculateViewSize(index);
            if (index < adapter.GetItemCount() - 1)
            {
                size += spacing;
            }
            float len = direction == Direction.Vertical ? size.y : size.x;
            return len;
        }

        public override float IndexToPosition(int index)
        {
            Vector2 position = CalculatePosition(index);
            if (direction == Direction.Vertical)
            {
                position.y = Mathf.Max(0, position.y);
                position.y = Mathf.Min(position.y, contentSize.y - viewportSize.y);
                return position.y;
            }
            else
            {
                position.x = Mathf.Max(0, position.x);
                position.x = Mathf.Min(position.x, contentSize.x - viewportSize.x);
                return position.x;
            }
        }

        public override int PositionToIndex(float position)
        {
            float len = 0;

            int itemCount = adapter.GetItemCount();
            for (int i = 0; i < itemCount; i++)
            {
                len += GetLength(i);

                if (len >= position)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
