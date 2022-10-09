using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitelyNotBirdMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float initialHorizontalSpeed;
    [SerializeField] private float flapSpeed;
    [SerializeField] private float deathFallSpeed;
    [SerializeField] private float birdyGravity;
    private float startDelay;

    private bool didSetBirdyGravity;

    private bool dead;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        startDelay = FindObjectOfType<Countdown>().getStartDelay();
    }

    void Start()
    {
        body.velocity = new Vector2(initialHorizontalSpeed, 0);
    }

    void Update()
    {
        if (dead)
        {
            return;
        }

        if (!didSetBirdyGravity)
        {
            if (Time.time > startDelay)
            {
                body.gravityScale = birdyGravity;
                didSetBirdyGravity = true;
            }
            return;
        }

        // body.velocity = new Vector2(body.velocity.x, 0);
        if (Input.GetKey(KeyCode.Space) || (Input.GetMouseButton(0)))
        {
            body.velocity = new Vector2(body.velocity.x, flapSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Ground")
        // {
        //     grounded = true;
        // }

        dead = true;
        body.velocity = new Vector2(0, 0);
        body.gravityScale = deathFallSpeed;
    }
}
