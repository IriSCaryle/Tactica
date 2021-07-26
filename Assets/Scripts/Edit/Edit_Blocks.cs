using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edit_Blocks : MonoBehaviour
{

    public int vertical;

    public int horizontal;

    [SerializeField] EditManager editManager;

    [SerializeField] GameObject RoadLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("vertical:"+vertical+",horizontal:"+horizontal);
        editManager.EdittingStage[vertical, horizontal] = editManager.CurrentBlockID;
    }
}
