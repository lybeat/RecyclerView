using System.Collections.Generic;
using NaiQiu.Framework.View;
using Newtonsoft.Json;
using UnityEngine;

namespace NaiQiu.Framework.Resource
{
    public class DataManager
    {
        // private List<ItemData> itemDatas;
        private Dictionary<string, List<ItemData>> itemDataDict = new();

        public DataManager(Dictionary<string, TextAsset> textDict)
        {
            foreach (var key in textDict.Keys)
            {
                // Debug.Log($"key: {key} -- value: {textDict[key].text}");
                itemDataDict[key] = JsonConvert.DeserializeObject<List<ItemData>>(textDict[key].text);
            }
        }

        public ItemData GetItemData(string name)
        {
            foreach (var items in itemDataDict.Values)
            {
                return items.Find(s => s.name == name);
            }
            return null;
        }

        public ItemData GetRandomItemData(string key)
        {
            int index = Random.Range(0, itemDataDict[key].Count);
            return itemDataDict[key][index];
        }

        public List<ItemData> GetItemDatas(string key)
        {
            return itemDataDict[key];
        }

        public List<ItemData> GetRandomList(string key, int count)
        {
            count = Mathf.Clamp(count, 0, itemDataDict[key].Count);
            return itemDataDict[key].GetRange(0, count);
        }
    }
}
