using System;
using UnityEngine;

public class PlayerTriggerDetection : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Enemy")) {
            GameManager.Instance.UpdateHealth(-0.01f);
        }
    }
}
