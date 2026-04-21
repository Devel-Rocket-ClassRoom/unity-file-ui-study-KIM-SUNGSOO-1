using UnityEngine;
using System;
using Newtonsoft.Json;
[Serializable]
public class SaveItemData
{
    public Guid instanceId;

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData itemData;
    public DateTime CreationTime;

    
    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.itemData = DataTableManager.ItemTable.GetRandom();
        return newItem;
    }
    public SaveItemData()
    {
        instanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }
    public override string ToString()
    {
        return $"{instanceId}\n{CreationTime}\n{itemData.Id}";
    }
}
