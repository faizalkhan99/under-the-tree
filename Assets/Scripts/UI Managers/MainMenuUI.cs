using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button playClick;
    [SerializeField] private Button quitClick;
    public void Awake()
    {
        playClick.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        quitClick.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
