using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour {

    public FantonHealth health;
    public GameObject inventoryUI;
    public GameObject pauseGameUI;

    public Sprite[] images;
    public string[] titles;
    public string[] texts;

    Image image;
    Text itemNumber;
    Text title;
    Text text;

    int showingItem = 0;
    bool gameIsPaused = false;
    bool inventoryActive = false;

    // Use this for initialization
    void Start()
    {
        image = inventoryUI.GetComponentsInChildren<Image>()[1];
        itemNumber = inventoryUI.GetComponentsInChildren<Text>()[0];
        title = inventoryUI.GetComponentsInChildren<Text>()[1];
        text = inventoryUI.GetComponentsInChildren<Text>()[2];
    }	
	// Update is called once per frame
	void Update () {
        
        // Don't do anything if Pause menu is open
        if (pauseGameUI.activeSelf) { return; }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (showingItem == 1)
                CloseInventory();
            else
                ShowInventoryInspector(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (showingItem == 2)
                CloseInventory();
            else
                ShowInventoryInspector(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (showingItem == 3)
                CloseInventory();
            else
                ShowInventoryInspector(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (showingItem == 4)
                CloseInventory();
            else
                ShowInventoryInspector(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (showingItem == 5)
                CloseInventory();
            else
                ShowInventoryInspector(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (showingItem == 6)
                CloseInventory();
            else
                ShowInventoryInspector(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (showingItem == 7)
                CloseInventory();
            else
                ShowInventoryInspector(7);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !inventoryActive && health.currentHealth > 0)
        {
            if (!gameIsPaused)
            {
                pauseGameUI.SetActive(true);
                PauseGame(true);
            }
            else
            {
                pauseGameUI.SetActive(false);
                PauseGame(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inventoryActive && health.currentHealth > 0)
        {
            CloseInventory();
        }
        if (health.currentHealth <= 0)
        {
            pauseGameUI.SetActive(false);
            CloseInventory();
        }
    }

    public void CloseInventory()
    {
        PauseGame(false);
        inventoryUI.SetActive(false);
        //Cursor.visible = false;
        inventoryActive = false;
        showingItem = -1;
    }


    /// <summary>
    /// Stop Fanton, Lovisa and all enemy movement. Also stop spawning enemies.
    /// </summary>
    /// <param name="paused"></param>
    void PauseGame(bool paused)
    {
        if (paused)
            gameIsPaused = true;
        else
            gameIsPaused = false;
        //// Stop/continue enemy movevemt
        //var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject enemy in enemies)
        //{
        //    enemy.GetComponent<NavMeshAgent>().enabled = !paused;
        //}

        //// Stop/resume Fanton movement
        //GameObject.FindGameObjectWithTag("Fanton").GetComponent<NavMeshAgent>().enabled = !paused;

        //// Stop/resume Lovisa movement
        //GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().enabled = !paused;

        //// Stop/continue spawning enemies
        //enemyManager.SetActive(!paused);
        if (paused) {
            Time.timeScale = 0.000001f;
        }
        else {
            Time.timeScale = 1f;
        }
    }

    void ShowInventoryInspector(int keyPressed)
    {
        showingItem = keyPressed;
        PauseGame(true);
        inventoryActive = true;

        // Brings up the inventory window
        inventoryUI.SetActive(true);

        //Cursor.visible = true;

        // Set the windows image and text
        int i = keyPressed - 1;
        image.sprite = images[i];
        itemNumber.text = "Iventory item " + keyPressed.ToString();
        title.text = titles[i];
        text.text = texts[i];
    }
}
