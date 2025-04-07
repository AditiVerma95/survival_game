using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    
    private float health = 1;
    [Header("Health")]
    [SerializeField] private float healthDepleteFactor;
    [SerializeField] private Slider healthSlider;
    
    private float food = 1;
    [Header("Food")]
    [SerializeField] private float foodDepleteFactor;
    [SerializeField] private Slider foodSlider;
    
    private float water = 1;
    [Header("Water")]
    [SerializeField] private float waterDepleteFactor;
    [SerializeField] private Slider waterSlider;

    private float bottle = 0;
    [Header("Bottle")] 
    [SerializeField] private Slider bottleSlider;

    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        StartCoroutine(FoodWaterDeplete());
        StartCoroutine(HealthDeplete());
    }

    IEnumerator FoodWaterDeplete() {
        float time = 0f;
        while (true) {
            if(food != 0) 
                UpdateFood(food - foodDepleteFactor);
            if(water != 0)
                UpdateWater(water - waterDepleteFactor);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator HealthDeplete() {
        while (true) {
            if (food == 0 || water == 0) {
                UpdateHealth(health - healthDepleteFactor);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void UpdateHealth(float val) {
        val = Mathf.Round(val * 1000f) / 1000f;
        health = val;
        healthSlider.value = val;
    }

    public void UpdateFood(float val) {
        val = Mathf.Round(val * 1000f) / 1000f;
        food = val;
        foodSlider.value = val;
    }
    
    public void UpdateWater(float val) {
        val = Mathf.Round(val * 1000f) / 1000f;
        water = val;
        waterSlider.value = val;
    }
    
    public void UpdateBottle(float val) {
        val = Mathf.Round(val * 1000f) / 1000f;
        bottle = val;
        bottleSlider.value = val;
    }
}
