using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitelyNotBirdMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float initialHorizontalSpeed;
    [SerializeField] private float flapSpeed;
    [SerializeField] private float deathFallSpeed;

    private bool dead;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
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

        body.velocity = new Vector2(body.velocity.x, 0);
        if (Input.GetKey(KeyCode.Space) || (Input.GetMouseButton(0)))
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y + flapSpeed);
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
