using System;
using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour {
    private float health = 5f;
    [SerializeField] private ParticleSystem rockBreakPS;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void BreakRock() {
        InventoryManager.Instance.UpdateRock(1);
        if (health == 1f) {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(RockCou());
    }

    private IEnumerator RockCou() {
        health--;
        rockBreakPS.Play();
        animator.SetBool("isBreak", true);
        yield return new WaitForSeconds(0.333f);
        animator.SetBool("isBreak", false);
    }
}
