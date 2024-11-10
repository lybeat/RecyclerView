using System.Collections.Generic;
using NaiQiu.Framework.View;
using UnityEngine.UI;

public class CharacterPropertyView : View
{
    private Button closeBtn;
    private RecyclerView propertyListView;

    private MixedAdapter adapter;

    public override void OnCreate(UIManager uiManager)
    {
        base.OnCreate(uiManager);

        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        propertyListView = transform.Find("PropertyListView").GetComponent<RecyclerView>();

        closeBtn.onClick.AddListener(() =>
        {
            Close();
        });

        adapter = new MixedAdapter(propertyListView);
        propertyListView.SetLayoutManager(new MixedLayoutManager());

        FillData();
    }

    private void FillData()
    {
        List<MixedData> propertyList = new()
        {
            new MixedData("PropertyTypeViewHolder", "基础属性", "", 18600, 0),
            new MixedData("PropertyViewHolder", "生命值上限", "Icon_Property_Hp", 18600, 0),
            new MixedData("PropertyViewHolder", "攻击力", "Icon_Property_Attack", 1984, 0),
            new MixedData("PropertyViewHolder", "防御力", "Icon_Property_Defense", 895, 0),
            new MixedData("PropertyViewHolder", "元素精通", "Icon_Property_Element", 0, 0),
            new MixedData("PropertyTypeViewHolder", "进阶属性", "", 0, 0),
            new MixedData("PropertyViewHolder", "体力上限", "Icon_Property_Stamina", 240, 0),
            new MixedData("PropertyViewHolder", "暴击率", "Icon_Property_Critical", 482, 1),
            new MixedData("PropertyViewHolder", "暴击伤害", "", 2429, 1),
            new MixedData("PropertyViewHolder", "治疗加成", "Icon_Property_Reply", 0, 1),
            new MixedData("PropertyViewHolder", "受治疗加成", "", 0, 1),
            new MixedData("PropertyViewHolder", "元素充能效率", "Icon_Property_Charge", 1427, 1),
            new MixedData("PropertyViewHolder", "冷却缩减", "Icon_Property_Cooldown", 0, 1),
            new MixedData("PropertyViewHolder", "护盾强效", "Icon_Property_Shield", 0, 1),
            new MixedData("PropertyTypeViewHolder", "元素属性", "", 18600, 0),
            new MixedData("PropertyViewHolder", "火元素伤害加成", "Icon_Property_Fire", 0, 1),
            new MixedData("PropertyViewHolder", "火元素抗性", "", 0, 1),
            new MixedData("PropertyViewHolder", "水元素伤害加成", "Icon_Property_Water", 0, 1),
            new MixedData("PropertyViewHolder", "水元素抗性", "", 0, 1),
            new MixedData("PropertyViewHolder", "草元素伤害加成", "Icon_Property_Grass", 0, 1),
            new MixedData("PropertyViewHolder", "草元素抗性", "", 0, 1),
            new MixedData("PropertyViewHolder", "雷元素伤害加成", "Icon_Property_Thunder", 0, 1),
            new MixedData("PropertyViewHolder", "雷元素抗性", "", 0, 1),
            new MixedData("PropertyViewHolder", "风元素伤害加成", "Icon_Property_Wind", 0, 1),
            new MixedData("PropertyViewHolder", "风元素抗性", "", 0, 1),
            new MixedData("PropertyViewHolder", "冰元素伤害加成", "Icon_Property_Ice", 0, 1),
            new MixedData("PropertyViewHolder", "冰元素抗性", "", 0, 1),
            new MixedData("PropertyViewHolder", "岩元素伤害加成", "Icon_Property_Stone", 0, 1),
            new MixedData("PropertyViewHolder", "岩元素抗性", "", 0, 1),
        };
        adapter.SetList(propertyList);
    }
}
