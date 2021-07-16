using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prayer : MonoBehaviour
{
    [SerializeField] int player_maxcount;
    int player_count;
    bool player_alive;

    void Start()
    {
        StageReset();
    }

    void Update()
    {
        
    }

    public void Countcheak()
    {
        if (player_maxcount > 0) player_maxcount--;
        else player_alive = false;
    }

    public void StageReset()
    {
        player_alive = true;
        player_count = player_maxcount;
    }
}
