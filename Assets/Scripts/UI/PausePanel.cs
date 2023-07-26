using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour {
    [SerializeField] private GameObject _pausePanel;

    /* * * Processing Variables * * */
    private bool isPause;
    public void OnButtonActive() {
        if (isPause) ContinueGame(); 
        else PauseGame();
    }

    private void PauseGame() {
        _pausePanel.SetActive(true);
        PlayerController.instance.DisablePlayerInput();
        Time.timeScale = 0;
        isPause = true;
    }

    private void ContinueGame() {
        _pausePanel.SetActive(false);
        PlayerController.instance.EnablePlayerInput();
        Time.timeScale = 1;
        isPause = false;
    }
}
