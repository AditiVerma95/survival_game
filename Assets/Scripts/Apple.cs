using UnityEngine;

public class Apple : MonoBehaviour {
    public void PickApple() {
        InventoryManager.Instance.UpdateApple(1);
        DisableVisuals();
        Invoke("EnableVisuals", 60f);
    }

    private void DisableVisuals() {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
    }
    
    private void EnableVisuals() {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }
}
