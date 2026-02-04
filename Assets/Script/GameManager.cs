using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
    ステージ１：誤差±15g。よほどのことがない限り、ミスらない。
    ステージ２：誤差± 5g。最初のステージよりはすこしハードに。
    ステージ３：誤差± 7g。デブねこちゃんオンリー。よく食べる。
    ステージ４：誤差± 3g。もっともっとハードになったステージ。
    ステージ５：誤差± 0g。全ねこの出現。ごはんの管理しっかり。
*/

public enum CatReaction {
    PERFECT,        // ぴったり
    WITHIN_MARGIN,  // 誤差の範囲内
    FEW_MANY,       // 少ない・多い
    LESS_MORE       // とても少ない・とても多い
};

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("StageData")]
    // 今のステージ数
    public static int CurrentStage = 1;
    // 「StageData_1」や「StageData_2」をセットする場所
    public List<StageData> allStageData;
    public StageData stageData;

    [Header("ねこデータ")]
    // ネコのプレハブ
    public Cat[] catPrefabs;
    // ネコの出現位置
    public Transform catSpawnPos;
    // ごはんをもらうネコ
    private Cat currentCat;
    // ネコデータベース
    private Cat_DataBase catDB;
    [Tooltip("ステージ１つに出現するネコの数")]
    [SerializeField] private int StageCatCount = 3;
    [Tooltip("今出ているネコが何匹目かのカウント")]
    [SerializeField] private int SpawnedCatCount = 0;

    [Header("ごはんデータ")]
    // ごはんの誤差
    private float Cat_margin;
    // ねこの理想ごはん量
    private float Target_meal;
    // 現在のごはん量
    private float Current_meal;
    // 袋の中のごはん量
    private float Catfood_Capa = 100;
    // 袋の中の最大ごはん量
    private float Catfood_MaxCapa = 100;
    // 無駄にしたごはん量
    private float Catfood_Wasted;

    [Header("ゲーム内テキスト")]
    // ごはん量テキスト
    [SerializeField] TextMeshProUGUI Meal_gram_Text;
    // ごはんの誤差テキスト
    [SerializeField] TextMeshProUGUI Cat_margin_Text;
    // ねこの理想ごはん量テキスト
    [SerializeField] TextMeshProUGUI Target_meal_Text;
    // ねこの感情テキスト
    [SerializeField] TextMeshProUGUI Cat_emotion_Text;
    // ねこのお名前テキスト
    [SerializeField] TextMeshProUGUI Cat_name_Text;
    // 袋のごはん量テキスト
    [SerializeField] TextMeshProUGUI Catfood_Capa_Text;
    // 制限時間表示テキスト
    [SerializeField] TextMeshProUGUI TimeLeft_Text;

    [Header("制限時間")]
    // 残り時間
    private float TimeLeft;
    // タイマーのオン・オフ
    private bool IsTimerActive;

    [Header("メニューUI")]
    [SerializeField] private GameObject MenuClose_Button;
    [SerializeField] private GameObject MenuPanal;

    [Header("ねこの感情Obj")]
    private GameObject cat_lovey;    // ぴったりのとき
    private GameObject cat_happy;    // 誤差以内のとき
    private GameObject cat_unhappy;  // 多い少ないのとき
    private GameObject cat_angry;    // 多すぎ少なすぎのとき

    [Header("感情Prefabs")]
    [SerializeField] GameObject catLoveyPrefab;
    [SerializeField] GameObject catHappyPrefab;
    [SerializeField] GameObject catUnhappyPrefab;
    [SerializeField] GameObject catAngryPrefab;

    [Header("感情UI Parent")]
    [SerializeField] Transform uiParent;

    private Dictionary<string, string> catsName = new Dictionary<string, string>() {
        {"ノルウェージャン" ,"cat_norwegian"},
        {"ベンガル"         ,"cat_bengal"},
        {"サバトラ"         ,"cat_sabatora"},
        {"ハチワレ"         ,"cat_hachiware"},
        {"マンチカン"       ,"cat_munchkin"},
        {"でぶサバトラ"     ,"cat_fat_sabatora"},
        {"でぶハチワレ"     ,"cat_fat_hachiware"},
    };


    // シングルトン設定
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        int stage = StageSelectManager.SelectStageNum;
        stageData = allStageData[stage];

        ApplyStage(stageData);

        // 出てくるネコの数の初期化
        SpawnedCatCount = 0;

        // スコアの初期化
        ScoreManager.Instance.ResetScore();

        // ネコの情報をCat_DataBaseから取得
        catDB = Cat_DataBase.Instance;
        if (catDB == null)
        {
            catDB = FindObjectOfType<Cat_DataBase>();
            Debug.LogError("Cat_DataBase が見つかりません！シーン内にあるか確認して！");
            return;
        }

        // 感情Prefab生成
        CreateEmotionPrefab();

        // 取得したデータをもとにネコを呼び出し！
        RandomSpawn_NextCat();

        //Debug.Log("Current StageData: " + stageData.name);
        Debug.Log("CatName List: " + string.Join(", ", stageData.CatName));
    }

    void ApplyStage(StageData stage)
    {
        Cat_margin = stage.margin;
        TimeLeft = stage.TimeLimit;
        StageCatCount = stage.CatCount;
    }

    void Update()
    {
        // メニューボタンがある時
        if(MenuClose_Button != null)
        {
            // メニュー画面を開いている時
            if (MenuClose_Button.activeSelf == true)
                IsTimerActive = false; // タイマー停止
            else
                IsTimerActive = true;  // タイマー動く
        }
        else
            return;

        // オフの時は、制限時間をしない
        if (!IsTimerActive) return;

        // 残り時間を減らす
        TimeLeft -= Time.deltaTime;
        UpdateTimerUI();

        // 制限時間が０秒になったら
        if (TimeLeft <= 0f)
        {
            TimeLeft = 0f; // ０秒に固定
            IsTimerActive = false; // タイマー動いてないよ！っていうフラグ
            OnTimeUp(); // 時間切れ時の処理
        }
    }

    // ネコをランダムに呼びだす関数
    void RandomSpawn_NextCat()
    {
        // StageData がセットされてない場合
        if (stageData == null)
        {
            Debug.LogError("StageData が設定されていません！");
            return;
        }

        // ステージの猫リストからランダム選択
        if (stageData.CatName.Count == 0)
        {
            Debug.LogError("このStageData には猫リストが登録されていません！");
            return;
        }

        // 登録されている猫の中からランダムに選ぶ
        string name = stageData.CatName[Random.Range(0, stageData.CatName.Count)];

        // CSVからごはん量取得
        float meal = catDB.GetFoodAmount(name);

        // 名前 → プレハブ名 に変換
        if (!catsName.TryGetValue(name, out string prefabKey))
        {
            Debug.LogError($"「{name}」に対応するプレハブ名が見つかりません");
            return;
        }

        // プレハブ検索
        Cat chosenCat = null;

        foreach (var cat in catPrefabs)
        {
            if (cat == null) continue;
            if (cat.name == prefabKey)
            {
                chosenCat = cat;
                break;
            }
        }

        /*
        Cat chosenCat = System.Array.Find(catPrefabs, c => c.name == prefabKey);
        Debug.Log(chosenCat);
        Debug.Log(chosenCat == null);
        */
        if (chosenCat == null)
        {
            Debug.LogError($"プレハブ「{prefabKey}」が見つかりません");
            return;
        }

        // 生成
        currentCat = Instantiate(chosenCat, catSpawnPos.position, Quaternion.identity);
        currentCat.SetCatData(name, meal);

        // 普通かでぶか判断
        if(currentCat.name == "でぶハチワレ" || currentCat.name == "でぶサバトラ")
        {
            SoundManager.instance.RandomPlay_VCF();
        }
        else
        {
            SoundManager.instance.RandomPlay_VCN();
        }

        SpawnedCatCount++;

        // UI更新 & タイマー開始 など
        SetupCatUI(name, meal);
        TimeLeft = stageData.TimeLimit;
        IsTimerActive = true;

        Debug.Log($"{name} が来た！（理想のごはん量：{meal}g）");
    }

    // ねこパネルの初期化
    public void SetupCatUI(string CatName, float CatMeal)
    {
        // CSVの「理想ごはん量」をゲーム内の「目標ごはん量」に
        Target_meal = CatMeal;

        // 0.0gからスタート
        Meal_gram_Text.text = ("0.0g");
        // 誤差を3.0gに
        Cat_margin_Text.text = ($"誤差：±{Cat_margin:F0}g");
        // 袋の重さ表示
        Catfood_Capa_Text.text = ($"{Catfood_Capa:F0}g");
        // ネコの目標グラムを設定
        Target_meal_Text.text = ($"目標グラム：{Target_meal:F0}g");
        // ネコの名前表示
        Cat_name_Text.text = CatName;
        // ネコのお言葉
        Cat_emotion_Text.text = ($"{CatName}はお腹が\n空いているみたい・・・");
    }

    // Plate.csから今のごはん量を受け取って表示
    public void UpdateMealAmount(float Meal_amount)
    {
        // Plate.csから受け取ったごはん量を代入
        Current_meal = Meal_amount;
        // 表示する
        Meal_gram_Text.text = ($"{Current_meal:F1}g");
    }

    // ごはん袋の総グラムの表示
    public void Show_CatfoodCapacity(float food_Capa)
    {
        Catfood_Capa = food_Capa;
        Catfood_Capa_Text.text = $"{Catfood_Capa:F0}g";
    }

    // 無駄にしたごはん量のカウント
    public void Count_CatfoodWasted(float food_Capa)
    {
        Catfood_Wasted += food_Capa;
        //Debug.Log($"無駄にしたごはん：{Catfood_Wasted}");
        //Debug.Log($"今回：{food_Capa} / 累計：{Catfood_Capa}");
    }

    void UpdateTimerUI()
    {
        TimeLeft_Text.text = $"{Mathf.CeilToInt(TimeLeft)} ";

        // 残り5秒で赤くする演出
        if (TimeLeft <= 5f)
            TimeLeft_Text.color = Color.red;
        else
            TimeLeft_Text.color = Color.magenta;
    }

    // 時間切れになったとき
    private void OnTimeUp()
    {
        // 出てきたネコが３匹目なら
        if(SpawnedCatCount >= StageCatCount)
        {
            // ChangeSceneスクリプトを探して呼び出す(非アクティブ対応)
            ChangeScene cs = MenuPanal.GetComponentInChildren<ChangeScene>(true);
            if (cs != null)
            {
                ScoreManager.Instance.CalculateTotal();

                // ここで保存
                ProgressManager.instance.SaveStageHighScore(
                    GameManager.CurrentStage,
                    ScoreManager.Instance.GetTotalScore()
                );

                // 無駄にしたごはん量もここ
                ProgressManager.instance.SaveTotalWastedMeal(
                    ScoreManager.Instance.GetTotalWastedMeal()
                );

                cs.GotoResult();
            }
            else
            {
                Debug.LogWarning("ChangeScene が見つかりません。");
            }

            return;
        }

        // 次のネコへ進む
        PrepareNextCat();
    }

    // １秒後にUpdateEmotionを呼び出す
    public void Late_1s_CallEmotion()
    {
        Invoke(nameof(FeedMeal_End), 1.0f);
    }

    // ごはん　あげ終わり
    private void FeedMeal_End()
    {
        // 目標ごはん量から、今のごはん量を引いた「ごはん量のズレ」
        float diff = Mathf.Abs(Target_meal - Current_meal);

        // ネコの反応を取得
        CatReaction reaction = GetCatReaction(diff);

        // スコアマネージャーへ反応を送る
        ScoreManager.Instance.AddReactionScore(reaction);
        ScoreManager.Instance.AddCombo(reaction);

        // 残りごはんペナルティを送る
        //ScoreManager.Instance.WastedMealPenalty(Catfood_Wasted);
        ScoreManager.Instance.ApplyMealDiffPenalty(diff);

        // 差分をintにして送る
        //ScoreManager.Instance.AddMealDiff(diff);
    }

    // 「ごはん量のズレ」によるネコの感情変化
    public CatReaction GetCatReaction(float diff)
    {
        // Current_meal は「今のごはん量」、Target_meal  は「ねこの目標量」

        // ぴったり
        if (Current_meal == Target_meal)
        {
            Cat_emotion_Text.text = "ごはんがぴったり！\nやったね！";
            // SEの再生
            PlayCatVoiceSE(CatReaction.PERFECT);
            // 感情表現
            ShowCatEmotion(CatReaction.PERFECT);

            return CatReaction.PERFECT;
        }
        // 誤差の範囲内
        else if (diff <= Cat_margin)
        {
            Cat_emotion_Text.text = "ちょうどいいごはんの量！";
            // SEの再生
            PlayCatVoiceSE(CatReaction.WITHIN_MARGIN);
            // 感情表現
            ShowCatEmotion(CatReaction.WITHIN_MARGIN);

            return CatReaction.WITHIN_MARGIN;
        }
        // 誤差が２０ｇを超える
        else if (diff >= 20.0f)
        {
            // とても少ない
            if (Current_meal < Target_meal)
            {
                Cat_emotion_Text.text = "とてもごはんが少ない！";
                // SEの再生
                PlayCatVoiceSE(CatReaction.LESS_MORE);
                // 感情表現
                ShowCatEmotion(CatReaction.LESS_MORE);

                return CatReaction.LESS_MORE;
            }
            // とても多い
            else
            {
                Cat_emotion_Text.text = "とてもごはんが多い！";
                // SEの再生
                PlayCatVoiceSE(CatReaction.LESS_MORE);
                // 感情表現
                ShowCatEmotion(CatReaction.LESS_MORE);

                return CatReaction.LESS_MORE;
            }
        }
        else
        {
            // 少ない
            if (Current_meal < Target_meal)
            {
                Cat_emotion_Text.text = "ごはんが少ない！";
                // SEの再生
                PlayCatVoiceSE(CatReaction.FEW_MANY);
                // 感情表現
                ShowCatEmotion(CatReaction.FEW_MANY);

                return CatReaction.FEW_MANY;
            }
            // 多い
            else
            {
                Cat_emotion_Text.text = "ごはんが多い！";
                // SEの再生
                PlayCatVoiceSE(CatReaction.FEW_MANY);
                // 感情表現
                ShowCatEmotion(CatReaction.FEW_MANY);

                return CatReaction.FEW_MANY;
            }
        }
    }

    // ねこの反応に合わせて、SEを鳴らす
    void PlayCatVoiceSE(CatReaction reaction)
    {
        if (SoundManager.instance == null)
        {
            Debug.LogWarning("SoundManagerが見つからないよ！");
            return;
        }

        // オーディオクリップの削除
        AudioClip clip = null;

        // 反応に合わせてオーディオクリップの再登録
        switch (reaction)
        {
            case CatReaction.PERFECT:
                clip = SoundManager.instance.SE_meowLovey;
                break;
            case CatReaction.WITHIN_MARGIN:
                clip = SoundManager.instance.SE_meowHappy;
                break;
            case CatReaction.FEW_MANY:
                clip = SoundManager.instance.SE_meowUnhappy;
                break;
            case CatReaction.LESS_MORE:
                clip = SoundManager.instance.SE_meowAngry;
                break;
        }

        // clipに登録されているSEがあるときにのみ鳴らす
        if (clip != null)
            SoundManager.instance.SE_audioSource.PlayOneShot(clip);
    }

    // 次のネコの準備をする
    void PrepareNextCat()
    {
        // ねこが３匹目の場合
        if (SpawnedCatCount >= StageCatCount)
            return;

        // 前のネコの削除
        // もし、「今画面に出ているねこ」がいる場合
        if(currentCat != null)
        {
            // そのネコを削除
            Destroy(currentCat.gameObject);
            // ネコがいない状態にする
            currentCat = null;
        }

        // 全ての感情表現を非表示に
        WontShowCatEmotion();

        // 今のごはん量のリセット（Plateから呼び出し）
        Plate plate  = FindObjectOfType<Plate>();
        if (plate != null)
        {
            plate.ResetMealAmount();
        }

        // ねこが変わるとき、ごはんを落とせるように
        FindObjectOfType<Meal_Fall>()?.ResetMealFlag();
        // ここで、袋の中のごはん量を満タンに
        FindAnyObjectByType<Meal_Fall>()?.RefillMealBag();

        // 次のネコを呼び出す
        RandomSpawn_NextCat();
    }

    public void CreateEmotionPrefab()
    {
        // プレハブから生成する
        cat_lovey = Instantiate(catLoveyPrefab, uiParent);
        cat_happy = Instantiate(catHappyPrefab, uiParent);
        cat_unhappy = Instantiate(catUnhappyPrefab, uiParent);
        cat_angry = Instantiate(catAngryPrefab, uiParent);

        WontShowCatEmotion();
    }

    public void WontShowCatEmotion()
    {
        // すべて非表示に
        cat_lovey.SetActive(false);
        cat_happy.SetActive(false);
        cat_unhappy.SetActive(false);
        cat_angry.SetActive(false);
    }

    public void ShowCatEmotion(CatReaction reaction)
    {
        // 一度全て消す
        WontShowCatEmotion();

        if (reaction == CatReaction.PERFECT)
            cat_lovey.SetActive(true);
        else if (reaction == CatReaction.WITHIN_MARGIN)
            cat_happy.SetActive(true);
        else if (reaction == CatReaction.FEW_MANY)
            cat_unhappy.SetActive(true);
        else if (reaction == CatReaction.LESS_MORE)
            cat_angry.SetActive(true);
    }

    // リプレイ時
    public static int GetCurrentStageForReplay()
    {
        return StageSelectManager.SelectStageNum; // そのまま返す
    }

    // 次のステージに進む時
    public static int GetNextStage()
    {
        CurrentStage++; // ステージ番号を進める
        GameManager.instance.stageData = 
            GameManager.instance.allStageData[CurrentStage - 1];

        return CurrentStage;
    }

    public bool isFinalStage()
    {
        return StageSelectManager.SelectStageNum >= allStageData.Count - 1;
    }
}