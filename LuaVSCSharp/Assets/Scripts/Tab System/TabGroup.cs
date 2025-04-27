using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    private TabButton[] _tabButtons;
    
    [SerializeField] private List<GameObject> objectsToSwap;

    private void Start()
    {
        _tabButtons = GetComponentsInChildren<TabButton>();
    }
    
    public void OnTabEnter(TabButton button)
    {
        
    }

    public void OnTabExit(TabButton button)
    {
        
    }
    
    public void OnTabSelected(TabButton button)
    {
        Debug.Log(button.name);
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
                objectsToSwap[i].SetActive(true);
            else
                objectsToSwap[i].SetActive(false);
        }
    }
}
