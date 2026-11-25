# Çocuk Bulmacası

**▶ Tarayıcıda oyna:** https://mertdmrr.github.io/cocuk-bulmacasi/

Unity 2D ile yapılmış, küçük çocuklar için bir gölge eşleştirme (şekil yerleştirme) oyunu. Çocuk, renkli nesneleri sürükleyip doğru gölgenin üstüne bırakır. Doğru yere oturan nesne konfeti ve alkış sesiyle kutlanır. Yanlış yere bırakılan nesne titreyip başladığı yere geri döner.

## Bölümler

| Bölüm | Tema | Nesne sayısı | Zorluk |
|---|---|---|---|
| 1 | Deniz | 3 hayvan (ahtapot, balık, kaplumbağa) | Gölgeler altta, tek sıra |
| 2 | Çayır | 5 geometrik şekil | Gölgeler karışık sırada |
| 3 | Mutfak | 6 meyve | Meyveler tezgahta, gölgeler raflarda |
| 4 | Bahçe | 7 böcek ve hayvan | Dağınık gölgeler, 1 ters (sahte) gölge |
| 5 | Uzay | 8 uzay nesnesi | 10 gölge, 2'si sahte, daha hassas yerleştirme |

Bölümler ilerledikçe nesne sayısı artar, yerleşme mesafesi küçülür (0.8 → 0.6) ve aynalanmış sahte gölgeler eklenir. Son bölüm bitince oyun ana menüye döner.

## Ana Menü

- **START:** oyunu başlatır
- **Ses butonu:** tüm sesleri açar ya da kapatır, seçim kaydedilir
- **EXIT:** oyundan çıkar

## Scriptler

| Script | Görevi |
|---|---|
| `SurukleBirak.cs` | Nesneyi fareyle sürükleme, doğru/yanlış kontrolü, ses ve partikül efekti |
| `BolumYoneticisi.cs` | Yerleşen nesneleri sayar, bölüm bitince sonraki sahneye geçer |
| `AnaMenuYoneticisi.cs` | Oyna, ses aç/kapat ve çıkış butonları |
| `WebdeGizle.cs` | Tarayıcı sürümünde çalışmayan çıkış butonunu gizler |

## Projeyi açma

1. Unity Hub ile **Unity 6000.2.11f1** sürümünde projeyi açın.
2. `Assets/Scenes/AnaMenu.unity` sahnesini açıp **Play**'e basın.

Proje URP 2D kullanır. `Project Settings → Player → Active Input Handling` ayarı **Both** olmalıdır, çünkü sürükleme `OnMouseDown` / `Input.mousePosition` ile yapılır.

Sahne sırası (Build Settings): `AnaMenu`, `Bolum1`, `Bolum2`, `Bolum3`, `Bolum4`, `Bolum5`

---

Hazırlayan: Mert Demir
