using UnityEngine;
using UnityEngine.UI;

public class HealthBarTest : TestRunner
{
    [SerializeField] private Image healthBar; //Change later to detect in Start/Awake
    protected override void Start()
    {
        base.Start();
        luaScript.Globals["set_color"] = (System.Action<string>)((colorName) =>
        {
            if (colorName.ToLower() == "red") healthBar.color = Color.red;
            else if (colorName.ToLower() == "green") healthBar.color = Color.green;
        });

        luaScript.Globals["set_fill"] = (System.Action<float>)((fillAmount) =>
        {
            healthBar.fillAmount = fillAmount;
        });
    }
    protected override void LuaTestLogic()
    {
        luaScript.Call(luaScript.Globals["update_health"]);
    }

    protected override void CSharpTestLogic()
    {
        System.Random rng = new System.Random(42);
        float health = 100;
        
        for (int i = 0; i < 1000; i++)
        {
            int damage = rng.Next(5, 21);
            int heal = rng.Next(1, 11);

            health -= damage;
            health = Mathf.Min(100, health + heal);

            if (health < 30)
                healthBar.color = Color.red;
            else
                healthBar.color = Color.green;

            healthBar.fillAmount = health / 100f;
        }
    }
}