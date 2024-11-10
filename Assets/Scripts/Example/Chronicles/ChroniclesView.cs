using System.Collections.Generic;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChroniclesView : View
{
    private Button closeBtn;
    private TMP_Text levelText;
    private TMP_Text expText;
    private TMP_Text expLimitText;
    private ProgressBar expBar;
    private Button buyLevelBtn;
    private RecyclerView prizeListView;
    private TMP_Text tarilLevelText;
    private ItemListView tarilEarthListView;
    private ItemListView tarilPearlListView;
    private TMP_Text remainingTimeText;
    private Button unlockPearlBtn;

    private Adapter<PrizeData> adapter;

    private int tarilIndex;

    public override void OnCreate(UIManager uiManager)
    {
        base.OnCreate(uiManager);

        closeBtn = transform.Find("Top/CloseBtn").GetComponent<Button>();
        levelText = transform.Find("LevelBar/Level/LevelText").GetComponent<TMP_Text>();
        expText = transform.Find("LevelBar/Exp/ExpText").GetComponent<TMP_Text>();
        expLimitText = transform.Find("LevelBar/Exp/ExpLimitText").GetComponent<TMP_Text>();
        expBar = transform.Find("LevelBar/Exp/ExpBar").GetComponent<ProgressBar>();
        buyLevelBtn = transform.Find("LevelBar/BuyLevelBtn").GetComponent<Button>();
        prizeListView = transform.Find("Chronicles/PrizeListView").GetComponent<RecyclerView>();
        tarilLevelText = transform.Find("Chronicles/Taril/LevelText").GetComponent<TMP_Text>();
        tarilEarthListView = transform.Find("Chronicles/Taril/BgImage/Earth/EarthListView").GetComponent<ItemListView>();
        tarilPearlListView = transform.Find("Chronicles/Taril/BgImage/Pearl/PearlListView").GetComponent<ItemListView>();
        remainingTimeText = transform.Find("Bottom/RemainingTimeText").GetComponent<TMP_Text>();
        unlockPearlBtn = transform.Find("Bottom/UnlockPearlBtn").GetComponent<Button>();

        closeBtn.onClick.AddListener(OnCloseClick);
        buyLevelBtn.onClick.AddListener(OnBuyLevelClick);
        unlockPearlBtn.onClick.AddListener(OnUnlockPearlClick);

        adapter = new Adapter<PrizeData>(prizeListView);
        prizeListView.SetLayoutManager(new LinearLayoutManager());
        prizeListView.OnScrollValueChanged = () =>
        {
            SetTarilData();
        };

        FillData();
    }

    private void SetTarilData()
    {
        int index = prizeListView.LayoutManager.GetEndIndex() + 1;
        index = Mathf.CeilToInt(index / 5f) * 5;
        if (index > adapter.GetItemCount() - 1)
        {
            index = adapter.GetItemCount() - 1;
        }

        if (index == tarilIndex) return;

        tarilIndex = index;
        PrizeData data = adapter.GetData(tarilIndex);

        tarilLevelText.text = tarilIndex == adapter.GetItemCount() - 1 ? $"{tarilIndex + 1}级" : $"{tarilIndex}级";
        tarilEarthListView.SetItemData(data.earthPrize);
        tarilPearlListView.SetItemList(data.pearlPrizes);
    }

    private void FillData()
    {
        List<string> keys = new() { "Weapons", "Relics", "Foods", "Materials", "Props" };
        List<PrizeData> items = new();
        for (int i = 0; i < 50; i++)
        {
            int index = Random.Range(0, 5);
            PrizeData item = new()
            {
                earthPrize = GameManager.DataManager.GetRandomItemData(keys[index]),
                pearlPrizes = GameManager.DataManager.GetRandomList(keys[index], 2)
            };
            items.Add(item);
        }
        adapter.SetList(items);
        SetTarilData();
    }

    private void OnCloseClick()
    {
        Destroy(gameObject);
    }

    private void OnBuyLevelClick()
    {

    }

    private void OnUnlockPearlClick()
    {

    }
}

public class PrizeData
{
    public ItemData earthPrize;
    public List<ItemData> pearlPrizes;
}
