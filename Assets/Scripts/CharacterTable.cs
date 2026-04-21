using System.Collections.Generic;
using UnityEngine;

// 1. CSV 파일 (ID / 이름 / 설명 / 공격력.... / 초상화 or 아이콘 ...)
// 2. DataTable 상속
// 3. DataTableManager 등록
// 4. 테스트 패널

public class CharacterData
{
    public string Id {  get; set; }
    public CharacterTypes Type {  get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Health  { get; set; }
    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString()
    {
        return $"{Id} / {Type} / {Name}/ {Desc} /{Attack}/{Defense}/{Health}/{Icon}";
    }
}

public class CharacterTable : DataTable
{

    private Dictionary<string, CharacterData> dict = new Dictionary<string, CharacterData>();

    public CharacterData Get(string id)
    {
        if (dict.TryGetValue(id, out CharacterData data))
            return data;

        Debug.LogError($"CharacterTable에 없는 Id: {id}");
        return null;
    }
    
    public override void Load(string filename)
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"DataTables/{filename}");

        if (textAsset == null)
        {
            Debug.LogError($"CharacterTable Load 실패 : {filename}");
            return;
        }

        string[] lines = textAsset.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // 0번은 헤더
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string line = lines[i].Trim();
            string[] values = line.Split(',');

            if (values.Length < 8)
            {
                Debug.LogWarning($"잘못된 CharacterTable 행 : {line}");
                continue;
            }

            CharacterData data = new CharacterData();

            data.Id = values[0].Trim();

            if (System.Enum.TryParse(values[1].Trim(), out CharacterTypes type) == false)
            {
                Debug.LogWarning($"CharacterTypes 변환 실패 : {values[1]}");
                continue;
            }

            data.Type = type;
            data.Name = values[2].Trim();
            data.Desc = values[3].Trim();

            if (int.TryParse(values[4].Trim(), out int attack) == false)
            {
                Debug.LogWarning($"Attack 변환 실패 : {values[4]}");
                continue;
            }
            data.Attack = attack;

            if (int.TryParse(values[5].Trim(), out int defense) == false)
            {
                Debug.LogWarning($"Defense 변환 실패 : {values[5]}");
                continue;
            }
            data.Defense = defense;

            if (int.TryParse(values[6].Trim(), out int health) == false)
            {
                Debug.LogWarning($"Health 변환 실패 : {values[6]}");
                continue;
            }
            data.Health = health;

            data.Icon = values[7].Trim();

            if (dict.ContainsKey(data.Id))
            {
                Debug.LogWarning($"중복된 Character Id : {data.Id}");
                continue;
            }

            dict.Add(data.Id, data);
        }
    }
}
