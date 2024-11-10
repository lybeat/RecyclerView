using System.Collections.Generic;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine.UI;

public class BackpackView : View
{
    private TMP_Text nameText;
    private TMP_Text countText;
    private Button closeBtn;
    private RecyclerView typeListView;
    private RecyclerView itemListView;
    private ItemDetailsView itemDetailsView;

    private Adapter<BagData> typeAdapter;
    private Adapter<ItemData> itemAdapter;

    public override void OnCreate(UIManager uiManager)
    {
        base.OnCreate(uiManager);

        nameText = transform.Find("TabView/NameText").GetComponent<TMP_Text>();
        countText = transform.Find("TabView/CountText").GetComponent<TMP_Text>();
        closeBtn = transform.Find("TabView/CloseBtn").GetComponent<Button>();
        typeListView = transform.Find("TabView/TypeListView").GetComponent<RecyclerView>();
        itemListView = transform.Find("ItemListView").GetComponent<RecyclerView>();
        itemDetailsView = transform.Find("ItemDetailsView").GetComponent<ItemDetailsView>();

        closeBtn.onClick.AddListener(() =>
        {
            Close();
        });

        typeAdapter = new Adapter<BagData>(typeListView);
        typeListView.SetLayoutManager(new LinearLayoutManager());
        typeAdapter.SetOnItemClick(data =>
        {
            nameText.text = data.name;
            countText.text = $"{data.name} {itemAdapter.GetItemCount()}/{data.maxCount}";

            itemAdapter.SetList(GameManager.DataManager.GetItemDatas(data.key));
            itemAdapter.Sort((a, b) => b.rarity.CompareTo(a.rarity));
            itemAdapter.ChoiceIndex = 0;
            itemDetailsView.SetItemData(itemAdapter.GetData(0));
        });

        itemAdapter = new Adapter<ItemData>(itemListView);
        itemListView.SetLayoutManager(new GridLayoutManager(8));
        itemAdapter.SetOnItemClick(data =>
        {
            itemDetailsView.SetItemData(data);
        });

        FillData();
    }

    private void FillData()
    {
        List<BagData> typeList = new()
        {
            new("Weapons", "武器", "Icon_Weapons", 2000, 620),
            new("Relics", "圣遗物", "Icon_Relics", 1800, 1796),
            new("Foods", "食物", "Icon_Foods", 2000, 1184),
            new("Materials", "材料", "Icon_Materials", 2000, 1184),
            new("Props", "道具", "Icon_Props", 2000, 1184),
        };
        typeAdapter.SetList(typeList);
        typeAdapter.ChoiceIndex = 0;

        nameText.text = typeList[0].name;
        countText.text = $"{typeList[0].name} {itemAdapter.GetItemCount()}/{typeList[0].maxCount}";

        itemAdapter.SetList(GameManager.DataManager.GetItemDatas(typeAdapter.GetData(0).key));
        itemAdapter.Sort((a, b) => b.rarity.CompareTo(a.rarity));
        itemAdapter.ChoiceIndex = 0;
        itemDetailsView.SetItemData(itemAdapter.GetData(0));
    }
}

public class BagData
{
    public string key;
    public string name;
    public string icon;
    public int maxCount;
    public int count;

    public BagData(string key, string name, string icon, int maxCount, int count)
    {
        this.key = key;
        this.name = name;
        this.icon = icon;
        this.maxCount = maxCount;
        this.count = count;
    }
}
