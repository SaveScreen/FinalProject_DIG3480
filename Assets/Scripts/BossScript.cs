using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

    //The boss is going to walk around randomly, but swiftly

    public float speed;
    public bool vertical;
    private Rigidbody2D rb2d;
    public float changetime;
    private float timer;
    private int direction = 1;
    private Animator animator;
    private bool broken;
    public ParticleSystem smoke;
    public ParticleSystem destroyed;
    public AudioClip hitsound;
    public AudioClip brokensound;
    private AudioSource audiosource;
    private RubyController ruby;
    public int bossmaxhealth;
    private int bosscurrenthealth;
    private int range;
    private int range1 = 0;
    private int range2 = 4;

    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = changetime;
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        smoke.Play();
        destroyed.Stop();
        broken = true;
        bosscurrenthealth = bossmaxhealth;
        
       

        GameObject rubytag = GameObject.FindWithTag("RubyController");
        ruby = rubytag.GetComponent<RubyController>();
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            range = Random.Range(range1,range2);
            Debug.Log(range);
            timer = changetime;
        }
        if (bosscurrenthealth <= 0) {
            Fix();
        }
        if (!broken) {
            return;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Have boss move randomly.
        Vector2 position = rb2d.position;
        if (range == 0) {
            
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        if (range == 1) {
            
            position.y = position.y + Time.deltaTime * speed * -direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", -direction);
        }
        if (range == 2) {
            
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        if (range == 3) {
            position.x = position.x + Time.deltaTime * speed * -direction;
            animator.SetFloat("Move X", -direction);
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
            player.ChangeHealth(-3);
            player.PlaySound(hitsound);
        }

    }


    public void ChangeHealth(int amount) {
        
        bosscurrenthealth = Mathf.Clamp(bosscurrenthealth + amount, 0, bossmaxhealth);
        UIBossHealthBar.instance.SetValue(bosscurrenthealth/(float)bossmaxhealth);
        Debug.Log(bosscurrenthealth + "/" + bossmaxhealth);

    }
    public void PlaySound(AudioClip clip) {
        audiosource.PlayOneShot(clip);
        Debug.Log(clip);
    }

    public void Fix() {

        ruby.ChangeScore();
        smoke.Stop();
        rb2d.simulated = false;
        destroyed.Play();
        ruby.PlaySound(brokensound);

    }

    /*
    public float timer;
    private float resettimer;
    public AudioClip hitsound;
    public ParticleSystem smoke;
    private RubyController ruby;
    private Animator animator;
    public int bossmaxhealth;
    private int bosscurrenthealth;
    private AudioSource audiosource;
    public float shootspeed;
    private Rigidbody2D rb2d;
    private Vector2 direction = Vector2.down;//new Vector2(0,1);
    private Vector2 leftdirection = Vector2.left;
    private Vector2 rightdirection = Vector2.right;
    public GameObject bossproj;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Functions for the boss:
        The boss will shoot a projectile at the player at a set interval.
        The boss will have a healthbar
        
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        smoke.Play();
        resettimer = timer;
        bosscurrenthealth = bossmaxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Direction boss is facing
        animator.SetFloat("Move Y",-1);
        animator.SetFloat("Move X",0);
        resettimer -= Time.deltaTime;
        if (resettimer < 0) {
            Launch();
            //Debug.Log("Launching at time = " + resettimer);
            resettimer = timer;
        }
        if (bosscurrenthealth <= 0) {

        }
        
        
        
    }

    void Launch() {
        Debug.Log("Instantiates bullet at = " + (rb2d.position + Vector2.down * 0.5f) + "in direction " + direction + "at shoot speed " + shootspeed);
        GameObject projectileobject = Instantiate(bossproj, rb2d.position + Vector2.down * 0.5f, Quaternion.identity);
        BossProjectile projectile = projectileobject.GetComponent<BossProjectile>();
        projectile.Launch(direction, shootspeed);
        projectile.Launch(leftdirection, shootspeed);
        projectile.Launch(rightdirection, shootspeed);
        //Debug.Log("Launched, direction = " + direction + " shootspeed = " + shootspeed);
    }

    public void ChangeHealth(int amount) {
        
        bosscurrenthealth = Mathf.Clamp(bosscurrenthealth + amount, 0, bossmaxhealth);
        UIBossHealthBar.instance.SetValue(bosscurrenthealth/(float)bossmaxhealth);

    }

    void OnCollisionEnter2D(Collision2D other) {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        

        if (player != null) {
            player.ChangeHealth(-1);
            player.PlaySound(hitsound);
        }

    }

    public void PlaySound(AudioClip clip) {
        audiosource.PlayOneShot(clip);
    }
    */
}
