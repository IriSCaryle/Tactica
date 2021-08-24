using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    [SerializeField]
    Animator PushStart;
    [SerializeField]
    SEAudioSource source;
    public void ButtonExit()
    {
        PushStart.SetTrigger("pushStart");
        source.OnPlayNo();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
