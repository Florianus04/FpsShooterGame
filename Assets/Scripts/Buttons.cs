using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject infoPanel;

    public static GameObject shootConsole;
    public static GameObject shootCountText;
    public static int shootCount = 0;

    private bool isOpen = false;
    void Start()
    {
        shootConsole = GameObject.Find("ShootConsole");
        shootCountText = GameObject.Find("ShootCount");
        infoPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                isOpen = false;
                infoPanel.SetActive(false);
            }
            else
            {
                isOpen = true;
                infoPanel.SetActive(true);
            }
        }
        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }         
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
