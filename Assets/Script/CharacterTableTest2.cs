using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTableTest2 : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;

    public TextMeshProUGUI textAttack;
    public TextMeshProUGUI textDefense;
    public TextMeshProUGUI textHealth;

    private string currentCharacterId;

    private void OnEnable()
    {
        Variables.OnLanguageChanged += OnLanguageChanged;
        SetEmpty();
    }

    private void OnDisable()
    {
        Variables.OnLanguageChanged -= OnLanguageChanged;
    }

    public void SetEmpty()
    {
        if (icon != null)
            icon.sprite = null;

        if (textName != null)
        {
            textName.id = string.Empty;
            textName.OnChangedId();
        }

        if (textAttack != null)
            textAttack.text = string.Empty;

        if (textDefense != null)
            textDefense.text = string.Empty;

        if (textHealth != null)
            textHealth.text = string.Empty;
    }

    public void SetCharcterData(string chId)
    {
        currentCharacterId = chId;

        CharacterData data = DataTableManager.CharacterTable.Get(chId);
        if (data == null)
        {
            Debug.LogError($"CharacterData 없음: {chId}");
            SetEmpty();
            return;
        }

        ApplyCharacterData(data);
    }

    private void ApplyCharacterData(CharacterData data)
    {
        if (icon != null)
            icon.sprite = data.SpriteIcon;

        if (textName != null)
        {
            textName.id = data.Name;
            textName.OnChangedId();
        }

        if (textAttack != null)
            textAttack.text = $"{DataTableManager.StringTable.Get("Attack")} : {data.Attack}";

        if (textDefense != null)
            textDefense.text = $"{DataTableManager.StringTable.Get("Defense")} : {data.Defense}";

        if (textHealth != null)
            textHealth.text = $"{DataTableManager.StringTable.Get("Health")} : {data.Health}";
    }

    private void OnLanguageChanged()
    {
        Debug.Log($"언어 변경 감지, currentCharacterId = [{currentCharacterId}]");

        if (string.IsNullOrEmpty(currentCharacterId))
            return;

        SetCharcterData(currentCharacterId);
    }
}