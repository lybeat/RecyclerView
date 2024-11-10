using NaiQiu.Framework.View;

public class ItemDetailsDialog : Dialog
{
    private ItemDetailsView itemDetailsView;

    public override void OnCreate(UIManager uiManager)
    {
        base.OnCreate(uiManager);

        itemDetailsView = transform.Find("ItemDetailsView").GetComponent<ItemDetailsView>();

        canClickBlock = true;
    }

    public void SetItemData(ItemData itemData)
    {
        itemDetailsView.SetItemData(itemData);
    }
}
