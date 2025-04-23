using UnityEngine;

public class CookedMeat : MonoBehaviour {
    public void PickCookedMeat() {
        InventoryManager.Instance.UpdateCookedMeat(1);
        Destroy(gameObject);
    }
}
