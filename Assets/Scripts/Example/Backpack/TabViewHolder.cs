using NaiQiu.Framework.View;
using UnityEngine.UI;

public class TabViewHolder : ViewHolder
{
    private Image iconImage;
    private Image indicatorImage;

    private BagData bagData;

    public override void FindUI()
    {
        iconImage = transform.Find("IconImage").GetComponent<Image>();
        indicatorImage = transform.Find("IndicatorImage").GetComponent<Image>();
    }

    public override void BindViewData<T>(T data)
    {
        bagData = data as BagData;

        iconImage.sprite = GameManager.SpriteManager.GetSprite(bagData.icon);
    }

    public override void BindChoiceState(bool state)
    {
        indicatorImage.gameObject.SetActive(state);
        string icon = state ? $"{bagData.icon}_Selected" : $"{bagData.icon}_Unselected";
        iconImage.sprite = GameManager.SpriteManager.GetSprite(icon);
    }
}
