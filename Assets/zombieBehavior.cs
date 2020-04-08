using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject eyes;
    private GameObject player;
    private GameObject listTarget;

    private Zombie zombie;
    private bool isArrived;

    void Start()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("CameraZombie");
        eyes = this.transform.GetChild(transform.GetChildCount() - 1).gameObject;
        player = GameObject.Find("Player");
        listTarget = GameObject.Find("ListTargetZombie");
        zombie = this.GetComponent<Zombie>();
        RandomTarget(listTarget);
        isArrived = false;
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < 30)
        {
            if (zombie.errant)
            {
                if (FieldVision(eyes.transform.position, 20f, 180, player))
                {
                    ZombieCharge();
                }

            }
           
        }
        TargetSystem();


    }


    public bool FieldVision( Vector3 initialPosition , float precision , int dimension,  GameObject target)
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
                        Debug.Log("Player vue !");
                        return true;
                    }
                }
            }
            vision.Normalize();
        }
        return false;
    }

    public void ZombieCharge()
    {
        zombie.errant = false;
    }

    public GameObject RandomTarget(GameObject listTargetParent)
    {
        int randomIndex = Random.Range(0, listTargetParent.transform.childCount - 1);
        Transform target = listTargetParent.transform.GetChild(randomIndex);
        zombie.target = target;

        return target.gameObject;

    }
    public void TargetSystem()
    {
        if (zombie.IsArrivedOfTarget() && !isArrived)
        {
            isArrived = true;
            StartCoroutine(DontMove(Random.Range(2, 10)));
        }
    }

    public IEnumerator DontMove(float seconde)
    {
        zombie.target = zombie.transform;
        yield return new WaitForSeconds(seconde);
        RandomTarget(listTarget);
        isArrived = false;
    }




}
