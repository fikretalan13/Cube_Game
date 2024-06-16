using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("---KÜP YÖNETÝMÝ---")]
    public KupYonetimi kupYonetimi;

    [Header("---EFEKT YÖNETÝMÝ---")]
    [SerializeField] ParticleSystem kupBirlesmeEfekti;
    ParticleSystem.MainModule kupEfektAnaModulu;

    [Header("---SES YÖNETÝMÝ---")]
    [SerializeField] AudioSource[] sesler;
    [SerializeField]
    Image[] butonGorselleri;

    [SerializeField]
    Sprite[] spriteObjeleri;



    [Header("---UI YÖNETÝMÝ---")]
    [SerializeField] GameObject[] paneller;
    public Slider ilerlemeSlider;
    public TextMeshProUGUI ilerlemeYuzde;
    public int hedefBirlesme;
    int yapilanBirlesme;

    int sahneIndex;



    private void Awake()
    {
        IlkKurulumIslemleri();
        sahneIndex = SceneManager.GetActiveScene().buildIndex;

        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);

    }

    private void Start()
    {
        kupEfektAnaModulu = kupBirlesmeEfekti.main;

        ilerlemeSlider.maxValue = hedefBirlesme;

    }

    public void IlerlemeKontrol()
    {
        yapilanBirlesme++;
        ilerlemeSlider.value = yapilanBirlesme;

        double degerVeMinFark = ilerlemeSlider.value - ilerlemeSlider.minValue;
        double maxVeMinFark = ilerlemeSlider.maxValue - ilerlemeSlider.minValue;
        double Oran = 100 * (degerVeMinFark / maxVeMinFark);
        ilerlemeYuzde.text = "%" + Mathf.RoundToInt((float)Oran).ToString();

        if (Oran == 100)
        {
            Kazandin();
        }
    }

    public void EfektOynat(Vector3 pozisyon, Color renk)
    {
        kupEfektAnaModulu.startColor = renk;
        kupBirlesmeEfekti.transform.position = pozisyon;
        kupBirlesmeEfekti.Play();
    }

    public void Kaybettin()
    {
        sesler[2].Play();
        PanelAc(4);
        Time.timeScale = 0;

    }


    public void Kazandin()
    {
        PlayerPrefs.SetInt("SonBolum", sahneIndex + 1);
        PanelAc(3);
        sesler[3].Play();
        Time.timeScale = 0;


    }

    public void SesCal(int index)
    {
        sesler[index].Play();
    }

    public void PanelAc(int panelIndex)
    {
        paneller[panelIndex].SetActive(true);
    }

    public void PanelKapat(int panelIndex)
    {
        paneller[panelIndex].SetActive(false);
    }

    public void ButonIslemi(string ButonDeger)
    {
        switch (ButonDeger)
        {
            case "Durdur":
                SesCal(1);
                PanelAc(2);
                Time.timeScale = 0;
                break;

            case "DevamEt":
                PanelKapat(2);
                SesCal(1);
                Time.timeScale = 1;
                break;

            case "OyunaBasla":
                PanelKapat(1);
                PanelAc(0);
                // Time.timeScale = 1;
                SesCal(1);
                break;


            case "Tekrar":
                SesCal(1);
                SceneManager.LoadScene(sahneIndex);
                Time.timeScale = 1;
                break;


            case "SonrakiLevel":
                SesCal(1);
                SceneManager.LoadScene(sahneIndex + 1);
                Time.timeScale = 1;
                break;

            case "Cikis":
                SesCal(1);
                PanelAc(5);
                break;

            case "Evet":
                SesCal(1);
                Application.Quit();
                break;

            case "Hayir":
                SesCal(1);
                PanelKapat(5);
                break;

            case "OyunSes":
                SesCal(1);
                //  PlayerPrefs.GetInt("OyunSes");
                // PlayerPrefs.GetInt("EfektSes");

                if (PlayerPrefs.GetInt("OyunSes") == 0)
                {
                    PlayerPrefs.SetInt("OyunSes", 1);
                    butonGorselleri[0].sprite = spriteObjeleri[0];
                    sesler[0].mute = false;
                }
                else
                {
                    PlayerPrefs.SetInt("OyunSes", 0);
                    butonGorselleri[0].sprite = spriteObjeleri[1];
                    sesler[0].mute = true;
                }
                break;

            case "EfektSes":
                SesCal(1);

                if (PlayerPrefs.GetInt("EfektSes") == 0)
                {
                    PlayerPrefs.SetInt("EfektSes", 1);
                    butonGorselleri[1].sprite = spriteObjeleri[2];

                    for (int i = 1; i < sesler.Length; i++)
                    {
                        sesler[i].mute = false;
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("EfektSes", 0);
                    butonGorselleri[1].sprite = spriteObjeleri[3];
                    for (int i = 1; i < sesler.Length; i++)
                    {
                        sesler[i].mute = true;
                    }
                }
                break;

        }
    }

    void IlkKurulumIslemleri()
    {
        if (PlayerPrefs.GetInt("OyunSes") == 0)
        {

            butonGorselleri[0].sprite = spriteObjeleri[1];
            sesler[0].mute = true;
        }
        else
        {

            butonGorselleri[0].sprite = spriteObjeleri[0];
            sesler[0].mute = false;
        }



        if (PlayerPrefs.GetInt("EfektSes") == 0)
        {

            butonGorselleri[1].sprite = spriteObjeleri[3];

            for (int i = 1; i < sesler.Length; i++)
            {
                sesler[i].mute = true;
            }
        }
        else
        {

            butonGorselleri[1].sprite = spriteObjeleri[2];
            for (int i = 1; i < sesler.Length; i++)
            {
                sesler[i].mute = false;
            }
        }
    }
}
