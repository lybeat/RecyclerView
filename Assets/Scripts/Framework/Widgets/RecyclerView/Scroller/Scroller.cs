using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NaiQiu.Framework.View
{
    public class Scroller : MonoBehaviour, IScroller, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler
    {
        protected float position;
        public float Position { get => position; set => position = value; }

        protected float velocity;
        public float Velocity => velocity;

        protected Direction direction;
        public Direction Direction
        {
            get => direction;
            set => direction = value;
        }

        /// <summary>
        /// 内容所需要大小
        /// </summary>
        protected Vector2 contentSize;
        public Vector2 ContentSize
        {
            get => contentSize;
            set => contentSize = value;
        }

        /// <summary>
        /// 所在 View 的真实大小
        /// </summary>
        protected Vector2 viewSize;
        public Vector2 ViewSize
        {
            get => viewSize;
            set => viewSize = value;
        }

        protected float scrollSpeed = 1f;
        public float ScrollSpeed
        {
            get => scrollSpeed;
            set => scrollSpeed = value;
        }

        protected float wheelSpeed = 30f;
        public float WheelSpeed
        {
            get => wheelSpeed;
            set => wheelSpeed = value;
        }

        protected bool snap;
        public bool Snap
        {
            get => snap;
            set => snap = value;
        }

        protected ScrollerEvent scrollerEvent = new();
        protected MoveStopEvent moveStopEvent = new();
        protected DraggingEvent draggingEvent = new();

        public float MaxPosition => direction == Direction.Vertical ?
                                Mathf.Max(contentSize.y - viewSize.y, 0) :
                                Mathf.Max(contentSize.x - viewSize.x, 0);

        public float ViewLength => direction == Direction.Vertical ? viewSize.y : viewSize.x;

        public ScrollerEvent OnValueChanged { get => scrollerEvent; set => scrollerEvent = value; }

        public MoveStopEvent OnMoveStoped { get => moveStopEvent; set => moveStopEvent = value; }

        public DraggingEvent OnDragging { get => draggingEvent; set => draggingEvent = value; }

        // 停止滑动的时间，但此时并未释放鼠标按键
        private float dragStopTime = 0f;

        public virtual void ScrollTo(float position, bool smooth = false)
        {
            if (position == this.position) return;

            if (!smooth)
            {
                this.position = position;
                OnValueChanged?.Invoke(this.position);
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(MoveTo(position));
            }
        }

        public virtual void ScrollToRatio(float ratio)
        {
            ScrollTo(MaxPosition * ratio, false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnDragging?.Invoke(true);
            StopAllCoroutines();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Inertia();
            Elastic();
            OnDragging?.Invoke(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            dragStopTime = Time.time;

            velocity = GetDelta(eventData);
            position += velocity;

            OnValueChanged?.Invoke(position);
        }

        public void OnScroll(PointerEventData eventData)
        {
            StopAllCoroutines();
            
            float rate = GetScrollRate() * wheelSpeed;
            velocity = direction == Direction.Vertical ? -eventData.scrollDelta.y * rate : eventData.scrollDelta.x * rate;
            position += velocity;

            OnValueChanged?.Invoke(position);

            Elastic();
        }

        internal virtual float GetDelta(PointerEventData eventData)
        {
            float rate = GetScrollRate();
            return direction == Direction.Vertical ? eventData.delta.y * rate : -eventData.delta.x * rate;
        }

        private float GetScrollRate()
        {
            float rate = 1f;
            if (position < 0)
            {
                rate = Mathf.Max(0, 1 - (Mathf.Abs(position) / ViewLength));
            }
            else if (position > MaxPosition)
            {
                rate = Mathf.Max(0, 1 - (Mathf.Abs(position - MaxPosition) / ViewLength));
            }
            return rate;
        }

        /// <summary>
        /// 松手时的惯性滑动
        /// </summary>
        protected virtual void Inertia()
        {
            // 松手时的时间 离 停止滑动的时间 超过一定时间，则认为此次惯性滑动无效
            if (!snap && (Time.time - dragStopTime) > 0.01f) return;

            if (Mathf.Abs(velocity) > 0.1f)
            {
                StopAllCoroutines();
                StartCoroutine(InertiaTo());
            }
            else
            {
                OnMoveStoped?.Invoke();
            }
        }

        /// <summary>
        /// 滑动到顶部/底部之后，松手时回弹
        /// </summary>
        protected virtual void Elastic()
        {
            if (position < 0)
            {
                StopAllCoroutines();
                StartCoroutine(ElasticTo(0));
            }
            else if (position > MaxPosition)
            {
                StopAllCoroutines();
                StartCoroutine(ElasticTo(MaxPosition));
            }
        }

        IEnumerator InertiaTo()
        {
            float timer = 0f;
            float p = position;
            float v = velocity > 0 ? Mathf.Min(velocity, 100) : Mathf.Max(velocity, -100);
            float duration = snap ? 0.1f : 1f;
            while (timer < duration)
            {
                float y = (float)EaseUtil.EaseOutCirc(timer) * 40;
                timer += Time.deltaTime;
                position = p + y * v;

                Elastic();

                OnValueChanged?.Invoke(position);

                yield return new WaitForEndOfFrame();
            }

            OnMoveStoped?.Invoke();
        }

        IEnumerator ElasticTo(float targetPos)
        {
            yield return ToPosition(targetPos, 7);
        }

        IEnumerator MoveTo(float targetPos)
        {
            yield return ToPosition(targetPos, scrollSpeed);
        }

        IEnumerator ToPosition(float targetPos, float speed)
        {
            float startPos = position;
            float time = Time.deltaTime;
            while (Mathf.Abs(targetPos - position) > 0.1f)
            {
                position = Mathf.Lerp(startPos, targetPos, time * speed);
                OnValueChanged?.Invoke(position);

                time += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            position = targetPos;
            OnValueChanged?.Invoke(position);
        }
    }
}
