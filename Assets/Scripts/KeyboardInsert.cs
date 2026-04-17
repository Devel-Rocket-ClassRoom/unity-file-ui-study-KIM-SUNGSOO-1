using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInsert : MonoBehaviour
{
    private string writeName;
    private KeyboardWindow keyboardWindow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyboardWindow = GetComponent<KeyboardWindow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnKeyBoardClick(Button button)
    {
        var text = button.GetComponentInChildren<TextMeshProUGUI>().text;

        writeName += text;

        
    }
}
