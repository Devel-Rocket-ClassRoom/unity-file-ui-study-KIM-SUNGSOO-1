using TMPro;
using UnityEngine;

public class UIPanelCharacterInventory : MonoBehaviour
{
    [Header("UI")]
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    [Header("Slot List")]
    public UiCharacterSlotList uiCharacterSlotList;

    private void OnEnable()
    {
        // 처음 열릴 때 기본 상태 적용
        OnChangeSorting(sorting.value);
        OnChangeFiltering(filtering.value);
    }

    public void OnChangeSorting(int index)
    {
        uiCharacterSlotList.SetSorting(index);
    }

    public void OnChangeFiltering(int index)
    {
        uiCharacterSlotList.SetFiltering(index);
    }

    public void OnCreateCharacter()
    {
        uiCharacterSlotList.AddRandomCharacter();
    }

    public void OnRemoveCharacter()
    {
        uiCharacterSlotList.RemoveCharacter();
    }
    
}