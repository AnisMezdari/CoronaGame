using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class GameManager : MonoBehaviour
{

    public SocketIOComponent socket;

    // Start is called before the first frame update
    void Start()
    {
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
            j.AddField("namePlayer", "anis");
            j.AddField("nameServer", "onestla");
            socket.Emit("create", j);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
            j.AddField("namePlayer", "Bedis");
            j.AddField("nameServer", "onestla");
            socket.Emit("join", j);
        }
    }
}

