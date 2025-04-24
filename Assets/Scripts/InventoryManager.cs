using System;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public int wood = 0;
    [SerializeField] private TextMeshProUGUI woodText;
    
    public int rock = 0;
    [SerializeField] private TextMeshProUGUI rockText;

    public int apple = 0;
    [SerializeField] private TextMeshProUGUI appleText;
    
    public int lighter = 0;
    [SerializeField] private TextMeshProUGUI lighterText;

    public int meat = 0;
    [SerializeField] private TextMeshProUGUI meatText;
    
    public int cookedMeat = 0;
    [SerializeField] private TextMeshProUGUI cookedMeatText;

    public static InventoryManager Instance;

    private void Awake() {
        Instance = this;
    }

    public void UpdateWood(int value) {
        wood += value;
        woodText.text = wood + "";
    }

    public void UpdateRock(int value) {
        rock += value;
        rockText.text = rock + "";
    }
    
    public void UpdateApple(int value) {
        apple += value;
        appleText.text = apple + "";
    }

    public void UpdateLighter(int value) {
        lighter += value;
        lighterText.text = lighter + "";
    }
    
    public void UpdateMeat(int value) {
        meat += value;
        meatText.text = meat + "";
    }
    
    public void UpdateCookedMeat(int value) {
        cookedMeat += value;
        cookedMeatText.text = cookedMeat + "";
    }

    public void AppleConsume() {
        if (apple > 0) {
            GameManager.Instance.UpdateFood(0.1f);
            UpdateApple(-1);
        }
    }

    public void CookedMeatConsume() {
        if (cookedMeat > 0) {
            UpdateCookedMeat(-1);
            GameManager.Instance.UpdateFood(0.5f);
            GameManager.Instance.UpdateHealth(0.5f);
        }
    }
}
