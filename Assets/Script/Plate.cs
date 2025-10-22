using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    // ごはん量テキスト
    [SerializeField] Text meal_g;

    // ごはん1粒の重さ
    public static float Meal_Weight = 0.5f;

    // 今現在のごはん量（表示する）
    private float Now_gram;


    // Update is called once per frame
    void Update()
    {
        // グラム数を表示する
        meal_g.text = Now_gram.ToString("N2") + "ｇ";
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ごはんにあたった時
        if (collision.gameObject.tag == "Meal")
        {
            // お皿に当たると、今のごはん量に１粒のグラムを足していく
            Now_gram += Meal_Weight;

            // お皿に当たったらご飯が消える
            Destroy(collision.gameObject);
        }
    }

}
