using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectModeAnimation : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] GameObject EditMapList;
    [SerializeField] GameObject NormalMapList;

    [SerializeField] Animator SelectModePanelanimator;

    [SerializeField] Animator EditMapPanelAnimator;
    [SerializeField] Animator NormalMapPanelAnimator;

    [SerializeField] SEAudioSource source;
    public void OnClickNormalStage()
    {
        SelectModePanelanimator.SetTrigger("normalstage");
        source.OnPlayYes();
    }
    public void OnClickEditStage()
    {
        SelectModePanelanimator.SetTrigger("editstage");
        source.OnPlayYes();
    }
    public void OnClickEdit()
    {
        SelectModePanelanimator.SetTrigger("edit");
        source.OnPlayYes();
    }
    public void ActiveBackground()
    {
        background.SetActive(true);
    }
    public void ActiveEditMapList()
    {
        EditMapList.SetActive(true);
        EditMapPanelAnimator.SetTrigger("editstart");
    }
    public void ActiveNormalMapList()
    {
        NormalMapList.SetActive(true);
        NormalMapPanelAnimator.SetTrigger("normalstart");
    }
    public void ChangeEditScene()
    {
        SceneManager.LoadScene("Edit");
    }
}
