using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loader : MonoBehaviour
{
    // Start is called before the first frame update
    void MErchant()
    {
        PlayerAttributes save = new PlayerAttributes();
        save.SavePlayer(); // Attribuuttien tallennus ennen merchant-sceneä
        SceneManager.LoadScene("MapScene");

    }
}
