using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour {

    public GameObject inventoryUI;

    public Sprite[] images;
    public string[] titles;
    public string[] texts;

    Image image;
    Text itemNumber;
    Text title;
    Text text;

    GameObject enemyManager;

    // Use this for initialization
    void Start()
    {
        image = inventoryUI.GetComponentsInChildren<Image>()[1];
        itemNumber = inventoryUI.GetComponentsInChildren<Text>()[0];
        title = inventoryUI.GetComponentsInChildren<Text>()[1];
        text = inventoryUI.GetComponentsInChildren<Text>()[2];

        enemyManager = GameObject.Find("EnemyManager");
    }	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowInventoryInspector(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShowInventoryInspector(2);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShowInventoryInspector(3);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ShowInventoryInspector(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ShowInventoryInspector(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ShowInventoryInspector(6);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }
    }

    public void CloseInventory()
    {
        PauseGame(false);
        inventoryUI.SetActive(false);
        Cursor.visible = true;
    }


    /// <summary>
    /// Stop Fanton, Lovisa and all enemy movement. Also stop spawning enemies.
    /// </summary>
    /// <param name="paused"></param>
    void PauseGame(bool paused)
    {
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
        PauseGame(true);
        
        // Brings up the inventory window
        inventoryUI.SetActive(true);

        // Set the windows image and text
        int i = keyPressed - 1;
        image.sprite = images[i];
        itemNumber.text = "Iventory item " + keyPressed.ToString();
        title.text = titles[i];
        text.text = texts[i];
    }
}
