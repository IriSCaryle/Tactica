using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScreen : MonoBehaviour
{
    Vector2 touchpos;
    Vector2 startpos;
    Collider2D collider2D;

    bool scrollStartFlg = false;
    const float SCROLL_END_LEFT = -2.5f;
    const float SCROLL_END_RIGHT = 2.5f;
    const float SCROLL_DISTANCE_CORRECTION =0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            collider2D = Physics2D.OverlapPoint(touchpos);
            Debug.Log(touchpos);
            if(scrollStartFlg == false && collider2D)
            {
                GameObject obj = collider2D.transform.gameObject;
                Debug.Log(obj.name);

            }
          
        }
    }
}
