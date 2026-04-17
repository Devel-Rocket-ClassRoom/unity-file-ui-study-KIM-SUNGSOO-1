
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;

    private const int totalstats = 6;

    private const int statsPerColumn = 3;

    public float statsDelay = 1f;
    public float scoreDuration = 2f;

    private int[] statsRolls = new int[totalstats];
    private int finalScore;
    public Button newButton;

    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;
    private Coroutine coroutine;





    private void Awake()
    {
        statsLabels = new TextMeshProUGUI[] { leftStatLabel, rightStatLabel };
        statsValues = new TextMeshProUGUI[] { leftStatValue, rightStatValue };
        newButton.onClick.AddListener(OnNext);
    }


    public override void Open()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        base.Open();
        ResetStats();
        coroutine = StartCoroutine(CoplayGameoverCoroutine());
        
    }
    public override void Close()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        base.Close();
    }
    public void OnNext()
    {
        windowManager.Open(0);
    }


    private void ResetStats()
    {
        for (int i = 0; i < totalstats; i++)
        {
            statsRolls[i] = Random.Range(0, 1000);
            finalScore += statsRolls[i];
        }
        
        for (int i = 0; i < statsLabels.Length; i++)
        {
            statsLabels[i].text = string.Empty;
            statsValues[i].text = string.Empty;
        }

        scoreValue.text = $"{0:D9}";
    }

    private IEnumerator CoplayGameoverCoroutine()
    {
        for (int i = 0; i < totalstats; i++)
        {
            yield return new WaitForSeconds(statsDelay);
            int column = i / statsPerColumn;
            var labelText = statsLabels[column];
            var valueText = statsValues[column];
            string newline = i% statsPerColumn == 0 ? string.Empty : "\n";
            labelText.text = $"{labelText.text}{newline}Stat{i}";
            valueText.text = $"{valueText.text}{newline}{statsRolls[i]:D4}";
        }

        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime/ scoreDuration;
            int current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreValue.text = $"{current:D9}";
            yield return null;
        }

        scoreValue.text = $"{finalScore:D9}";
        coroutine = null;
    }
}
