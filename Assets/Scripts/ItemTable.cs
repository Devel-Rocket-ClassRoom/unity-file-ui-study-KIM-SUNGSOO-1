using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;

/*
Id, Type, Name, Desc, Value, Cost, Icon
Item1, Weapon, Item1Name, Item1Desc,10,500, Icon_Sword01
Item2, Equip, Item2Name, Item2Desc,20,200, Icon_Shield01
Item3, Weapon, Item3Name, Item3Desc,5,1000, Icon_Bow01
Item4, Consumable, Item4Name, Item4Desc,50,50, Icon_Heart01
*/

public class ItemData
{
    public string Id { get; set; }
    public ItemTypes Type { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Value { get; set; }
    public int Cost { get; set; }
    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString()
    {
        return $"{Id} / {Type} / {Name} / {Desc} / {Value} / {Cost} / {Icon}";
    }
}

public class ItemTable : DataTable
{
    private readonly Dictionary<string, ItemData> table =
        new Dictionary<string, ItemData>();

    public override void Load(string filename)
    {
        table.Clear();

        string  path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<ItemData> list = LoadCSV<ItemData>(textAsset.text);

        foreach (var item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table.Add(item.Id, item);
            }
            else
            {
                Debug.LogError("아이템 아이디 중복");
            }
        }
    }

    public ItemData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError("아이템 아이디 없음");
            return null;
        }
        return table[id];
    }
}
