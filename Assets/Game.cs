using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        PlaceAllPlayerInSpot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceAllPlayerInSpot()
    {
        GameObject listSpot = GameObject.Find("ListSpotPlayer");
        foreach (GameObject player in players) {

            int randomInt = Random.Range(0, listSpot.transform.childCount-1);
            GameObject randomChild = listSpot.transform.GetChild(randomInt).gameObject;
            player.transform.position = randomChild.transform.position;

        }
    }
}
