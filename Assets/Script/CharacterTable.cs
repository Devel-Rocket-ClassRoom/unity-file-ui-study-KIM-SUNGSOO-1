using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


//1. CSV 파일 (ID/ 이름/설명/공격력/방어력/초상화or아이콘/)
//2. DATATAble 상속
//3. DataTableMAnager에 등록
//4. 테스트 패널

public class CharacterData
{
    public string Id { get; set;}

    public CharacterTypes Type { get; set;}
    public string Name { get; set;}
    public string Desc { get; set;}
    public int Attack { get; set;}
    public int Defense { get; set;}
    public int Health { get; set;}

    public string Icon { get; set;}

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
    public override string ToString()
    {
        return $"{Id}/{Type}/{Name}/{Desc}/{Attack}/{Defense}/{Health}/{Icon}";
    }

}
public class CharacterTable : DataTable
{
    public readonly Dictionary<string , CharacterData> data = new Dictionary<string , CharacterData>();

    public override void Load(string filename)
    {
        data.Clear();
        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCSV<CharacterData>(textAsset.text);

        foreach(var character in list)
        {
            if(!data.ContainsKey(character.Name))
            {
                data.Add(character.Id, character);
            }
            else
            {
                Debug.LogError("캐릭터 아이디 중복");
            }
        }
    }

    public CharacterData Get(string id)
    {
        Debug.Log($"요청된 id = [{id}]");

        if (!data.ContainsKey(id))
        {
            Debug.LogError($"캐릭터 데이터 없음: [{id}]");

            foreach (var key in data.Keys)
            {
                Debug.Log($"등록된 키 = [{key}]");
            }

            return null;
        }

        return data[id];
    }
}
