using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameIntroUI : MonoBehaviour
{
    [SerializeField] private GameObject shhhhhObj;
    [SerializeField] private GameObject crewmateObj;

    [SerializeField] private Text playerType;
    [SerializeField] private Image gradiantImage;
    [SerializeField] private IntroCharacter myCharacter;
    [SerializeField] private List<IntroCharacter> otherCharacters = new List<IntroCharacter>();
    [SerializeField] private Color crewColor;
    [SerializeField] private Color imposterColor;

    [SerializeField] private CanvasGroup canvasGroup;

    public IEnumerator ShowIntroSequence()
    {
        shhhhhObj.SetActive(true);
        yield return new WaitForSeconds(3f);
        shhhhhObj.SetActive(false);

        ShowPlayerType();
        crewmateObj.SetActive(true);
    }
    public void ShowPlayerType()
    {
        var players = GameSystem.Instance.GetPlayerList();

        InGameCharacterMover myPlayer = null;
        foreach(var player in players)
        {
            if (player.isOwned)
            {
                myPlayer = player;
                break;
            }
        }

        myCharacter.SetIntroCharacter(myPlayer.nickname, myPlayer.playerColor);

        if(myPlayer.playerType == EPlayerType.Imposter)
        {
            playerType.text = "임포스터";
            playerType.color = gradiantImage.color = imposterColor;

            int i = 0;
            foreach(var player in players)
            {
                if(!player.isOwned && player.playerType == EPlayerType.Imposter)
                {
                    otherCharacters[i].SetIntroCharacter(player.nickname, player.playerColor);
                    otherCharacters[i].gameObject.SetActive(true);
                    i++;
                }
            }
        }
        else
        {
            playerType.text = "크루원";
            playerType.color = gradiantImage.color = crewColor;

            int i = 0;
            foreach (var player in players)
            {
                if (!player.isOwned)
                {
                    otherCharacters[i].SetIntroCharacter(player.nickname, player.playerColor);
                    otherCharacters[i].gameObject.SetActive(true);
                    i++;
                }
            }
        }
    }

    public void Close()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;
        while(timer <= 1f)
        {
            yield return null;
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer);
        }

        gameObject.SetActive(false);
    }
}
