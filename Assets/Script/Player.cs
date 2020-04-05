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

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);

        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}