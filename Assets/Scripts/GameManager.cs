using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] UI_Mangarer _uiManager;
    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private AudioClip _gameOverMusic;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void LoadMainmenuScene()
    {
        SceneManager.LoadScene(0); //0 = maion menu
    }
    public void RestartLevel()
    {
        if (_isGameOver) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }
    public void GoToMainMenu()
    {
        if (_isGameOver) LoadMainmenuScene();
    }

    public void GameOver()
    {
        _isGameOver = true;
        Sounds_SFX.Instance.PlayGameOverMusic(_gameOverMusic);
        _uiManager.DisplayBestScore();
    }
    public void ActivatePauseMenuPanel()
    {
        _uiManager.SetActivePauseMenuPanel();
        _uiManager.DisablePauseButtonOutsidePauseMenuPanel();
    }
    public void ResumeGame()
    {
        _uiManager.SetActiveFalsePauseMenuPanel();
        _uiManager.EnablePauseButtonOutsidePauseMenuPanel();
    }
}
