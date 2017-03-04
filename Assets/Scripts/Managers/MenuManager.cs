using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas playerNameMenu;
    public Canvas highscoreMenu;
    public Canvas quitMenu;
    public Button startButton;
    public Button playerButton;
    public Button highscoreButton;
    public Button exitButton;

    // Used by Player Preferences. Don't like strings :)
    public const string playerName = "PlayerName";


    void Start()
    {
        // Show mouse
        Cursor.visible = true;

        // Main menu buttons
        startButton = startButton.GetComponent<Button>();
        playerButton = playerButton.GetComponent<Button>();
        highscoreButton = highscoreButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();

        // Popup menus
        playerNameMenu = playerNameMenu.GetComponent<Canvas>();
        highscoreMenu = highscoreMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();

        // Disable popup menus
        playerNameMenu.enabled = false;
        highscoreMenu.enabled = false;
        quitMenu.enabled = false;
    }


    #region Start new game
    public void NewGame(string newGameLevel)
    {
        var name = PlayerPrefs.GetString(playerName);
        if (string.IsNullOrEmpty(name))
        {
            PlayerName_Clicked();
            return;
        }
        SceneManager.LoadScene(newGameLevel);
    }
    #endregion

    #region Player name
    /// <summary>
    /// Opens the player name menu
    /// </summary>
    public void PlayerName_Clicked()
    {
        playerNameMenu.enabled = true;
        EnableStartMenu(false);

        // Save player name
        var name = PlayerPrefs.GetString(playerName);
        if (!string.IsNullOrEmpty(name)) { playerNameMenu.GetComponentInChildren<InputField>().text = name; }
    }

    public void SavePlayerName_Clicked()
    {
        var name = playerNameMenu.GetComponentInChildren<InputField>().text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString(playerName, name);
        }
        playerNameMenu.enabled = false;
        EnableStartMenu(true);
    }
    #endregion

    #region Highscore
    public void Highscore_Clicked()
    {
        highscoreMenu.enabled = true;
        EnableStartMenu(false);
    }

    public void CloseHighscore_Clicked()
    {
        highscoreMenu.enabled = false;
        EnableStartMenu(true);
    }
    #endregion

    #region Exit game
    /// <summary>
    /// Brings up an "Are you sure you want to exit?"-menu
    /// </summary>
    public void ExitGame_Clicked()
    {
        quitMenu.enabled = true;
        EnableStartMenu(false);
    }

    /// <summary>
    /// Close the menu and return to main menu.
    /// </summary>
    public void ExitGameNo_Clicked()
    {
        quitMenu.enabled = false;
        EnableStartMenu(true);
    }

    /// <summary>
    /// Exit game
    /// </summary>
    public void ExitGameYes_Clicked()
    {
        Application.Quit();
    }
    #endregion

    #region Helper functions
    private void EnableStartMenu(bool enabled)
    {
        startButton.enabled = enabled;
        playerButton.enabled = enabled;
        highscoreButton.enabled = enabled;
        exitButton.enabled = enabled;
    }
    #endregion
}
