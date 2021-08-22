using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editpref : MonoBehaviour
{
    public string i;
    // Start is called before the first frame update
    public void OnClick()
    {
        PlayerPrefs.SetString("StageName", i);
        PlayerPrefs.SetInt("isEdit", 1);
        PlayerPrefs.Save();
    }
}
