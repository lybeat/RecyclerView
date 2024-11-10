using System;
using UnityEngine;
using UnityEngine.UI;

namespace NaiQiu.Framework.View
{
    public class RecyclerView : MonoBehaviour
    {
        [SerializeField] private Direction direction;
        public Direction Direction
        {
            get => direction;
            set => direction = value;
        }

        [SerializeField] private Alignment alignment;
        public Alignment Alignment
        {
            get => alignment;
            set => alignment = value;
        }

        [SerializeField] private Vector2 spacing;
        public Vector2 Spacing
        {
            get => spacing;
            set => spacing = value;
        }

        [SerializeField] private Vector2 padding;
        public Vector2 Padding
        {
            get => padding;
            set => padding = value;
        }

        [SerializeField] private bool scroll;
        public bool Scroll
        {
            get => scroll;
            set => scroll = value;
        }

        [SerializeField] private bool snap;
        public bool Snap
        {
            get => snap;
            set => snap = value & scroll;
        }

        [SerializeField, Range(1f, 50f)] private float scrollSpeed = 7f;
        public float ScrollSpeed
        {
            get => scrollSpeed;
            set => scrollSpeed = value;
        }

        [SerializeField, Range(10f, 50f)] private float wheelSpeed = 30f;
        public float WheeelSpeed
        {
            get => wheelSpeed;
            set => wheelSpeed = value;
        }

        [SerializeField] private ViewHolder[] templates;
        public ViewHolder[] Templates
        {
            get => templates;
            set => templates = value;
        }

        private ViewProvider viewProvider;
        private LayoutManager layoutManager;
        private Scroller scroller;
        private Scrollbar scrollbar;

        private int startIndex, endIndex;
        private int currentIndex;
        public int CurrentIndex
        {
            get => currentIndex;
            set => currentIndex = value;
        }

        public bool CanScroll => true;

        private RectTransform content;
        public RectTransform Content
        {
            get
            {
                if (content == null)
                {
                    content = transform.GetChild(0).GetComponent<RectTransform>();
                }
                return content;
            }
        }

        public ViewProvider ViewProvider
        {
            get
            {
                viewProvider ??= templates.Length > 1 ? new MixedViewProvider(this, templates) : new SimpleViewProvider(this, templates);
                return viewProvider;
            }
        }

        public Scroller Scroller
        {
            get
            {
                if (scroller == null)
                {
                    if (scroll)
                    {
                        scroller = gameObject.AddComponent<Scroller>();
                        ConfigScroller();
                    }
                }
                return scroller;
            }
        }

        public Scrollbar Scrollbar
        {
            get
            {
                if (scrollbar == null)
                {
                    scrollbar = GetComponentInChildren<Scrollbar>();
                    if (scrollbar != null)
                    {
                        scrollbar.gameObject.SetActive(scroll);
                        scrollbar.onValueChanged.AddListener(OnScrollbarChanged);
                        scrollbar.gameObject.AddComponent<ScrollbarEx>().OnDragEnd = OnScrollbarDragEnd;
                    }
                }
                return scrollbar;
            }
        }

        public IAdapter Adapter { get; set; }

        public LayoutManager LayoutManager => layoutManager;

        public Action<int> OnIndexChanged;
        public Action OnScrollValueChanged;

        private void OnValidate()
        {
            if (scroller != null)
            {
                scroller.ScrollSpeed = scrollSpeed;
                scroller.WheelSpeed = wheelSpeed;
            }
        }

        private void OnScrollChanged(float pos)
        {
            layoutManager.UpdateLayout();

            if (Scrollbar != null)
            {
                Scrollbar.SetValueWithoutNotify(pos / Scroller.MaxPosition);
            }

            if (layoutManager.IsFullInvisibleStart(startIndex))
            {
                viewProvider.RemoveViewHolder(startIndex);
                startIndex += layoutManager.Unit;
            }
            else if (layoutManager.IsFullVisibleStart(startIndex))
            {
                if (startIndex == 0)
                {
                    // TODO Do something, eg: Refresh
                }
                else
                {
                    startIndex -= layoutManager.Unit;
                    viewProvider.CreateViewHolder(startIndex);
                }
            }

            if (layoutManager.IsFullInvisibleEnd(endIndex))
            {
                viewProvider.RemoveViewHolder(endIndex);
                endIndex -= layoutManager.Unit;
            }
            else if (layoutManager.IsFullVisibleEnd(endIndex))
            {
                if (endIndex >= viewProvider.GetItemCount() - layoutManager.Unit)
                {
                    // TODO Do something, eg: Load More
                }
                else
                {
                    endIndex += layoutManager.Unit;
                    viewProvider.CreateViewHolder(endIndex);
                }
            }

            // 使用滚动条快速定位时，刷新整个列表
            if (!layoutManager.IsVisible(startIndex) || !layoutManager.IsVisible(endIndex))
            {
                Refresh();
            }

            layoutManager.DoItemAnimation();

            OnScrollValueChanged?.Invoke();
        }

        private void OnMoveStoped()
        {
            if (Snap)
            {
                SnapTo();
            }
        }

        private void OnScrollbarChanged(float ratio)
        {
            Scroller.ScrollToRatio(ratio);
        }

        private void OnScrollbarDragEnd()
        {
            if (Scroller.Position < Scroller.MaxPosition)
            {
                if (Snap)
                {
                    SnapTo();
                }
            }
        }

        public void Reset()
        {
            viewProvider?.Reset();
            if (scroller != null)
            {
                scroller.Position = 0;
            }
            if (scrollbar != null)
            {
                scrollbar.SetValueWithoutNotify(0);
            }
        }

        public void SetLayoutManager(LayoutManager layoutManager)
        {
            this.layoutManager = layoutManager;

            ViewProvider.Adapter = Adapter;
            ViewProvider.LayoutManager = layoutManager;

            this.layoutManager.RecyclerView = this;
            this.layoutManager.Adapter = Adapter;
            this.layoutManager.ViewProvider = viewProvider;
            this.layoutManager.Direction = direction;
            this.layoutManager.Alignment = alignment;
            this.layoutManager.Spacing = spacing;
            this.layoutManager.Padding = padding;
            this.layoutManager.CanScroll = CanScroll;
        }

        public void SetScroller(Scroller newScroller)
        {
            if (!scroll) return;

            if (scroller != null)
            {
                scroller.OnValueChanged.RemoveListener(OnScrollChanged);
                scroller.OnMoveStoped.RemoveListener(OnMoveStoped);
                Destroy(scroller);
            }

            scroller = newScroller;
            ConfigScroller();
        }

        private void ConfigScroller()
        {
            scroller.ScrollSpeed = scrollSpeed;
            scroller.WheelSpeed = wheelSpeed;
            scroller.Snap = Snap;
            scroller.OnValueChanged.AddListener(OnScrollChanged);
            scroller.OnMoveStoped.AddListener(OnMoveStoped);
        }

        public void Refresh()
        {
            ViewProvider.Clear();

            startIndex = layoutManager.GetStartIndex();
            endIndex = layoutManager.GetEndIndex();
            for (int i = startIndex; i <= endIndex; i += layoutManager.Unit)
            {
                ViewProvider.CreateViewHolder(i);
            }

            layoutManager.DoItemAnimation();
        }

        public void RequestLayout()
        {
            layoutManager.SetContentSize();

            if (Scroller == null) return;

            Scroller.Direction = direction;
            Scroller.ViewSize = layoutManager.ViewportSize;
            Scroller.ContentSize = layoutManager.ContentSize;

            if (Scrollbar != null && Scroller.ContentSize != Vector2.zero)
            {
                if ((direction == Direction.Vertical && layoutManager.ContentSize.y <= layoutManager.ViewportSize.y) ||
                    (direction == Direction.Horizontal && layoutManager.ContentSize.x <= layoutManager.ViewportSize.x) ||
                    (direction == Direction.Custom))
                {
                    Scrollbar.gameObject.SetActive(false);
                }
                else
                {
                    Scrollbar.gameObject.SetActive(true);
                    Scrollbar.direction = direction == Direction.Vertical ?
                                    Scrollbar.Direction.TopToBottom :
                                    Scrollbar.Direction.LeftToRight;
                    Scrollbar.size = direction == Direction.Vertical ?
                                    Scroller.ViewSize.y / Scroller.ContentSize.y :
                                    Scroller.ViewSize.x / Scroller.ContentSize.x;
                }
            }
        }

        public float GetScrollPosition()
        {
            return Scroller ? Scroller.Position : 0;
        }

        public void ScrollTo(int index, bool smooth = false)
        {
            if (!scroll) return;

            Scroller.ScrollTo(layoutManager.IndexToPosition(index), smooth);
            if (!smooth)
            {
                Refresh();
            }

            index %= Adapter.GetItemCount();
            index = index < 0 ? Adapter.GetItemCount() + index : index;

            if (currentIndex != index)
            {
                currentIndex = index;
                OnIndexChanged?.Invoke(currentIndex);
            }
        }

        private void SnapTo()
        {
            var index = layoutManager.PositionToIndex(GetScrollPosition());
            ScrollTo(index, true);
        }
    }
}
