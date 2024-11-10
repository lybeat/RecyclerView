using DG.Tweening;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TaskViewHolder : ViewHolder, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text nameText;

    public override void FindUI()
    {
        nameText = transform.Find("NameText").GetComponent<TMP_Text>();
    }

    public override void BindViewData<T>(T data)
    {
        GroupData groupData = data as GroupData;

        nameText.text = groupData.name;
    }

    public override void BindChoiceState(bool state)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform.DOScale(Vector3.one * 1.1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RectTransform.DOScale(Vector3.one, 0.2f);
    }
}
