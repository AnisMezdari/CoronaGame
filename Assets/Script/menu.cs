using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class menu : MonoBehaviour
{
    public GameObject canvas2;

    public SocketIOComponent socket;
    // Start is called before the first frame update
    void Start()
    {
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickCreateServer()
    {
        GameObject canvas = GameObject.Find("Canvas_1");
        canvas.SetActive(false);

        canvas2.SetActive(true);
    }

    public void OnclickJoinServer()
    {

    }


    public void OnclickCreateServerWithName()
    {
        string playerName = GameObject.Find("Text_playerNameVal").GetComponent<Text>().text;
        string serverName = GameObject.Find("Text_serverNameVal").GetComponent<Text>().text;

        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("namePlayer", playerName);
        json.AddField("nameServer", serverName);
        socket.Emit("create", json);
    }
}
