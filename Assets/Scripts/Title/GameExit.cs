using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    [SerializeField]
    Animator PushStart;

    public void ButtonExit()
    {
        PushStart.SetTrigger("pushStart");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
