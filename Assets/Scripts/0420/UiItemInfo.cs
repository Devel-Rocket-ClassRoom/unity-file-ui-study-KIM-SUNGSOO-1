using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UiItemInfo : MonoBehaviour
{

    public static readonly string ForMatCommon = "{0}: {1}";
    public Image imageIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDescription;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textCost;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDescription.text = string.Empty;
        textType.text = string.Empty;
        textValue.text = string.Empty;
        textCost.text = string.Empty;
    }

    public void SetSaveItemData(SaveItemData saveItemData)
    {
        ItemData data = saveItemData.itemData;
        var st = DataTableManager.StringTable;
        imageIcon.sprite = data.SpriteIcon;
        textName.text = 
            string.Format(ForMatCommon, DataTableManager.StringTable.Get("NAME"),data.StringName);
        textDescription.text = 
            string.Format(ForMatCommon, DataTableManager.StringTable.Get("DESC"), data.StringDesc);
        textType.text = data.Type.ToString();
        textValue.text = data.Value.ToString();
        textCost.text = data.Cost.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetEmpty();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            SetSaveItemData(SaveItemData.GetRandomItem());
        }
    }
}
