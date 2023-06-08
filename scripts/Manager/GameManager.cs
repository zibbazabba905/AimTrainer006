using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MenuScripts;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    public HudScript Hud;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : 1;
        Cursor.lockState = IsPaused ? CursorLockMode.Confined : CursorLockMode.Locked;
        MenuScripts.MenuManager.Instance.TogglePauseMenu(IsPaused);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Clicked");
        Application.Quit();
    }
}
