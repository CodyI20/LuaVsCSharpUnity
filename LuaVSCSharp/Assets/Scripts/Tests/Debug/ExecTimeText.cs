using UnityEngine;
using TMPro;

/// <summary>
/// This class should be placed directly on the text object which displays the ACTUAL elapsed time.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class ExecTimeText : CodyUtilities.Singleton<ExecTimeText>
{
    private TextMeshProUGUI _uiText;
    
    void Awake()
    {
        _uiText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        _uiText.text = text;
    }
}
