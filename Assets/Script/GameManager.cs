using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("ネコデータ関連")]
    // ネコのプレハブ
    public Cat[] catPrefabs;
    // ネコの出現位置
    public Transform catSpawnPos;
    // ごはんをもらうネコ
    private Cat currentCat;
    // ネコデータベース
    private Cat_DataBase catDB;
    // ステージ１つに出現するネコの数
    [SerializeField] private int StageCatCount = 3;
    // 今出ているネコが何匹目かのカウント
    [SerializeField] private int SpawnedCatCount = 0;

    [Header("テキスト")]
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

    [Header("ごはんデータ")]
    // ごはんの誤差
    public float Cat_margin = 3.0f;
    // ねこの理想ごはん量
    private float Target_meal;
    // 現在のごはん量
    private float Current_meal;
    // 袋の中のごはん量
    private float Catfood_Capa;

    [Header("制限時間")]
    // ネコ１匹の制限時間
    public float CatTimeLimit = 10.0f;
    // 残り時間
    private float TimeLeft;
    // タイマーのオン・オフ
    private bool IsTimerActive;

    [Header("メニューUI")]
    [SerializeField] private GameObject MenuClose_Button;
    [SerializeField] private GameObject MenuPanal;

    private Dictionary<string, string> catsName = new Dictionary<string, string>() {
        {"ノルウェージャン","cat_norwegian"},
        {"ラグドール","cat_ragdoll"},
        {"ベンガル","cat_bengal"},
        {"サバトラ","cat_sabatora"},
        {"チャトラ","cat_chatora"},
        {"ハチワレ","cat_hachiware"},
        {"アメショー","cat_american"},
        {"クロネコ","cat_black"},
        {"シロネコ","cat_white"},
        {"マンチカン","cat_munchkin"},
        {"スコティッシュ","cat_scottish"},
    };


    // シングルトン設定
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        // シーン開始時にテキストUIなどを再取得
        // ねこの出現位置
        if (catSpawnPos == null)
        {
            var catPos = GameObject.Find("Cat_SpawnPos");
            if(catPos != null)
                catSpawnPos = catPos.GetComponent<Transform>();
            else
                Debug.LogError("Cat_SpawnPosが見つからない！");
        }
        else
        {
            //Debug.LogWarning("Cat_SpawnPosはあるよ！");
        }

        // ごはん量テキスト
        if (Meal_gram_Text == null)
        {
            Meal_gram_Text = GameObject.Find("Meal_Gram")?.GetComponent<TextMeshProUGUI>();
            Debug.Log("Meal_Gramを設定！");
        }
            
        // ごはんの誤差テキスト
        if (Cat_margin_Text == null)
        {
            Cat_margin_Text = GameObject.Find("Catmargin")?.GetComponent<TextMeshProUGUI>();
            Debug.Log("Catmarginを設定！");
        }
        // ねこの理想ごはん量テキスト
        if (Target_meal_Text == null)
        {
            Target_meal_Text = GameObject.Find("targetMeal")?.GetComponent<TextMeshProUGUI>();
            Debug.Log("targetMealを設定！");
        }
        // ねこの感情テキスト
        if (Cat_emotion_Text == null)
        {
            Cat_emotion_Text = GameObject.Find("Cat_Emotion")?.GetComponent<TextMeshProUGUI>();
            Debug.Log("Cat_Emotionを設定！");
        }
        // ねこのお名前テキスト
        if (Cat_name_Text == null)
        {
            Cat_name_Text = GameObject.Find("CatName")?.GetComponent<TextMeshProUGUI>();
            Debug.Log("CatNameを設定！");
        }
        // 袋のごはん量テキスト
        if (Catfood_Capa_Text == null)
        {
            Catfood_Capa_Text = GameObject.Find("Catfood_Gram")?.GetComponent<TextMeshProUGUI>();
            Debug.Log("Catfood_Gramを設定！");
        }
        // 制限時間表示テキスト
        if (TimeLeft_Text == null)
        {
            TimeLeft_Text = GameObject.Find("CatTimeLimit")?.GetComponent<TextMeshProUGUI>();
            Debug.Log("CatTimeLimitを設定！");
        }

        // メニューUI
        if (MenuClose_Button == null)
        {
            MenuClose_Button = GameObject.Find("MenuClose_Button")?.GetComponent<GameObject>();
            Debug.Log("MenuClose_Buttonを設定！");
        }
        if (MenuPanal == null)
        {
            MenuPanal = GameObject.Find("MenuPanal")?.GetComponent<GameObject>();
            Debug.Log("MenuPanalを設定！");
        }

    }

    void Start()
    {
        // 出てくるネコの数の初期化
        SpawnedCatCount = 0;
        // ネコの情報をCat_DataBaseから取得
        catDB = Cat_DataBase.Instance;
        // 取得したデータをもとにネコを呼び出し！
        RandomSpawn_NextCat();
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
        if (catPrefabs.Length == 0)
        {
            Debug.LogError("ネコのプレハブが設定されていません！");
            return;
        }

        // プレハブをランダムに選択
        int index = Random.Range(0, catPrefabs.Length);

        // CSVからランダムで名前とごはん量を取得
        string name = catDB.GetRandomCatName();
        float meal = catDB.GetFoodAmount(name);

        // まずは Dictionary でプレハブ名に変換
        if (!catsName.TryGetValue(name, out string prefabKey))
        {
            Debug.LogError($"「{name}」に対応する英語名が見つかりません。");
            return;
        }

        // プレハブの中から対応するものを探す
        Cat chosenCat = System.Array.Find(catPrefabs, c => c.name == prefabKey);

        // ネコを出現！
        currentCat = Instantiate(chosenCat, catSpawnPos.position, Quaternion.identity);
        currentCat.SetCatData(name, meal);

        // ステージに出現するネコの数を増やす
        SpawnedCatCount++;

        TimeLeft = CatTimeLimit;
        IsTimerActive = true;

        // CSVの「理想ごはん量」をゲーム内の「目標ごはん量」に
        Target_meal = meal;

        // 0.00gからスタート
        Meal_gram_Text.text = ("0.0g");
        // 誤差を3.0gに
        Cat_margin_Text.text = ($"誤差：±{Cat_margin:F0}g");
        // ネコの目標グラムを設定
        Target_meal_Text.text = ($"目標グラム：{Target_meal:F0}g");
        // ネコの名前表示
        Cat_name_Text.text = ($"{name}");
        // ネコのお言葉
        Cat_emotion_Text.text = ($"{name}はお腹が\n空いている・・・");
        // 袋の重さ表示
        Catfood_Capa_Text.text = ($"180g");


        Debug.Log($"{name} が来た！（理想のごはん量：{meal}g）");
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
    public void show_CatfoodCapacity(float food_Capa)
    {
        Catfood_Capa = food_Capa;
        Catfood_Capa_Text.text = ($"{Catfood_Capa:F0}g");
    }

    void UpdateTimerUI()
    {
        TimeLeft_Text.text = $"{Mathf.CeilToInt(TimeLeft)} 秒";

        // 残り5秒で赤くする演出
        if (TimeLeft <= 5f)
            TimeLeft_Text.color = Color.red;
        else
            TimeLeft_Text.color = Color.magenta;
    }

    // 時間切れになったとき
    private void OnTimeUp()
    {
        Debug.Log("制限時間終了！");

        // 出てきたネコが３匹目なら
        if(SpawnedCatCount >= StageCatCount)
        {
            Debug.Log("全てのネコにごはんをあげた！");

            // ChangeSceneスクリプトを探して呼び出す(非アクティブ対応)
            ChangeScene cs = MenuPanal.GetComponentInChildren<ChangeScene>(true);
            if (cs != null)
            {
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

        UpdateEmotion(diff);
    }
    // 「ごはん量のズレ」によるネコの感情変化
    public void UpdateEmotion(float diff)
    {
        if (Current_meal == Target_meal)
        {
            // ぴったり
            Cat_emotion_Text.text = "ぴったりのごはん！やった！";
            // 満面のにゃん
        }
        else if (diff <= Cat_margin)
        {
            // 誤差の範囲内
            Cat_emotion_Text.text = "ちょうどいいごはんの量！";
            // にこにこ
        }
        else if (Current_meal < Target_meal)
        {
            // すくない
            Cat_emotion_Text.text = "ごはんが少ない！";
            // しょんぼり
        }
        else
        {
            // おおい！
            Cat_emotion_Text.text = "ごはんが多い！";
            // ムッおこ
        }
    }

    // 次のネコの準備をする
    void PrepareNextCat()
    {
        // ・前のネコの削除
        // もし、「今画面に出ているねこ」がいる場合
        if(currentCat != null)
        {
            // そのネコを削除
            Destroy(currentCat.gameObject);
            // ネコがいない状態にする
            currentCat = null;
        }

        // 今のごはん量のリセット（Plateから呼び出し）
        Plate plate  = FindObjectOfType<Plate>();
        if (plate != null)
        {
            plate.ResetMealAmount();
        }

        // 総ごはん量のリセット（meal_fallから呼び出し）
        Meal_Fall meal_Fall = FindObjectOfType<Meal_Fall>();
        if (meal_Fall != null)
        {
            meal_Fall.ResetMealCapacity();
        }

        // 次のネコを呼び出す
        RandomSpawn_NextCat();
    }
}