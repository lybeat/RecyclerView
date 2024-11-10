using System.Collections.Generic;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine.UI;

public class PaletteView : View
{
    private RecyclerView tabListView;
    private RecyclerView coverListView;
    private TMP_Text nameText;
    private Button previousBtn;
    private Button nextBtn;
    private Button closeBtn;

    private Adapter<BirthDay> tabAdapter;
    private LoopAdapter<BirthDay> coverAdapter;

    public override void OnCreate(UIManager uiManager)
    {
        base.OnCreate(uiManager);

        tabListView = transform.Find("TabView").GetComponent<RecyclerView>();
        coverListView = transform.Find("CoverListView").GetComponent<RecyclerView>();
        nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        previousBtn = transform.Find("PreviousBtn").GetComponent<Button>();
        nextBtn = transform.Find("NextBtn").GetComponent<Button>();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();

        previousBtn.onClick.AddListener(OnPreviousClick);
        nextBtn.onClick.AddListener(OnNextClick);
        closeBtn.onClick.AddListener(OnCloseClick);

        tabAdapter = new Adapter<BirthDay>(tabListView);
        tabListView.SetLayoutManager(new LinearLayoutManager());
        tabAdapter.SetOnItemClick(data =>
        {
            coverListView.ScrollTo(tabAdapter.ChoiceIndex);
        });

        coverAdapter = new LoopAdapter<BirthDay>(coverListView);
        coverListView.SetLayoutManager(new PageLayoutManager(0.9f));
        coverListView.OnIndexChanged = index =>
        {
            int idx = index % tabAdapter.GetItemCount();
            tabAdapter.ChoiceIndex = idx;
            nameText.text = coverAdapter.GetData(idx).name;
        };

        FillData();
    }

    private void FillData()
    {
        List<BirthDay> coverList = new()
        {
            new("anbo", "安博"),
            new("babala", "芭芭拉"),
            new("beidou", "北斗"),
            new("diaona", "迪奥娜"),
            new("dixiya", "迪希娅"),
            new("falushan", "珐露珊"),
            new("feixieer", "菲谢尔"),
            new("ganyu", "甘雨"),
            new("hutao", "胡桃"),
            new("jiuqiren", "久绮忍"),
            new("jiutiaoshaluo", "九条裟萝"),
            new("kelai", "柯莱"),
            new("keli", "可莉"),
            new("keqing", "刻晴"),
            new("laiyila", "莱依莱"),
            new("linnite", "琳妮特"),
        };
        tabAdapter.SetList(coverList);
        coverAdapter.SetList(coverList);
        coverListView.ScrollTo(1000 * coverAdapter.GetRealCount());
    }

    private void OnCloseClick()
    {
        Close();
    }

    private void OnPreviousClick()
    {
        int index = coverListView.CurrentIndex - 1;
        tabAdapter.ChoiceIndex = index % tabAdapter.GetItemCount();
        coverListView.ScrollTo(index, true);
    }

    private void OnNextClick()
    {
        int index = coverListView.CurrentIndex + 1;
        tabAdapter.ChoiceIndex = index % tabAdapter.GetItemCount();
        coverListView.ScrollTo(index, true);
    }
}

public class BirthDay
{
    public string cover;
    public string name;

    public BirthDay(string cover, string name)
    {
        this.cover = cover;
        this.name = name;
    }
}
