using System;
using UnityEngine;

public class PlayerEquipState : MonoBehaviour {
    public GameObject currentEquipped;

    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject bow;

    public static PlayerEquipState Instance;
    public bool canSwitch = true;
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        axe.SetActive(false);
        bow.SetActive(false);
        currentEquipped = axe;
        currentEquipped.SetActive(true);
    }

    private void OnEnable() {
        InputManager.Instance.axeEvent += EquipAxe;
        InputManager.Instance.bowEvent += EquipBow;
    }

    private void OnDisable() {
        InputManager.Instance.axeEvent -= EquipAxe;
        InputManager.Instance.bowEvent -= EquipBow;
    }

    private void EquipAxe(object sender, EventArgs e) {
        if (canSwitch) {
            ChangeCurrentEquip(axe);
        }
    }


    private void EquipBow(object sender, EventArgs e) {
        if (canSwitch) {
            ChangeCurrentEquip(bow);
        }
    }


    private void ChangeCurrentEquip(GameObject obj) {
        currentEquipped.SetActive(false);
        obj.SetActive(true);
        currentEquipped = obj;
    }
}
