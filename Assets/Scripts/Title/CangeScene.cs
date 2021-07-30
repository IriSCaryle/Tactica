using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CangeScene : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickOptionButton()
    {
        SceneManager.LoadScene("Option");
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("ModeSelect");
    }
    
}