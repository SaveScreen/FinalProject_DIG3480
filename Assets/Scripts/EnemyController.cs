using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    private Rigidbody2D rb2d;
    public float changetime;
    private float timer;
    private int direction = 1;
    private Animator animator;
    private bool broken;
    public ParticleSystem smoke;
    public AudioClip hitsound;
    private RubyController ruby;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = changetime;
        animator = GetComponent<Animator>();
        smoke.Play();
        broken = true;

        GameObject rubytag = GameObject.FindWithTag("RubyController");
        ruby = rubytag.GetComponent<RubyController>();
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            direction = -direction;
            timer = changetime;
        }
        if (!broken) {
            return;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        if (vertical) {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rb2d.MovePosition(position);
        if (!broken) {
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null) {
            player.ChangeHealth(-1);
            player.PlaySound(hitsound);
        }

    }

    public void Fix() {
        broken = false;
        rb2d.simulated = false;
        animator.SetTrigger("Fixed");
        ruby.ChangeScore();
        smoke.Stop();
    }
}
