using UnityEngine;
using DG.Tweening;
using System;

namespace NaiQiu.Framework.View
{
    public class UIAnimationHelper
    {
        private const float duration = 0.2f;

        public static void ZoomIn(View view, Action onOpened)
        {
            view.transform.localScale = Vector3.one * 0.5f;
            view.transform.DOScale(Vector3.one, duration)
                        .OnComplete(() =>
                        {
                            onOpened?.Invoke();
                        });
        }

        public static void ZoomOut(View view, Action onClosed)
        {
            view.transform.DOScale(Vector3.one * 0.5f, duration)
                        .OnComplete(() =>
                        {
                            onClosed?.Invoke();
                        });
        }

        public static void FadeIn(View view)
        {
            view.CanvasGroup.alpha = 0f;
            view.CanvasGroup.DOFade(1, duration);
        }

        public static void FadeOut(View view, Action onClosed)
        {
            view.CanvasGroup.DOFade(0f, duration)
                            .OnComplete(() =>
                            {
                                onClosed?.Invoke();
                            });
        }

        public static void FlipIn(View view)
        {
            view.transform.localScale = new Vector3(1, 0, 1);
            view.transform.DOScale(Vector3.one, duration);
        }

        public static void FlipOut(View view, Action onClosed)
        {
            view.transform.DOScale(new Vector3(1, 0, 1), duration)
                        .OnComplete(() =>
                        {
                            onClosed?.Invoke();
                        });
        }

        public static void TransferIn(View view)
        {
            RectTransform rectTrans = view.GetComponent<RectTransform>();
            Vector2 pos = rectTrans.anchoredPosition;
            rectTrans.anchoredPosition = new Vector2(-rectTrans.sizeDelta.x, pos.y);
            rectTrans.DOAnchorPos(pos, duration);
        }

        public static void TransferOut(View view, Action onClosed)
        {
            RectTransform rectTrans = view.GetComponent<RectTransform>();
            Vector2 pos = rectTrans.anchoredPosition;
            rectTrans.DOAnchorPos(new Vector2(-rectTrans.sizeDelta.x, pos.y), duration)
                    .OnComplete(() =>
                    {
                        onClosed?.Invoke();
                    });
        }
    }
}
