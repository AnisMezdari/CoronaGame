using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject eyes;
    private GameObject[] players;
    private GameObject listTarget;
    public Transform target;

    private NavMeshAgent agent;

    private GameObject playerChasing;

    public bool errant;
    public bool withoutTarget;

    void Start()
    {
        eyes = this.transform.GetChild(transform.GetChildCount() - 1).gameObject;
        agent = GetComponent<NavMeshAgent>();
        players = GameObject.FindGameObjectsWithTag("Player");
        listTarget = GameObject.Find("ListTargetZombie");
        RandomTarget();
        InvokeRepeating("FiedlVisionRepeating", 1, 0.5f);
        withoutTarget = true;
        errant = false;
    }

    // Update is called once per frame
    void Update()
    {
/*
        if (zombie.errant)
        {
            if (detectPlayer)
            {
                ZombieCharge();
            }
        }*/

        if (withoutTarget)
        {
            TargetSystem();
        }
        else
        {
            if (!errant) 
            { 
                ChargePlayer();
            }
        }
        HasLoosePlayer();
        IsArriveInTarget();
        agent.SetDestination(target.position);
    }

    public void FiedlVisionRepeating()
    {
        if (errant)
        {
            for (int i = 0; i < players.Length; i++)
            {
                float dist = Vector3.Distance(players[i].transform.position, transform.position);

                if (dist < 30)
                {

                    FieldVision(eyes.transform.position, 5f, 180, players[i]);

                }
            }
        }
        
       
    }

    public void FieldVision( Vector3 initialPosition , float precision , int dimension,  GameObject target)
    {
        Vector3 vision = new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0));
        RaycastHit hit;
        for (float i = 0; i < dimension; i += precision)
        {
            for (float j = 0; j < dimension; j += precision)
            {       
                Debug.DrawRay(initialPosition, vision * 10, Color.white);
                
                Ray landingRay = new Ray(initialPosition, vision);
                //vision = new Vector3(Mathf.Cos(j * Mathf.PI / 180), Mathf.Cos(i * Mathf.PI / 180) * Mathf.Sin(i * Mathf.PI / 180), Mathf.Sin(j * Mathf.PI / 180));

                float angle = Vector3.SignedAngle(Vector3.forward, eyes.transform.forward , Vector3.up);
               
                float X = Mathf.Sin(j * Mathf.PI / 180) * Mathf.Cos((i - angle) * Mathf.PI / 180);
                float Y = Mathf.Cos(j * Mathf.PI / 180);
                float Z = Mathf.Sin(j * Mathf.PI / 180 ) * Mathf.Sin((i - angle) * Mathf.PI / 180);

                vision = new Vector3(X, Y, Z);
                if (Physics.Raycast(landingRay, out hit, 200))
                {
                    if(target.CompareTag(hit.collider.tag))
                    {
                        errant = false;
                        playerChasing = target;
                    }
                }
                //Debug.Log("sa passe");
            }
            vision.Normalize();
        }
    }


    public void RandomTarget()
    {
       
        int randomIndex = Random.Range(0, listTarget.transform.childCount - 1);
        Transform target = listTarget.transform.GetChild(randomIndex);
        this.target = target;
    }

    public void TargetSystem()
    {
        if (withoutTarget)
        {
            StartCoroutine(DontMove(Random.Range(2, 10)));
            RandomTarget();
            withoutTarget = false;
            errant = true;
        }
    }

    public void IsArriveInTarget()
    {
        if (errant)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            if (dist < 5 )
            {
                withoutTarget = true;
                errant = false;
            }
        }
    }

    public IEnumerator DontMove(float seconde)
    {
        this.target = this.transform;
        yield return new WaitForSeconds(seconde);
        
    }

    private void ChargePlayer()
    {
        if (!playerChasing.GetComponent<Corona.Player>().isDead) 
        {
            errant = false;
            target = playerChasing.transform;
            agent.speed = 4;
        }
        else
        {
            StartCoroutine(DontMove(Random.Range(2, 10)));
            withoutTarget = true;
            agent.speed = 2;
        }

    }

    private void HasLoosePlayer()
    {
        if (!errant)
        {
            float dist = Vector3.Distance(playerChasing.transform.position, transform.position);
            if (dist > 30)
            {
                withoutTarget = true;
                agent.speed = 2;
            }
        }
        
    }




}
