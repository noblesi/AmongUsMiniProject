using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineUI : MonoBehaviour
{
    [SerializeField] private InputField InputField_NickName;
    [SerializeField] private GameObject createRoomUI;

    public void OnClickCreateRoomButton()
    {
        if (!string.IsNullOrWhiteSpace(InputField_NickName.text))
        {
            PlayerSettings.nickName = InputField_NickName.text;
            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            InputField_NickName.GetComponent<Animator>().SetTrigger("On");
        }
               
    }

    
}
