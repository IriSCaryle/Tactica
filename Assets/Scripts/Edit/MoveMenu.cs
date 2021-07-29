using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveMenu : MonoBehaviour
{

    [SerializeField] Toggle toggle;

    [SerializeField] Vector2 openPos;
    [SerializeField] Vector2 closePos;
    [SerializeField]RectTransform rectTransform;
    [SerializeField] float easing;
    bool isOpen;
    bool isOpened;
    Vector2 v;
    Vector2 diff;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen && isOpened == false)
        {
            diff = rectTransform.anchoredPosition - openPos;
            v = diff * easing;
            rectTransform.anchoredPosition -= v;
            if (diff.magnitude < 0.01f)
            {
                isOpened = true;
            }
        }
        else if(!isOpen && isOpened)
        {
            diff = closePos - rectTransform.anchoredPosition;
            v = diff * easing;
            rectTransform.anchoredPosition += v;
            if (diff.magnitude < 0.01f)
            {
                isOpened = false;
            }
        }
    }

    public void OnClickMenuButton()
    {
        switch (toggle.isOn)
        {
            case true:
                isOpen = true;
                break;
            case false:
                isOpen = false;
                break;
        }
    }
}
