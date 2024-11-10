using NaiQiu.Framework.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsView : MonoBehaviour
{
    private Image iconImage;
    private TMP_Text nameText;
    private TMP_Text typeText;
    private TMP_Text contentText;
    private Image rarityImage;

    private void Awake()
    {
        nameText = transform.Find("Title/NameText").GetComponent<TMP_Text>();
        iconImage = transform.Find("Sub/IconImage").GetComponent<Image>();
        rarityImage = transform.Find("Sub/RarityImage").GetComponent<Image>();
        typeText = transform.Find("Sub/TypeText").GetComponent<TMP_Text>();
        contentText = transform.Find("ContentText").GetComponent<TMP_Text>();
    }

    public void SetItemData(ItemData itemData)
    {
        iconImage.sprite = GameManager.SpriteManager.GetSprite(itemData.icon);
        nameText.text = itemData.name;
        typeText.text = itemData.type;
        contentText.text = itemData.content;
        rarityImage.sprite = GameManager.SpriteManager.GetSprite($"Rarity_{itemData.rarity}_Star");
        rarityImage.SetNativeSize();
    }
}
