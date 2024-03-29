using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosslog : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chaseRange = 5f;

    private Transform player;
    private Vector2 startingPosition;
    private bool isChasing = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPosition, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, startingPosition) < 0.1f)
        {
            Vector2 newPatrolPosition = startingPosition + new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            startingPosition = newPatrolPosition;
        }

    }
}