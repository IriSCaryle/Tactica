using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EditMapSetting : MonoBehaviour
{

    [SerializeField] InputField filename;

    [SerializeField] InputField life;


    [SerializeField] InputField X;

    [SerializeField] InputField Y;


    public string FileName;

    public string Life;

    public string PlayerVertical;

    public string PlayerHorizontal;
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

    public void ChangePlayerXpos()
    {
        PlayerHorizontal = X.text;
    }

    public void ChangePlayerYpos()
    {
        PlayerVertical = Y.text;
    }
}
