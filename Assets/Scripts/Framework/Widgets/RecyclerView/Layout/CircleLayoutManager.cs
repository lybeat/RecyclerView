using System.Collections.Generic;
using UnityEngine;

namespace NaiQiu.Framework.View
{
    public class CircleLayoutManager : LayoutManager
    {
        private float radius;
        private new CircleDirection direction;
        private float intervalAngle;
        private float initalAngle;

        public override RecyclerView RecyclerView
        {
            get => recyclerView;
            set
            {
                recyclerView = value;
                recyclerView.SetScroller(recyclerView.gameObject.AddComponent<CircleScroller>());
            }
        }

        public CircleLayoutManager(CircleDirection direction = CircleDirection.Positive, float initalAngle = 0)
        {
            this.direction = direction;
            this.initalAngle = initalAngle;
        }

        public override Vector2 CalculateContentSize()
        {
            Vector2 size = viewProvider.CalculateViewSize(0);
            radius = (Mathf.Min(viewportSize.x, viewportSize.y) - Mathf.Min(size.x, size.y)) / 2f - Mathf.Max(padding.x, padding.y);
            intervalAngle = adapter.GetItemCount() > 0 ? 360f / adapter.GetItemCount() : 0;

            return viewportSize;
        }

        public override Vector2 CalculateContentOffset()
        {
            return Vector2.zero;
        }

        public override Vector2 CalculateViewportOffset()
        {
            return Vector2.zero;
        }

        public override void Layout(ViewHolder viewHolder, int index)
        {
            viewHolder.RectTransform.anchoredPosition3D = CalculatePosition(index);
        }

        public override Vector2 CalculatePosition(int index)
        {
            float angle = index * intervalAngle;
            angle = direction == CircleDirection.Positive ? angle : -angle;
            angle += initalAngle + ScrollPosition;
            float radian = angle * (Mathf.PI / 180f);
            float x = radius * Mathf.Sin(radian);
            float y = radius * Mathf.Cos(radian);

            return new Vector2(x, y);
        }

        public override int GetStartIndex()
        {
            return 0;
        }

        public override int GetEndIndex()
        {
            return adapter.GetItemCount() - 1;
        }

        public override bool IsFullVisibleStart(int index) => false;

        public override bool IsFullInvisibleStart(int index) => false;

        public override bool IsFullVisibleEnd(int index) => false;

        public override bool IsFullInvisibleEnd(int index) => false;

        public override bool IsVisible(int index) => true;

        public override float IndexToPosition(int index)
        {
            float position = index * intervalAngle;

            return -position;
        }

        public override int PositionToIndex(float position)
        {
            int index = Mathf.RoundToInt(position / intervalAngle);
            return -index;
        }

        public override void DoItemAnimation()
        {
            List<ViewHolder> viewHolders = viewProvider.ViewHolders;
            for (int i = 0; i < viewHolders.Count; i++)
            {
                float angle = i * intervalAngle + initalAngle;
                angle = direction == CircleDirection.Positive ? angle + ScrollPosition : angle - ScrollPosition;
                float delta = (angle - initalAngle) % 360;
                delta = delta < 0 ? delta + 360 : delta;
                delta = delta > 180 ? 360 - delta : delta;
                float scale = delta < intervalAngle ? (1.4f - delta / intervalAngle) : 1;
                scale = Mathf.Max(scale, 1);

                viewHolders[i].RectTransform.localScale = Vector3.one * scale;
            }
        }
    }

    public enum CircleDirection
    {
        Positive,
        Negative
    }
}
