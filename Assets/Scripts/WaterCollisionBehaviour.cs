using System;
using UnityEngine;

public class WaterCollisionBehaviour : MonoBehaviour {
    [SerializeField] private GameObject waterUI;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            waterUI.SetActive(true);
            if (InputManager.Instance.InteractPressed) {
                GameManager.Instance.UpdateWater(1);
                GameManager.Instance.UpdateBottle(1);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            waterUI.SetActive(false);
        }
    }
}
