using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 10f;

    private GameManager gameManager = null;
    private CameraShake cameraShake;
    private AudioSource audioSource;
    private bool reflect = false;
    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        cameraShake = GetComponent<CameraShake>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        if (reflect == true)
        {
            transform.Translate(Vector2.right * speed * 2.5f * Time.deltaTime);
        }

        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (transform.localPosition.x < gameManager.MinPosition.x - 5f)
        {
            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Bitting"))
        {
            cameraShake.Shake();
            gameObject.tag = "Bitted";
            reflect = true;
        }
    }

    public void Despawn()
    {
        transform.SetParent(gameManager.poolManager.transform, false);
        gameObject.SetActive(false);
    }
}
