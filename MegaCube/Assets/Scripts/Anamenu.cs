using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anamenu : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SonBolum"))
        {
            PlayerPrefs.SetInt("SonBolum", 1);
            PlayerPrefs.SetInt("OyunSes", 1);
            PlayerPrefs.SetInt("EfektSes", 1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("SonBolum"));
    }
}
