using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    private readonly StringBuilder sb = new StringBuilder();
    public TextMeshProUGUI inputField;
    public GameObject rootKeyboard;
    public int maxCharacters = 7;

    private float timer = 0f;
    private float cursorDelay = 0.5f;

    private bool blink;


    private void Awake()
    {
        var keys = rootKeyboard.GetComponentsInChildren<Button>();
        foreach (var key in keys)
        {
            var text = key.GetComponentInChildren<TextMeshProUGUI>();
            key.onClick.AddListener(() => Onkey(text.text));
            
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >cursorDelay)
        {
            timer = 0f;
            blink = !blink;
            UpdateInputField();
        }
    }
    public override void Open()
    {
        sb.Clear();
        timer = 0f;
        blink = false;
        base.Open();
        UpdateInputField();
    }

    public void Onkey(string key)
    {
        if(sb.Length < maxCharacters)
        {
            sb.Append(key);
            UpdateInputField();
        }
    }   
    private void UpdateInputField()
    {
        bool showCursor = sb.Length < maxCharacters && !blink;
        if(showCursor)
        {
            sb.Append('_');
        }
        inputField.SetText(sb);
        if(showCursor)
        {
            sb.Length -= 1;
        }
    }
    public void OnCancel()
    {
        sb.Clear();
        UpdateInputField();
    }

    public void onDelete()
    {
        if(sb.Length >0)
        {
            sb.Length -= 1;
            UpdateInputField();

        }
    }
    public void OnAccept()
    {

    }
    

    
}
