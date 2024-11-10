using System.Collections.Generic;
using System.Linq;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicView : View
{
    private RecyclerView musicListView;
    private Image coverImage;
    private TMP_Text nameText;
    private TMP_Text scoreText;
    private RecyclerView levelListView;
    private Button arrangerBtn;
    private Button playBtn;

    private Button settingsBtn;
    private Button helpBtn;
    private Button closeBtn;

    private Adapter<MusicData> musicAdapter;
    private Adapter<LevelData> levelAdapter;

    private void Awake()
    {
        coverImage = transform.Find("CoverImage").GetComponent<Image>();
        musicListView = transform.Find("MusicListView").GetComponent<RecyclerView>();
        coverImage = transform.Find("CoverImage").GetComponent<Image>();
        nameText = transform.Find("DetailView/NameText").GetComponent<TMP_Text>();
        scoreText = transform.Find("DetailView/Score/ScoreText").GetComponent<TMP_Text>();
        levelListView = transform.Find("DetailView/LevelListView").GetComponent<RecyclerView>();
        arrangerBtn = transform.Find("DetailView/BtnContainer/ArrangerBtn").GetComponent<Button>();
        playBtn = transform.Find("DetailView/BtnContainer/PlayBtn").GetComponent<Button>();
        settingsBtn = transform.Find("Topbar/SettingsBtn").GetComponent<Button>();
        helpBtn = transform.Find("Topbar/HelpBtn").GetComponent<Button>();
        closeBtn = transform.Find("Topbar/CloseBtn").GetComponent<Button>();

        musicAdapter = new Adapter<MusicData>(musicListView);
        musicListView.SetLayoutManager(new CircleLayoutManager(CircleDirection.Positive, 90));
        musicListView.OnIndexChanged = index =>
        {
            MusicData data = musicAdapter.GetData(index);
            coverImage.sprite = GameManager.SpriteManager.GetSprite(data.icon);
            musicAdapter.ChoiceIndex = index;
            SetDetailData(data);
        };
        musicAdapter.SetOnItemClick(data =>
        {
            musicListView.ScrollTo(musicAdapter.ChoiceIndex, true);
        });

        levelAdapter = new Adapter<LevelData>(levelListView);
        levelListView.SetLayoutManager(new LinearLayoutManager());

        arrangerBtn.onClick.AddListener(OnArrangerClick);
        playBtn.onClick.AddListener(OnPlayClick);
        settingsBtn.onClick.AddListener(OnSettingsClick);
        helpBtn.onClick.AddListener(OnHelpClick);
        closeBtn.onClick.AddListener(OnCloseClick);

        FillData();
    }

    private void FillData()
    {
        LevelData levelData1 = new("普通", "评级达到声动梁尘", GameManager.DataManager.GetRandomList("Foods", 3).ToArray());
        LevelData levelData2 = new("困难", "评级达到声动梁尘", GameManager.DataManager.GetRandomList("Materials", 5).ToArray());
        LevelData levelData3 = new("大师", "评级达到声动梁尘", GameManager.DataManager.GetRandomList("Materials", 10).ToArray());
        MusicData item1 = new("芙宁娜的午后甜点", "Avatar_Funingna", "完美", new LevelData[] { levelData1, levelData2, levelData3 });
        MusicData item2 = new("叫一声前辈来听听", "Avatar_Falushan", "未评级", new LevelData[] { levelData1, levelData2, levelData3 });
        MusicData item3 = new("届不到的爱恋", "Avatar_Keqing", "完美", new LevelData[] { levelData1, levelData2, levelData3 });
        MusicData item8 = new("你想，死一次吗？", "Avatar_Hutao", "刚好", new LevelData[] { levelData1, levelData2, levelData3 });
        MusicData item9 = new("智者，不死于愚口", "Avatar_Naxida", "刚好", new LevelData[] { levelData1, levelData2, levelData3 });
        MusicData item4 = new("磐山的意志", "Avatar_Nuoaier", "刚好", new LevelData[] { levelData1, levelData2, levelData3 });
        MusicData item5 = new("龙王不哭", "Avatar_Xiaogong", "未评级", new LevelData[] { levelData1, levelData2, levelData3 });
        MusicData item6 = new("世纪末的魔术师", "Avatar_Xinhai", "完美", new LevelData[] { levelData1, levelData2, levelData3 });
        MusicData item7 = new("天上天下，唯我独尊", "Avatar_Ying", "完美", new LevelData[] { levelData1, levelData2, levelData3 });
        MusicData item10 = new("这个仇，我记下了", "Avatar_Youla", "刚好", new LevelData[] { levelData1, levelData2, levelData3 });
        List<MusicData> musicDatas = new()
        {
            item1, item2, item3, item4, item5,
            item6, item7, item8, item9, item10,
        };

        musicAdapter.SetList(musicDatas);
        musicAdapter.ChoiceIndex = 0;
        SetDetailData(musicAdapter.GetData(0));
        coverImage.sprite = GameManager.SpriteManager.GetSprite(musicAdapter.GetData(0).icon);
    }

    private void SetDetailData(MusicData musicData)
    {
        nameText.text = musicData.name;
        scoreText.text = musicData.score;
        levelAdapter.SetList(musicData.levelDatas.ToList());
    }

    private void OnArrangerClick()
    {
        Debug.Log("编曲");
    }

    private void OnPlayClick()
    {
        Debug.Log("演奏");
    }

    private void OnSettingsClick()
    {
    }

    private void OnHelpClick()
    {
    }

    private void OnCloseClick()
    {
        Close();
    }
}

public class MusicData
{
    public string name;
    public string icon;
    public string score;
    public LevelData[] levelDatas;

    public MusicData(string name, string icon, string score, LevelData[] levelDatas)
    {
        this.name = name;
        this.icon = icon;
        this.score = score;
        this.levelDatas = levelDatas;
    }
}

public class LevelData
{
    public string level;
    public string target;
    public ItemData[] itemDatas;

    public LevelData() { }

    public LevelData(string level, string target, ItemData[] itemDatas)
    {
        this.level = level;
        this.target = target;
        this.itemDatas = itemDatas;
    }
}
