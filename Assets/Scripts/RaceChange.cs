using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RaceChange : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CharacterObject characterObject;



    private string[] races = { "human", "orc" };
    public GameObject[] skeleton;
    [SerializeField] private GameObject[] manHair;

    public int raceIndex = 0; //0-3
    public int skeletonIndex; //0-7
    //public int activeSkeletonIndex = 0;

    public bool isFemale = false;
    bool skeletonActive;

    

    void Awake()
    {
        int index = characterObject.characterData.skins.skeleton;
        int non = Mathf.Abs(index - 1);

        skeleton[index].GetComponent<MeshRenderer>().enabled = true; 
        skeleton[non].GetComponent<MeshRenderer>().enabled = false;

        skeletonIndex = index;

        if (index == 1) { isFemale = true; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenderButton(int gender)
    {
        skeletonIndex = gender + (raceIndex * 2);

        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (i != skeletonIndex)
            {
                skeletonActive = true;

                skeleton[skeletonIndex].GetComponent<MeshRenderer>().enabled = true;
                skeleton[i].GetComponent<MeshRenderer>().enabled = false;

                skeleton[skeletonIndex].GetComponent<SkinChange>().LoadSkinFromSO();
            }

            else
            {
                skeletonActive = false;
            }

            if (skeletonIndex == 1)
            {
                isFemale = true;

                //otkluchaem vse buttons v manHairs

                for (int j = 0; j <= 3; j++)
                {
                    foreach (Transform child in manHair[j].transform)
                    {
                        if (child.TryGetComponent(out Button button))
                        {
                            button.interactable = false;
                        }
                    }
                }


            }
            else
            {
                isFemale = false;

                for (int j = 0; j <= 3; j++)
                {
                    foreach (Transform child in manHair[j].transform)
                    {
                        if (child.TryGetComponent(out Button button))
                        {
                            button.interactable = true;
                        }
                    }
                }
            }
        }

            


        //Debug.Log(skeletonIndex);
    }
}
