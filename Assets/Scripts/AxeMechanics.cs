using System;
using System.Collections;
using UnityEngine;

public class AxeMechanics : MonoBehaviour {
    private Animator animator;
    public bool isAnimating = false;
    private float swingAnimationLength = 0.583f;
    [SerializeField] private Camera mainCamera;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!isAnimating && InputManager.Instance.ShootPressed) {
            StartCoroutine(AxeSwing());
        }
    }

    private IEnumerator AxeSwing() {
        PlayerEquipState.Instance.canSwitch = false;
        isAnimating = true;
        animator.SetBool("isFire", true);
        Debug.Log("Axe swing started");

        // Wait until midway through the animation
        yield return new WaitForSeconds(swingAnimationLength / 2f);

        // Ray from the center of the screen
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        int cuttableLayerMask = LayerMask.GetMask("Cuttable");

        if (Physics.Raycast(ray, out hit, 2f, cuttableLayerMask)) {
            if (hit.collider.CompareTag("Tree")) {
                hit.collider.GetComponent<Tree>().BreakTree();
            }
            else if (hit.collider.CompareTag("Rock")) {
                hit.collider.GetComponent<Rock>().BreakRock();
            }
        }

        yield return new WaitForSeconds(swingAnimationLength / 2f);

        animator.SetBool("isFire", false);
        isAnimating = false;
        PlayerEquipState.Instance.canSwitch = true;
    }


}