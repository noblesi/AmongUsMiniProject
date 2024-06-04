using UnityEngine;
using Mirror;

public class AmongUsRoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public EPlayerColor playerColor;

    private bool hasSpawned = false;

    public void Start()
    {
        base.Start();

        if (isServer && !hasSpawned)
        {
            SpawnLobbyPlayerCharacter();
            hasSpawned = true;
        }
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


        Vector3 spawnPos = FindObjectOfType<SpawnPositions>().GetSpawnPosition();

        var playerCharacter = Instantiate(AmongUsRoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyCharacterMover>();
        NetworkServer.Spawn(playerCharacter.gameObject, connectionToClient);
        playerCharacter.playerColor = color;
    }
}
