using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedclip;
    //public ParticleSystem healthparticle;

   void OnTriggerEnter2D(Collider2D other) {
       RubyController controller = other.GetComponent<RubyController>();

       if (controller != null) {
           if (controller.health < controller.maxhealth) {
                controller.ChangeHealth(1);
                //healthparticle.Play();
                controller.PlaySound(collectedclip);
                Destroy(gameObject);   
           }
           
       }
   }
}
