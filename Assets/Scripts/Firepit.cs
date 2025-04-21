using System;
using UnityEngine;

public class Firepit : MonoBehaviour {
    [SerializeField] private Material unconstructedMat;
    [SerializeField] private Material constructedMat;
    [SerializeField] private GameObject firePS;
    [SerializeField] private GameObject pointLight;

    [SerializeField] private GameObject firepitUI;

    private bool isConstructed = false;

    private void OnTriggerStay(Collider other) {
        if (!isConstructed) {
            firepitUI.SetActive(true);
            InventoryManager im = InventoryManager.Instance;
            int woodCount = im.wood;
            int rockCount = im.rock;
            int lighterCount = im.lighter;
            if (InputManager.Instance.InteractPressed && woodCount >= 5 && rockCount >= 5 && lighterCount >= 1) {
                im.UpdateLighter(-1);
                im.UpdateWood(-5);
                im.UpdateRock(-5);
                firePS.SetActive(true);
                GetComponent<MeshRenderer>().material = constructedMat;
                pointLight.SetActive(true);
                firepitUI.SetActive(false);
                isConstructed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        firepitUI.SetActive(false);
    }
}
