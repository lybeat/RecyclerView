using UnityEngine;
using UnityEngine.UI;

public static class RectTransformExtension
{
    /// <summary>
    /// 铺满父布局
    /// </summary>
    /// <param name="rectTransform"></param>
    public static void Fill(this RectTransform rectTransform)
    {
        rectTransform.anchoredPosition3D = Vector3.zero;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    /// <summary>
    /// 设置控件在父布局中的相对位置
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <param name="anchor"></param>
    public static void SetRelativePosition(this RectTransform rectTransform, Vector2 anchor)
    {
        rectTransform.anchoredPosition3D = Vector3.zero;
        rectTransform.anchorMin = anchor;
        rectTransform.anchorMax = anchor;
        rectTransform.pivot = anchor;
    }

    /// <summary>
    /// 设置控件层级
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="order"></param>
    public static void SetSortingOrder(this GameObject gameObject, int order)
    {
        var canvas = gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = order;
        gameObject.AddComponent<GraphicRaycaster>();
        gameObject.AddComponent<CanvasGroup>();
    }
}
