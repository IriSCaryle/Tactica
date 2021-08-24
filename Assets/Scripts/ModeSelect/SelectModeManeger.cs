using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectModeManeger : MonoBehaviour
{
    [Header("各パネル")]
    [SerializeField] GameObject selectModePanel;
    [SerializeField] GameObject normalStageList;
    [SerializeField] GameObject editStageList;
    [Header("prefabの親")]
    [SerializeField] GameObject normalContentParent;
    [SerializeField] GameObject editStageContentParent;
    [Header("prefab")]
    [SerializeField] GameObject normalContentPrefab;
    [SerializeField] GameObject editContentPrefab;
    [Header("各スクリプト")]
    [SerializeField] MapCSVLoad mapCSV;
    // Start is called before the first frame update
    string PARENT_DIR = Application.streamingAssetsPath+"/";

    [Header("各リスト")]
    [SerializeField]List<string> normalmapFolderPath = new List<string>();
    [SerializeField] List<string> editmapFolderPath = new List<string>();
    [SerializeField] List<string> normalFolderName = new List<string>();
    [SerializeField] List<string> editFolderName = new List<string>();

    [SerializeField] SEAudioSource source;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        
    }
    void Start()
    {
        LoadEditPath();
        LoadNormalPath();
        CreateNormalObject();
        CreateEditObject();
        FadeManager.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBackScene()
    {
        source.OnPlayNo();
        FadeManager.FadeOut(2);
    }

    public void OnClickNormalStage()
    {
        selectModePanel.SetActive(false);
        normalStageList.SetActive(true);
     
    }
    public void OnClickEditStage()
    {
        selectModePanel.SetActive(false);
        editStageList.SetActive(true);
    
    }
    public void OnClickEditMode()
    {
        selectModePanel.SetActive(false);
    }


    void LoadEditPath()
    {    
        string path = PARENT_DIR;
        editmapFolderPath.AddRange(Directory.GetDirectories(path, "*", System.IO.SearchOption.AllDirectories));
        editmapFolderPath.RemoveAll(s => s.Contains("defaultstages"));
        foreach (string tmp in editmapFolderPath)
        {
           
            editFolderName.Add(System.IO.Path.GetFileName(tmp));
            Debug.Log("edit:"+tmp);
        } 
    }

    public void OnClickButton()
    {
        source.OnStageStart();
    }

    void LoadNormalPath()
    {
        normalmapFolderPath.AddRange(mapCSV.Maps.Values);
        foreach(string tmp in mapCSV.Maps.Values)
        {
            normalFolderName.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(tmp)));
            Debug.Log("normal:"+tmp);
        }


    }
    void CreateNormalObject()
    {
        for (int i = 0; i < mapCSV.Maps.Count; i++)
        {
            GameObject prefab= Instantiate(normalContentPrefab, 
                                            Vector3.zero, 
                                            Quaternion.identity, 
                                            normalContentParent.transform);

            Text txt = prefab.GetComponentInChildren<Text>();
            normalpref normalpref = prefab.GetComponent<normalpref>();
            Button button = prefab.GetComponent<Button>();
            normalpref.i = i;
            txt.text = "1-" + (i + 1);
          
            
        }
    }

    void CreateEditObject()
    {
        for (int i = 0; i < editmapFolderPath.Count; i++)
        {
            GameObject prefab = Instantiate(editContentPrefab,
                                           Vector3.zero,
                                           Quaternion.identity,
                                           editStageContentParent.transform);

            Text txt = prefab.GetComponentInChildren<Text>();
            editpref editpref = prefab.GetComponent<editpref>();
            Button button = prefab.GetComponent<Button>();
            editpref.i = editFolderName[i];
            txt.text = editFolderName[i];
            
        }
    }
}
