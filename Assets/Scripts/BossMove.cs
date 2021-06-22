using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    private int hp = 10;
    [SerializeField]
    private int score = 30000;

    private GameManager gameManager = null;
    private Collider2D col = null;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = FindObjectOfType<Collider2D>();
    }

    void Update()
    {
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
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
