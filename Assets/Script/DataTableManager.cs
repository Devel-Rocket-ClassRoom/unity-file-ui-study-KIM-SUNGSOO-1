using System;
using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables =
        new Dictionary<string, DataTable>();

    public static StringTable StringTable => Get<StringTable>(DataTableIDs.String);

    public static ItemTable ItemTable => Get<ItemTable>(DataTableIDs.Item);


    public static CharacterTable CharacterTable => Get<CharacterTable>(DataTableIDs.character);
#if UNITY_EDITOR
    public static StringTable GetStringTable(Languages lang)
    {
        return Get<StringTable>(DataTableIDs.StringTableIds[(int)lang]);
    }
#endif

    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
        Debug.Log("DataTableManager Init 시작");
#if !UNITY_EDITOR
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
#else
        foreach (var id in DataTableIDs.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
            Debug.Log("StringTable 추가됨: " + id);
        }
#endif

        var itemTable = new ItemTable();
        itemTable.Load(DataTableIDs.Item);
        tables.Add(DataTableIDs.Item, itemTable);
        Debug.Log("ItemTable 추가됨: " + DataTableIDs.Item);

        var characterTable = new CharacterTable();
        characterTable.Load(DataTableIDs.character);
        tables.Add(DataTableIDs.character, characterTable);
        Debug.Log("CharacterTable 추가됨: " + DataTableIDs.character);

        Debug.Log("총 테이블 수: " + tables.Count);
    }

    public static void ChangeLanguage(Languages lang)
    {
        
        if (tables.ContainsKey(DataTableIDs.StringTableIds[(int)lang]))
            return;
        string oldId = string.Empty;
        foreach(var id in DataTableIDs.StringTableIds)
        {
            if(tables.ContainsKey(id))
            {
                oldId = id;
                break;
            }
        }
        
        var stringTable = tables[oldId];
        stringTable.Load(DataTableIDs.StringTableIds[(int)lang]);
        //tables.Remove(oldId);
        //tables.Add(DataTableIDs.String, stringTable);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError($"테이블 없음: {id}");
            foreach (var key in tables.Keys)
            {
                Debug.Log("현재 등록된 키: " + key);
            }
            return null;
        }

        return tables[id] as T;
    }
}
