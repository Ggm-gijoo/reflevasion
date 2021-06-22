using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private int hp = 1;
    [SerializeField]
    private int score = 100;
    private GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    private Collider2D col = null;
    private SpriteRenderer rend;
    private CameraShake cameraShake;
    AudioSource[] audioSource;



    private bool isDamaged = false;
    private bool isDead = false;
    private bool reflect = false;
    private bool isBitted = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = FindObjectOfType<Collider2D>();
        spriteRenderer = FindObjectOfType<SpriteRenderer>();
        rend = FindObjectOfType<SpriteRenderer>();
        cameraShake = GetComponent<CameraShake>();
        audioSource = GetComponents<AudioSource>();
    }

    
    private void Update()
    {
        if(reflect == true)
        {
            transform.Translate(Vector2.right * speed * 2.5f * Time.deltaTime);
            
        }
        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (isBitted == true)
        {
            gameObject.tag = "Bitted";
        }

        if (transform.position.x < gameManager.MinPosition.x - 7)
        {
            Destroy(gameObject);
            
        }

        else if (transform.position.x > gameManager.MaxPosition.x + 10)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Bitting"))
        {
            if (!Slowed) StartCoroutine(TimeSlow());
            audioSource[0].Play();
            cameraShake.Shake();
            reflect = true;
            isBitted = true;
            rend.flipX = true;
            gameManager.Add(score);
        }
        if (collision.CompareTag("Bitted"))
        {
            Time.timeScale = 1f;
            hp--;
            if (hp <= 0)
            {
                StartCoroutine(Dead());
            }
        }


    }

    bool Slowed = false;
    private IEnumerator TimeSlow()
    {
        if (Slowed) yield break;
        Slowed = true;
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.07f);
        Time.timeScale = 1.0f;
        Slowed = false;
    }

    private IEnumerator Dead()
        {
            audioSource[1].Play();
            col.enabled = true;
            isDead = true;
            gameManager.Add(score);
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
