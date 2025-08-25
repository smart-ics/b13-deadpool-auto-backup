
---

# **SOP Pemulihan Database (Recovery Plan)**

## A. Penetapan Server

1. **IT Manager** menetapkan tiga server berikut:
    1. **Production Server**: Terpasang SQL Server, digunakan untuk menjalankan database utama.
    2. **Failover Server**: Terpasang SQL Server, digunakan sebagai server cadangan jika server produksi tidak berfungsi.
    3. **Backup Storage Server**: Terpasang FTP Server dan _ICS-Deadpool Backup Tools_, digunakan sebagai penyimpanan file backup.
        
2. **EDP** memastikan ketiga server berada dalam satu jaringan LAN dan dapat saling berkomunikasi.
3. **EDP** mendokumentasikan detail server, termasuk alamat IP masing-masing, dan menyerahkannya kepada ICS.
## B. Konfigurasi Rencana Kontinjensi

1. **EDP** memberikan akses penuh ke ketiga server tersebut untuk tim ICS.
2. **ICS** melakukan instalasi semua tools yang diperlukan serta mengatur konfigurasi sesuai standar ICS pada ketiga server.
3. **EDP** menjadwalkan simulasi rencana kontinjensi untuk memastikan kesiapan sistem.
4. **EDP** dan **ICS** melaksanakan _User Acceptance Test (UAT)_ sesuai jadwal yang ditetapkan.
## C. Operasional Harian

1. Setiap hari kerja, **EDP** memastikan tools _ICS-Deadpool Backup_ berjalan dengan baik di Backup Storage Server.
2. Setiap hari Senin, **EDP** memastikan bahwa **Full Backup** pada hari Minggu pukul 02.00 WIB telah berhasil dibuat.
3. Setiap hari Senin, **EDP** menghapus file backup yang berusia lebih dari dua minggu (W-2).
4. Setiap pagi pada hari kerja, **EDP** memastikan bahwa **Differential Backup** pukul 03.00 WIB telah berhasil dibuat.
5. Setiap sore pada hari kerja, **EDP** memastikan bahwa **Transaction Log Backup** berjalan setiap 15 menit dan berhasil.
6. Setiap hari, **EDP** memastikan bahwa Failover Server dalam kondisi siap digunakan.
## D. Prosedur Pemulihan (Recovery)

1. **IT Manager** menentukan server yang akan digunakan untuk proses restore database.
2. **EDP** memastikan server terpilih siap digunakan untuk proses restore.
3. **EDP** menjalankan proses restore dengan menekan tombol **Restore** pada aplikasi _ICS-Deadpool Backup_ sesuai server yang dipilih.
4. **EDP** memastikan proses restore berhasil dan database dapat diakses.
5. Jika menggunakan Failover Server, **EDP** memperbarui konfigurasi koneksi database pada seluruh modul aplikasi _MyHospital_ agar terhubung ke Failover Server.
6. **EDP** memastikan semua modul aplikasi berjalan normal setelah proses restore.
7. **EDP** melaporkan kepada **IT Manager** bahwa sistem telah siap digunakan kembali.
8. **IT Manager** memberikan instruksi resmi agar operasional rumah sakit dilanjutkan dengan sistem yang sudah dipulihkan.
    

---

## E. Tanggung Jawab

- **IT Manager**
    - Menetapkan server yang digunakan dalam proses pemulihan.
    - Memberikan keputusan final atas dimulainya kembali operasional sistem.
        
- **EDP**
    - Melakukan pemantauan harian terhadap proses backup.
    - Menjalankan prosedur pemulihan sesuai instruksi IT Manager.
    - Melaporkan status pemulihan ke IT Manager.
        
- **ICS**
    - Menyediakan tools dan konfigurasi standar.
    - Mendukung pelaksanaan simulasi dan proses pemulihan saat diperlukan.
        
