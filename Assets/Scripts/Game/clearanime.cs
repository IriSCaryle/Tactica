using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearanime : MonoBehaviour
{
    [SerializeField] Gamemanager gamemanager;
    [SerializeField] MapCSVLoad MapCSV;
    public void animfinish()
    {
        if (gamemanager.loadtype == 0)
        {
            gamemanager.stagenumber++;
            try 
            {
                string str = MapCSV.MapNameList[gamemanager.stagenumber];
                gamemanager.gamepassload();
                gamemanager.gamereset();
            } catch {
                gamemanager.stagenumber--;
                FadeManager.FadeOut(2);
            } 
        }
        gameObject.SetActive(false);
    }
}
