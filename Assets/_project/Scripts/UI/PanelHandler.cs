using System.Collections.Generic;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    [SerializeField] private List<Panel> _panels;
    [SerializeField] private PausePanel _pausePanel;

    public void StartGame()
    {
        _pausePanel.Pause(false);
        _pausePanel.gameObject.SetActive(false);
    }
    
    public void PauseGame()
    {
        _pausePanel.Pause(true);
        _pausePanel.gameObject.SetActive(true);
    }

    public void CloseAllPanels()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (Panel p in _panels)
            {
                p.gameObject.SetActive(false);
            }
        }
    }
}