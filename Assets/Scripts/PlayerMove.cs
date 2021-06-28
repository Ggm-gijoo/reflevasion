using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector2 targetPosition = Vector2.zero;

    private GameManager gameManager = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer;
    private Touch touch;

    private bool isDamaged = false;
    private bool isDead = false;



    Vector3 vec;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        touch = GetComponent<Touch>();
    }


    void Update()
    {
        vec = new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, transform.position.z);
        Vector3 vecplus = new Vector3(transform.position.x + vec.x, transform.position.y + vec.y, transform.position.z);

        vecplus.x = Mathf.Clamp(vecplus.x, -8, 8);
        vecplus.y = Mathf.Clamp(vecplus.y, -5, 5);

        transform.position = vecplus;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ref();
        }

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(i) == false)
                {
                    touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Began)
                    {
                        {
                            targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            targetPosition.x = Mathf.Clamp(targetPosition.x, gameManager.MinPosition.x - 2f, gameManager.MaxPosition.x + 5f);
                            targetPosition.y = Mathf.Clamp(targetPosition.y, gameManager.MinPosition.y - 2f, gameManager.MaxPosition.y + 2f);
                            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;
        StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        if (!isDamaged)
        {
            gameManager.Dead();
            isDamaged = true;

            for (int i = 0; i < 5; i++)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
        spriteRenderer.enabled = true;
        isDamaged = false;
    }

    public void Ref()
    {
        isDamaged = true;
        StartCoroutine(Reflect());
    }

    private IEnumerator Reflect()
    {
        
        animator.Play("Bit");
        gameObject.tag = "Bitting";
        yield return new WaitForSeconds(1f);
        isDamaged = false;
        gameObject.tag = "Player";
       
    }


}
