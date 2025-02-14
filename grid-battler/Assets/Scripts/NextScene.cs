using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    public void ToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ToBattle()
    {
        SceneManager.LoadScene("BattleScreen");
    }

    public void ToTeam1()
    {
        SceneManager.LoadScene("Team1Selection");
    }

    public void ToTeam2()
    {
        SceneManager.LoadScene("Team2Selection");
    }
}
