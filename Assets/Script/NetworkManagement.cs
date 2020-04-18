using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.AI;

public class NetworkManagement : MonoBehaviour
{
    private SocketIOComponent socket;
    public bool isAdmin;
    // Start is called before the first frame update
    void Start()
    {
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();

        socket.On("instantiateZombieEmit", NetworkInstantiateZombieNotAdmin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NetworkInstantiateZombie(GameObject prefab, int index)
    {
        GameObject newZombie = NetworkInstantitate(prefab, index, "instantiateZombie");
        newZombie.GetComponent<NavMeshAgent>().Warp(prefab.transform.position);
        newZombie.GetComponent<NetworkIdentityZombie>().index = index;
    }

    public void NetworkInstantiate(GameObject prefab, int index)
    {
        GameObject exit = NetworkInstantitate(prefab, index, "instantiateExit");
        exit.transform.position = prefab.transform.position;
    }

    public GameObject NetworkInstantitate(GameObject prefab, int index , string socketRequest)
    {
        JSONObject jsonPosition = new JSONObject();
        jsonPosition.AddField("name", prefab.name);
        jsonPosition.AddField("x", prefab.transform.position.x.ToString());
        jsonPosition.AddField("y", prefab.transform.position.y.ToString());
        jsonPosition.AddField("z", prefab.transform.position.z.ToString());
        jsonPosition.AddField("rx", prefab.transform.eulerAngles.x.ToString());
        jsonPosition.AddField("ry", prefab.transform.eulerAngles.y.ToString());
        jsonPosition.AddField("rz", prefab.transform.eulerAngles.z.ToString());
        jsonPosition.AddField("index", index);

        //"instantiateZombie"
        socket.Emit( socketRequest, jsonPosition);

        return Instantiate(prefab);
    }

   

    public void NetworkInstantiateZombieNotAdmin(SocketIOEvent e)
    {


        string zombieString = e.data.GetField("zombie").GetField("name").ToString();
        string indexString = e.data.GetField("zombie").GetField("index").ToString();
        indexString = indexString.Replace("\"", "");
        int index = int.Parse(indexString);


        zombieString = zombieString.Replace("\"", "");
        GameObject zombieObject = Resources.Load(zombieString) as GameObject;
        string xs = e.data.GetField("zombie").GetField("position").GetField("x").ToString();
        string ys = e.data.GetField("zombie").GetField("position").GetField("y").ToString();
        string zs = e.data.GetField("zombie").GetField("position").GetField("z").ToString();
        xs = xs.Replace("\"", "");
        ys = ys.Replace("\"", "");
        zs = zs.Replace("\"", "");
        float x = float.Parse(xs);
        float y = float.Parse(ys);
        float z = float.Parse(zs);

        Debug.Log(e.data.GetField("zombie"));
        string xrs = e.data.GetField("zombie").GetField("rotation").GetField("x").ToString();
        string yrs = e.data.GetField("zombie").GetField("rotation").GetField("y").ToString();
        string zrs = e.data.GetField("zombie").GetField("rotation").GetField("z").ToString();
        xrs = xrs.Replace("\"", "");
        yrs = yrs.Replace("\"", "");
        zrs = zrs.Replace("\"", "");
        float xr = float.Parse(xrs);
        float yr = float.Parse(yrs);
        float zr = float.Parse(zrs);

        zombieObject  = Instantiate(zombieObject);
        //zombieObject.transform.position = new Vector3(x, y, z);
        zombieObject.GetComponent<NavMeshAgent>().Warp(new Vector3(x, y, z));

        Quaternion angle = Quaternion.identity;
        angle.eulerAngles = new Vector3(xr, yr, zr);
        zombieObject.transform.rotation = angle;
        zombieObject.GetComponent<NetworkIdentityZombie>().index = index;


    }

}
