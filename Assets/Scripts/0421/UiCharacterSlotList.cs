using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharacterSlotList : MonoBehaviour
{
    public CharacterSlotUI prefab;
    public ScrollRect scrollRect;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveCharacterData> onSelectSlot;

    private readonly List<CharacterSlotUI> uiSlotList = new List<CharacterSlotUI>();
    private readonly List<SaveCharacterData> saveCharacterDataList = new List<SaveCharacterData>();

    private SortingOptions sorting = SortingOptions.CreationTimeAsc;
    private FilteringOptions filtering = FilteringOptions.None;
    private int selectedSlotIndex = -1;

    public enum SortingOptions
    {
        CreationTimeAsc,
        CreationTimeDesc,
        NameAsc,
        NameDesc,
    }

    public enum FilteringOptions
    {
        None,
        Warriar,
        Archer,
        Tanker,
    }
    public readonly System.Comparison<SaveCharacterData>[] comparisons =
{
    (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
    (lhs, rhs) => rhs.creationTime.CompareTo(lhs.creationTime),
    (lhs, rhs) => lhs.characterData.StringName.CompareTo(rhs.characterData.StringName),
    (lhs, rhs) => rhs.characterData.StringName.CompareTo(lhs.characterData.StringName),
    (lhs, rhs) => lhs.characterData.Attack.CompareTo(rhs.characterData.Attack),
    (lhs, rhs) => rhs.characterData.Attack.CompareTo(lhs.characterData.Attack),
};

    public readonly Predicate<SaveCharacterData>[] filterings =
    {
    (x) => true,
    (x) => x.characterData.Type == CharacterTypes.Warriar,
    (x) => x.characterData.Type == CharacterTypes.Archer,
    (x) => x.characterData.Type == CharacterTypes.Tanker,
};
    public List<SaveCharacterData> GetSaveCharacterDataList()
    {
        return saveCharacterDataList;
    }

    public void SetSaveCharacterDataList(List<SaveCharacterData> source)
    {
        saveCharacterDataList.Clear();

        if (source != null)
            saveCharacterDataList.AddRange(source);

        UpdateSlots();
    }

    private void UpdateSlots()
    {
        List<SaveCharacterData> list = new List<SaveCharacterData>(saveCharacterDataList);

        list = list.FindAll(filterings[(int)filtering]);
        list.Sort(comparisons[(int)sorting]);
        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                CharacterSlotUI newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.gameObject.SetActive(false);

                int capturedIndex = i;
                newSlot.Button.onClick.AddListener(() =>
                {
                    if (capturedIndex < 0 || capturedIndex >= saveCharacterDataList.Count)
                        return;

                    selectedSlotIndex = capturedIndex;
                    RefreshSelectedState();
                    onSelectSlot?.Invoke(saveCharacterDataList[capturedIndex]);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetData(list[i].characterId);
                uiSlotList[i].SetSlotIndex(i);
                uiSlotList[i].SetSelected(i == selectedSlotIndex);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        onUpdateSlots?.Invoke();
    }

    private void RefreshSelectedState()
    {
        for (int i = 0; i < uiSlotList.Count; i++)
        {
            bool selected = (i == selectedSlotIndex);
            uiSlotList[i].SetSelected(selected);
        }
    }
    public void SetSorting(int index)
    {
        if (index < 0 || index >= comparisons.Length)
            return;

        sorting = (SortingOptions)index;

        UpdateSlots();
    }

    public void SetFiltering(int index)
    {
        if (index < 0 || index >= filterings.Length)
            return;

        filtering = (FilteringOptions)index;

        UpdateSlots();
    }

    public void AddRandomCharacter()
    {
        saveCharacterDataList.Add(SaveCharacterData.GetRandomCharacter());
        UpdateSlots();
    }

    public void RemoveCharacter()
    {
        if (selectedSlotIndex < 0 || selectedSlotIndex >= saveCharacterDataList.Count)
            return;

        saveCharacterDataList.RemoveAt(selectedSlotIndex);
        selectedSlotIndex = -1;
        UpdateSlots();
    }
}