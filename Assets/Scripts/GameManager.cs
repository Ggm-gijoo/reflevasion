﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    [SerializeField]
    public int life = 3;
    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int highscore = 0;
    [SerializeField]
    private Text textLife = null;
    [SerializeField]
    private Text textScore = null;
    [SerializeField]
    private Text textHighscore = null;
    [SerializeField]
    private GameObject enemyPrefab = null;

    void Start()
    {
        MinPosition = new Vector2(-3f, -1.5f);
        MaxPosition = new Vector2(3f, 1.5f);
        UpdateUI();
        highscore = PlayerPrefs.GetInt("BEST, 0");
        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float randomSpawn = Random.Range(3f, 5f);
            float randomY = Random.Range(3f, -1.5f);
            float randomDelay = Random.Range(0, 1.5f);

            for (int i = 0; i < randomSpawn; i++)
            {
                GameObject enemy = null;
                enemy = Instantiate(enemyPrefab, new Vector2(10f, randomY), Quaternion.identity);
                enemy.transform.SetParent(null);
                yield return new WaitForSeconds(0.33f);
            }
            yield return new WaitForSeconds(1f);
            
        }
    }

    public void Dead()
    {
        life--;
        if (life <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        UpdateUI();
    }
    
    public void Add(int addscore)
    {
        score += addscore;
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.GetInt("HIGHSCORE", highscore);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        textScore.text = string.Format("SCORE : {0}", score);
        textLife.text = string.Format("LIFE : {0}", life);
        textHighscore.text = string.Format("BEST : {0}", highscore);
    }
}
