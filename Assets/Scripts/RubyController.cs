using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public float speed;
    public float timeinvincible;

    //   \/ DO NOT TOUCH IN INSPECTOR!!!
    public bool isinvincible;
    public bool youwin;
    public bool youlose;
    //   /\ DO NOT TOUCH IN INSPECTOR!!!

    private float invincibletimer;
    public int maxhealth;
    private int currenthealth;
    public int health {get {return currenthealth; }}
    private Rigidbody2D rb2d;
    private float horizontal;
    private float vertical;
    private Animator animator;
    private Vector2 lookdirection = new Vector2(1,0);
    public GameObject projectileprefab;
    private AudioSource audiosource;
    public AudioClip throwsound;
    public ParticleSystem healthparticle;
    public ParticleSystem hurtparticle;
    private int myscore;
    public GameObject winmessage;
    public GameObject losemessage;
    public GameObject bgm;
    public Text score;
    public GameObject winmusic;
    public GameObject losemusic;
    private SpriteRenderer spritedisable;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currenthealth = maxhealth;
        audiosource = GetComponent<AudioSource>();
        spritedisable = GetComponent<SpriteRenderer>();
        youwin = false;
        youlose = false;
        winmessage.SetActive(false);
        losemessage.SetActive(false);
        score.text = "Score: " + myscore.ToString();
        losemusic.SetActive(false);
        winmusic.SetActive(false);
       
        // currenthealth = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!youlose && !youwin) {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector2 move = new Vector2(horizontal, vertical);

            if (!Mathf.Approximately(move.x,0.0f) || !Mathf.Approximately(move.y,0.0f)) {
                lookdirection.Set(move.x,move.y);
                lookdirection.Normalize();

            }

            animator.SetFloat("Look X", lookdirection.x);
            animator.SetFloat("Look Y", lookdirection.y);
            animator.SetFloat("Speed", move.magnitude);

            if (isinvincible) {
                invincibletimer -= Time.deltaTime;
                if (invincibletimer < 0) {
                    isinvincible = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.C)) {
                Launch();
                audiosource.PlayOneShot(throwsound);
            }

            if (Input.GetKeyDown(KeyCode.X)) {
                RaycastHit2D hit = Physics2D.Raycast(rb2d.position + Vector2.up * 0.2f, lookdirection, 1.5f, LayerMask.GetMask("NPC"));
                if (hit.collider != null)
                {
                    NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                    if (character != null) {
                        character.DisplayDialogue();
                    }
                }
            }

        }
        if (youlose) {
            // audiosource.PlayOneShot(losemusic);
            losemusic.SetActive(true);
            //gameObject.SetActive(false);
            Destroy(spritedisable);
            if (Input.GetKeyDown(KeyCode.R)) {
                losemessage.SetActive(false);
                youlose = false;
                speed = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (youwin) {
            // audiosource.PlayOneShot(winmusic);
            winmusic.SetActive(true);
            //gameObject.SetActive(false);
            Destroy(spritedisable);
            if (Input.GetKeyDown(KeyCode.R)) {
                losemessage.SetActive(false);
                youwin = false;
                speed = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        //For winning
        if (myscore >= 6) {
            youwin = true;
            winmessage.SetActive(true);
            bgm.SetActive(false);
        }

        //For losing
        if (currenthealth <= 0) {
            youlose = true;
            losemessage.SetActive(true);
            bgm.SetActive(false);

        }
        
    }

    void FixedUpdate() {
        Vector2 position = rb2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rb2d.MovePosition(position);

    }

    public void ChangeHealth(int amount) {
        if (amount < 0) {
            animator.SetTrigger("Hit");
            
            if (!isinvincible) {
                Instantiate(hurtparticle, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);
            }
            if (isinvincible) {
                return;

            }
            isinvincible = true;
            invincibletimer = timeinvincible;
            
        }
        if (amount > 0) {
            Instantiate(healthparticle, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);

        }
        
        currenthealth = Mathf.Clamp(currenthealth + amount, 0, maxhealth);
        UIHealthBar.instance.SetValue(currenthealth/(float)maxhealth);

        // Debug.Log(currenthealth + "/" + maxhealth);
    }

    public void ChangeScore() {
            myscore += 1;
            score.text = "Score: " + myscore.ToString();
    }

    void Launch()
    {
        GameObject projectileobject = Instantiate(projectileprefab, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileobject.GetComponent<Projectile>();
        projectile.Launch(lookdirection, 300);

        animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip) {
        audiosource.PlayOneShot(clip);

    }

    /*
    void OnCollisionEnter2D (Collision2D other) {
        HealthCollectible healththingy = other.gameObject.GetComponent<HealthCollectible>();
        if(healththingy != null) {
            healthparticle.Play();
        }
    } 
    */

    
}
