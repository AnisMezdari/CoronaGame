using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Moving : MonoBehaviour
{

    public SocketIOComponent socket;
    public int vitesse = 2;
    public int vitesseRotation = 1;

    private bool isMoving = false;
    private bool isRunning = false;
    private bool isSquatting = false;
    // Start is called before the first frame update
    void Start()
    {
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
        if (this.GetComponent<Corona.Player>().isLocalPlayer)
        {
            InvokeRepeating("SharePosition", 0.1f, 0.01f);
        }
        socket.On("positionEmit", SyncronizePositionPlayer);
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
                    vitesse = 1;
                    this.GetComponent<Animator>().SetBool("crouching", true);
                    this.GetComponent<Animator>().SetBool("running", false);
                    this.GetComponent<Animator>().SetBool("squatting", false);
                }
                else
                {
                    vitesse = 4;
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

    public void SharePosition()
    {
        JSONObject jsonPosition = new JSONObject();
        jsonPosition.AddField("x", this.transform.position.x.ToString());
        jsonPosition.AddField("y", this.transform.position.y.ToString());
        jsonPosition.AddField("z", this.transform.position.z.ToString());
        jsonPosition.AddField("index", this.GetComponent<Corona.Player>().index);
        socket.Emit("position", jsonPosition);
    }

    public void SyncronizePositionPlayer(SocketIOEvent e)
    {
        if (this.GetComponent<Corona.Player>().isLocalPlayer == false)
        {
            if (int.Parse(e.data.GetField("player").GetField("index").ToString()) == this.GetComponent<Corona.Player>().index)
            {
                string xs = e.data.GetField("player").GetField("position").GetField("x").ToString();
                string ys = e.data.GetField("player").GetField("position").GetField("y").ToString();
                string zs = e.data.GetField("player").GetField("position").GetField("z").ToString();
                xs = xs.Replace("\"", "");
                ys = ys.Replace("\"", "");
                zs = zs.Replace("\"", "");
                float x = float.Parse(xs);
                float y =  float.Parse(ys);
                float z = float.Parse(zs);
                this.GetComponent<Corona.Player>().transform.position = new Vector3(x, y, z);
            }
        }
    }
}
