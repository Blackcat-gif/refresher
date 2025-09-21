using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    Rigidbody rb;
    [SerializeField] float speed;
    NavMeshPath navPath;
    Queue<Vector3> remainingPoints;
    Vector3 currentTargetPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        navPath = new NavMeshPath();
        remainingPoints = new Queue<Vector3>();

        if(agent.CalculatePath(target.position, navPath))
        {
            Debug.Log("found path to target");
            foreach(Vector3 p in navPath.corners)
            {
                remainingPoints.Enqueue(p);
            }

            currentTargetPoint = remainingPoints.Dequeue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);

        transform.forward = (currentTargetPoint - transform.position).normalized;

        float distToPoint = Vector3.Distance(transform.position, currentTargetPoint);

        if(distToPoint<1)
        {
            currentTargetPoint = remainingPoints.Dequeue();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnDrawGizmos()
    {
        if(navPath == null)
        {
            return;
        }
        foreach (Vector3 node in navPath.corners)
        {
            Gizmos.DrawWireSphere(node, 0.5f);          
        }
    }
}
