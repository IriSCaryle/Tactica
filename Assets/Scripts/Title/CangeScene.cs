using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CangeScene : MonoBehaviour
{
    [SerializeField]
    Animator Push;
    [SerializeField]
    Image Buck;
    public void OnClickStartButton()
    {
        Push.SetTrigger("ChangeScene");
    }

    public void Chage()
    {
        SceneManager.LoadScene("ModeSelect");
    }

    public void ActiveBlack()
    {
        Buck.gameObject.SetActive(true);
    }

}