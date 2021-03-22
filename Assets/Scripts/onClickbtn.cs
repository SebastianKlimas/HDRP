using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class onClickbtn : MonoBehaviour
{
    [SerializeField] private GameObject optionsP;
    [SerializeField] private GameObject AutorzyP;
    [SerializeField] private Text title;
    private int rozdzialka=0;
   

    public void start()
    {
       // SceneManager.LoadScene("StartScene");
       // ladowanie sceny
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void settings()
    {
        
        optionsP.SetActive(true);
        
    }

    public void zobacz_autorow()
    {

        AutorzyP.SetActive(true);
    }
    public void returnek_dwa()
    {

        AutorzyP.SetActive(false);
    }
    public void returnek()
    {

        optionsP.SetActive(false);
    }

    public void zmien_rozdzialke()
    {
        rozdzialka++;
        switch (rozdzialka) {
            case 1:
                title.fontSize = 50;
                Screen.SetResolution(1600, 800, true);
                break;

            case 2:
                Screen.SetResolution(1366, 768, true);
                break;


            case 3:
                Screen.SetResolution(1024, 768, true);
                break;

            case 4:
                title.fontSize = 70;
                Screen.SetResolution(1980, 1080, true);
                rozdzialka=0;
                break;


        }
       
    }
    public void windowed_mode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void end()
    {
      
        Application.Quit();
    }
}
