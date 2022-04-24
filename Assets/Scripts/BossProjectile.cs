using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force) {
        Debug.Log("Launched projectile... Direction: " + direction + " Force: " + force + "AddForce = " + (direction * force));
        rb2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other) {

        RubyController ruby = other.gameObject.GetComponent<RubyController>();
        
        if (ruby != null) {
            ruby.ChangeHealth(-1);
        }
        //Destroy(gameObject);
    }

    void Update() {
        Debug.Log("Mag = " + transform.position.magnitude);
        if (transform.position.magnitude > 7.0f) {
            Destroy(gameObject);
        }
    }
}
