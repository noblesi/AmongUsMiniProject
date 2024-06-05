using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Button Btn_MouseControl;
    [SerializeField] private Button Btn_KeyboardMouseControl;

    private Animator Animator_Settings;

    private void Awake()
    {
        Animator_Settings = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        switch(PlayerSettings.controlType)
        {
            case EControlType.Mouse:
                Btn_MouseControl.image.color = Color.green;
                Btn_KeyboardMouseControl.image.color = Color.white;
                break;

            case EControlType.KeyboardMouse:
                Btn_MouseControl.image.color = Color.white;
                Btn_KeyboardMouseControl.image.color = Color.green;
                break;
        }
    }

    public void SetControlMode(int controlType)
    {
        PlayerSettings.controlType = (EControlType)controlType;

        switch (PlayerSettings.controlType)
        {
            case EControlType.Mouse:
                Btn_MouseControl.image.color = Color.green;
                Btn_KeyboardMouseControl.image.color = Color.white;
                break;

            case EControlType.KeyboardMouse:
                Btn_MouseControl.image.color = Color.white;
                Btn_KeyboardMouseControl.image.color = Color.green;
                break;
        }
    }

    public virtual void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        Animator_Settings.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        Animator_Settings.ResetTrigger("Close");
    }
}
