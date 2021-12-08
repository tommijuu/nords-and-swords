using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;
    public Rigidbody CameraRB;

    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        // Kääntää kameraa hitaasti
        CameraRB.transform.Rotate(0, 0.005f, 0 * Time.deltaTime);
    }

    // Alla tehdään funktiot joita kutsutaan menujen napeista

    public void QuitGame()
    {
        Debug.Log("Quitting application / Quitting test mode");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OpenSettings()
    {
        SettingsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void OpenCredits()
    {
        CreditsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void BackButton()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void PlayButton()
    {
        // SceneManager.LoadScene("CombatScene");
        SceneManager.LoadScene("AttributeScene");
        Debug.Log("Play pressed");
    }

}
