using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveScreen : MonoBehaviour
{
    Vector2 touchpos;
    Vector3 startpos;
    Collider2D collider2D;

    bool scrollStartFlg = false;

    bool isFreeLook =false;

    [SerializeField] Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) )//&& isFreeLook)
        {
            
            touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            collider2D = Physics2D.OverlapPoint(touchpos);
            Debug.Log(touchpos);
            if (scrollStartFlg == false && collider2D)
            {
                GameObject obj = collider2D.transform.gameObject;
                Debug.Log(obj.name);
                startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                scrollStartFlg = true;
            }
            else
            {
                Vector3 diff = startpos - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                this.transform.position += diff;
            }
        }  
    }
    public void OnClickFreeLook()
    {
        if (toggle.isOn)
        {
            isFreeLook = true;
        }
        else
        {
            isFreeLook =false;
        }
    }
}
