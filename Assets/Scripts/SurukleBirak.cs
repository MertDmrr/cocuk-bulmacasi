using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SurukleBirak : MonoBehaviour
{
    [Header("Hedef ve Bölüm Bağlantısı")]
    public Transform hedefGolge;
    public BolumYoneticisi bolumYoneticisi;
    public float yerlesmeMesafesi = 1.0f;

    [Header("Görsel Efektler (Particle System)")]
    public ParticleSystem dogruEfekti;

    [Header("Ses Efektleri")]
    public AudioClip tutmaSesi;   // Kısa 'pop' sesi
    public AudioClip dogruSesi;   // 'Ding' veya alkış sesi
    public AudioClip yanlisSesi;  // Yumuşak 'boing' sesi

    private Vector3 baslangicPozisyonu;
    private Vector3 baslangicBoyutu;
    private bool surukleniyor = false;
    private bool yerlesti = false;
    private Camera anaKamera;
    private AudioSource sesKaynagi;

    void Start()
    {
        baslangicPozisyonu = transform.position;
        baslangicBoyutu = transform.localScale;
        anaKamera = Camera.main;
        sesKaynagi = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (yerlesti) return;

        surukleniyor = true;
        GetComponent<SpriteRenderer>().sortingOrder = 10;

        // Tutma sesi ve hafif büyüme efekti
        if (tutmaSesi != null) sesKaynagi.PlayOneShot(tutmaSesi);
        StopAllCoroutines();
        StartCoroutine(BoyutDegistir(baslangicBoyutu * 1.15f, 0.1f));
    }

    void OnMouseDrag()
    {
        if (!surukleniyor || yerlesti) return;

        Vector3 farePozisyonu = anaKamera.ScreenToWorldPoint(Input.mousePosition);
        farePozisyonu.z = 0f;
        transform.position = farePozisyonu;
    }

    void OnMouseUp()
    {
        if (!surukleniyor || yerlesti) return;

        surukleniyor = false;
        GetComponent<SpriteRenderer>().sortingOrder = 5;

        float mesafe = Vector2.Distance(transform.position, hedefGolge.position);

        if (mesafe <= yerlesmeMesafesi)
        {
            // DOĞRU EŞLEŞME
            transform.position = hedefGolge.position;
            yerlesti = true;

            if (dogruSesi != null) sesKaynagi.PlayOneShot(dogruSesi);
            
            if (dogruEfekti != null)
            {
                dogruEfekti.transform.position = hedefGolge.position;
                dogruEfekti.Play();
            }

            // Bölüm yöneticisine haber ver
            if (bolumYoneticisi != null)
            {
                bolumYoneticisi.NesneYerlesti();
            }

            StopAllCoroutines();
            StartCoroutine(YerlesmeEfekti());
        }
        else
        {
            // YANLIŞ EŞLEŞME
            if (yanlisSesi != null) sesKaynagi.PlayOneShot(yanlisSesi);
            StopAllCoroutines();
            StartCoroutine(YanlisVeGeriDon());
        }
    }

    // Yumuşak boyut değişimi
    IEnumerator BoyutDegistir(Vector3 hedefBoyut, float sure)
    {
        Vector3 mevcutBoyut = transform.localScale;
        float gecenSure = 0f;
        while (gecenSure < sure)
        {
            transform.localScale = Vector3.Lerp(mevcutBoyut, hedefBoyut, gecenSure / sure);
            gecenSure += Time.deltaTime;
            yield return null;
        }
        transform.localScale = hedefBoyut;
    }

    // Doğru yerleşince zıplama efekti
    IEnumerator YerlesmeEfekti()
    {
        yield return StartCoroutine(BoyutDegistir(baslangicBoyutu * 1.25f, 0.1f));
        yield return StartCoroutine(BoyutDegistir(baslangicBoyutu, 0.12f));
    }

    // Yanlış bırakılınca titreme ve başlangıca yumuşak dönüş
    IEnumerator YanlisVeGeriDon()
    {
        StartCoroutine(BoyutDegistir(baslangicBoyutu, 0.15f));
        
        // Kısa sağ-sol titreme
        Vector3 hataNoktasi = transform.position;
        for (int i = 0; i < 3; i++)
        {
            transform.position = hataNoktasi + Vector3.right * 0.15f;
            yield return new WaitForSeconds(0.04f);
            transform.position = hataNoktasi - Vector3.right * 0.15f;
            yield return new WaitForSeconds(0.04f);
        }

        // Başlangıç noktasına yumuşak kayma
        Vector3 anlikPozisyon = transform.position;
        float gecenSure = 0f;
        float donusSuresi = 0.25f;

        while (gecenSure < donusSuresi)
        {
            transform.position = Vector3.Lerp(anlikPozisyon, baslangicPozisyonu, gecenSure / donusSuresi);
            gecenSure += Time.deltaTime;
            yield return null;
        }
        transform.position = baslangicPozisyonu;
    }
}
