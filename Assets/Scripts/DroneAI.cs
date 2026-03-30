using UnityEngine;

public class DroneAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] waypoints; 
    public float patrolSpeed = 3f;
    private int currentTargetIndex = 0;

    [Header("Detection Settings")]
    public float viewAngle = 90f; 
    public float detectionDistance = 15f; 
    public LayerMask obstacleMask; 

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        Patrol();
        SearchForPlayer();
    }

    void Patrol()
    {
        if (waypoints.Length < 2) return;

        Transform target = waypoints[currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);

        // Face the waypoint [cite: 25]
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        // Switch waypoints when reached
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % waypoints.Length;
        }
    }

    void SearchForPlayer()
    {
        if (player == null) return;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);

        if (angleToPlayer < viewAngle / 2f)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionDistance)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dirToPlayer, out hit, detectionDistance))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        player.GetComponent<PlayerMovement>().TeleportToStart();
                    }
                }
            }
        }
    }
}