using Unity.VisualScripting;
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
    public float MealCapacity = 200.0f;

    // ごはんを落とす時間を測るタイマー
    private float Mealtimer = 0f;

    // ごはんを一度だけあげるフラグ
    private bool hasGivenMeal = false;

    void Update()
    {
        // 左クリックしたとき＆ごはん袋が０ｇになっていなかったら
        if (Input.GetMouseButton(0) && MealCapacity > 0f)
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

        // 右クリックしたとき
        if (Input.GetMouseButton(1))
        {
            Debug.Log("ごはんを新しくするよ！");
            // ごはん袋を満タンにする
            RefillMealBag();
            GameManager.instance.Show_CatfoodCapacity(MealCapacity);
        }

        // マウスから手を離したとき＆ごはんをあげてなかったら
        if (Input.GetMouseButtonUp(0) && !hasGivenMeal)
        {
            // UIの上にカーソルが乗っていなかったら
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // 「ごはん量のズレ」によるネコの感情変化
                GameManager.instance.Late_1s_CallEmotion();
                // ごはんをこれ以上落とせないように
                hasGivenMeal = true;
            }
        }
    }

    void DropMeal()
    {
        // hasGivenMealがtrueのとき、ごはんを落とさない
        if (hasGivenMeal) return;

        // 袋のごはんが0gを下回ったら
        if (MealCapacity <= 0f)
        {
            Debug.Log("ごはんが無いよ！");
            // 0gにして、もうごはんが出ないようにする
            MealCapacity = 0.0f;
            return;
        }

        UpdateMealBag();

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

    void UpdateMealBag()
    {
        // 袋の中からごはん１粒分の量を減らす
        MealCapacity -= Plate.Meal_weight;

        // 今のグラムをGamemanager.csに伝えて表示してもらう
        GameManager.instance.Show_CatfoodCapacity(MealCapacity);
    }

    void RefillMealBag()
    {
        MealCapacity = 200.0f;
    }

    public void ResetMealFlag()
    {
        hasGivenMeal = false;
    }
}