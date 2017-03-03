using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas playerNameMenu;
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;

    // Used by Player Preferences. Don't like strings :)
    private const string playerName = "PlayerName";


    void Start()
    {
        Cursor.visible = true;
        playerNameMenu = playerNameMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

        // Disable popup menus
        playerNameMenu.enabled = false;
        quitMenu.enabled = false;
    }


    #region Start new game
    public void NewGame(string newGameLevel)
    {
        var name = PlayerPrefs.GetString(playerName);
        if (string.IsNullOrEmpty(name))
        {
            EditPlayerName_Clicked();
            return;
        }
        SceneManager.LoadScene(newGameLevel);
    }
    #endregion

    #region Player name
    public void EditPlayerName_Clicked()
    {
        playerNameMenu.enabled = true;
        EnableStartMenu(false);
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
        startText.enabled = true;
        exitText.enabled = true;
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
        startText.enabled = enabled;
        exitText.enabled = enabled;
    }

    #endregion
}
