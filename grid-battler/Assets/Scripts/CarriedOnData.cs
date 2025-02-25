using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarriedOnData : MonoBehaviour
{
    [SerializeField] Dropdown carriedA;
    [SerializeField] Dropdown carriedB;
    [SerializeField] Dropdown carriedC;
    public void setSelectCharA()
    {
        CharList.selectCharA = carriedA.options[carriedA.value].text;
        //Debug.Log(CharList.selectCharA);
    }
    public void setSelectCharB()
    {
        CharList.selectCharB = carriedB.options[carriedB.value].text;
        //Debug.Log(CharList.selectCharA);
    }
    public void setSelectCharC()
    {
        CharList.selectCharC = carriedC.options[carriedC.value].text;
        //Debug.Log(CharList.selectCharA);
    }
    public void setSelectCharD()
    {
        CharList.selectCharD = carriedA.options[carriedA.value].text;
        //Debug.Log(CharList.selectCharA);
    }
    public void setSelectCharE()
    {
        CharList.selectCharE = carriedB.options[carriedB.value].text;
        //Debug.Log(CharList.selectCharA);
    }
    public void setSelectCharF()
    {
        CharList.selectCharF = carriedC.options[carriedC.value].text;
        //Debug.Log(CharList.selectCharA);
    }
}
