using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edit_Blocks : MonoBehaviour
{

    public int vertical;

    public int horizontal;

    [SerializeField] EditManager editManager;

    [SerializeField] GameObject Layer1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        if (editManager.eraser.isOn)
        {
            editManager.EdittingStage[vertical, horizontal] = 0;
            Destroy(Layer1);
        }
        else
        {
            AddBlockImage(editManager.CurrentBlockID);
        }
    }

    public void AddBlockImage(int currentID)
    {
        Debug.Log("vertical:" + vertical + ",horizontal:" + horizontal);
        editManager.EdittingStage[vertical, horizontal] = currentID;
        Destroy(Layer1);
        Layer1 = Instantiate<GameObject>(editManager.csvLoad.Blocks[currentID], new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        Layer1.transform.localPosition = new Vector3(0, 0, 0);
        Layer1.transform.localScale = new Vector3(0.01f, 0.01f, 1);
    }
}
