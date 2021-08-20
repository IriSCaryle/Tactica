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
    string PARENT_DIR = Application.streamingAssetsPath;

    List<string> normalmapFolderPath = new List<string>();
    List<string> editmapFolderPath = new List<string>();
    List<string> normalFolderName = new List<string>();
    List<string> editFolderName = new List<string>();
    void Start()
    {
        LoadEditPath();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        string path = PARENT_DIR;//更にサブフォルダを作って親にするので後日記入
        editmapFolderPath.AddRange(Directory.GetDirectories(path, "*", System.IO.SearchOption.AllDirectories));
        editmapFolderPath.RemoveAll(s => s.Contains("defaultstages"));
        foreach (string tmp in editmapFolderPath)
        {
            editFolderName.Add(System.IO.Path.GetDirectoryName(tmp));
            Debug.Log(tmp);
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

            txt.text = "1-" + i + 1;
        }
    }

    void CreateEditObject()
    {
        for (int i = 0; i < editmapFolderPath.Count; i++)
        {
            GameObject prefab = Instantiate(editContentPrefab,
                                           Vector3.zero,
                                           Quaternion.identity,
                                           normalContentParent.transform);

            Text txt = prefab.GetComponentInChildren<Text>();

            txt.text = "1-" + i + 1;
        }
    }
}
