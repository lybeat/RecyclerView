using DG.Tweening;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FunViewHolder : ViewHolder, IPointerEnterHandler, IPointerExitHandler
{
    private Image iconImage;
    private TMP_Text nameText;

    public override void FindUI()
    {
        iconImage = GetComponent<Image>();
        nameText = transform.Find("NameText").GetComponent<TMP_Text>();
    }

    public override void BindViewData<T>(T data)
    {
        FunData funData = data as FunData;

        iconImage.sprite = GameManager.SpriteManager.GetSprite(funData.icon);
        nameText.text = funData.name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform.DOScale(Vector3.one * 1.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RectTransform.DOScale(Vector3.one, 0.2f);
    }
}
