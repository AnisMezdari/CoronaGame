using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    public bool errant;
    public GameObject player;

    private Vector3 lastPos; 

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //this.transform.localPosition = new Vector3(0, 0, 0);
        errant = true;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        if (!errant)
        {
            ChargePlayer();
            ReErrant();
        }
        if (IsMoving())
        {
            this.GetComponent<Animator>().SetBool("moving", true);
        }

        this.GetComponent<Animator>().SetBool("chasing", !errant);
    }

    private void ChargePlayer()
    {
        target = player.transform;
        agent.speed =  4;
    }

    private void ReErrant()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > 30)
        {
            errant = true;
            setTarget(this.transform);
        }
    }

    public void setTarget(Transform target)
    {
        this.target = target;
    }

    public bool IsArrivedOfTarget()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if(dist < 5)
        {
            return true;
        }
        return false;
    }

    public bool IsMoving()
    {
        Vector3 curPos = this.transform.position;
        if (curPos  == lastPos)
        {
            this.GetComponent<Animator>().SetBool("moving", false);
            return false;
        }
        lastPos = curPos;
        return true;
    }
}
