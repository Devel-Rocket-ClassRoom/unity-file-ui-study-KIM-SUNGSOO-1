using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image iconImage;
    public string id;
    public TextMeshProUGUI itemText;

    private ItemData itemData;

    private event Action<ItemData> onButtoned;
    

    private void OnEnable()
    {
        itemData = DataTableManager.ItemTable.Get(id);
        ChangeItems();
    }

    private void OnValidate()
    {
        itemData = DataTableManager.ItemTable.Get(id);
        ChangeItems();
    }
    
    private void ChangeItems()
    {
        iconImage.sprite = itemData.SpriteIcon;
        itemText.text = itemData.StringName;
    }
    public void OnButtonClicked()
    {
        onButtoned?.Invoke(itemData);
    }


}
