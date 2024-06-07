using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerType
{
    Crew, Imposter
}

public class InGameCharacterMover : CharacterMover
{
    [SyncVar] public EPlayerType playerType;


    public override void Start()
    {
        base.Start();

        if(isOwned)
        {
            IsMoveable = true;

            var myRoomPlayer = AmongUsRoomPlayer.MyRoomPlayer;
            CommandSetPlayerCharacter(myRoomPlayer.nickname, myRoomPlayer.playerColor);
        }

        GameSystem.Instance.AddPlayer(this);
    }

    [Command]
    private void CommandSetPlayerCharacter(string nickname, EPlayerColor color)
    {
        this.nickname = nickname;
        playerColor = color;
    }
}
