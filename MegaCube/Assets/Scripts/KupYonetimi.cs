using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KupYonetimi : MonoBehaviour
{
    public Kup[] kupHavuzu;

    [SerializeField] Color[] kupRenkleri;
    [SerializeField] Transform kupOlusmaNoktasi;

    Kup olusanKup;

    public void KupOlustur(int KupNumarasi,Vector3 pozisyon)
    {
        foreach (var item in kupHavuzu)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                olusanKup = item;
                olusanKup.transform.position= pozisyon;
                olusanKup.NumaraOlustur(KupNumarasi);
                olusanKup.gameObject.SetActive(true);
                olusanKup.RenkTanimla(RenkBelirle(KupNumarasi));

                break;
            }
        }
    }
   
    public void KupPasiflestir(Kup pasiflesecekKup)
    {
        pasiflesecekKup.kupRB.velocity = Vector3.zero;
        pasiflesecekKup.kupRB.angularVelocity = Vector3.zero;
        pasiflesecekKup.transform.rotation = Quaternion.identity;
        pasiflesecekKup.anaKup = false;
        pasiflesecekKup.gameObject.SetActive(false);
    }

    public Kup RastgeleKupOlustur()
    {
        KupOlustur(RastgeleSayiOlustur(), kupOlusmaNoktasi.position);

        return olusanKup;
    }
    public int RastgeleSayiOlustur()
    {
        return Random.Range(0,5);
    }

    public Kup OlusanKupuGetir()
    {
        return olusanKup;
    }
    Color RenkBelirle(int number)
    {
        return kupRenkleri[number];
    }
}
