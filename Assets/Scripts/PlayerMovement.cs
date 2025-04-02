using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private float moveSpeed = 5f;
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(x *  moveSpeed, rb.linearVelocity.y,z *  moveSpeed));
    }
}
