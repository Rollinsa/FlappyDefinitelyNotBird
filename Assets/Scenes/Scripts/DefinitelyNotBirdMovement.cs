using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefinitelyNotBirdMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private GameObject notBird;
    [SerializeField] private float initialHorizontalSpeed;
    [SerializeField] private float flapSpeed;
    [SerializeField] private float deathFallSpeed;
    [SerializeField] private float birdyGravity;
    [SerializeField] private Text gameOver;
    private float startDelay;

    private bool didSetBirdyGravity;

    private bool dead;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = notBird.GetComponent<Animator>();
        startDelay = FindObjectOfType<Countdown>().getStartDelay();
        gameOver.enabled = false;
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
        dead = true;
        body.velocity = new Vector2(0, 0);
        anim.SetTrigger("dead");
        body.gravityScale = deathFallSpeed;
        transform.Rotate(180.0f, 0f, 0f, Space.World);

        GameOver();
    }

    private void GameOver()
    {
        gameOver.enabled = true;
    }
}
