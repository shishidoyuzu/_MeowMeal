using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    // ごはん量テキスト
    [SerializeField] TextMeshProUGUI meal_g;

    // ごはん1粒の重さ
    public static float Meal_weight = 0.25f;

    // 今現在のごはん量（表示する）
    private float Now_gram;

    // ごはんの誤差
    public float Cat_margin = 3.0f;

    // 出ているネコの規定ごはん量
    private int Target_Meal;

    // ねこの感情テキスト
    [SerializeField] TextMeshProUGUI Cat_emotion_Text;

    void Start()
    {
        // テスト用に固定（今回はハチワレ）
        Target_Meal = Cat_DataBase.Instance.GetFoodAmount("ハチワレ");
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


    public void DecideMeal()
    {
        // ゲーム内の鵜土岐では、「このくらいにする！」ボタンを押した時点で
        // ネコがごはんを食べる描写が入り、ネコの感情表現になる。
        // そして、そのまま次のネコへと進んでいく


        // ごはん量のズレ
        float diff = Now_gram - Target_Meal;

        if (Mathf.Abs(diff) <= Cat_margin)
        {
            // ぴったり
            Cat_emotion_Text.text = "ちょうどいい！";
            Debug.Log("ちょうどいい！ネコが喜んでる！");
        }
        else if (diff < 0)
        {
            // すくない
            Cat_emotion_Text.text = "少ない！";
            Debug.Log("少なかったみたい…");
        }
        else
        {
            // おおい！
            Cat_emotion_Text.text = "多い！";
            Debug.Log("多すぎた！");
        }
    }
}
