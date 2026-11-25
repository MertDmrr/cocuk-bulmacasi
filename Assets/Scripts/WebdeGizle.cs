using UnityEngine;

// Tarayıcıda (WebGL) Application.Quit çalışmadığı için
// bu bileşenin bağlı olduğu objeyi (Çıkış butonu) web sürümünde gizler.
public class WebdeGizle : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            gameObject.SetActive(false);
        }
    }
}
