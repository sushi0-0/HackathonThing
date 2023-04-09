using enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class PlayerAttack : MonoBehaviour
    {
        private GameObject attackArea = default;
        public bool attacking = false;
        public GameObject enemy = null;
        private float timeToAttack = 0.25f;
        private float timer = 0f;
        public float badHP;
        public float maxBadHP = 3;
        public bool lookingAtEnemy = false;
       

        // Start is called before the first frame update
        void Start()
        {
            attackArea = transform.GetChild(0).gameObject;
            badHP = maxBadHP;
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
            }

            if (attacking)
            {
                timer += Time.deltaTime;

                if (timer >= timeToAttack)
                {
                    timer = 0;
                    attacking = false;
                    attackArea.SetActive(attacking);
                }
            }
            if(badHP <= 0)
            {

                Destroy(enemy);
                enemy = null;
               
            }

            if (enemy == null)
            {
                badHP = maxBadHP;
            }

            if (lookingAtEnemy == false)
            {
                badHP= maxBadHP;
            }

            

        }
        private void Attack()
        {
            attacking = true;
            attackArea.SetActive(attacking);
            badHP--;
            Debug.Log("Hit" + badHP);
            
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                lookingAtEnemy = true;
                enemy = other.gameObject;
                Debug.Log("Enemy Detected");

            }
            

            
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                
                lookingAtEnemy = false;
                Debug.Log("Enemy Gone");
                

            }
        }
    
       
    }
}
