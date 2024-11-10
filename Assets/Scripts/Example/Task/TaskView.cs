using System.Collections.Generic;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine.UI;

public class TaskView : View
{
    private Button closeBtn;
    private RecyclerView typeListView;
    private RecyclerView taskListView;
    private TMP_Text nameText;

    private Adapter<string> typeAdapter;
    private GroupAdapter taskGroupAdapter;

    private List<GroupData> taskList;

    private int lastIndex;

    public override void OnCreate(UIManager uiManager)
    {
        base.OnCreate(uiManager);

        closeBtn = transform.Find("Topbar/CloseBtn").GetComponent<Button>();
        typeListView = transform.Find("Topbar/TypeListView").GetComponent<RecyclerView>();
        taskListView = transform.Find("TaskListView").GetComponent<RecyclerView>();
        nameText = transform.Find("TaskDetails/NameText").GetComponent<TMP_Text>();

        closeBtn.onClick.AddListener(OnCloseClick);

        typeAdapter = new Adapter<string>(typeListView);
        typeListView.SetLayoutManager(new LinearLayoutManager());
        typeAdapter.SetOnItemClick(data =>
        {
            if (lastIndex == typeAdapter.ChoiceIndex) return;

            if (typeAdapter.ChoiceIndex == 0)
            {
                taskGroupAdapter.SetList(taskList);
            }
            else
            {
                taskGroupAdapter.SetList(taskList.FindAll(s => s.type == typeAdapter.ChoiceIndex - 1));
            }

            lastIndex = typeAdapter.ChoiceIndex;
        });

        taskGroupAdapter = new GroupAdapter(taskListView, "HeadViewHolder");
        taskListView.SetLayoutManager(new MixedLayoutManager());
        taskGroupAdapter.SetOnItemClick(data =>
        {
            SetTaskDetails(data);
        });

        FillData();
    }

    private void OnCloseClick()
    {
        Close();
    }

    private void SetTaskDetails(GroupData data)
    {
        nameText.text = data.name;
    }

    private void FillData()
    {
        List<string> typeList = new()
        {
            "Icon_Task_All", "Icon_Task_Daily",  "Icon_Task_World", "Icon_Task_Legend",
        };
        typeAdapter.SetList(typeList);
        typeAdapter.ChoiceIndex = 0;

        GroupData data1 = new(0, "TaskViewHolder", "寻找刻晴");
        GroupData data2 = new(0, "TaskViewHolder", "拜访师父");
        GroupData data3 = new(1, "TaskViewHolder", "为甘雨采摘10朵清心");
        GroupData data4 = new(2, "TaskViewHolder", "参演芙宁娜的舞台剧");
        taskList = new()
        {
            data1, data2, data3, data4,
            data3, data3, data3, data3,
            data3, data3, data3, data3,
            data3, data3, data3, data3,
        };

        taskGroupAdapter.SetList(taskList);
    }
}
