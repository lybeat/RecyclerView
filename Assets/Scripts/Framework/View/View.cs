using System;
using UnityEngine;
using UnityEngine.UI;

namespace NaiQiu.Framework.View
{
    public abstract class View : MonoBehaviour, IView
    {
        [SerializeField] protected ViewType viewType;
        [SerializeField] protected AnimationType animationType;
        [SerializeField] protected bool cache;

        public string Name { get; set; }
        public ViewType ViewType { get => viewType; set => viewType = value; }
        public bool Cache { get => cache; set => cache = value; }
        public bool Visibility { get; private set; }

        public View Parent { get; set; }
        public string Child { get; set; }

        protected UIManager uiManager;
        protected Canvas canvas;

        private CanvasGroup canvasGroup;
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
                return canvasGroup;
            }
        }

        public virtual void OnCreate(UIManager uiManager)
        {
            this.uiManager = uiManager;
            AddCanvas();
        }

        public virtual void OnStart()
        {
            Visibility = true;

            DoAnimationOpen(OnOpenAnimation);
        }

        public virtual void OnStop()
        {
            Visibility = false;

            DoAnimationClose(OnCloseAnimation);
        }

        public virtual void OnDestroy()
        {
            uiManager = null;
            Destroy(gameObject);
        }

        protected virtual void Close()
        {
            uiManager.Close(this);
        }

        protected virtual void OnOpenAnimation()
        {

        }

        protected virtual void OnCloseAnimation()
        {
            gameObject.SetActive(false);
            uiManager.HandleClosed(this);
        }

        private void AddCanvas()
        {
            if (!TryGetComponent(out canvas))
            {
                canvas = gameObject.AddComponent<Canvas>();
            }
            canvas.overrideSorting = true;
            canvas.sortingOrder = (int)ViewType;

            gameObject.AddComponent<GraphicRaycaster>();
        }

        public void SetSortingOrder(int sortOrder)
        {
            canvas.sortingOrder = (int)(ViewType + sortOrder);
        }

        public int GetSortingOrder()
        {
            return canvas.sortingOrder;
        }

        private void DoAnimationOpen(Action onOpened)
        {
            gameObject.SetActive(true);

            switch (animationType)
            {
                case AnimationType.Zoom:
                    UIAnimationHelper.ZoomIn(this, onOpened);
                    break;
                case AnimationType.Fade:
                    UIAnimationHelper.FadeIn(this);
                    break;
                case AnimationType.Flip:
                    UIAnimationHelper.FlipIn(this);
                    break;
                case AnimationType.Transfer:
                    UIAnimationHelper.TransferIn(this);
                    break;
                case AnimationType.Custom:
                    break;
                default:
                    break;
            }
        }

        public void DoAnimationClose(Action onClosed)
        {
            switch (animationType)
            {
                case AnimationType.Zoom:
                    UIAnimationHelper.ZoomOut(this, onClosed);
                    break;
                case AnimationType.Fade:
                    UIAnimationHelper.FadeOut(this, onClosed);
                    break;
                case AnimationType.Flip:
                    UIAnimationHelper.FlipOut(this, onClosed);
                    break;
                case AnimationType.Transfer:
                    UIAnimationHelper.TransferOut(this, onClosed);
                    break;
                case AnimationType.Custom:
                    break;
                default:
                    onClosed();
                    break;
            }
        }
    }

    public enum AnimationType
    {
        None,
        Zoom,
        Fade,
        Flip,
        Transfer,
        Custom
    }

    public enum ViewType
    {
        Fragment = 100,         // 碎片化的 UI
        Main = 200,             // 主界面
        Activity = 300,         // 全屏的活动界面
        Dialog = 400,           // 对话框
        Toast = 500,            // 提示UI
        Top = 1000              // 永远显示在最上层
    }

    public enum CloseType
    {
        Normal,
        Stack,
    }
}
