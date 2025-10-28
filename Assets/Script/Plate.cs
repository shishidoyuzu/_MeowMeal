using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    // ごはん1粒の重さ
    public static float Meal_weight = 0.25f;

    // 今現在のごはん量（表示する）
    private float Now_gram;
    // ごはん量テキスト
    [SerializeField] TextMeshProUGUI meal_g;

    // ごはんの誤差
    public float Cat_margin = 3.0f;
    // ごはんの誤差テキスト
    [SerializeField] TextMeshProUGUI Cat_margin_Text;

    // ねこの理想ごはん量
    private int Target_Meal;
    // ねこの理想ごはん量テキスト
    [SerializeField] TextMeshProUGUI Target_Meal_Text;

    // ねこの感情テキスト
    [SerializeField] TextMeshProUGUI Cat_emotion_Text;

    private GameManager gameManager;

    void Start()
    {
        // テスト用に固定（今回はハチワレ）
        Target_Meal = Cat_DataBase.Instance.GetFoodAmount("ハチワレ");

        Target_Meal_Text.text = "目標グラム：" + Target_Meal + "g";
        Cat_margin_Text.text = "誤差：" + Cat_margin + "g";

        Debug.Log("目標グラム：" + Target_Meal);
    }

    void Update()
    {
        // グラム数を表示する
        meal_g.text = Now_gram.ToString("N2") + "g";
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ごはんにあたった時
        if (collision.gameObject.tag == "Meal")
        {
            // お皿に当たると、今のごはん量に１粒のグラムを足していく
            Now_gram += Meal_weight;

            // お皿に当たったらご飯が消える
            Destroy(collision.gameObject);
        }
    }

    public void Reset_Meal()
    {
        Now_gram = 0.0f;
    }

    public void Meal_35()
    {
        Now_gram = 35.0f;
    }

    public void DecideMeal()
    {
        // ゲーム内では、「このくらいにする！」ボタンを押した時点で
        // ネコがごはんを食べる描写が入り、ネコの感情表現になる。

        // ごはん量のズレ
        float diff = Now_gram - Target_Meal;

        if (Now_gram == Target_Meal)
        {
            // ぴったり
            Cat_emotion_Text.text = "ぴったりのごはん！やった！";
            Debug.Log("ピッタリ！ネコが喜んでる！");
            // 満面のにゃん
        }
        else if (Mathf.Abs(diff) <= Cat_margin)
        {
            // 誤差の範囲内
            Cat_emotion_Text.text = "ちょうどいいごはんの量！";
            Debug.Log("ちょうどいい！ネコが喜んでる！");
            // にこにこ
        }
        else if (diff < 0)
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

        // 感情描画が終われば次のネコへ

        // ネコに反応させる
        if (gameManager != null)
        {
            gameManager.DecideCatMeal(diff, Cat_margin);
        }
    }
}
