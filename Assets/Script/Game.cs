using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    GameObject[] players;

    public GameObject exit;
    public GameObject zombie;
    public int numberOfZombie = 5;
    private NetworkManagement network;
    private bool isPassed = false;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        network = this.GetComponent<NetworkManagement>();
        StartCoroutine(SpawnAllPlayerInSpot());
        //SpawnExit();
        //
        Debug.Log(this.GetComponent<NetworkManagement>().isAdmin);


    }


    // Update is called once per frame
    void Update()
    {
        if (network.isAdmin && !isPassed)
        {
            SpawnZombie(numberOfZombie);
            isPassed = true;
        }
    }

    public IEnumerator SpawnAllPlayerInSpot()
    {
        GameObject listSpot = GameObject.Find("ListSpotPlayer");
        yield return new WaitForSeconds(1f);
        foreach (GameObject player in players)
        {
            int randomInt = Random.Range(0, listSpot.transform.childCount - 1);
            GameObject randomChild = listSpot.transform.GetChild(randomInt).gameObject;
            player.transform.position = randomChild.transform.position;
            player.GetComponent<Rigidbody>().useGravity = true;

        }
    }

    public void SpawnExit()
    {
        GameObject listSpot = GameObject.Find("ListSpotExit");
        int randomInt = Random.Range(0, listSpot.transform.childCount - 1);
        GameObject randomChild = listSpot.transform.GetChild(randomInt).gameObject;
        GameObject exitPre = Instantiate(exit);
        exitPre.transform.position = randomChild.transform.position;
    }

    public void SpawnZombie(int number)
    {
        GameObject listSpot = GameObject.Find("Spots");
        if (number > listSpot.transform.childCount)
        {
            number = listSpot.transform.childCount;
        }
        for(int i =0; i < number; i++)
        {
            int randomInt = Random.Range(0, listSpot.transform.childCount - 1);
            GameObject randomChild = listSpot.transform.GetChild(randomInt).gameObject;
            zombie.transform.position = randomChild.transform.position;
            Destroy(listSpot.transform.GetChild(i).gameObject);
            this.GetComponent<NetworkManagement>().NetworkInstantiateZombie(zombie, i);

        }
    }

    public void SpawnExit(int number)
    {
        GameObject listSpot = GameObject.Find("SpotExit");
        if (number > listSpot.transform.childCount)
        {
            number = listSpot.transform.childCount;
        }

        for(int i =0; i< number; i++)
        {
            int randomInt = Random.Range(0, listSpot.transform.childCount - 1);
            GameObject randomChild = listSpot.transform.GetChild(randomInt).gameObject;

        }
    }
        

}
