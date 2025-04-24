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

    private bool isConstructed = false;

    private float cookCooldown = 1f; // seconds
    private float lastCookTime = 0f;

    private void OnTriggerStay(Collider other) {
        if (!other.CompareTag("Player")) return;

        InventoryManager im = InventoryManager.Instance;

        if (!isConstructed) {
            firepitUI.SetActive(true);

            if (InputManager.Instance.InteractPressed &&
                im.wood >= 5 && im.rock >= 5 && im.lighter >= 1) {

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
        else {
            cookMeatUI.SetActive(true);

            // Cook one meat every 1 second if holding interact and player has meat
            if (InputManager.Instance.InteractPressed &&
                Time.time - lastCookTime >= cookCooldown &&
                im.meat > 0) {

                im.UpdateMeat(-1);
                Instantiate(cookedMeatPrefab, cookedMeatSpawnPoint.position, cookedMeatSpawnPoint.rotation);

                lastCookTime = Time.time;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) return;

        firepitUI.SetActive(false);
        cookMeatUI.SetActive(false);
    }
}
