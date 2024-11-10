using NaiQiu.Framework.View;
using UnityEngine.UI;

public class TaskTypeViewHolder : ViewHolder
{
    private Image iconImage;
    private Image selectImage;

    private string type;

    public override void FindUI()
    {
        iconImage = transform.Find("IconImage").GetComponent<Image>();
        selectImage = transform.Find("SelectImage").GetComponent<Image>();
    }

    public override void BindViewData<T>(T data)
    {
        type = data as string;

        iconImage.sprite = GameManager.SpriteManager.GetSprite(type);
    }

    public override void BindChoiceState(bool state)
    {
        selectImage.gameObject.SetActive(state);
        string icon = state ? $"{type}_Selected" : $"{type}_Unselected";
        iconImage.sprite = GameManager.SpriteManager.GetSprite(icon);
    }
}
