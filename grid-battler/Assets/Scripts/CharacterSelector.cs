using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private readonly Character tempChar;
    private Character selectedCharacter = null;
    private Character[] personalParty = new Character[3];
    private void Start()
    {
        if (Singleton.Instance.getTeam() == null)
        {
            for (int i = 0; i < personalParty.Length; i++)
            {
                personalParty[i] = tempChar;
            }
        }
        else
        {
            for (int i = 0; i < Singleton.Instance.getTeam().Length; i++)
            {
                if (Singleton.Instance.getTeam()[i] == null)
                {
                    selectedCharacter = tempChar;
                    callUpdateSlot(i + 1);
                }
                else
                {
                    personalParty[i] = Singleton.Instance.getTeam()[i];
                }
            }
        }
        
        for(int i = 1; i<=3; i++)
        {
            Singleton.Instance.updateSlot(i, Singleton.Instance.getTeam()[i-1]);
        }

        //cleanse useless game object
        Destroy(tempChar);
    }

    public void updateSelectedCharacter(Character chara)
    {
        if (chara != null)
        {
            selectedCharacter = chara;
        }
    }

    public void callUpdateSlot(int slotNumber)
    {
        Debug.Log("calling singleton..");
        Singleton.Instance.updateSlot(slotNumber, selectedCharacter);
    }
}
