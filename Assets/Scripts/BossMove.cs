using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private int hp = 10;
    [SerializeField]
    private int score = 30000;

    private SpriteRenderer spriteRenderer = null;
    private GameManager gameManager = null;
    private Collider2D col = null;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = FindObjectOfType<Collider2D>();
        spriteRenderer = FindObjectOfType<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bitted"))
        {
            hp--;
            if (hp <= 0)
            {
                StartCoroutine(Dead());
            }
        }
    }

    private IEnumerator Dead()
    {
        col.enabled = true;

        gameManager.Add(score);
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
