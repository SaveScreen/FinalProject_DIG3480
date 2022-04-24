using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public AudioClip brokensound;
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force) {
        rb2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other) {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        HardEnemyController harde = other.collider.GetComponent<HardEnemyController>();
        BossScript boss = other.collider.GetComponent<BossScript>();
        if (e != null) {
            e.Fix();
        }
        if (harde != null) {
            harde.Fix();
        }
        if (boss != null) {
            boss.ChangeHealth(-1);
            boss.PlaySound(brokensound);
        }

        Destroy(gameObject);
    }

    void Update() {
        if (transform.position.magnitude > 1000.0f) {
            Destroy(gameObject);
        }
    }

}
