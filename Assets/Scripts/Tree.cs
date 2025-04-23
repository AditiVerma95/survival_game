using System.Collections;
using UnityEngine;

public class Tree : MonoBehaviour {
    private float health = 5f;
    private Animator animator;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject treeVisual;

    private void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void BreakTree() {
        audioSource.Play();
        InventoryManager.Instance.UpdateWood(1);
        if (health == 1f) {
            treeVisual.SetActive(false);
            GetComponent<CapsuleCollider>().enabled = false;
            Invoke("RespawnTree", 50f);
            return;
        }
        StartCoroutine(TreeCou());
    }

    private void RespawnTree() {
        treeVisual.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = true;
        health = 5f;
    }

    private IEnumerator TreeCou() {
        health--;
        animator.SetBool("isBreak", true);
        yield return new WaitForSeconds(0.333f);
        animator.SetBool("isBreak", false);
    }
}
