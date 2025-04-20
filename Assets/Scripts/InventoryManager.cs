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

    public void AppleConsume() {
        if (apple > 0) {
            GameManager.Instance.UpdateFood(0.1f);
            UpdateApple(-1);
        }
    }
}
