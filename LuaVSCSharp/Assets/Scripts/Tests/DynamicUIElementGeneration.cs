using MoonSharp.Interpreter;
using UnityEngine;

public class DynamicUIElementGeneration : TestRunner
{
    [SerializeField] private GameObject uiPrefab; // Prefab of the UI element
    [SerializeField] private Transform parent; // Parent UI object (e.g., Canvas)

    protected override void Start()
    {
        base.Start();

        // Register GameObject as a user data type
        UserData.RegisterType<GameObject>();
        UserData.RegisterType<Transform>();
        UserData.RegisterType<RectTransform>();
        UserData.RegisterType<Rect>();

        luaScript.Globals["get_ui_prefab"] = (System.Func<GameObject>)(() => {
            return uiPrefab;
        });
        luaScript.Globals["get_ui_parent"] = (System.Func<Transform>)(() => {
            return parent;
        });

        luaScript.Globals["instantiate_ui_prefab"] = (System.Func<GameObject, Transform, GameObject>)((prefab, pParent) => {
            return Instantiate(uiPrefab, parent);
        });

        luaScript.Globals["destroy_ui_element"] = (System.Action<GameObject>)((uiElement) => {
            Destroy(uiElement);
        });
        
        luaScript.Globals["set_position"] = (System.Action<GameObject, float, float>)((uiElement, x, y) => {
            uiElement.transform.position = new Vector3(x,y,0);
        });
    }

    protected override void LuaTestLogic()
    {
        luaScript.Call(luaScript.Globals["dynamic_ui_element_generation"]);
    }

    protected override void CSharpTestLogic()
    {
        for (int i = 0; i < 1000; i++)
        {
            // Instantiate a new UI element
            GameObject uiElement = Instantiate(uiPrefab, parent);
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
            if (i % 100 == 0 && i > 0)
            {
                Destroy(uiElement);
            }
        }
    }
}