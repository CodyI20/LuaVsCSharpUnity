using System;
using UnityEngine;
using UnityEngine.UI;
public class TabButton : Button
{
    private TabGroup _tabGroup;
    
    protected override void Awake()
    {
        base.Awake();
        _tabGroup = GetComponentInParent<TabGroup>();
        colors = new ColorBlock
        {
            normalColor = Color.white,
            highlightedColor = ColorUtility.TryParseHtmlString("#E99797", out var highlightedColor) ? highlightedColor : Color.black,
            pressedColor = ColorUtility.TryParseHtmlString("#D41313", out var pressedColor) ? pressedColor : Color.black,
            selectedColor = ColorUtility.TryParseHtmlString("#54002E", out var selectedColor) ? selectedColor : Color.black,
            disabledColor = Color.white,
            colorMultiplier = 1f,
            fadeDuration = 0.1f
        };
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(MouseDownAction);
        onClick.AddListener(MouseEnterAction);
        onClick.AddListener(MouseExitAction);
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        onClick.RemoveAllListeners();
    }

    private void MouseEnterAction()
    {
        _tabGroup.OnTabEnter(this);
    }
    
    private void MouseExitAction()
    {
        _tabGroup.OnTabExit(this);
    }

    private void MouseDownAction()
    {
        Debug.Log("Mouse Down");
        _tabGroup.OnTabSelected(this);
    }
}
