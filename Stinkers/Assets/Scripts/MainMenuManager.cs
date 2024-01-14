using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject LevelSelectionPanel;


    private void Start()
    {
        CloseLevelSelection();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void OpenLevelSelection()
    {
        LevelSelectionPanel.SetActive(true);
    }

    public void CloseLevelSelection()
    {
        LevelSelectionPanel.SetActive(false);
    }
}
