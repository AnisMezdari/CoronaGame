using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NetworkIdentityZombie : MonoBehaviour
{
    private SocketIOComponent socket;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
        GameObject game = GameObject.Find("Game");
        if (game.GetComponent<NetworkManagement>().isAdmin)
        {
            InvokeRepeating("SharePosition", 0.1f, 0.01f);
        }
        socket.On("positionZombieEmit", SyncronizePositionZombie);
    }

    // Update is called once per frame
    void Update()
    { 

       

    }

    public void SharePosition()
    {
        JSONObject jsonPosition = new JSONObject();
        jsonPosition.AddField("x", this.transform.position.x.ToString());
        jsonPosition.AddField("y", this.transform.position.y.ToString());
        jsonPosition.AddField("z", this.transform.position.z.ToString());
        jsonPosition.AddField("rx", this.transform.eulerAngles.x.ToString());
        jsonPosition.AddField("ry", this.transform.eulerAngles.y.ToString());
        jsonPosition.AddField("rz", this.transform.eulerAngles.z.ToString());
        jsonPosition.AddField("index", this.index);
        socket.Emit("positionZombie", jsonPosition);
    }

    public void SyncronizePositionZombie(SocketIOEvent e)
    {

        string indexString = e.data.GetField("zombie").GetField("index").ToString();
        indexString = indexString.Replace("\"", "");

        if(this.index != int.Parse(indexString))
        {
            return;
        }
        string xs = e.data.GetField("zombie").GetField("position").GetField("x").ToString();
        string ys = e.data.GetField("zombie").GetField("position").GetField("y").ToString();
        string zs = e.data.GetField("zombie").GetField("position").GetField("z").ToString();
        xs = xs.Replace("\"", "");
        ys = ys.Replace("\"", "");
        zs = zs.Replace("\"", "");
        float x = float.Parse(xs);
        float y = float.Parse(ys);
        float z = float.Parse(zs);


        string xrs = e.data.GetField("zombie").GetField("rotation").GetField("x").ToString();
        string yrs = e.data.GetField("zombie").GetField("rotation").GetField("y").ToString();
        string zrs = e.data.GetField("zombie").GetField("rotation").GetField("z").ToString();

        xrs = xrs.Replace("\"", "");
        yrs = yrs.Replace("\"", "");
        zrs = zrs.Replace("\"", "");

        float xr = float.Parse(xrs);
        float yr = float.Parse(yrs);
        float zr = float.Parse(zrs);
        //zombieObject.transform.position = new Vector3(x, y, z);
        this.GetComponent<NavMeshAgent>().Warp(new Vector3(x, y, z));

        Quaternion angle = Quaternion.identity;
        angle.eulerAngles = new Vector3(xr, yr, zr);
        this.transform.rotation = angle;

    }
}
