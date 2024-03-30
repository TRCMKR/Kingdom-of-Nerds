using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosslog : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chaseRange = 5f;
    public string Status = "default";

    private Transform player;
    private Vector2 startingPosition;
    private bool isChasing = false;

    [SerializeField] public float ReloadTimeTumbleweed;
    [SerializeField] public int speed;
    private float _timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Start");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startingPosition = transform.position;
        StartCoroutine(ReloadTumbleweed());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Status);
        if (Status == "tumbleweed")
        {
            var rb = gameObject.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<BossTumbleweed>().Direction = rb.velocity;
            // Debug.Log("tururu");
            return;
        }
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

    void TumbleweedAttack()
    {
        // Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
        Status = "tumbleweed";
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        float actualAngle = angle;
        Vector2 direction = Quaternion.AngleAxis(actualAngle - angle, Vector3.forward) * difference.normalized;
        
        float vectorAngle = -Vector2.SignedAngle(direction, Vector2.right);
        // Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * speed);
        // gameObject.GetComponent<BossTumbleweed>().Direction = rb.velocity;
        // Debug.Log(rb.velocity);
    }

    public IEnumerator ReloadTumbleweed()
    {
        while(_timer < ReloadTimeTumbleweed)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        _timer = 0;
        TumbleweedAttack();
    }
}