using System;
using UnityEngine;

public class Firepit : MonoBehaviour {
    [SerializeField] private Material unconstructedMat;
    [SerializeField] private Material constructedMat;
    [SerializeField] private GameObject firePS;
    [SerializeField] private GameObject pointLight;
    
    [SerializeField] private GameObject firepitUI;

    [SerializeField] private GameObject cookedMeatPrefab;
    [SerializeField] private GameObject cookMeatUI;
    [SerializeField] private Transform cookedMeatSpawnPoint;
    private bool isCooked = false;

    private bool isConstructed = false;

    private void OnTriggerStay(Collider other) {
        if (!isConstructed && other.CompareTag("Player")) {
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

        else if (isConstructed && other.CompareTag("Player") && !isCooked) {
            cookMeatUI.SetActive(true);
            InventoryManager im = InventoryManager.Instance;
            if (InputManager.Instance.InteractPressed && im.meat > 0) {
                im.UpdateMeat(-1);
                Instantiate(cookedMeatPrefab, cookedMeatSpawnPoint.position, cookedMeatSpawnPoint.rotation);
                isCooked = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        firepitUI.SetActive(false);
    }
}
