using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişleri için şart

public class BolumYoneticisi : MonoBehaviour
{
    [Header("Bölüm Ayarları")]
    public int toplamNesneSayisi = 3; // Bu bölümde kaç tane eşleşecek nesne var?
    public float gecisBeklemeSuresi = 1.5f; // Hepsi bitince kaç saniye sonra geçsin?

    private int yerlesenSayisi = 0;

    // Her nesne doğru yere oturduğunda bu fonksiyon çağrılır
    public void NesneYerlesti()
    {
        yerlesenSayisi++;

        // Eğer sahnedeki tüm nesneler yerleştiyse sonraki bölüme geç
        if (yerlesenSayisi >= toplamNesneSayisi)
        {
            StartCoroutine(SonrakiBolumeGec());
        }
    }

    IEnumerator SonrakiBolumeGec()
    {
        // Çocuğun konfetiyi ve alkışı görmesi için kısa bir bekleme
        yield return new WaitForSeconds(gecisBeklemeSuresi);

        // Build Settings listesindeki bir sonraki sahneyi yükle
        int mevcutSahneIndex = SceneManager.GetActiveScene().buildIndex;
        int sonrakiSahneIndex = mevcutSahneIndex + 1;

        // Eğer son bölüm bittiyse Ana Menüye (0. sahneye) dön
        if (sonrakiSahneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sonrakiSahneIndex);
        }
        else
        {
            SceneManager.LoadScene(0); 
        }
    }

    // Ana Menüdeki "Oyna" butonu için
    public void OyunaBasla()
    {
        SceneManager.LoadScene(1); // 1 numaralı ilk oyun bölümünü açar
    }

    // Oyun içindeki "Tekrar Oyna" butonu için
    public void BolumuTekrarla()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Oyun içindeki "Ana Menü / Ev" butonu için
    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene(0);
    }
}
