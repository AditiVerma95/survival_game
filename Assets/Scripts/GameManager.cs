using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    [Header("Health")]
    [SerializeField] private float health = 0.5f;
    [SerializeField] private float healthDepleteFactor;
    [SerializeField] private Slider healthSlider;

    [Header("Food")]
    [SerializeField] private float food = 0.5f;
    [SerializeField] private float foodDepleteFactor;
    [SerializeField] private Slider foodSlider;

    [Header("Water")]
    [SerializeField] public float water = 0.5f;
    [SerializeField] private float waterDepleteFactor;
    [SerializeField] private Slider waterSlider;

    [Header("Bottle")] 
    [SerializeField] public float bottle = 0f;
    [SerializeField] private Slider bottleSlider;

    public static GameManager Instance;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        StartCoroutine(FoodWaterDeplete());
        StartCoroutine(HealthDeplete());
    }

    IEnumerator FoodWaterDeplete() {
        while (true) {
            if (food > 0f) UpdateFood(-foodDepleteFactor);
            if (water > 0f) UpdateWater(-waterDepleteFactor);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator HealthDeplete() {
        while (true) {
            if (food <= 0f || water <= 0f) {
                UpdateHealth(-healthDepleteFactor);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void UpdateHealth(float delta) {
        health = Clamp01WithPrecision(health + delta);
        healthSlider.value = health;
    }

    public void UpdateFood(float delta) {
        food = Clamp01WithPrecision(food + delta);
        foodSlider.value = food;
    }

    public void UpdateWater(float delta) {
        water = Clamp01WithPrecision(water + delta);
        waterSlider.value = water;
    }

    public void UpdateBottle(float delta) {
        bottle = Clamp01WithPrecision(bottle + delta);
        bottleSlider.value = bottle;
    }

    private float Clamp01WithPrecision(float value) {
        return Mathf.Clamp(Mathf.Round(value * 1000f) / 1000f, 0f, 1f);
    }
}
