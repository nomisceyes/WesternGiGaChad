using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelHandler : MonoBehaviour
{
    [SerializeField] private List<Panel> _panels;
    [SerializeField] private PausePanel _pausePanel;
    
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            CloseAllPanels();
    }
    
    public void StartGame()
    {
        Global.AudioManager.ResumeMusic();
        _pausePanel.Pause(false);
        _pausePanel.gameObject.SetActive(false);
    }
    
    public void PauseGame()
    {
        Global.AudioManager.PauseMusic();
        _pausePanel.Pause(true);
        _pausePanel.gameObject.SetActive(true);
    }

    public void CloseAllPanels()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (Panel p in _panels)
            {
                p.HideAnimation();
            }
        }
    }
}