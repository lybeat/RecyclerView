using NaiQiu.Framework.View;
using UnityEngine;
using UnityEngine.UI;

public class DotViewHolder : ViewHolder
{
    private Image dotImage;

    public override void FindUI()
    {
        dotImage = GetComponent<Image>();
    }

    public override void BindViewData<T>(T data)
    {
    }

    public override void BindChoiceState(bool state)
    {
        Color color;
        if (state)
        {
            ColorUtility.TryParseHtmlString("#FF8E12FF", out color);
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FFFFFFFF", out color);
        }
        dotImage.color = color;
    }
}
