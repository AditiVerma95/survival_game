using System;
using UnityEngine;

public class PickableMechanics : MonoBehaviour {
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private Camera mainCamera;
    
    private void Start() {
        InputManager.Instance.pickEvent += Pick;
    }

    private void Pick(object sender, EventArgs e) {
        Vector3 screenCentre = new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = mainCamera.ScreenPointToRay(screenCentre);
        RaycastHit raycastHit;
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);
        if (Physics.Raycast(ray, out raycastHit, 10f, pickableLayerMask)) {
            if (raycastHit.collider.tag == "Apple") {
                raycastHit.collider.GetComponent<Apple>().PickApple();
            }
        }
    }
}
