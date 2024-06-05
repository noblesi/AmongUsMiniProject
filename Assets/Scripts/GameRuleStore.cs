using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public enum EKillRange
{
    Short, Normal, Long
}

public enum ETaskBarUpdates
{
    Always, Meetings, Never
}

public struct GameRuleData
{
    public bool confirmEjects;
    public int emergencyMeetings;
    public int emergencyMeetingsCooldown;
    public int meetingsTime;
    public int voteTime;
    public bool anonymousVotes;
    public float moveSpeed;
    public float crewSight;
    public float imposterSight;
    public float killCooldown;
    public EKillRange killRange;
    public bool visualTasks;
    public ETaskBarUpdates taskBarUpdates;
    public int commonTask;
    public int complexTask;
    public int simpleTask;
}
public class GameRuleStore : NetworkBehaviour
{
    
    [SyncVar] private bool confirmEjects;
    [SyncVar] private int emergencyMeetings;
    [SyncVar] private int emergencyMeetingsCooldown;
    [SyncVar] private int meetingsTime;
    [SyncVar] private int voteTime;
    [SyncVar] private bool anonymousVotes;
    [SyncVar] private float moveSpeed;
    [SyncVar] private float crewSight;
    [SyncVar] private float imposterSight;
    [SyncVar] private float killCooldown;
    [SyncVar] private EKillRange killRange;
    [SyncVar] private bool visualTasks;
    [SyncVar] private ETaskBarUpdates taskBarUpdates;
    [SyncVar] private int commonTask;
    [SyncVar] private int complexTask;
    [SyncVar] private int simpleTask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
