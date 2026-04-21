using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textType;
    [SerializeField] private TMP_Text slotIndexText;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedObject;

    private string characterId;

    public string CharacterId => characterId;
    public Button Button => button;

    public void SetData(string id)
    {
        characterId = id;

        CharacterData data = DataTableManager.CharacterTable.Get(id);
        if (data == null)
        {
            SetEmpty();
            return;
        }

        if (textName != null)
            textName.text = data.StringName;

        if (iconImage != null)
            iconImage.sprite = data.SpriteIcon;

        if (textType != null)
            textType.text = data.Type.ToString();
    }

    public void SetSlotIndex(int index)
    {
        if (slotIndexText != null)
            slotIndexText.text = index.ToString();
    }

    public void SetSelected(bool selected)
    {
        if (selectedObject != null)
            selectedObject.SetActive(selected);
    }

    public void SetEmpty()
    {
        characterId = string.Empty;

        if (textName != null)
            textName.text = string.Empty;

        if (iconImage != null)
            iconImage.sprite = null;

        if (textType != null)
            textType.text = string.Empty;

        if (slotIndexText != null)
            slotIndexText.text = string.Empty;

        SetSelected(false);
    }
}