using System;
using UnityEngine;

public class BottleFunctioning : MonoBehaviour {
    private void Start() {
        InputManager.Instance.bottleEvent += DrinkWaterFromBottle;
    }
    

    private void DrinkWaterFromBottle(object sender, EventArgs e)
    {
        Debug.Log("Bottle");
        float bottle = GameManager.Instance.bottle;

        // Set how much water the player can drink in one go
        float drinkAmount = 0.2f;

        // Ensure there's enough water in the bottle
        if (bottle >= drinkAmount)
        {
            // Update water level
            GameManager.Instance.UpdateWater(drinkAmount);

            // Reduce the amount from the bottle
            GameManager.Instance.UpdateBottle(-drinkAmount);
        }
        else if (bottle > 0f)
        {
            // Drink whatever is left in the bottle
            GameManager.Instance.UpdateWater(bottle);
            GameManager.Instance.UpdateBottle(-bottle);
        }
    }

}
