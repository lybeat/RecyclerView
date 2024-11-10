using System.Collections;
using System.Collections.Generic;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine;

public class PropertyTypeViewHolder : ViewHolder
{
    private TMP_Text nameText;

    public override void FindUI()
    {
        nameText = transform.Find("NameText").GetComponent<TMP_Text>();
    }

    public override void BindViewData<T>(T data)
    {
        MixedData mixedData = data as MixedData;

        nameText.text = mixedData.name;
    }
}
