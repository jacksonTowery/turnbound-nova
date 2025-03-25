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

    public void ToHowToPlay() 
        {
        SceneManager.LoadScene("HowToPlay");
    }
}
