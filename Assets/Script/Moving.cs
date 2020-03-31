using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Moving : MonoBehaviour
{

    public SocketIOComponent socket;
    public int vitesse = 1;
    // Start is called before the first frame update
    void Start()
    {
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();

        if (this.GetComponent<Corona.Player>().isLocalPlayer)
        {
            InvokeRepeating("SharePosition", 0.1f, 0.05f);
        }


        socket.On("positionEmit", SyncronizePositionPlayer);



    }

    // Update is called once per frame
    void Update()
    {

        if (this.GetComponent<Corona.Player>().isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                this.transform.position += new Vector3(0, 0, 1 * Time.deltaTime * vitesse);
            }

            if (Input.GetKey(KeyCode.D))
            {
                this.transform.position += new Vector3(1 * Time.deltaTime * vitesse, 0, 0);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                this.transform.position += new Vector3(-1 * Time.deltaTime * vitesse, 0, 0);
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
                Debug.Log(e.data.GetField("player"));

                string xs = e.data.GetField("player").GetField("position").GetField("x").ToString();
                string ys = e.data.GetField("player").GetField("position").GetField("y").ToString();
                string zs = e.data.GetField("player").GetField("position").GetField("z").ToString();

                /*xs = xs.Split(char.Parse(","))[0];
                ys = ys.Split(char.Parse(","))[0];
                zs = zs.Split(char.Parse(","))[0];*/

                xs = xs.Replace("\"", "");
                ys = ys.Replace("\"", "");
                zs = zs.Replace("\"", "");

                Debug.Log("'"+xs + "' '" + ys + "' '" + zs + "'");

                float x = float.Parse(xs);
                Debug.Log("sa passe  1 ");
                float y =  float.Parse(ys);
                Debug.Log("sa passe  2 ");
                float z = float.Parse(zs);
                Debug.Log("sa passe  3 " + x + " " + y + " " + z);
                Debug.Log("sa passe  4 ");

                Debug.Log(x + " " + y +  "  " + z);
                this.GetComponent<Corona.Player>().transform.position = new Vector3(x, y, z);
            }
        }
    }
}
