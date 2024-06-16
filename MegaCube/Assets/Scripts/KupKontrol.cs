using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KupKontrol : MonoBehaviour
{

    [SerializeField] float hareketHizi;
    [SerializeField] float gidisHizi;
    [SerializeField] float hareketSiniri;

    [SerializeField] TouchSlider touchSlider;

    Kup kup;

    bool dokunmaBasladi;
    bool hareketEdiyor;

    Vector3 kupPos;


    private void Start()
    {
        KupOlustur();
        hareketEdiyor = true;

        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (dokunmaBasladi && hareketEdiyor)
        {
            kup.transform.position = Vector3.Lerp(kup.transform.position, kupPos, hareketHizi * Time.deltaTime);
        }
    }

    void OnPointerDown()
    {
        dokunmaBasladi = true;
    }

    void OnPointerDrag(float hareketDegeri)
    {
        if (dokunmaBasladi && hareketEdiyor)
        {
            kupPos = kup.transform.position;
            kupPos.x = hareketDegeri * hareketSiniri;
        }
    }

    void OnPointerUp()
    {
        if (dokunmaBasladi && hareketEdiyor)
        {
            dokunmaBasladi = false;
            hareketEdiyor = false;

            kup.kupRB.AddForce(Vector3.forward * gidisHizi, ForceMode.Impulse);
            kup.kupizi.SetActive(false);
            Invoke("YeniKupOlustur", .3f);
        }
    }

    void YeniKupOlustur()
    {
        kup.anaKup = false;
        kup = null;
        KupOlustur();
       

    }

    void KupOlustur()
    {

        kup = GameManager.Instance.kupYonetimi.RastgeleKupOlustur();
        kup.anaKup = true;
        kup.kupizi.SetActive(true);

        kupPos = kup.transform.position;
        hareketEdiyor = true;
    }

    private void OnDestroy()
    {
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}
