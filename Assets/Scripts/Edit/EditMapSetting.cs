using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EditMapSetting : MonoBehaviour
{

    [SerializeField] InputField filename;

    [SerializeField] InputField life;

    public string FileName;

    public string Life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickChangeFileName()
    {
        FileName = filename.text; 
    }

    public void OnClickChangeStepName()
    {
        Life = life.text;
    }
}
