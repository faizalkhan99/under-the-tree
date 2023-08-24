using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Mangarer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _badFuitsCountText;
    [SerializeField] private TextMeshProUGUI _goodFuitsCountText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _bestGoodFruitsCounterText;
    [SerializeField] private TextMeshProUGUI _bestBadFruitsCounterText;
    [SerializeField] private int _tempBadFruitCounter;
    [SerializeField] private int _tempGoodFruitCounter;
    [SerializeField] private int _bestGoodFruitCounter;
    [SerializeField] private int _bestBadFruitCounter;
    [SerializeField] private Image _currentLives;
    [SerializeField] private Sprite[] _livesSprite;
    [SerializeField] private Button _restartButtonAfterGameIsOver;
    [SerializeField] private Button _mainMenuButtonAfterGameIsOver;
    [SerializeField] private GameObject _PauseMenuPanel;
    [SerializeField] private GameObject _resumeButtonInsidePausePanel;
    [SerializeField] private GameObject _resumeButtonOutsidePausePanel;
    [SerializeField] private GameObject _GoodAndBadFruitsCountertexts;
    [SerializeField] private GameObject _mobileControlsOnOff;
    GameManager _gameManager;
    PlayerMovement player;
    void Start()
    {
         _bestBadFruitCounter = PlayerPrefs.GetInt("Best_Bad_Fruits", 0);
         _bestGoodFruitCounter = PlayerPrefs.GetInt("Best_Good_Fruits", 0);

        _bestBadFruitsCounterText.text = "x" + _bestBadFruitCounter;
        _bestGoodFruitsCounterText.text = "x" + _bestGoodFruitCounter;
        _resumeButtonOutsidePausePanel.gameObject.SetActive(true);
        _restartButtonAfterGameIsOver.gameObject.SetActive(false);
        _mainMenuButtonAfterGameIsOver.gameObject.SetActive(false);
        _PauseMenuPanel.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _gameManager = GameObject.Find("Game_Manager").gameObject.GetComponent<GameManager>();  
        if(player != null)
        {
            Debug.Log("Player is not NULL");
        }
        if(_gameManager != null)
        {
            Debug.Log("GamManager is not NULL");
        }
    }
    void Update()
    {
        SetBadFruitCount();
        SetGoodFruitCount();
        DisplayBestScore();
    }
    void SetBadFruitCount()
    {
        _badFuitsCountText.text = "x" + player._badFruitsCount;
    }
    void SetGoodFruitCount()
    {
        _goodFuitsCountText.text = "x" + player._goodFruitsCount;
    } 
    public void UpdateLives( int currentLives)
    {
        _currentLives.sprite = _livesSprite[currentLives];
        if(currentLives <= 0)
        {
            GameOver();
            _restartButtonAfterGameIsOver.gameObject.SetActive(true);
            _mainMenuButtonAfterGameIsOver.gameObject.SetActive(true);
        }
    }
    void GameOver()
    {
        _gameManager.GameOver();
        StartCoroutine(FlickerGameOvertext());
    }
    IEnumerator FlickerGameOvertext()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void SetActivePauseMenuPanel()
    {
        _mobileControlsOnOff.GetComponent<CanvasGroup>().alpha = 0.0f;
        _PauseMenuPanel.gameObject.SetActive(true);

        Time.timeScale = 0;
        Sounds_SFX.Instance.PauseBGM();
        _GoodAndBadFruitsCountertexts.SetActive(false);
    } 
   
    public void SetActiveFalsePauseMenuPanel()
    {
        _mobileControlsOnOff.GetComponent<CanvasGroup>().alpha = 1;
        _PauseMenuPanel.gameObject.SetActive(false);
        _GoodAndBadFruitsCountertexts.SetActive(true);
        Time.timeScale = 1f;
        Sounds_SFX.Instance.UnPauseBGM();
    }
    public void DisplayBestScore()
    {
        _tempBadFruitCounter = player._badFruitsCount;
        _tempGoodFruitCounter = player._goodFruitsCount;
        if ( (_tempGoodFruitCounter > _bestGoodFruitCounter || _tempBadFruitCounter > _bestBadFruitCounter))
        {
            _bestBadFruitCounter = _tempBadFruitCounter;
            _bestGoodFruitCounter = _tempGoodFruitCounter;
            
            _bestBadFruitsCounterText.text = "x" + _bestBadFruitCounter;
            _bestGoodFruitsCounterText.text = "x" + _bestGoodFruitCounter;

            PlayerPrefs.SetInt("Best_Bad_Fruits", _bestBadFruitCounter);
            PlayerPrefs.SetInt("Best_Good_Fruits", _bestGoodFruitCounter);
        }
    }
    public void DisablePauseButtonOutsidePauseMenuPanel()
    {
        _resumeButtonOutsidePausePanel.gameObject.SetActive(false);

    }
    public void EnablePauseButtonOutsidePauseMenuPanel()
    {
        _resumeButtonOutsidePausePanel.gameObject.SetActive(true);
    }
}