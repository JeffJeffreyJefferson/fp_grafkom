# Panduan Setup Solar System - Final Project Grafkom

## ⚡ SETUP CEPAT (Recommended!)

**Cara termudah untuk setup semua:**
1. Buat Empty GameObject → Rename "GameManager"
2. Add Component → `AutoSetup`
3. Di Inspector, centang semua opsi yang diinginkan
4. Drag audio clip ke field "Ambience Clip" (opsional)
5. **Play game** atau klik kanan di script → "Setup All"
6. Done! Semua sudah ter-setup otomatis.

---

## Checklist Target Project

### ✅ Animation (Planet Rotation & Orbit)
- **Script:** `PlanetController.cs`
- Planet berputar pada porosnya (self-rotation)
- Planet mengelilingi matahari (orbit)

### ✅ Lighting & Material
- **Script:** `SunEmission.cs`
- Matahari dengan material Emission (bersinar)
- Point Light otomatis sebagai sumber cahaya
- Efek pulse untuk animasi emission

### ✅ VFX (Trail Renderer)
- **Script:** `TrailSetup.cs`
- Trail Renderer di belakang planet
- Customizable color, width, dan duration

### ✅ Audio (Ambience Space)
- **Script:** `SpaceAmbience.cs`
- Audio ambience yang di-loop
- Fitur fade-in untuk transisi halus

### ✅ Interaktif (UI Slider)
- **Script:** `SpeedController.cs`
- UI Slider untuk kontrol kecepatan planet
- Mengontrol semua planet sekaligus

---

## 🎮 Keyboard Controls (Langsung Berfungsi!)

| Key | Fungsi |
|-----|--------|
| **Space** | Pause / Play |
| **Right Arrow (→)** | Fast Forward (3x speed) |
| **Left Arrow (←)** | Slow Motion (0.3x speed) |
| **R** | Reset ke normal speed |
| **Scroll Mouse** | Zoom in/out |
| **Klik Kanan + Drag** | Pan kamera |
| **Klik Tengah + Drag** | Orbit/rotate kamera |

---

## Cara Setup Manual di Unity

### 1. Setup Matahari (Sun)
1. Pilih objek Matahari di Hierarchy
2. Add Component → `SunEmission`
3. Atur settings:
   - Emission Color: Kuning/oranye
   - Emission Intensity: 2-3
   - Light Intensity: 2
   - Light Range: 100 (sesuaikan dengan ukuran scene)
   - Enable Pulse Emission: ✓

### 2. Setup Planet
1. Pilih objek planet (Bumi/Mars/dll) di Hierarchy
2. Add Component → `PlanetController`
3. Atur settings:
   - Orbit Center: Drag objek Matahari ke sini
   - Orbit Speed: 30 (sesuaikan)
   - Self Rotation Speed: 50 (sesuaikan)

### 3. Setup Trail VFX
1. Pilih objek planet yang ingin diberi trail
2. Add Component → `TrailSetup`
3. Atur settings:
   - Trail Time: 2-3 (durasi trail terlihat)
   - Start Width: 0.3-0.5
   - End Width: 0.05-0.1
   - Start Color: Warna planet dengan alpha 1
   - End Color: Warna planet dengan alpha 0

### 4. Setup Audio Ambience
1. Pilih Main Camera atau buat Empty GameObject
2. Add Component → `SpaceAmbience`
3. Atur settings:
   - Ambience Clip: Drag file `dronehum.aif` atau `dronedarkandscary.aif`
   - Volume: 0.3-0.5
   - Play On Start: ✓
   - Loop: ✓
   - Fade In: ✓

### 5. Setup UI Speed Control
1. Buat Canvas (jika belum ada):
   - Hierarchy → UI → Canvas
   - Set Canvas Scaler ke "Scale With Screen Size"

2. Buat Slider:
   - Klik kanan Canvas → UI → Slider
   - Posisikan di pojok bawah/atas layar
   - Rename jadi "SpeedSlider"

3. Buat Text untuk menampilkan nilai:
   - Klik kanan Canvas → UI → Text - TextMeshPro
   - Posisikan di atas/samping slider
   - Rename jadi "SpeedText"

4. Setup SpeedController:
   - Buat Empty GameObject, rename "SpeedController"
   - Add Component → `SpeedController`
   - Drag SpeedSlider ke field "Speed Slider"
   - Drag SpeedText ke field "Speed Text"
   - Planets akan otomatis terdeteksi

---

## Fitur Tambahan

### Camera Controller
- **Script:** `CameraController.cs`
- Zoom dengan scroll mouse
- Pan dengan klik kanan + drag
- Rotation dengan Alt + klik kiri

### Time Controller
- **Script:** `TimeController.cs`
- Keyboard shortcuts:
  - Space: Pause/Play
  - Right Arrow: Fast Forward
  - Left Arrow: Slow Motion
  - R: Reset

### Planet Info
- **Script:** `PlanetInfo.cs`
- Menampilkan informasi planet saat di-klik
- Setup:
  1. Add Component ke planet
  2. Isi data planet (nama, diameter, dll)
  3. Buat UI Panel untuk info
  4. Link panel ke script

---

## Struktur Hierarki yang Disarankan

```
Scene
├── Main Camera
│   ├── LookAtTarget
│   ├── CameraController
│   └── SpaceAmbience
│
├── Directional Light (opsional, bisa dimatikan)
│
├── Sun
│   ├── SunEmission
│   └── Point Light (dibuat otomatis)
│
├── Planets
│   ├── Earth
│   │   ├── PlanetController
│   │   ├── TrailSetup
│   │   ├── PlanetInfo
│   │   └── ChangeLookAtTarget
│   │
│   ├── Mars
│   │   ├── PlanetController
│   │   ├── TrailSetup
│   │   └── ...
│   │
│   └── ... (planet lainnya)
│
├── Canvas
│   ├── SpeedSlider
│   ├── SpeedText
│   ├── TimeControls (opsional)
│   └── PlanetInfoPanel (opsional)
│
├── SpeedController
└── TimeController
```

---

## Tips Pengaturan

### Kecepatan Orbit Realistis (relatif)

| Objek    | Orbit Center | Orbit Speed | Self Rotation |
|----------|--------------|-------------|---------------|
| Merkurius | Sun         | 47          | 1.5           |
| Venus    | Sun          | 35          | -0.4 (retrograde) |
| Bumi     | Sun          | 30          | 15            |
| Mars     | Sun          | 24          | 15            |
| Bulan    | Bumi         | 50          | 5             |

> **Note:** Bulan mengorbit Bumi, bukan Matahari! Set Orbit Center ke Earth.
---

## Troubleshooting

### Trail tidak muncul
- Pastikan planet bergerak (ada orbit)
- Periksa Trail Time tidak 0
- Periksa alpha color tidak 0

### Audio tidak terdengar
- Pastikan Audio Clip sudah di-assign
- Periksa volume tidak 0
- Periksa Audio Listener ada di camera

### Emission tidak terlihat
- Pastikan menggunakan URP atau Built-in RP yang support emission
- Periksa Post Processing bloom enabled
- Tingkatkan emission intensity
