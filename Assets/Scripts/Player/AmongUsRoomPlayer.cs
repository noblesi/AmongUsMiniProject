using UnityEngine;
using Mirror;

public class AmongUsRoomPlayer : NetworkRoomPlayer
{
    private static AmongUsRoomPlayer myRoomPlayer;

    public static AmongUsRoomPlayer MyRoomPlayer
    {
        get
        {
            if(myRoomPlayer == null)
            {
                var players = FindObjectsOfType<AmongUsRoomPlayer>();
                foreach(var player in players)
                {
                    if (player.isOwned)
                    {
                        myRoomPlayer = player;
                    }
                }
            }
            return myRoomPlayer;
        }
    }

    [SyncVar(hook = nameof(SetPlayerColor_Hook))]
    public EPlayerColor playerColor;

    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        LobbyUIManager.Instance.CustomizeUI.UpdateUnselectColorButton(oldColor);
        LobbyUIManager.Instance.CustomizeUI.UpdateSelectColorButton(newColor);
    }

    [SyncVar]
    public string nickname;

    public CharacterMover lobbyPlayerCharacter;

    [SyncVar]
    public bool _readyToBegin;
    public void SetReadyToBegin(bool value)
    {
        _readyToBegin = value;
    }

    public void Start()
    {
        base.Start();

        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
            LobbyUIManager.Instance.ActiveStartButton();
        }

        if(isLocalPlayer)
        {
            CommandSetNickname(PlayerSettings.nickName);
        }

        LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
    }

    private void OnDestroy()
    {
        if (LobbyUIManager.Instance != null)
        {
            LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
            LobbyUIManager.Instance.CustomizeUI.UpdateUnselectColorButton(playerColor);
        }
    }

    [Command]
    public void CommandSetNickname(string nick)
    {
        nickname = nick;
        lobbyPlayerCharacter.nickname = nick;
    }

    [Command]
    public void CommandSetPlayerColor(EPlayerColor color)
    {
        playerColor = color;
        lobbyPlayerCharacter.playerColor = color;
    }

    private void SpawnLobbyPlayerCharacter()
    {
        var roomSlots = (NetworkManager.singleton as AmongUsRoomManager).roomSlots;
        EPlayerColor color = EPlayerColor.Red;

        for(int i = 0; i < (int)EPlayerColor.Lime + 1; i++)
        {
            bool isFindSameColor = false;
            foreach(var roomPlayer in roomSlots)
            {
                var amongUsRoomPlayer = roomPlayer as AmongUsRoomPlayer;
                if(amongUsRoomPlayer.playerColor == (EPlayerColor)i && roomPlayer.netId != netId)
                {
                    isFindSameColor = true;
                    break;
                }
            }

            if(!isFindSameColor)
            {
                color = (EPlayerColor)i;
                break;
            }
            
        }
        playerColor = color;

        var spawnPositions = FindObjectOfType<SpawnPositions>();
        int index = spawnPositions.Index;
        Vector3 spawnPos = spawnPositions.GetSpawnPosition();

        var playerCharacter = Instantiate(AmongUsRoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyCharacterMover>();
        playerCharacter.transform.localScale = index < 5 ? new Vector3(0.5f, 0.5f, 1f) : new Vector3(-0.5f, 0.5f, 1f);
        NetworkServer.Spawn(playerCharacter.gameObject, connectionToClient);
        playerCharacter.ownerNetId = netId;
        playerCharacter.playerColor = color;
    }
}
