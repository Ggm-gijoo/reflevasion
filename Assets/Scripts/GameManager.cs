using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    [SerializeField]
    private GameObject enemyPrefab = null;

    void Start()
    {
        MinPosition = new Vector2(-3f, -1.5f);
        MaxPosition = new Vector2(3f, 1.5f);

        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float randomY = Random.Range(3f, -1.5f);
            float randomDelay = Random.Range(0, 1.5f);

            for (int i = 0; i < 1; i++)
            {
                GameObject enemy = null;
                enemy = Instantiate(enemyPrefab, new Vector2(10f, randomY), Quaternion.identity);
                enemy.transform.SetParent(null);
                yield return new WaitForSeconds(1f);
            }

            
        }
    }


    void Update()
    {
        
    }
}
