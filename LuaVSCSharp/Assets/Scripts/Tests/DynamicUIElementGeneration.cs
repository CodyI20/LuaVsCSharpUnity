using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine;
using System.Collections.Generic;

public class DynamicUIElementGeneration : TestRunner
{
    [SerializeField] private GameObject uiPrefab; // Prefab of the UI element
    [SerializeField] private Transform parent; // Parent UI object
    private HashSet<GameObject> uiElements = new HashSet<GameObject>();
    protected override void Start()
    {
        base.Start();

        // Register GameObject as a user data type
        UserData.RegisterType<GameObject>();
        UserData.RegisterType<Transform>();
        UserData.RegisterType<RectTransform>();
        UserData.RegisterType<Rect>();
        
        luaScript.Globals["get_ui_parent"] = (System.Func<Transform>)(() => parent);

        luaScript.Globals["instantiate_ui_prefab"] = (System.Func<GameObject>)(() => {
            GameObject uiElement = Instantiate(uiPrefab, parent);
            uiElements.Add(uiElement);
            return uiElement;
        });

        luaScript.Globals["destroy_ui_element"] = (System.Action<GameObject>)(uiElement => {
            uiElements.Remove(uiElement);
            Destroy(uiElement);
        });
        
        luaScript.Globals["set_position"] = (System.Action<GameObject, float, float>)((uiElement, x, y) => {
            uiElement.transform.localPosition = new Vector3(x,y,0);
        });
    }

    private void OnEnable()
    {
        TabButton.OnTabSelected += DeleteAllUIElements;
    }
    private void OnDisable()
    {
        TabButton.OnTabSelected -= DeleteAllUIElements;
    }
    
    private void DeleteAllUIElements()
    {
        foreach (var uiElement in uiElements)
        {
            Destroy(uiElement);
        }
        uiElements.Clear();
    }

    protected override void LuaTestLogic(int iterations)
    {
        DeleteAllUIElements(); // Clear UI elements at the start
        luaScript.Call(luaScript.Globals["dynamic_ui_element_generation"], iterations);
    }
    

    protected override void CSharpTestLogic(int iterations)
    {
        DeleteAllUIElements();
        for (int i = 0; i < iterations; i++)
        {
            // Instantiate a new UI element
            GameObject uiElement = Instantiate(uiPrefab, parent);
            uiElements.Add(uiElement);
            
            RectTransform parentRect = parent.GetComponent<RectTransform>();
            if (parentRect != null)
            {
                // Generate random positions within the parent's bounds
                float x = Random.Range(parentRect.rect.xMin, parentRect.rect.xMax);
                float y = Random.Range(parentRect.rect.yMin, parentRect.rect.yMax);

                // Clamp the position within the parent's bounds
                float clampedX = Mathf.Clamp(x, parentRect.rect.xMin, parentRect.rect.xMax);
                float clampedY = Mathf.Clamp(y, parentRect.rect.yMin, parentRect.rect.yMax);
                uiElement.transform.localPosition = new Vector3(clampedX, clampedY, 0);
            }
            else
            {
                Debug.LogWarning("Parent does not have a RectTransform. Positioning may not be constrained.");

                // Generate random positions without bounds
                float x = Random.Range(-500, 500);
                float y = Random.Range(-500, 500);
                uiElement.transform.localPosition = new Vector3(x, y, 0);
            }

            // Optionally destroy some elements after a few iterations
            if (i % 10 == 0 && i > 0)
            {
                Destroy(uiElement);
                uiElements.Remove(uiElement);
            }
        }
    }
}