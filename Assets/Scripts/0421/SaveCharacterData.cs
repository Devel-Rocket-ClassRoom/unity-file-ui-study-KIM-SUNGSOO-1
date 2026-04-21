using UnityEngine;

[System.Serializable]
public class SaveCharacterData
{
    public string characterId;
    public float creationTime;

    public CharacterData characterData => DataTableManager.CharacterTable.Get(characterId);

    public static SaveCharacterData Create(string id)
    {
        return new SaveCharacterData
        {
            characterId = id,
            creationTime = Time.time
        };
    }

    public static SaveCharacterData GetRandomCharacter()
    {
        string[] ids =
        {
            "Character1",
            "Character2",
            "Character3",
            "Character4"
        };

        int index = Random.Range(0, ids.Length);
        return Create(ids[index]);
    }
}