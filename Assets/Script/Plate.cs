using UnityEngine;

public class Plate : MonoBehaviour
{
    // ごはん1粒の重さ
    public static float Meal_weight = 2.5f;
    // 今現在のごはん量（表示する）
    private float Now_gram;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ごはんが当たった時
        if (collision.gameObject.tag == "Meal")
        {
            // SEの再生
            SoundManager.instance.RandomPlay_HPM();

            // お皿に当たると、今のごはん量に１粒のグラムを足していく
            Now_gram += Meal_weight;

            // 今のグラムをGamemanager.csに伝えて表示してもらう
            GameManager.instance.UpdateMealAmount(Now_gram);

            // お皿に当たったらご飯が消える
            Destroy(collision.gameObject);
        }
    }

    public void ResetMealAmount()
    {
        // 今のごはん量をリセットする
        Now_gram = 0.0f;

        // 今のグラムをGamemanager.csに伝えて表示してもらう
        GameManager.instance.UpdateMealAmount(Now_gram);
    }
}
