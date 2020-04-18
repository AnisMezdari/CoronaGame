using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public SocketIOComponent socket;


    public GameObject canvas2;
    public GameObject canvas3;
    public GameObject canvas4;

    public List<GameObject> listFieldAndButton;

    private int  currentNbPlayer;

    public GameObject  player;

    public GameObject buttonStart;

    // Start is called before the first frame update
    void Start()
    {
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
        socket.On("newPlayerJoin", DisplayNewPlayerJoin);

        socket.On("player", SetPlayer);

        socket.On("startGame", StartGame);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void  SetPlayer(SocketIOEvent e)
    {
        player.GetComponent<Corona.Player>().index = int.Parse(e.data.GetField("playerObject").GetField("index").ToString());
        player.GetComponent<Corona.Player>().name = e.data.GetField("playerObject").GetField("name").str;
        player.GetComponent<Corona.Player>().isLocalPlayer  =  true;

        GameObject playerObejct = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        DontDestroyOnLoad(playerObejct);
    }

    public void OnclickCreateServer()
    {
        GameObject canvas = GameObject.Find("Canvas_1");
        canvas.SetActive(false);
        canvas2.SetActive(true);
    }

    public void OnclickJoinServer()
    {
        GameObject canvas = GameObject.Find("Canvas_1");
        canvas.SetActive(false);
        canvas4.SetActive(true);
        
        socket.Emit("listLobby");
        socket.On("listLobby", GetListLobby);
    }


    public void OnclickCreateServerWithName()
    {
        currentNbPlayer++;
        string playerName = GameObject.Find("Text_playerNameVal").GetComponent<Text>().text;
        string serverName = GameObject.Find("Text_serverNameVal").GetComponent<Text>().text;

        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("namePlayer", playerName);
        json.AddField("nameServer", serverName);
        socket.Emit("create", json);

        canvas2.SetActive(false);
        canvas3.SetActive(true);

        GameObject textPlayer = GameObject.Find("Text_player0");
        textPlayer.GetComponent<Text>().text = "Player 1 : " + playerName;

        GameObject textServer = GameObject.Find("Text_Server");
        textServer.GetComponent<Text>().text = "[" + serverName + "]";

        GameObject playerObject = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log(playerObject.GetComponent<Corona.Player>().isAdmin);


        StartCoroutine(SetPlayer(playerObject.GetComponent<Corona.Player>(), playerName));
        playerObject.GetComponent<Corona.Player>().name = playerName;
        playerObject.GetComponent<Corona.Player>().isLocalPlayer = true;
        playerObject.GetComponent<Corona.Player>().index = 0;

        LocalPlayerIsAdmin();
    }

    public IEnumerator SetPlayer(Corona.Player player , string playerName)
    {
        yield return new WaitForSeconds(0.5f);
        player.isAdmin = true;
    }

    public void GetListLobby(SocketIOEvent e)
    {
        JSONObject listLobbyJson = e.data.GetField("lobbyList");
        string[] listLobbyNameString = new string[listLobbyJson.Count];
        for (int i = 0; i < listLobbyJson.Count; i++)
        {
            listLobbyNameString[i] = listLobbyJson[i].GetField("name").ToString();
            listFieldAndButton[i].SetActive(true);
            GameObject lobbyUI = GameObject.Find("server" + i);
            lobbyUI.transform.GetChild(0).GetComponent<Text>().text = listLobbyJson[i].GetField("name").str;
        }
    }

    public void OnClickJoinPreciseServer(string message) {
        JSONObject json = new JSONObject();

        GameObject server = GameObject.Find("server" + message);
        string nameServer = server.transform.GetChild(0).GetComponent<Text>().text;
        string namePlayer = server.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<Text>().text;

        json.AddField("namePlayer", namePlayer);
        json.AddField("nameServer", nameServer);
        socket.Emit("join",json);

        canvas4.SetActive(false);
        canvas3.SetActive(true);

        JSONObject json2 = new JSONObject();
        json2.AddField("nameServer", nameServer);

        socket.On("listPlayer", DisplayListPlayer);
        socket.Emit("listPlayer", json2);

        GameObject.Find("Text_Server").GetComponent<Text>().text = "[" + nameServer + "]";
    }

    public void DisplayListPlayer(SocketIOEvent e)
    {
        JSONObject json = e.data.GetField("playerList");
        currentNbPlayer = json.Count;
        for (int i = 0; i < json.Count; i++)
        {
            GameObject.Find("Text_player"+i).GetComponent<Text>().text =  "Player " + (i+1) + " : " +  json[i].GetField("name").str;
           
            if( i < json.Count -1)
            {
                GameObject playerObject = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                playerObject.GetComponent<Corona.Player>().name = json[i].GetField("name").str;
                playerObject.GetComponent<Corona.Player>().isLocalPlayer = false;
                playerObject.GetComponent<Corona.Player>().index = i;
                playerObject.GetComponent<Corona.Player>().isAdmin = bool.Parse(json[i].GetField("isAdmin").ToString());
            }
           
        }
        LocalPlayerIsAdmin();
    }

    
    public void DisplayNewPlayerJoin(SocketIOEvent e)
    {
        string nameNewPlayer = e.data.GetField("nameNewPlayer").str;
        GameObject.Find("Text_player" + (currentNbPlayer)).GetComponent<Text>().text = "Player " + (currentNbPlayer+1) + " : " + nameNewPlayer;
        GameObject playerObject = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        playerObject.GetComponent<Corona.Player>().name = nameNewPlayer;
        playerObject.GetComponent<Corona.Player>().isLocalPlayer = false;
        playerObject.GetComponent<Corona.Player>().index = currentNbPlayer;
        playerObject.GetComponent<Corona.Player>().isAdmin = false;
        currentNbPlayer++;
    }

    public void LocalPlayerIsAdmin()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject item in players)
        {
            if(item.GetComponent<Corona.Player>().isLocalPlayer  && item.GetComponent<Corona.Player>().isAdmin)
            {
                DisplayButtonStartParty();
            }
        }
    }

    public void DisplayButtonStartParty()
    {
        buttonStart.SetActive(true);
    }


    public void OnclickStartGame()
    {
        SceneManager.LoadScene("Party", LoadSceneMode.Single);
        socket.Emit("startGame");

    }

    public void StartGame(SocketIOEvent e)
    {
        SceneManager.LoadScene("Party", LoadSceneMode.Single);
    }
}
