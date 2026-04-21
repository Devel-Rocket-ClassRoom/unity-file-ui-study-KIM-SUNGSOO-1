using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UiInvenSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDecsending,
        NameAscending,
        NameDecsending,
        CostAscending,
        CostDescending,
    }
    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable,
        nonConsumable,
        
    }

    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs,rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime), //오름차순
        (lhs,rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime), //내림차순
        (lhs,rhs) => lhs.itemData.StringName.CompareTo(rhs.itemData.StringName),
        (lhs,rhs) => rhs.itemData.StringName.CompareTo(lhs.itemData.StringName),
        (lhs,rhs) => lhs.itemData.Cost.CompareTo(rhs.itemData.Cost),
        (lhs,rhs) => rhs.itemData.Cost.CompareTo(lhs.itemData.Cost),
    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.itemData.Type == ItemTypes.Weapon,
        (x) => x.itemData.Type == ItemTypes.Equip,
        (x) => x.itemData.Type == ItemTypes.Consumable,
        (x) => x.itemData.Type != ItemTypes.Consumable,
    };
    public UiInvenSlot prefab;
    public ScrollRect scrollRect;

    

    private List<UiInvenSlot> uiSlotList = new List<UiInvenSlot>();
    

    public int uiSlotMaxCount = 50;

    private List<SaveItemData> saveItemDataList = new List<SaveItemData>();
    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots(saveItemDataList);
            }
            
        }
    }
    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots(saveItemDataList);
            }
            
        }
    }

    private int selectedSlotIndex = -1;


    private void OnSelectSlot(SaveItemData saveItemData)
    {
        Debug.Log(saveItemData);
    }
    private void Start()
    {
        
    }

    private void OnEnable()
    {

        
    }
    
    private void OnDisable()
    {
        

    }
    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }
    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots(saveItemDataList);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddRandomItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            RemoveItem();
        }
        
    }

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;
    private void UpdateSlots(List<SaveItemData> itemlist)
    {
        var list = itemlist.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                var capturedSlot = newSlot;
                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = capturedSlot.slotIndex;
                    onSelectSlot?.Invoke(capturedSlot.SaveItemData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        
        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].slotIndex = i;
                uiSlotList[i].gameObject.SetActive(true);
                
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots?.Invoke();
    }
    
    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots(saveItemDataList);
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }
        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].SaveItemData);
        UpdateSlots(saveItemDataList);


    }

}
