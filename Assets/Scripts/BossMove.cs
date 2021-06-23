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
    private float speed = 10f;

    private GameManager gameManager = null;
    private Collider2D col = null;
    [SerializeField]
    private Transform bulletPosition = null;
    [SerializeField]
    private GameObject bulletPrefab = null;
    private Vector2 targetPosition = Vector2.zero;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = FindObjectOfType<Collider2D>();
        StartCoroutine(Fire());
        StartCoroutine(Moving());
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

    private IEnumerator Moving()
    {
        float randomMove = Random.Range(3f, -3f);
        transform.Translate(Vector2.up* speed * randomMove * Time.deltaTime);
        transform.localPosition =Vector2.MoveTowards(transform.localPosition,targetPosition, speed * Time.deltaTime);
        targetPosition.x = Mathf.Clamp(targetPosition.x, gameManager.MinPosition.x - 5f, gameManager.MaxPosition.x + 5f);
        targetPosition.y = Mathf.Clamp(targetPosition.y, gameManager.MinPosition.y - 2f, gameManager.MaxPosition.y + 2f);
        yield return new WaitForSeconds(1f);

    }

    private IEnumerator Fire()
    {
        while (true)
        {
            SpawnOrInstantiate();
            yield return new WaitForSeconds(4f);
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

    private IEnumerator Dead()
    {
        col.enabled = true;

        gameManager.Add(score);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
