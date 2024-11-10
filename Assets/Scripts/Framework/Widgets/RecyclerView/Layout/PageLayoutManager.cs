using System;
using System.Collections.Generic;
using UnityEngine;

namespace NaiQiu.Framework.View
{
    public class PageLayoutManager : LinearLayoutManager
    {
        private float minScale;

        public PageLayoutManager(float minScale = 0.9f)
        {
            this.minScale = minScale;
        }

        public override Vector2 CalculateContentSize()
        {
            Vector2 size = viewProvider.CalculateViewSize(0);
            lineHeight = direction == Direction.Vertical ? size.y : size.x;

            int index = adapter.GetItemCount();
            float position;
            if (direction == Direction.Vertical)
            {
                position = index * (lineHeight + spacing.y) - spacing.y;
                position += viewportSize.y - lineHeight;
                return new Vector2(contentSize.x, position + padding.y * 2);
            }
            position = index * (lineHeight + spacing.x) - spacing.x;
            position += viewportSize.x - lineHeight;
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
            return Vector2.zero;
        }

        public override Vector2 CalculateViewportOffset()
        {
            return Vector2.zero;
        }

        protected override float GetOffset()
        {
            float offset = direction == Direction.Vertical ? viewportSize.y - lineHeight : viewportSize.x - lineHeight;
            return offset / 2;
        }

        public override int PositionToIndex(float position)
        {
            float len = direction == Direction.Vertical ? lineHeight + spacing.y : lineHeight + spacing.x;
            float pos = IndexToPosition(recyclerView.CurrentIndex);
            // 根据是前划还是后划，来加减偏移量
            int index = position > pos ? Mathf.RoundToInt(position / len + 0.25f) : Mathf.RoundToInt(position / len - 0.25f);

            return index;
        }

        public override void DoItemAnimation()
        {
            List<ViewHolder> viewHolders = viewProvider.ViewHolders;
            for (int i = 0; i < viewHolders.Count; i++)
            {
                float viewPos = direction == Direction.Vertical ?
                                -viewHolders[i].RectTransform.anchoredPosition.y :
                                viewHolders[i].RectTransform.anchoredPosition.x;
                float scale = 1 - Mathf.Min(Mathf.Abs(viewPos) * 0.0006f, 1f);
                scale = Mathf.Max(scale, minScale);

                viewHolders[i].RectTransform.localScale = Vector3.one * scale;
            }
        }
    }
}
