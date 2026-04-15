using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest1 : MonoBehaviour
{

    public string itemId;

    public Image icon;
    public LocalizationText textName;

    public ItemTableTest2 itemInfo;


    private void OnEnable()
    {
        OnChangedItemId();
    }
    private void OnValidate()
    {
        OnChangedItemId();
    }
    public void OnChangedItemId()
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        if(data != null)
        {
            icon.sprite = data.SpriteIcon;
            textName.id = data.Name;
            textName.OnChangedId();
        }
        
        
    }
    public void Onclick()
    {
        itemInfo.SetItemData(itemId);
        
    }
}
