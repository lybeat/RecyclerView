using NaiQiu.Framework.View;
using TMPro;
using UnityEngine.UI;

public class MusicViewHolder : ViewHolder
{
    private Image iconImage;
    private Image arrowImage;
    private TMP_Text nameText;

    public override void FindUI()
    {
        iconImage = transform.Find("IconImage").GetComponent<Image>();
        arrowImage = transform.Find("ArrowImage").GetComponent<Image>();
        nameText = transform.Find("NameText").GetComponent<TMP_Text>();
    }

    public override void BindViewData<T>(T data)
    {
        MusicData musicData = data as MusicData;
        
        iconImage.sprite = GameManager.SpriteManager.GetSprite(musicData.icon);
    }

    public override void BindChoiceState(bool state)
    {
        // arrowImage.gameObject.SetActive(state);
    }
}
