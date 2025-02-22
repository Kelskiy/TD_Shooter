using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text healpPointsText;
    public TMP_Text weaponText;

    public Button startButton;
    public Button settingsButton;
    public Button exitButton;

    public GameObject mainMenu;
    public GameObject view;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(ExitGame);

        mainMenu.SetActive(true);
        view.SetActive(false);

        Time.timeScale = 0;
    }


    public void StartGame()
    {
        mainMenu.SetActive(false);
        view.SetActive(true);

        Time.timeScale = 1;
 
        healpPointsText.text = "Health: " + PlayerInfoKeeper.HP;
        weaponText.text = "Selected weapon: " + PlayerInfoKeeper.selectedWeaponName;
    }

    public void OpenSettings()
    {
        Debug.Log("Settings");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void UpdateHealthValue(int currentHealth)
    {
        healpPointsText.text = "Health: " + currentHealth.ToString();
    }

    public void UpdateWeaponName(string weaponName)
    {
        if (weaponText != null)
            weaponText.text = "Selected weapon: " + weaponName;
    }


    public void ShowView()
    {
        view.SetActive(true);
    }

    public void HideView()
    {
        view.SetActive(false);
    }
}
