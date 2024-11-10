using System.Collections.Generic;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PropertyViewHolder : ViewHolder
{
    private Image iconImage;
    private TMP_Text nameText;
    private TMP_Text numberText;
    private Image bgImage;

    public override void FindUI()
    {
        iconImage = transform.Find("IconImage").GetComponent<Image>();
        bgImage = transform.Find("BgImage").GetComponent<Image>();
        nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        numberText = transform.Find("NumberText").GetComponent<TMP_Text>();
    }

    public override void BindViewData<T>(T data)
    {
        MixedData mixedData = data as MixedData;

        if (string.IsNullOrEmpty(mixedData.icon))
        {
            iconImage.gameObject.SetActive(false);
        }
        else
        {
            iconImage.gameObject.SetActive(true);
            iconImage.sprite = GameManager.SpriteManager.GetSprite(mixedData.icon);
        }
        nameText.text = mixedData.name;
        numberText.text = mixedData.percent == 0 ? $"{mixedData.number}" : $"{(mixedData.number / 10f).ToString("0.0")}%";

        Color color;
        if (Index % 2 == 0)
        {
            ColorUtility.TryParseHtmlString("#EABDFF1B", out color);
        }
        else
        {
            ColorUtility.TryParseHtmlString("#64268036", out color);
        }
        bgImage.color = color;
    }
}
