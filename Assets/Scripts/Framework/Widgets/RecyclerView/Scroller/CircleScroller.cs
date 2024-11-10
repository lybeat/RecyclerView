using UnityEngine;
using UnityEngine.EventSystems;

namespace NaiQiu.Framework.View
{
    public class CircleScroller : Scroller
    {
        private Vector2 centerPosition;

        private void Awake()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Vector2 position = transform.position;
            Vector2 size = rectTransform.sizeDelta;

            if (rectTransform.pivot.x == 0)
            {
                centerPosition.x = position.x + size.x / 2f;
            }
            else if (rectTransform.pivot.x == 0.5f)
            {
                centerPosition.x = position.x;
            }
            else
            {
                centerPosition.x = position.x - size.x / 2f;
            }

            if (rectTransform.pivot.y == 0)
            {
                centerPosition.y = position.y + size.y / 2f;
            }
            else if (rectTransform.pivot.y == 0.5f)
            {
                centerPosition.y = position.y;
            }
            else
            {
                centerPosition.y = position.y - size.y / 2f;
            }
        }

        internal override float GetDelta(PointerEventData eventData)
        {
            float delta;
            if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            {
                delta = eventData.position.y > centerPosition.y ? eventData.delta.x : -eventData.delta.x;
            }
            else
            {
                delta = eventData.position.x < centerPosition.x ? eventData.delta.y : -eventData.delta.y;
            }
            return delta * 0.1f;
        }

        protected override void Elastic()
        {
        }
    }
}
