using UnityEngine;
using UnityEngine.EventSystems;

public class Meal_Fall : MonoBehaviour
{
    // ごはんのプレハブ
    public GameObject Meal_prefab;
    // カップの位置
    public Transform Cup_pos;
    // ごはんを落とす間隔（秒）
    public float dropInterval = 0.025f;

    // ごはん袋の総容量
    public float MealCapacity = 180.0f;

    // ごはんを落とす時間を測るタイマー
    private float Mealtimer = 0f;

    void Update()
    {
        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            // UIの上にカーソルが乗っていなかったら
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // 経過時間を足していく
                Mealtimer += Time.deltaTime;
                // dropIntervalの値になると、ごはんを落としタイマーをリセット
                if (Mealtimer >= dropInterval)
                {
                    DropMeal(); // ごはんを落とす
                    Mealtimer = 0f; // 経過時間のリセット
                }
            }
        }
        else
        {
            // クリックしてないときはタイマーリセット
            Mealtimer = 0f;
        }

        // マウスから手を離したとき、
        if (Input.GetMouseButtonUp(0))
        {
            // UIの上にカーソルが乗っていなかったら
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // 「ごはん量のズレ」によるネコの感情変化
                GameManager.instance.Late_1s_CallEmotion();
            }
        }
    }

    void DropMeal()
    {        
        // 袋のごはんが0gを下回ったら
        if(MealCapacity <= 0)
        {
            Debug.Log("ごはんが無いよ！");
            // 0gにして、もうごはんが出ないようにする
            MealCapacity = 0.0f;
            return;
        }

        // ランダムにごはんを落とす座標を決める
        Vector3 randomOffset = new Vector3(
            Random.Range(-0.3f, 0.3f),
            Random.Range(-0.1f, 0.1f),
            0
        );

        // 決めた座標を、ごはんをおとす座標にする
        Vector3 spawnPos = Cup_pos.position + randomOffset;

        // ごはんプレハブを生成
        GameObject meal = Instantiate(Meal_prefab, spawnPos, Quaternion.identity);

        // 袋の中からごはん１粒分の量を減らす
        MealCapacity -= Plate.Meal_weight;

        // 今のグラムをGamemanager.csに伝えて表示してもらう
        GameManager.instance.show_CatfoodCapacity(MealCapacity);

        // ごはんプレハブにRigidbody2Dをつける
        Rigidbody2D rb = meal.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // ごはんに加える力をランダムに決める
            Vector2 force = new Vector2(Random.Range(-1f, 1f), Random.Range(-2f, -5f));
            // 決めた力をごはんに加える
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public void ResetMealCapacity()
    {
        // 袋のごはん量をリセットする
        MealCapacity = 180.0f;
    }
}