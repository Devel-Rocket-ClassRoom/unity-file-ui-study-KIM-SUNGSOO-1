using Newtonsoft.Json;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DifficultyData
{
    public int selected;

    public bool easy;
    public bool normal;
    public bool hard;
}
public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;

    public Button[] buttons;
    public int selected;

    private string saveDir;
    private string savePath;
    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
        buttons[0].onClick.AddListener(OnCancelClick);
        buttons[1].onClick.AddListener(OnApplyClick);

        saveDir = Path.Combine(Application.persistentDataPath, "Difficulty");
        savePath = Path.Combine(saveDir, "difficulty.json");

    }
    public override void Open()
    {
        base.Open();
        toggles[selected].isOn = true;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active)
    {
        if(active)
        {
            selected = 0;
            Debug.Log("OnEasy");
        }
    }
    public void OnNormal(bool active)
    {
        if(active)
        {
            selected = 1;
            Debug.Log("OnNormal");
        }
        
    }
    public void OnHard(bool active)
    {
        if (active)
        {
            selected = 2;
            Debug.Log("OnHard");
        }
        
    }

    public void OnCancelClick()
    {
        windowManager.Open(0);
    }
    public void OnApplyClick()
    {
        
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
        }
        DifficultyData data = new DifficultyData();
        data.selected = selected;
        data.easy = toggles[0].isOn;
        data.normal = toggles[1].isOn;
        data.hard = toggles[2].isOn;

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(savePath, json);

        
    }
}
