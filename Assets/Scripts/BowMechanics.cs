using System;
using System.Collections;
using UnityEngine;

public class BowMechanics : MonoBehaviour {
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private GameObject arrowPrefab;
    private Animator animator;
    private bool isShooting = false;
    private float shootAnimationLength = 0.567f;
    private float reloadAnimationLength = 1.5f;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!isShooting && InputManager.Instance.ShootPressed) {
            StartCoroutine(BowShoot());
        }
    }

    private IEnumerator BowShoot() {
        isShooting = true;
        animator.SetBool("isDrawing", true);
        
        yield return new WaitForSeconds(shootAnimationLength);
        
        // shooting logic
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        arrowRb.useGravity = true;
        arrowRb.AddForce(transform.right * 20f, ForceMode.Impulse);

        yield return new WaitForSeconds(reloadAnimationLength);
        animator.SetBool("isDrawing", false);
        isShooting = false;
    }
}
