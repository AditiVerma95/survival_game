using System;
using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour {
    private float health = 5f;
    [SerializeField] private ParticleSystem rockBreakPS;
    private Animator animator;

    [SerializeField] private GameObject rockVisual1;
    [SerializeField] private GameObject rockVisual2;
    [SerializeField] private GameObject rockVisual3;

    private AudioSource audioSource;

    private void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void BreakRock() {
        InventoryManager.Instance.UpdateRock(1);
        if (health == 1f) {
            rockVisual1.SetActive(false);
            rockVisual2.SetActive(false);
            rockVisual3.SetActive(false);
            GetComponent<MeshCollider>().enabled = false;
            Invoke("RespawnRock", 50f);
            return;
        }
        StartCoroutine(RockCou());
    }

    private void RespawnRock() {
        health = 5f;
        rockVisual1.SetActive(true);
        rockVisual2.SetActive(true);
        rockVisual3.SetActive(true);
        GetComponent<MeshCollider>().enabled = true;
    }

    private IEnumerator RockCou() {
        health--;
        rockBreakPS.Play();
        audioSource.Play();
        animator.SetBool("isBreak", true);
        yield return new WaitForSeconds(0.333f);
        animator.SetBool("isBreak", false);
    }
}
