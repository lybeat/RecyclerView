using System.Linq;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine.UI;

public class LevelViewHolder : ViewHolder
{
    private TMP_Text levelText;
    private TMP_Text targetText;
    private ItemListView itemListView;
    private Button getBtn;

    private LevelData levelData;

    public override void FindUI()
    {
        levelText = transform.Find("LevelText").GetComponent<TMP_Text>();
        targetText = transform.Find("TargetText").GetComponent<TMP_Text>();
        itemListView = transform.Find("ItemListView").GetComponent<ItemListView>();
        getBtn = transform.Find("GetBtn").GetComponent<Button>();

        getBtn.onClick.AddListener(OnGetClick);
    }

    public override void BindViewData<T>(T data)
    {
        levelData = data as LevelData;

        levelText.text = levelData.level;
        targetText.text = levelData.target;
        itemListView.SetItemList(levelData.itemDatas.ToList());
    }

    private void OnGetClick()
    {
        if (GameManager.UIManager.Open("ItemGetView", out ItemGetView itemGetView))
        {
            itemGetView.SetItemList(levelData.itemDatas.ToList());
        }
    }
}
