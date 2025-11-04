
using System.Collections;
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

    [Header("ごはんデータ")]
    // ごはんの誤差
    public float Cat_margin = 3.0f;
    // ねこの理想ごはん量
    private float Target_meal;
    // 現在のごはん量
    private float Current_meal;

    private Dictionary<string, string> nameMap = new Dictionary<string, string>
{
    {"ノルウェージャン", "cat_norwegian"},
    {"ラグドール", "cat_ragdoll"},
    {"ベンガル", "cat_bengal"},
    {"サバトラ", "cat_sabatora"},
    {"チャトラ", "cat_chatara"},
    {"ハチワレ", "cat_hachiware"},
    {"アメショー", "cat_american"},
    {"クロネコ", "cat_black"},
    {"シロネコ", "cat_white"},
    {"マンチカン", "cat_munchkin"},
    {"スコティッシュ", "cat_scottish"}
};



    // シングルトン設定
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        catDB = Cat_DataBase.Instance;
        RandomCatSpawn();
    }

    // ネコをランダムに呼びだす関数
    void RandomCatSpawn()
    {
        if (catPrefabs.Length == 0)
        {
            Debug.LogError("ネコのプレハブが設定されていません！");
            return;
        }

        // CSVからランダムで名前とごはん量を取得
        string name = catDB.GetRandomCatName();
        float meal = catDB.GetFoodAmount(name);

        // プレハブの中からねこの名前と一致するものを探す
        Cat chosenCat = System.Array.Find(catPrefabs,c=>c.name== name);

        if (nameMap.TryGetValue(name, out string prefabKey))
        {
            chosenCat = System.Array.Find(catPrefabs, c => c.name == prefabKey);
        }
        else
        {
            Debug.LogError($"「{name}」に対応する英語名が見つかりません。");
            return;
        }

        // ネコを出現！
        currentCat = Instantiate(chosenCat, catSpawnPos.position, Quaternion.identity);
        currentCat.SetCatData(name, meal);

        // CSVの「理想ごはん量」をゲーム内の「目標ごはん量」に
        Target_meal = meal;

        // 0.0gからスタート
        Meal_gram_Text.text = ("0.0g");
        // 誤差を3.0gに
        Cat_margin_Text.text = ($"誤差：±{Cat_margin:F0}g");
        // ネコの目標グラムを設定
        Target_meal_Text.text = ($"目標グラム：{Target_meal:F0}g");
        // ネコのお言葉
        Cat_emotion_Text.text = ($"{name}は\nお腹を空かせている・・・");
        // ネコのお名前
        Cat_name_Text.text = ($"{name}");


        Debug.Log($"{name} が来たよ！（理想のごはん量：{meal}g）");
    }

    // Plate.csから今のごはん量を受け取って表示
    public void UpdateMealAmount(float Meal_amount)
    {
        // Plate.csから受け取ったごはん量を代入
        Current_meal = Meal_amount;
        // 表示する
        Meal_gram_Text.text = ($"{Current_meal:F1}g");
    }

    public void OnDecideButton_Click()
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
            Debug.Log("ピッタリ！ネコが喜んでる！");
            // 満面のにゃん
        }
        else if (diff <= Cat_margin)
        {
            // 誤差の範囲内
            Cat_emotion_Text.text = "ちょうどいいごはんの量！";
            Debug.Log("ちょうどいい！ネコが喜んでる！");
            // にこにこ
        }
        else if (Current_meal < Target_meal)
        {
            // すくない
            Cat_emotion_Text.text = "ごはんが少ない！";
            Debug.Log("少なかったみたい…");
            // しょんぼり
        }
        else
        {
            // おおい！
            Cat_emotion_Text.text = "ごはんが多い！";
            Debug.Log("多すぎた！");
            // ムッおこ
        }
    }

    // Plate から判定結果を伝える関数
    public void DecideCatMeal(float diff, float margin)
    {
        if (currentCat != null)
        {
            currentCat.ReactToMeal(diff, margin);
        }
    }

    public Cat GetCurrentCat()
    {
        return currentCat;
    }
}
