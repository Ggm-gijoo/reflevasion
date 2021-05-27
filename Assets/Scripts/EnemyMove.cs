using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private GameManager gameManager = null;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < gameManager.MinPosition.x - 4)
        {
            Destroy(gameObject);
        }
    }
}
