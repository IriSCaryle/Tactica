using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edit_Blocks : MonoBehaviour
{

    public int vertical;

    public int horizontal;

    [SerializeField] EditManager editManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        editManager.EdittingStage[vertical, horizontal] = editManager.CurrentBlockID;
    }
}
