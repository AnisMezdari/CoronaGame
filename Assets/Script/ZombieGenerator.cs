using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject zombiePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newZombie = GameObject.Instantiate(zombiePrefab);
        newZombie.GetComponent<Zombie>().target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
