using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField] private List<Image> Images_Crew;
    [SerializeField] private List<Button> Btns_ImposterCount;
    [SerializeField] private List<Button> Btns_MaxPlayerCount;

    private CreateRoomData roomData;

    private void Start()
    {
        for(int i = 0; i < Images_Crew.Count; i++)
        {
            Material materialInstance = Instantiate(Images_Crew[i].material);
            Images_Crew[i].material = materialInstance;
        }

        roomData = new CreateRoomData() { imposterCount = 1, maxPlayerCount = 10 };
        UpdateCrewImages();
    }

    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        for(int i = 0; i < Btns_MaxPlayerCount.Count; i++)
        {
            if (i == count - 5)
            {
                Btns_MaxPlayerCount[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                Btns_MaxPlayerCount[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        UpdateCrewImages();
    }

    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        for (int i = 0; i < Btns_ImposterCount.Count; i++)
        {
            if (i == count - 1)
            {
                Btns_ImposterCount[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                Btns_ImposterCount[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        int limitMaxPlayer = count == 1 ? 4 : count == 2 ? 7 : count == 3 ? 9 : 11;
        if(roomData.maxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }

        for(int i = 0; i< Btns_MaxPlayerCount.Count; i++)
        {
            var text = Btns_MaxPlayerCount[i].GetComponentInChildren<Text>();

            if(i < limitMaxPlayer - 5)
            {
                Btns_MaxPlayerCount[i].interactable = false;
                text.color = Color.grey;
            }
            else
            {
                Btns_MaxPlayerCount[i].interactable = true;
                text.color = Color.white;
            }
        }
    }

    private void UpdateCrewImages()
    {
        for(int i = 0; i < Images_Crew.Count; i++)
        {
            Images_Crew[i].material.SetColor("_PlayerColor", Color.white);
        }

        int imposterCount = roomData.imposterCount;
        int idx = 0;
        while(imposterCount != 0)
        {
            if(idx >= roomData.maxPlayerCount)
            {
                idx = 0;
            }

            if (Images_Crew[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0, 5) == 0)
            {
                Images_Crew[idx].material.SetColor("_PlayerColor", Color.red);
                imposterCount--;
            }
            idx++;
        }

        for(int i = 0; i< Images_Crew.Count; i++)
        {
            if(i < roomData.maxPlayerCount)
            {
                Images_Crew[i].gameObject.SetActive(true);
            }
            else
            {
                Images_Crew[i].gameObject.SetActive(false);
            }
        }
    }
}

public class CreateRoomData
{
    public int imposterCount;
    public int maxPlayerCount;
}
