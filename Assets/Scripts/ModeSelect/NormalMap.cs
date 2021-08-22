using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMap : MonoBehaviour
{
    [SerializeField] GameObject background;
    // Start is called before the first frame update
    public void DisActiveBackgroound()
    {
        background.SetActive(false);
    }
}
