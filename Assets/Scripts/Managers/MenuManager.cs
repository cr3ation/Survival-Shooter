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
        if (true)
        {
            playerNameMenu.enabled = true;
            return;
        }
        SceneManager.LoadScene(newGameLevel);
    }
    #endregion

    #region Player name
    public void EditPlayerName_Clicked()
    {
        playerNameMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void SavePlayerName_Clicked()
    {
        playerNameMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }


    #endregion

    #region Exit game
    /// <summary>
    /// Brings up an "Are you sure you want to exit?"-menu
    /// </summary>
    public void ExitGame_Clicked()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
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
}
