using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkIdentityPlayer : MonoBehaviour
{
    private SocketIOComponent socket;
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
                float y = float.Parse(ys);
                float z = float.Parse(zs);


                string xrs = e.data.GetField("player").GetField("rotation").GetField("x").ToString();
                string yrs = e.data.GetField("player").GetField("rotation").GetField("y").ToString();
                string zrs = e.data.GetField("player").GetField("rotation").GetField("z").ToString();
                xrs = xrs.Replace("\"", "");
                yrs = yrs.Replace("\"", "");
                zrs = zrs.Replace("\"", "");
                float xr = float.Parse(xrs);
                float yr = float.Parse(yrs);
                float zr = float.Parse(zrs);

                this.transform.position = new Vector3(x, y, z);
                Quaternion angle = Quaternion.identity;
                angle.eulerAngles = new Vector3(xr, yr, zr);
                this.transform.rotation = angle;
            }
        }
    }
}
