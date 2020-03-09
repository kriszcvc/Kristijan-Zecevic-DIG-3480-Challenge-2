using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;
    private int healthValue = 3;

    public Text winlose;

    public Text health;

    public GameObject player;

    public AudioSource musicSource;
    public AudioClip musicClip1;
    public AudioClip musicClip2;

    private bool facingRight = true;

    private int state;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        health.text = healthValue.ToString();
        winlose.text = "";
        musicSource.clip = musicClip1;
        musicSource.Play();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (scoreValue == 9)
        {
            musicSource.clip = musicClip2;
            musicSource.Play();
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.W))
        {
            state = 2;
            anim.SetInteger("State", 2);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            state = 2;
            anim.SetInteger("State", 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 5)
            {
                transform.position = new Vector2(-55.0f, 1.0f);
                healthValue = 3;
                health.text = healthValue.ToString();
            }

            if (scoreValue == 9)
            {
                winlose.text = "You Win! Game created by Kristijan Zecevic";
            }
        }

        if (collision.collider.tag == "Enemy")
        {

            Destroy(collision.collider.gameObject);

            if (scoreValue < 9)
            {
                healthValue -= 1;
                health.text = healthValue.ToString();
            }

            if (healthValue == 0)
            {
                Destroy(player);
                winlose.text = "You Lose!";
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
           if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }

           if (Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }

           if (state == 2)
            {
                anim.SetInteger("State", 0);
                state = 0;
            }

           if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                anim.SetInteger("State", 1);
            }

           if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                anim.SetInteger("State", 0);
            }

           if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                anim.SetInteger("State", 1);
            }

           if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                anim.SetInteger("State", 0);
            }

           if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetInteger("State", 1);
            }

           if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetInteger("State", 0);
            }

           if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetInteger("State", 1);
            }

           if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetInteger("State", 0);
            }

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}