using System.Collections;
using UnityEngine;

public class Tree : MonoBehaviour {
    private float health = 5f;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void BreakTree() {
        InventoryManager.Instance.UpdateWood(1);
        if (health == 1f) {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(TreeCou());
    }

    private IEnumerator TreeCou() {
        health--;
        animator.SetBool("isBreak", true);
        yield return new WaitForSeconds(0.333f);
        animator.SetBool("isBreak", false);
    }
}
