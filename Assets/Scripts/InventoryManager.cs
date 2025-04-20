using System;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public int wood = 0;
    [SerializeField] private TextMeshProUGUI woodText;
    
    public int rock = 0;
    [SerializeField] private TextMeshProUGUI rockText;
    
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
}
