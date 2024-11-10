using NaiQiu.Framework.View;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ViewCreator : Editor
{
    [MenuItem("GameObject/NaiQiu UI/ProgressBar", false, 1)]
    private static void CreateProgressBar()
    {
        RectTransform parent = FindParent();

        GameObject progressBarObj = new("ProgressBar");
        progressBarObj.transform.SetParent(parent);
        progressBarObj.AddComponent<ProgressBar>();
        RectTransform rectTransform = progressBarObj.AddComponent<RectTransform>();
        rectTransform.SetRelativePosition(new Vector2(0.5f, 0.5f));
        rectTransform.sizeDelta = new Vector2(200, 20);

        GameObject background = new("Background");
        background.transform.SetParent(progressBarObj.transform);
        Image bgImage = background.AddComponent<Image>();
        bgImage.sprite = Resources.Load<Sprite>("Sprites/progress_bar_bg");
        background.GetComponent<RectTransform>().Fill();

        GameObject fill = new("Fill");
        fill.transform.SetParent(progressBarObj.transform);
        Image fillImage = fill.AddComponent<Image>();
        fillImage.sprite = Resources.Load<Sprite>("Sprites/progress_bar_fill");
        fillImage.type = Image.Type.Filled;
        fillImage.fillMethod = Image.FillMethod.Horizontal;
        fillImage.fillAmount = 0.2f;
        fill.GetComponent<RectTransform>().Fill();

        Selection.activeGameObject = progressBarObj;
    }

    [MenuItem("GameObject/NaiQiu UI/RecyclerView", false, 1)]
    private static void CreateRecyclerView()
    {
        RectTransform parent = FindParent();

        GameObject recyclerViewObj = new("RecyclerView");
        recyclerViewObj.transform.SetParent(parent, false);
        recyclerViewObj.AddComponent<Image>();
        recyclerViewObj.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);

        RecyclerView recyclerView = recyclerViewObj.AddComponent<RecyclerView>();
        recyclerView.Direction = Direction.Vertical;
        recyclerView.Scroll = true;

        GameObject contentObj = new("Content");
        contentObj.transform.SetParent(recyclerViewObj.transform, false);
        contentObj.AddComponent<Image>();
        contentObj.AddComponent<Mask>().showMaskGraphic = false;
        RectTransform contentTrans = contentObj.GetComponent<RectTransform>();
        contentTrans.anchorMin = Vector2.zero;
        contentTrans.anchorMax = Vector2.one;
        contentTrans.pivot = new Vector2(0.5f, 0.5f);
        contentTrans.offsetMax = new Vector2(-10, 0);
        contentTrans.offsetMin = Vector2.zero;

        GameObject scrollbarObj = new("Scrollbar");
        scrollbarObj.transform.SetParent(recyclerViewObj.transform, false);
        scrollbarObj.AddComponent<Image>();
        Scrollbar scrollbar = scrollbarObj.AddComponent<Scrollbar>();
        RectTransform scrollbarTrans = scrollbarObj.GetComponent<RectTransform>();
        scrollbarTrans.sizeDelta = new Vector2(10, 0);
        scrollbarTrans.anchorMin = new Vector2(1, 0);
        scrollbarTrans.anchorMax = Vector2.one;
        scrollbarTrans.pivot = new Vector2(1f, 0.5f);

        GameObject slidingAreaObj = new("SlidingArea");
        slidingAreaObj.transform.SetParent(scrollbarObj.transform, false);
        RectTransform slidingAreaTrans = slidingAreaObj.AddComponent<RectTransform>();
        slidingAreaTrans.Fill();

        GameObject handleObj = new("Handle");
        handleObj.transform.SetParent(slidingAreaObj.transform, false);
        Image handleImage = handleObj.AddComponent<Image>();
        RectTransform handleTrans = handleObj.GetComponent<RectTransform>();
        handleTrans.pivot = new Vector2(0.5f, 0.5f);
        handleTrans.offsetMax = Vector2.zero;
        handleTrans.offsetMin = Vector2.zero;

        scrollbar.targetGraphic = handleImage;
        scrollbar.handleRect = handleTrans;
        scrollbar.direction = Scrollbar.Direction.TopToBottom;
        scrollbar.size = 0.4f;

        Selection.activeGameObject = recyclerViewObj;
    }

    private static RectTransform FindParent()
    {
        GameObject selectedObj = Selection.activeGameObject;
        if (selectedObj != null)
        {
            if (selectedObj.TryGetComponent(out RectTransform transform))
            {
                return transform;
            }
            else
            {
                transform = CreateCanvas().GetComponent<RectTransform>();
                transform.SetParent(selectedObj.transform, false);
                return transform;
            }
        }

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            canvas = CreateCanvas();
        }
        return canvas.GetComponent<RectTransform>();
    }

    private static Canvas CreateCanvas()
    {
        GameObject canvasObj = new("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventObj = new("EventSystem");
            eventObj.AddComponent<EventSystem>();
            eventObj.AddComponent<StandaloneInputModule>();
        }

        return canvas;
    }
}
