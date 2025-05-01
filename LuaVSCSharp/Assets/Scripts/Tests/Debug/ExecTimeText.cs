using UnityEngine;
using TMPro;

/// <summary>
/// This class should be placed directly on the text object which displays the ACTUAL elapsed time.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class ExecTimeText : CodyUtilities.Singleton<ExecTimeText>
{
    private TextMeshProUGUI _uiText;
    
    protected override void Awake()
    {
        base.Awake();
        _uiText = GetComponent<TextMeshProUGUI>();
        _uiText.text = "N/A - Run a test";
        //_uiText.text = string.Format("<color=green>{0}</color>", _uiText.text);
    }

    public void SetText(string text, ExecutionTimeSeverity ets)
    {
        _uiText.text = text;
        switch (ets)
        {
            case ExecutionTimeSeverity.Low:
                _uiText.color = Color.green;
                break;
            case ExecutionTimeSeverity.Medium:
                _uiText.color = Color.yellow;
                break;
            case ExecutionTimeSeverity.High:
                _uiText.color = Color.red;
                break;
            default:
                _uiText.color = Color.white;
                break;
        }
    }
}