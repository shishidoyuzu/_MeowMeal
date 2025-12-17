using UnityEngine;

public class Plate : MonoBehaviour
{
    // ごはん1粒の重さ
    public static float Meal_weight = 2.5f;
    // 今現在のごはん量（表示する）
    private float Now_gram;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ごはんにあたった時
        if (collision.gameObject.tag == "Meal")
        {
            // お皿に当たると、今のごはん量に１粒のグラムを足していく
            Now_gram += Meal_weight;

            // SEの再生
            if (SoundManager.instance != null)
                SoundManager.instance.SE_audioSource.PlayOneShot(SoundManager.instance.SE_fallPlate);
            else
                Debug.LogWarning("SoundManagerが見つからないよ！");

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
