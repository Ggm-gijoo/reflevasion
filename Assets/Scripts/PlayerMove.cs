using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector2 targetPosition = Vector2.zero;

    private GameManager gameManager = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer;

    

    Vector3 vec;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }


    void Update()
    {
        vec = new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, transform.position.z);
        Vector3 vecplus = new Vector3(transform.position.x + vec.x , transform.position.y + vec.y, transform.position.z);

        vecplus.x = Mathf.Clamp(vecplus.x, -8, 8);
        vecplus.y = Mathf.Clamp(vecplus.y, -5, 5);

        transform.position = vecplus;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            StartCoroutine(Reflect());
        }
    }

    
    
    
    private IEnumerator Reflect()
    {
        
        animator.Play("Bit");
        yield return new WaitForSeconds(0.5f);
        
    }
}
