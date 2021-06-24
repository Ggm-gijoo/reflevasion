using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    private int hp = 10;
    [SerializeField]
    private int score = 30000;
    [SerializeField]
    private float speed = 3f;

    private GameManager gameManager = null;
    private Collider2D col = null;
    [SerializeField]
    private Transform bulletPosition = null;
    [SerializeField]
    private GameObject bulletPrefab = null;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = FindObjectOfType<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(Fire());
    }

    void Update()
    {
        Shield();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bitted"))
        {
            StartCoroutine(Damaged());
            Destroy(collision.gameObject);
            if (hp <= 0)
            {
                StartCoroutine(Dead());
            }
        }
    }
    
    void Shield()
    {
        transform.localPosition = ClampPosition(transform.localPosition);
    }

    private IEnumerator Moving()
    {
       
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Vector2 randomMove = new Vector2(0f, Random.Range(3f, -3f));
            for (int i = 0; i < 10; i++)
            {
                transform.Translate(randomMove * (speed));
                yield return new WaitForSeconds(0.02f);
            }
        }
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            SpawnOrInstantiate();
            StartCoroutine (Moving());
        }
    }

    private void SpawnOrInstantiate()
    {
        GameObject bullet;

        if (gameManager.poolManager.transform.childCount > 0)
        {
            bullet = gameManager.poolManager.transform.GetChild(0).gameObject;
            bullet.transform.SetParent(bulletPosition, false);
            bullet.transform.position = bulletPosition.position;
            bullet.SetActive(true);

        }
        else
        {
            bullet = Instantiate(bulletPrefab, bulletPosition);
        }
        if (bullet != null)
        {
            bullet.transform.SetParent(null);
        }


        bullet.transform.SetParent(null);
    }
    private IEnumerator Damaged()
    {
        hp--;
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Dead()
    {
        col.enabled = true;
        animator.Play("Explosion");
        gameManager.Add(score);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }


    public Vector2 ClampPosition(Vector2 position)
    {
        {
            return new Vector2
            (
                8f, Mathf.Clamp(transform.localPosition.y, gameManager.MinPosition.y-2f , gameManager.MaxPosition.y+2f)
            );
        }
    }
}
