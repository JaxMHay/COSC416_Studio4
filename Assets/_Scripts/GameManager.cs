using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int score = 0;
    [SerializeField] private CoinCounterUI coinCounter;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject settingsMenu;

    private bool isSettingsMenuActive;
    //this creates a public getter for the bool
    //this way the variable is read only without making it public
    public bool IsSettingsMenuActive => isSettingsMenuActive;



    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inputManager.OnSettingsMenu.AddListener(ToggleSettingsMenu);
        // the game starts with the settings menu disabled
        DisableSettingsMenu();
    }

    public void IncreaseScore()
    {
        score++;
        coinCounter.UpdateScore(score);

    }

    private void ToggleSettingsMenu()
    {
        if (isSettingsMenuActive)
            DisableSettingsMenu();
        else 
            EnableSettingsMenu();
    }

    private void EnableSettingsMenu()
    {
        //pause the game?
        Time.timeScale = 0f;
        //enable the settings menu UI
        settingsMenu.SetActive(true);
        //make the cursor visible and unlock it
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //set boolean
        isSettingsMenuActive = true;
    }

    public void DisableSettingsMenu()
    {
        //resume the game?
        Time.timeScale = 1f;
        //disable the settings menu UI
        settingsMenu.SetActive(false);
        //Re-lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //set boolean
        isSettingsMenuActive = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
