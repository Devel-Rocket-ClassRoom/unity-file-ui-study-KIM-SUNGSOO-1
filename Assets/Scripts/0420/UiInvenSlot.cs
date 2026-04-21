using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInvenSlot : MonoBehaviour
{
    public Image imageIcon;

    public int slotIndex = -1;
    public TextMeshProUGUI textName;
    public Button button;
    public SaveItemData SaveItemData { get; private set; }
    

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        SaveItemData = null;
    }

    public void SetItem(SaveItemData data)
    {
        SaveItemData = data;
        imageIcon.sprite = SaveItemData.itemData.SpriteIcon;
        textName.text = DataTableManager.GetString(SaveItemData.itemData.Name);
    }

    //private void Update()
    //{
    //    //if(Input.GetKeyDown(KeyCode.Alpha1))
    //    //{
    //    //    SetEmpty();
    //    //}
    //    //if(Input.GetKeyDown(KeyCode.Alpha2))
    //    //{
    //    //    var saveItemData = new SaveItemData();
    //    //    saveItemData.itemData = DataTableManager.ItemTable.Get("Item1");
    //    //    SetItem(ItemData);
    //    //}
    //}
}
