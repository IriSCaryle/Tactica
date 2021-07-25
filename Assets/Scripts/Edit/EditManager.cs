using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EditManager : MonoBehaviour
{
    public int[,] EdittingStage = new int[,] {

    {18,19,19,19,19,19,19,19,19,12},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {16,15,15,15,15,15,15,15,15,14},
    
    };

    [Range(0, 36)] public int CurrentBlockID =0;

    public CSVLoad csvLoad;

    [SerializeField] Image CurrentImage;
    GameObject tmp;
    // Start is called before the first frame update
    void Start()
    {
        OnClickBlockChange();
        initBoard();
    }


    void initBoard()
    {


    }


    void GenerateWall()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickBlockChange()
    {
        if (CurrentBlockID == 36)
        {
            Debug.Log("ID:1に戻りました");
            CurrentBlockID =1;
        }
        else
        {
            CurrentBlockID += 1;
        }
        
        ChangeImage(CurrentBlockID);
    }

    void ChangeImage(int number)
    {
        Destroy(tmp);

        
        tmp = Instantiate<GameObject>(csvLoad.Blocks[number],new Vector2(0,0),Quaternion.identity,CurrentImage.transform);
        tmp.transform.localPosition = new Vector3(0, 0, 0);
        Debug.Log("ChangeBlock:" + number);
    }
}
