using DG.Tweening;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrizeViewHolder : ViewHolder, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text levelText;
    private ItemListView earthListView;
    private ItemListView pearlListView;
    private GameObject shadow;
    private RectTransform bgImage;

    public override void FindUI()
    {
        levelText = transform.Find("LevelText").GetComponent<TMP_Text>();
        earthListView = transform.Find("BgImage/EarthListView").GetComponent<ItemListView>();
        pearlListView = transform.Find("BgImage/PearlListView").GetComponent<ItemListView>();
        shadow = transform.Find("Shadow").gameObject;
        bgImage = transform.Find("BgImage").GetComponent<RectTransform>();
    }

    public override void BindViewData<T>(T data)
    {
        PrizeData prizeData = data as PrizeData;

        levelText.text = $"{Index + 1}级";
        earthListView.SetItemData(prizeData.earthPrize);
        pearlListView.SetItemList(prizeData.pearlPrizes);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bgImage.DOScale(Vector3.one * 1.08f, 0.2f);
        shadow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bgImage.DOScale(Vector3.one, 0.2f);
        shadow.SetActive(false);
    }
}
