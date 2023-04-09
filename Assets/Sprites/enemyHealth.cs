using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;

namespace enemy
{ 
    public class enemyHealth : MonoBehaviour
    {

        [SerializeField] public float badHP = 3;
        public float maxBadHP = 3;
        public float finalbadHP; 
        public PlayerAttack PA;
        public bool inRange = false;
        public GameObject enemy;

        // Start is called before the first frame update
        void Start()
        {
            badHP = maxBadHP;
            
            
        }

        // Update is called once per frame
        void Update()
        {
            if(finalbadHP == 0)
            {
                Destroy(enemy);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
       
        }
    }
}
