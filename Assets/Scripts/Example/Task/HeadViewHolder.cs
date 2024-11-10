using NaiQiu.Framework.View;
using TMPro;
using UnityEngine.UI;

public class HeadViewHolder : ViewHolder
{
    private TMP_Text nameText;

    GroupData groupData;

    public override void FindUI()
    {
        nameText = transform.Find("NameText").GetComponent<TMP_Text>();
    }

    public override void BindViewData<T>(T data)
    {
        groupData = data as GroupData;

        switch (groupData.type)
        {
            case 0:
                nameText.text = "每日";
                break;
            case 1:
                nameText.text = "世界";
                break;
            case 2:
                nameText.text = "传说";
                break;
        }
    }
}
