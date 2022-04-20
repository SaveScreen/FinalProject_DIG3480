using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public AudioClip hit;

    void OnTriggerStay2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null) {
            if (!controller.isinvincible) {
                controller.PlaySound(hit);
            }
            controller.ChangeHealth(-1);
        }

    }
        
}
