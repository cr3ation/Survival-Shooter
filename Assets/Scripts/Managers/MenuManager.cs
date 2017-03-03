using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;


    void Start()
    {
        Cursor.visible = true;
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
    }

    public void NewGame(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    #region Exit game
    public void ExitGame_Clicked()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void ExitGameNo_Clicked()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void ExitGameYes_Clicked()
    {
        Application.Quit();
    }

    #endregion
}
