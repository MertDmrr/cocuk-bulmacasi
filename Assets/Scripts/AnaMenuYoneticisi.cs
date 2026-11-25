using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenuYoneticisi : MonoBehaviour
{
    [Header("Ses Butonu Ayarları")]
    public Image sesButonuResmi;      // Sahnedeki Ses butonunun Image bileşeni
    public Sprite sesAcikIkonu;       // Turuncu hoparlör ikonu
    public Sprite sesKapaliIkonu;      // Turuncu çarpılı hoparlör ikonu

    private bool sesAcik = true;

    void Start()
    {
        // Oyun daha önce sessize alındıysa o ayarı hatırla
        sesAcik = PlayerPrefs.GetInt("SesDurumu", 1) == 1;
        SesDurumunuGuncelle();
    }

    // Yeşil OYNA butonuna bağlanacak
    public void OyunuBaslat()
    {
        // Build Settings'deki 1 numaralı ilk oyun sahnesini açar
        SceneManager.LoadScene(1);
    }

    // Turuncu SES butonuna bağlanacak
    public void SesiAcKapat()
    {
        sesAcik = !sesAcik;
        PlayerPrefs.SetInt("SesDurumu", sesAcik ? 1 : 0);
        SesDurumunuGuncelle();
    }

    private void SesDurumunuGuncelle()
    {
        // Unity'deki tüm sesleri (müzik + efektler) tek seferde açar veya kapatır
        AudioListener.volume = sesAcik ? 1f : 0f;

        if (sesButonuResmi != null)
        {
            sesButonuResmi.sprite = sesAcik ? sesAcikIkonu : sesKapaliIkonu;
        }
    }

    // Çıkış butonu eklerseniz bağlayabilirsiniz
    public void OyundanCik()
    {
        Application.Quit();
    }
}
