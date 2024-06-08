using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CharacterMover : NetworkBehaviour
{
    private Animator Animator_Character;

    private bool isMoveable;
    public bool IsMoveable
    {
        get { return isMoveable; }
        set
        {
            if (!value)
            {
                Animator_Character.SetBool("isMove", false);
            }
            isMoveable = value;
        }
    }

    [SyncVar]
    public float speed = 2f;

    [SerializeField]
    private float characterSize = 0.5f;

    [SerializeField]
    private float cameraSize = 2.5f;

    private SpriteRenderer spriteRenderer;

    [SyncVar(hook = nameof(SetPlayerColor_Hook))]
    public EPlayerColor playerColor;

    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(newColor));
    }

    [SyncVar(hook = nameof(SetNickname_Hook))]
    public string nickname;
    [SerializeField] protected Text nicknameText;
    public void SetNickname_Hook(string _, string value)
    {
        nicknameText.text = value;
    }
    

    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        Animator_Character = GetComponent<Animator>();
        if(isOwned)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
            cam.orthographicSize = cameraSize;
        }
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if(isOwned && IsMoveable)
        {
            bool isMove = false;
            if(PlayerSettings.controlType == EControlType.KeyboardMouse)
            {
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f), 1f);
                if (dir.x < 0f) transform.localScale = new Vector3(-characterSize, characterSize, 1f);
                else if (dir.x > 0f) transform.localScale = new Vector3(characterSize, characterSize, 1f);
                transform.position += dir * speed * Time.deltaTime;
                isMove = dir.magnitude != 0f;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;
                    if (dir.x < 0f) transform.localScale = new Vector3(-characterSize, characterSize, 1f);
                    else if (dir.x > 0f) transform.localScale = new Vector3(characterSize, characterSize, 1f);
                    transform.position += dir * speed * Time.deltaTime;
                    isMove = dir.magnitude != 0f;
                }
            }
            Animator_Character.SetBool("isMove", isMove);
        }
        if(transform.localScale.x < 0)
        {
            nicknameText.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (transform.localScale.x > 0)
        {
            nicknameText.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
