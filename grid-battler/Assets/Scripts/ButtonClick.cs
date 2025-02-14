using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] TestGrid toClick;

    public void attack()
    {
        toClick.attackTrue();
    }
    public void move()
    {
        toClick.moveTrue();
    }
    public void act()
    {
        toClick.actTrue();
    }
}
