using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControllerC : MonoBehaviour
{

    
    public int vitesse;
    public int vitesseRotation = 1;

    private bool isMoving = false;
    private bool isRunning = false;
    private bool isSquatting = false;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Corona.Player>().isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.Z) && !isSquatting || Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.LeftAlt))
            {
                this.transform.position += this.transform.forward * Time.deltaTime * vitesse;
                if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.LeftAlt))
                {
                    this.GetComponent<Animator>().SetBool("crouching", true);
                    this.GetComponent<Animator>().SetBool("running", false);
                    this.GetComponent<Animator>().SetBool("squatting", false);
                }
                else
                {
                    this.GetComponent<Animator>().SetBool("running", true);
                    this.GetComponent<Animator>().SetBool("crouching", false);
                    this.GetComponent<Animator>().SetBool("squatting", false);
                }
            }
            else
            {
                this.GetComponent<Animator>().SetBool("running", false);
                this.GetComponent<Animator>().SetBool("crouching", false);

                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    this.GetComponent<Animator>().SetBool("squatting", true);
                    isSquatting = true;
                }
                else
                {
                    this.GetComponent<Animator>().SetBool("squatting", false);
                    isSquatting = false;
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                //this.transform.position += new Vector3(1 * Time.deltaTime * vitesse, 0, 0);
                transform.Rotate(0, 1 * Time.deltaTime * vitesseRotation,0); ;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                //this.transform.position += new Vector3(-1 * Time.deltaTime * vitesse, 0, 0);
                transform.Rotate(0, -1 * Time.deltaTime * vitesseRotation,0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                this.transform.position += new Vector3(0, 0, -1 * Time.deltaTime * vitesse);
            }        
        }
    }


}
