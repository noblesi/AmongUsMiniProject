using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Rendering;

public class AmongUsRoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public EPlayerColor playerColor;

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


        //Vector3 spawnPos = FindObjectOfType<SpawnPositions>().GetSpawnPosition();

        //var player = Instantiate(AmongUsRoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity);
        //NetworkServer.Spawn(player, connectionToClient);
    }
}
