using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public Character[] Party1 = null;
    public Character[] Party2 = null;
    public static Character[] tempParty = new Character[3];
    public int currentTeam = 0;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    public void updateParty(Character[] oldparty, Character[] newparty)
    {
        oldparty = newparty;
    }

    public void updateParty(Character[] newparty)
    {
        newparty = tempParty;
    }

    public void updateParty()
    {
        updateParty(getTeam());
    }

    public Character[] getTempParty()
    {
        return tempParty;
    }

    public Character[] setTempParty(Character[] party)
    {
        tempParty = party;
        return tempParty;
    }

    public int getTeamNumber()
    {
        return currentTeam;
    }

    public Character[] getTeam(int teamFetch)
    {
        if (teamFetch == 1)
        {
            return Party1;
        }

        else if (teamFetch == 2)
        {
            return Party2;
        }

        else
            return null;
    }

    public Character[] getTeam()
    {
        return getTeam(getTeamNumber());
    }

    public void setTeam(int num)
    {
        currentTeam = num;

        setTempParty(getTeam(num));
    }

    //using slot-1 format again for simplicity
    //this method should only ever be called in regards to the slot buttons, so adding image update functionality to this as well
    public void updateSlot(int slot, Character chara)
    {
        Debug.Log("Arrived in singleton..");

        Character[] newTempParty = getTempParty();
        Debug.Log("successfully established newtempparty");

        //if statement (hopefully) prevents from setting something to something it already is, helping runtime to be faster
        if (tempParty[slot - 1] != chara)
        {
            Debug.Log("tempparty != chara");
            newTempParty[slot - 1] = chara;
        }

        if (chara != null && chara.getSprite() != null)
        {
            Debug.Log("chara!=null and sprite!=null");
            GameObject.Find("Slot" + slot).GetComponentInChildren<UnityEngine.UI.Image>().sprite = chara.getSprite();
            Debug.Log("sprite successfully found and set");
        }
        else
        {
            Debug.Log("Nothing flagged right, sprite is set to question mark");
            GameObject.Find("Slot" + slot).GetComponentInChildren<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Sprites/Question");
        }

        Debug.Log("set new party");
        setTempParty(newTempParty);
    }

}
