using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 10f;

    private GameManager gameManager = null;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    void Update()
    {


        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.localPosition.x < gameManager.MinPosition.x - 5f)
        {
            Despawn();
        }
    }

    public void Despawn()
    {
        transform.SetParent(gameManager.poolManager.transform, false);
        gameObject.SetActive(false);
    }
}
