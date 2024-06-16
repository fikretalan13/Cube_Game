using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Kup : MonoBehaviour
{
    [HideInInspector] public Color kupRengi;
    [HideInInspector] public int kupNumarasi;
    [HideInInspector] public Rigidbody kupRB;
    [HideInInspector] public bool anaKup;

    public GameObject kupizi;
    MeshRenderer kupMeshRenderer;

    float uygulanacakGuc = 3f; // yeni kup olustugunda uygulanacak guç

    private void Awake()
    {
        kupRB = GetComponent<Rigidbody>();
        kupMeshRenderer = GetComponent<MeshRenderer>();
        anaKup = false;
    }




    public void RenkTanimla(Color renk)
    {
        kupRengi = renk;
        kupMeshRenderer.material.color = renk;
    }

    public void NumaraOlustur(int numara)
    {
        kupNumarasi = numara;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Kup carpilanKup = collision.gameObject.GetComponent<Kup>();

        if (carpilanKup != null && kupNumarasi == carpilanKup.kupNumarasi && kupNumarasi != 11)
        {
            Vector3 temasNoktasi = collision.contacts[0].point;

            GameManager.Instance.kupYonetimi.KupOlustur(++kupNumarasi, temasNoktasi + Vector3.up * 1.5f);
            Kup yeniKup = GameManager.Instance.kupYonetimi.OlusanKupuGetir();

            //güç uyguluyoruz
            yeniKup.kupRB.AddForce(new Vector3(0, .2f, 1.5f) * uygulanacakGuc, ForceMode.Impulse);

            //tork - opsiyonel
            Vector3 torkYonu = Vector3.one * Random.Range(-30, 30f);
            yeniKup.kupRB.AddTorque(torkYonu);


            //ANA YÖNTEM

            int maksimumColliderSayisi = 10;
            Collider[] kapsamaAlani = new Collider[maksimumColliderSayisi];

            int etkilesimdekiKupler = Physics.OverlapSphereNonAlloc(temasNoktasi,3f,kapsamaAlani);
            float patlamaGucu = 400f;
            float patlamaMenzili = 2.5f;

            for (int i = 0; i < etkilesimdekiKupler; i++)
            {
                if (kapsamaAlani[i].attachedRigidbody != null)
                {
                    kapsamaAlani[i].attachedRigidbody.AddExplosionForce(patlamaGucu, temasNoktasi, patlamaMenzili);
                }
            }


            //*************************************************************************************************

            //YÖNTEM 2

            /*
            Collider[] kupler = Physics.OverlapSphere(temasNoktasi,2f);

            float patlamaGucu = 400f;
            float patlamaMenzili = 1.5f;
            foreach (Collider coll in kupler)
            {
                if (coll.attachedRigidbody !=null)
                {
                    coll.attachedRigidbody.AddExplosionForce(patlamaGucu, temasNoktasi, patlamaMenzili);
                }
            }
            */



            //*************************************************************************************************

            GameManager.Instance.EfektOynat(temasNoktasi,kupRengi);
            GameManager.Instance.SesCal(4);
            GameManager.Instance.IlerlemeKontrol();
            GameManager.Instance.kupYonetimi.KupPasiflestir(gameObject.GetComponent<Kup>());
            GameManager.Instance.kupYonetimi.KupPasiflestir(carpilanKup);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BolumKontrol"))
        {
            if (!anaKup && kupRB.velocity.magnitude < .1f)
                GameManager.Instance.Kaybettin();
        }
    }
}
