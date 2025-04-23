using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Bear : MonoBehaviour
{
    public float detectionRadius = 30f;
    public float wanderRadius = 20f;
    public float wanderInterval = 5f;
    public Transform player;
    public LayerMask groundMask;

    private NavMeshAgent agent;
    private float wanderTimer;
    private Animator anim;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceHit;
    [SerializeField] private AudioClip bearWalkClip;
    [SerializeField] private AudioClip bearRunClip;
    private bool isAudioPlaying = false;
    
    public int health = 20;
    private bool isAlive = true;
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>(); // In case model is a child
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        wanderTimer = wanderInterval;
        anim.SetBool("isAlive", true);
        audioSource.Play();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Arrow")) {
            audioSourceHit.Play();
            Destroy(other.gameObject);
            health -= 5;
        }
        
        
        if (health == 0) {
            isAlive = false;
            InventoryManager.Instance.UpdateMeat(2);
            audioSource.Stop();
            Destroy(gameObject, 10f);
        }
    }
    

    void Update() {
        if (isAlive) {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRadius) {
                // Chase mode
                anim.SetBool("isRun", true);
                agent.speed = 5f;
                agent.SetDestination(player.position);
                if (audioSource.clip != bearRunClip) {
                    audioSource.clip = bearRunClip;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            else {
                // Wander mode
                anim.SetBool("isRun", false);
                wanderTimer += Time.deltaTime;
                agent.speed = 2.5f;
                if (wanderTimer >= wanderInterval) {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius);
                    agent.SetDestination(newPos);
                    wanderTimer = 0;
                }
                if (audioSource.clip != bearWalkClip) {
                    audioSource.clip = bearWalkClip;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
        }
        else {
            anim.SetBool("isAlive", false);
            agent.speed = 0;
        }
    }

    void LateUpdate()
    {
        AlignToGround();
    }

    void AlignToGround()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, groundMask))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    // Helper method to get a random point on NavMesh
    public static Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, distance, NavMesh.AllAreas))
        {
            return navHit.position;
        }

        return origin;
    }
}
