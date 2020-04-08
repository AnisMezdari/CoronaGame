using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject zombiePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newZombie = GameObject.Instantiate(zombiePrefab) ;
        //newZombie.transform.position = this.transform.position;
        newZombie.GetComponent<NavMeshAgent>().Warp(this.transform.position);
        //newZombie.transform.localPosition = new Vector3(0, 0, 0);
        //newZombie.GetComponent<Zombie>().target = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
