using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioSource audioSourceComponent;
    public AudioClip soundClick;
    public GameObject selectIcon;
    public List<GameObject> menuList;
    int menuIndex = 0;
    public string menuName = "Menu-StartGame";

    GamepadInput gamepadInputComponent;

    private void Awake()
    {
        gamepadInputComponent = FindObjectOfType<GamepadInput>();
    }
   
    private void Update()
    {

        if (gamepadInputComponent.onButtonDown["Select Up"] == true)
        {
            playSoundClick();
            selectPrevMenu();
        }
        if (gamepadInputComponent.onButtonDown["Select Down"] == true)
        {
            playSoundClick();
            selectNextMenu();
        }

        if (gamepadInputComponent.onButtonDown["Enter"] == true)
        {
            menuSelect();
            playSoundClick();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playSoundClick();
            selectPrevMenu();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playSoundClick();
            selectNextMenu();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            menuSelect();
            playSoundClick();
        }
            
    }

    void menuSelect()
    {
        if (menuName == "Menu-StartGame")
        {
            ApplicationVariables.loadingSceneName = "Level1";
            SceneManager.LoadScene("LoadingScene");
        }
        if (menuName == "Menu-Settings")
        {
            ApplicationVariables.loadingSceneName = "Settings";
            SceneManager.LoadScene("LoadingScene");
        }
        if (menuName == "Menu-Exit")
        {
            Application.Quit();
        }
    }
    void selectNextMenu()
    {
        menuIndex++;
        if (menuIndex >= 3)
        {
            menuIndex = 0;
        }
        GameObject selectingMenu = menuList[menuIndex];
        selectIcon.transform.position = new Vector2(selectIcon.transform.position.x, selectingMenu.transform.position.y);
        menuName = selectingMenu.name;
    }

    void selectPrevMenu()
    {
        menuIndex--;
        if (menuIndex < 0)
        {
            menuIndex = 2;
        }
        GameObject selectingMenu = menuList[menuIndex];
        selectIcon.transform.position = new Vector2(selectIcon.transform.position.x, selectingMenu.transform.position.y);
        menuName = selectingMenu.name;
    }

    void playSoundClick()
    {
        audioSourceComponent.PlayOneShot(soundClick);
    }
}
