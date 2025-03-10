
---

# Super Mario VR — (IDVR '24 Midterm Project)

本專案為一款結合 **Seated-Walking-in-Place (Seated-WIP)** 技術與 **Electrical Muscle Stimulation (EMS)** 所打造的 VR 版「超級瑪利歐」遊戲。玩家可透過真實的走路與跳躍動作，配合 EMS 提供的觸覺回饋，更沉浸地體驗經典瑪利歐關卡。

## 目錄
- [專案簡介](#專案簡介)
- [主要特色與功能](#主要特色與功能)
  - [Task 1: Walk & Run & Stair](#task-1-walk--run--stair)
  - [Task-2: Jump](#task-2-jump)
  - [Task-3: Uphill & Downhill](#task-3-uphill--downhill)
  - [Task-4: Teleport](#task-4-teleport)
  - [Task-5: Switch Lane & Fight the Monster/Boss](#task-5-switch-lane--fight-the-monsterboss)
- [優點與檢討](#優點與檢討)
- [工作分配](#工作分配)
- [影片與專案連結](#影片與專案連結)
- [未來改進方向](#未來改進方向)
- [授權與版權](#授權與版權)

---

## 專案簡介

- **組別：** Group 2 - EMS Mario  
- **成員：** 黃永恩、顏紹同、王婉怡、施孟廷

**設計動機：**  
「超級瑪利歐」是一款跨世代經典遊戲，搭配 VR 能帶來更強的沉浸感。然而，VR 常見的缺點是容易造成暈眩或動暈症，尤其在移動過程中無法有真實的觸覺回饋。為此，我們嘗試使用 **Seated-WIP**（坐姿原地行走）與 **EMS**（肌肉電刺激）兩項技術，讓玩家透過腳部或身體的自然動作控制遊戲角色，並在行走、跳躍、上下坡等情境時提供適度的肌肉刺激，增加動作時的臨場感與真實感。

---

## 執行方式

1. **專案下載 / Clone：**  
   ```bash
   git clone https://github.com/mikaiyen/EMS_VRMario.git
   ```
    **開啟 Unity：**  
   - 使用Unity 2022.3.22f1 新增專案。 
   - 將本專案全部內容複製貼上到Assets資料夾
2. **雲端下載**  
    詳見最後的雲端連結下載

---

## 主要特色與功能

### Task 1: Walk & Run & Stair
- **Walk**：玩家交替踩左右腳前掌，驅動角色向前行走。  
- **Run**：玩家在踏步的同時擺動手或控制器，可加速角色移動速度。  
- **Backward**：玩家交替踩左右腳後腳跟，可向後移動。  
- **Stair**：當場景中有階梯物件，玩家在 Seated-WIP 動作中能平順地登上階梯。

### Task 2: Jump
- 若玩家右腳抬起、左腳踩地（或相反），觸發角色向前跳躍。  
- **EMS 效果**：在跳躍瞬間刺激腿部肌肉，模擬真實起跳時的感受。  
- 可用於撞擊問號方塊（Mystery Box）或踩怪（如經典瑪利歐玩法）。

### Task 3: Uphill & Downhill
- **Uphill**：EMS 抬高前腳掌，增加玩家向前踏步的「難度」，模擬登坡的真實感。  
- **Downhill**：EMS 抬高腳跟，降低移動阻力，模擬下坡更輕鬆的行走體驗。

### Task 4: Teleport
- 場景中設置了經典「水管 (Pipe)」的傳送機制。  
- 玩家必須跳到水管上，短暫抬起雙腳，再落地即可觸發傳送到另一個水管。

### Task 5: Switch Lane & Fight the Monster/Boss
- 為了保持玩家在固定軌道並強化 Seated-WIP 的體驗，設計「頭部傾斜」實現左右換線 (Lane Switching)。  
- 場景中有小怪與 Boss：
  - **普通敵人**：可透過跳躍踩擊或使用道具攻擊。  
  - **Boss**：能發射飛彈攻擊；玩家需閃避並可使用槍枝反擊。

---

## 優點與檢討

- **優點：**  
  1. **真實感**：利用 EMS 讓玩家感受到行走、跳躍、上下坡的阻力差異，減少動暈症。  
  2. **高互動性**：結合瑪利歐經典玩法（撞箱子、踩怪、躲避飛彈）並融入 VR 操控特性。  
  3. **沉浸度**：Seated-WIP 與手擺動跑步、EMS 震動搭配，更貼近真實運動體驗。

- **反思與缺點：**  
  1. **EMS 整合度**：刺激時機與強度難以完美匹配動作。例如跳躍過程中 EMS 應該在起跳瞬間才介入，而非全程刺激。  
  2. **教學介面不足**：目前遊戲流程簡單明瞭，但缺乏完整的新手教學或動作引導，可能導致第一次體驗的玩家不易上手。  
  3. **系統校正**：Seated-WIP 與 EMS 的參數需要更精細的調整，才能讓上下坡及跑步時的阻力感受更連貫。

---

## 影片與專案連結

- **Demo Video**:  
  [https://youtu.be/vRl3PtsoIu4](https://youtu.be/vRl3PtsoIu4)

- **Unity 專案下載**:  
  [Google Drive Link](https://drive.google.com/file/d/11X4ioelfF-pqmztf3fJfh9L2beoHijzy/view?usp=sharing)

如連結失效或需其他檔案取得方式，請聯繫專案成員。

---

## 未來改進方向
1. **EMS 參數自動校正**：根據使用者體型或動作幅度，自動調整電刺激強度與施放時機。  
2. **更完善的新手教學**：在遊戲初始階段加入引導訊息或互動教學，降低操作難度。  
3. **拓展更多關卡**：可增加更多經典瑪利歐元素，如公主城堡、更多地形與道具，使遊戲更加多元。  
4. **加強多人互動**：未來可嘗試多人連線或合作模式，增添遊戲樂趣。

---

## 授權與版權

本專案為課程期中作業使用，部分素材（音效、模型、圖片等）可能來自官方或第三方資源，其版權歸原作者所有。若要重複使用或公開發行，請先確認授權及相關條款。  

---  