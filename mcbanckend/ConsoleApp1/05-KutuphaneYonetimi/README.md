# 05 - Kütüphane Yönetimi (Bitirme Projesi)

Microsoft "C# ile Programlamaya Giriş" kursunun bitirme projesi. 5 kitap kapasiteli, konsolda çalışan küçük bir kütüphane sistemi.

## Dosyalar
| Dosya | Açıklama |
|---|---|
| [LibraryManager.cs](LibraryManager.cs) | Projenin kodu |
| [LibraryManager-Rehber.pdf](LibraryManager-Rehber.pdf) | Kodu hiç yazılım bilmeyenler için satır satır anlatan rehber |

## Özellikler
- `add` / `remove`: kitap ekleme ve silme (en fazla 5 kitap)
- `search`: kitabı başlığa göre arama (büyük/küçük harf fark etmez)
- `borrow`: ödünç alma, aynı anda en fazla 3 kitap
- `return`: ödünç alınan kitabı iade etme
- Ödünç alınan kitaplar listede `(borrowed)` olarak işaretlenir

## Kullanılan konular
Sadece kursta öğrenilenler: diziler, `for`, `if/else`, `switch`, `while`, static metotlar.

## Çalıştırma
```
cd mcbanckend/ConsoleApp1
dotnet run
```
Menüden **6 - Kütüphane Yönetimi (Final)** seçilir.

## Kodu okumaya nereden başlamalı?
Önce PDF rehberi oku, sonra `LibraryManager.cs` içindeki `// ===== SORU ... =====` yorumlarını takip et. Her özelliğin kodu bu işaretlerle ayrılmıştır:
1. **Arama:** `FindBook` ve `SearchBook`
2. **Ödünç limiti:** `borrowedCount`, `maxBorrow` ve `BorrowBook` içindeki kontrol
3. **Ödünç işareti:** `isBorrowed` dizisi
