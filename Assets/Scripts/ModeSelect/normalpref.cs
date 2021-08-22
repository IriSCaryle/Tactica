using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalpref : MonoBehaviour
{
    public int i=0;
    // Start is called before the first frame update
    public void OnClick()
    {
        PlayerPrefs.SetInt("StageNum", i);
        PlayerPrefs.SetInt("isEdit", 0);
        PlayerPrefs.Save();
        FadeManager.FadeOut(3);
    }
}
