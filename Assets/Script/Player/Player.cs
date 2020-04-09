using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Corona
{
    public class Player : MonoBehaviour
    {

        public int index;
        public string name;
        public bool isLocalPlayer;
        public bool isAdmin;
        public bool isDead;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            if (!isLocalPlayer)
            {
                this.transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(0).gameObject.SetActive(false);
            }

        }

        // Update is called once per frame
        void Update()
        {
            Die();
            
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.CompareTag("Zombie"))
            {
                isDead = true;
            }
        }

        private void Die()
        {
            if (isDead)
            {
                this.GetComponent<Animator>().SetBool("dying", true);
            }
        }

    }
}