using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTravel : MonoBehaviour
{

    //updates teams based on team variable, resets variable back to 0 when on title.
    public void ToTitle()
    {
        SceneManager.LoadScene("TitleScreen");

        if (Singleton.Instance.getTeamNumber() == 1 || Singleton.Instance.getTeamNumber() == 2)
        {
            Singleton.Instance.updateParty();
        }
        else
        {
            Debug.Log("Team Variable wasn't set properly... go fix it!");
        }

        Singleton.Instance.setTeam(0);
    }

    //sends you to battle
    public void ToBattle()
    {
        SceneManager.LoadScene("BattleScreen");
    }

    //sends you to teams
    public void ToTeam(int num)
    {
        if (num == 1 || num == 2)
        {
            Singleton.Instance.setTeam(num);
            SceneManager.LoadScene("TeamSelection");
        }
        else
        {
            Debug.Log("ERROR ERROR!!");
            return;
        }
    }
}
