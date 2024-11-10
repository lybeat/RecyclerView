using NaiQiu.Framework.View;
using UnityEngine.UI;

public class CoverViewHolder : ViewHolder
{
    private Image coverImage;
    
    public override void FindUI()
    {
        coverImage = transform.GetComponent<Image>();
    }

    public override void BindViewData<T>(T data)
    {
        BirthDay birthDay = data as BirthDay;
        
        coverImage.sprite = GameManager.SpriteManager.GetSprite(birthDay.cover);
    }
}
