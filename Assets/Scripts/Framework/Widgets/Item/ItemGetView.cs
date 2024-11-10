using System.Collections.Generic;
using NaiQiu.Framework.View;
using UnityEngine.UI;

public class ItemGetView : View
{
    private ItemListView itemListView;
    private Button maskBtn;

    public override void OnCreate(UIManager uiManager)
    {
        base.OnCreate(uiManager);

        itemListView = transform.Find("ItemListView").GetComponent<ItemListView>();
        maskBtn = transform.Find("MaskBtn").GetComponent<Button>();

        maskBtn.onClick.AddListener(() =>
        {
            Close();
        });
    }

    public void SetItemList(List<ItemData> itemDatas)
    {
        itemListView.SetItemList(itemDatas);
    }
}
