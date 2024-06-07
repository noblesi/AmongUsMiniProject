using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;

    [SerializeField] private InGameIntroUI ingameIntroUI;
    public InGameIntroUI InGameIntroUI { get { return ingameIntroUI; } }

    private void Awake()
    {
        Instance = this;
    }
}
