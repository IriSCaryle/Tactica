using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject Menu;
    [SerializeField] Toggle MenuToggle;

    [SerializeField] GameObject FileSetting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   

    public void OnClickMenu()
    {
        switch (MenuToggle.isOn)
        {
            case true:
                Menu.SetActive(true);
                break;
            case false:
                Menu.SetActive(false);
                break;
        }
    }
    public void OnCloseMenu()
    {
        Menu.SetActive(false);
        MenuToggle.isOn = false;
    }

    public void OnClickFileSetting()
    {
        FileSetting.SetActive(true);
    }
    public void OnClickCloseFileSetting()
    {
        FileSetting.SetActive(false);
    }
}
