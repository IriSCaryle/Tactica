using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainBGM : MonoBehaviour
{

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] string beforeScene = "Title";

    bool isstop;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnActiveSceneChanged(Scene pScene,Scene nextScene)
    {
        if(nextScene.name == "Game" || nextScene.name== "Edit")
        {
            source.Stop();
            isstop = true;
        }
        if(nextScene.name =="ModeSelect" || nextScene.name == "Title")
        {
            if (isstop)
            {
                source.Play();
                isstop = false;
            }
        }
        beforeScene = nextScene.name;
    }
    
}
