using UnityEngine;

public class Lighter : MonoBehaviour {
    public void PickLighter() {
        InventoryManager.Instance.UpdateLighter(1);
        Destroy(gameObject);
    }
}
