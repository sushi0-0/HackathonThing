using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;

    bool isFacingRight;
    public PlayerAttack fite;

    Rigidbody2D myRigidbody;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        fite = GetComponent<PlayerAttack>();
    }
   
    void Update()
    {
        if(IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0.0f);
        }else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }
    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
        }
        if(other.gameObject.tag == null)
        {
            moveSpeed = 5.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Owie"))
        {
            moveSpeed = 2.5f;
        }
    }

   
}