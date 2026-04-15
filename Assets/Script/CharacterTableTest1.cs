using UnityEngine;
using UnityEngine.UI;

public class CharacterTableTest1 : MonoBehaviour
{
    public string characterId;
    public Image icon;
    public LocalizationText textName;
    public CharacterTableTest2 itemInfo;

    private void Start()
    {
        OnChangedCharcterId();
    }

    public void OnChangedCharcterId()
    {
        if (string.IsNullOrEmpty(characterId))
        {
            Debug.LogError("characterId가 비어 있음");
            return;
        }

        if (DataTableManager.CharacterTable == null)
        {
            Debug.LogError("CharacterTable이 null임");
            return;
        }

        CharacterData data = DataTableManager.CharacterTable.Get(characterId);

        if (data == null)
        {
            Debug.LogError($"CharacterData 없음: {characterId}");
            return;
        }

        if (icon != null)
            icon.sprite = data.SpriteIcon;

        if (textName != null)
        {
            textName.id = data.Name;
            textName.OnChangedId();
        }
    }

    public void Onclick()
    {
        if (itemInfo == null)
        {
            Debug.LogError("itemInfo가 연결되지 않음");
            return;
        }

        Debug.Log("클릭한 캐릭터 ID: " + characterId);
        itemInfo.SetCharcterData(characterId);
    }
}