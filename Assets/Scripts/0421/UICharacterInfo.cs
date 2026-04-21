using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterInfo : MonoBehaviour
{
    [Header("아이콘")]
    public Image icon;

    [Header("값 (Value)")]
    public TMP_Text nameValue;
    public TMP_Text descValue;
    public TMP_Text attackValue;
    public TMP_Text defenseValue;
    public TMP_Text healthValue;

    public void SetCharacter(SaveCharacterData saveData)
    {
        if (saveData == null)
        {
            Clear();
            return;
        }

        CharacterData data = saveData.characterData;
        if (data == null)
        {
            Clear();
            return;
        }

        icon.sprite = data.SpriteIcon;
        nameValue.text = data.StringName;
        descValue.text = data.StringDesc;

        attackValue.text = data.Attack.ToString();
        defenseValue.text = data.Defense.ToString();
        healthValue.text = data.Health.ToString();
    }

    public void Clear()
    {
        icon.sprite = null;
        nameValue.text = "";
        descValue.text = "";
        attackValue.text = "";
        defenseValue.text = "";
        healthValue.text = "";
    }
}
