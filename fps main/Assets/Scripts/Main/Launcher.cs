using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;
using UnityEngine.Audio;


public class Launcher : MonoBehaviourPunCallbacks

{
    [SerializeField] TMP_InputField roomNameInputField;

    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;

    [SerializeField] TMP_Dropdown mapSelectorDropdown;

    [SerializeField] Transform roomListContent;
    [SerializeField] Transform playerListContent;

    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject mapSelectorHolder;



    public AudioMixer masterMixer;

    public static Launcher Instance;
   
    void Awake()
    {
        Instance = this;
        //all setting check from setting menu

        if(PlayerPrefs.HasKey("ResolutionWidth") && PlayerPrefs.HasKey("ResolutionHeight"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), Screen.fullScreen); 
        }
        else
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }

        if(PlayerPrefs.HasKey("QualityLevel"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel"));
        }
        else
        {
            QualitySettings.SetQualityLevel(1);
        }

        if(PlayerPrefs.HasKey("VolumeMasterValue"))
        {
            masterMixer.SetFloat("volumeMaster", PlayerPrefs.GetFloat("VolumeMasterValue"));
        }
        else
        {
            masterMixer.SetFloat("volumeMaster", -20);
        }

        if(PlayerPrefs.HasKey("Vsync"))
        {
            QualitySettings.vSyncCount = PlayerPrefs.GetInt("Vsync");
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        if(PlayerPrefs.HasKey("FPSMax"))
        {
            Application.targetFrameRate = PlayerPrefs.GetInt("FPSMax");
        }
        else
        {
            Application.targetFrameRate = 60;
        }

        //crosshair check? idk if there is better solution
        if(!PlayerPrefs.HasKey("CrosshairGap"))
        {
            PlayerPrefs.SetInt("CrosshairGap", 5);
        }

        if(!PlayerPrefs.HasKey("CrosshairHeight"))
        {
            PlayerPrefs.SetInt("CrosshairHeight", 5);
        }

        if(!PlayerPrefs.HasKey("CrosshairWidth"))
        {
            PlayerPrefs.SetInt("CrosshairWidth", 2);
        }

        if(!PlayerPrefs.HasKey("CrosshairRed"))
        {
            PlayerPrefs.SetFloat("CrosshairRed", 0f);
        }

        if(!PlayerPrefs.HasKey("CrosshairGreen"))
        {
            PlayerPrefs.SetFloat("CrosshairGreen", 0.91f);
        }

        if(!PlayerPrefs.HasKey("CrosshairBlue"))
        {
            PlayerPrefs.SetFloat("CrosshairBlue", 0f);
        }

    }
    
    


    public void Start()
    {
        Debug.Log("connecting to master");
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("joined lobby");
    }
   
    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("match");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        mapSelectorHolder.SetActive(PhotonNetwork.IsMasterClient);

    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        mapSelectorHolder.SetActive(PhotonNetwork.IsMasterClient);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Match creation failed: " + message; 
        MenuManager.Instance.OpenMenu("errors");
    }

    public void StartGame()
    {

        StartCoroutine(ConnectionDelayForJoining());
           
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");

        
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent) 
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPLayer)
    {

        StartCoroutine(ConnectionDelayForDebugging());
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPLayer);
    }

    IEnumerator ConnectionDelayForJoining()
        {
            while(PhotonNetwork.IsConnected == false)
                yield return null;      
            PhotonNetwork.LoadLevel(mapSelectorDropdown.value + 1);
        }


    IEnumerator ConnectionDelayForDebugging()
        {
            yield return new WaitForSeconds(5);
        }


}