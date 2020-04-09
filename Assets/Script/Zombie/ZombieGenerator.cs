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
        newZombie.GetComponent<NavMeshAgent>().Warp(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
