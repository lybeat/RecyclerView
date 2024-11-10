using System.Collections.Generic;
using NaiQiu.Framework.View;

public class FunView : View
{
    private RecyclerView recyclerView;

    private Adapter<FunData> adapter;

    private void Awake()
    {
        recyclerView = transform.Find("RecyclerView").GetComponent<RecyclerView>();

        adapter = new Adapter<FunData>(recyclerView);
        recyclerView.SetLayoutManager(new LinearLayoutManager());
        adapter.SetOnItemClick(data =>
        {
            switch (data.name)
            {
                case "音色绘迹":
                    GameManager.UIManager.Open("MusicView");
                    break;
                case "珍珠纪行":
                    GameManager.UIManager.Open("ChroniclesView");
                    break;
                case "任务":
                    GameManager.UIManager.Open("TaskView");
                    break;
                case "角色属性":
                    GameManager.UIManager.Open("CharacterPropertyView");
                    break;
                case "背包":
                    GameManager.UIManager.Open("BackpackView");
                    break;
                case "特瓦特生日会":
                    GameManager.UIManager.Open("PaletteView");
                    break;
            }
        });

        Invoke("FillData", 0.2f);
    }

    private void FillData()
    {
        List<FunData> funList = new()
        {
            new FunData("Avatar_Falushan", "音色绘迹"),
            new FunData("Avatar_Funingna", "珍珠纪行"),
            new FunData("Avatar_Hutao", "任务"),
            new FunData("Avatar_Keqing", "角色属性"),
            new FunData("Avatar_Nuoaier", "背包"),
            new FunData("Avatar_Xiaogong", "特瓦特生日会"),
        };
        adapter.SetList(funList);
    }
}

public class FunData
{
    public string icon;
    public string name;

    public FunData(string icon, string name)
    {
        this.icon = icon;
        this.name = name;
    }
}
