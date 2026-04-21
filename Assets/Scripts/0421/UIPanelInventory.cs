using TMPro;
using UnityEngine;

public class UIPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    public UiInvenSlotList uiInvenSlotList;

    private void OnEnable()
    {

        OnLoad();
        OnChangeFilterting(filtering.value);
        OnChangeSorting(sorting.value);

    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }
    public void OnChangeFilterting(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.inventorySorting = (int)uiInvenSlotList.Sorting;
        SaveLoadManager.Data.inventoryFiltering = (int)uiInvenSlotList.Filtering;
        SaveLoadManager.Save();
    }
    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);

        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)SaveLoadManager.Data.inventorySorting;
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)SaveLoadManager.Data.inventoryFiltering;
        sorting.value = SaveLoadManager.Data.inventorySorting;
        filtering.value = SaveLoadManager.Data.inventoryFiltering;
        
    }
    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }
    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();

    }

}
