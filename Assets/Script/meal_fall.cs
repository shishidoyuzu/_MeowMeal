using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Meal_Fall : MonoBehaviour
{
    // ごはんのプレハブ
    public GameObject Meal_prefab;
    // カップの位置
    public Transform Cup_pos;
    // ごはんを落とす間隔（秒）
    public float DropInterval = 0.15f;
    // ごはん袋の総容量
    public float MealCapacity = 100.0f;
    // ごはんを落とす時間を測るタイマー
    private float Mealtimer = 0f;
    // ごはんを一度だけあげるフラグ
    private bool HasGivenMeal = false;
    // ごはんをあげ始めたか
    bool HasStartedDrop = false;
    // 離した後もごはんを落とす時間
    [SerializeField] private float AfterDropTime = 0.5f;
    // 余韻時間
    private float AfterTimer = 0f;

    [SerializeField] Slider s_MealCapacity;

    UpDown updown;

    void Start()
    {
        updown = FindObjectOfType<UpDown>();

        // 値の初期化
        s_MealCapacity.minValue = 0f;
        s_MealCapacity.maxValue = 100f;
        s_MealCapacity.value = MealCapacity;

    }

    void Update()
    {
        // UIの上にカーソルが乗っていなかったら
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // 右クリック
            bool isHolding = Input.GetMouseButton(0);
            // 左クリック
            bool isRefill = Input.GetMouseButtonDown(1);
            // ごはんが落ちているか
            bool isCanFall = false;

            // 左クリックしているとき
            if (isHolding)
            {
                // 押している間は余韻リセット
                AfterTimer = AfterDropTime;
                isCanFall = true;
                HasStartedDrop = true;
            }
            else if (AfterTimer > 0f)
            {
                AfterTimer -= Time.deltaTime;
                isCanFall = true;
            }
            else
            {
                Mealtimer = 0f;
            }

            // ごはんをあげれるとき
            if (isCanFall)
            {
                HandleDrop();
            }
            // ごはんが落ち切った時
            else if (HasStartedDrop && !HasGivenMeal)
            {
                // 「ごはん量のズレ」によるネコの感情変化
                GameManager.instance.Late_1s_CallEmotion();
                // ごはんをこれ以上落とせないように
                HasGivenMeal = true;
            }

            // 右クリックしたとき
            if (isRefill)
            {
                //Debug.Log("ごはんを新しくするよ！");
                // 残っているごはんをGameManagerに送る
                GameManager.instance.Count_CatfoodWasted(MealCapacity);
                // ごはん袋を満タンにする
                RefillMealBag();
                // 表示を更新
                GameManager.instance.Show_CatfoodCapacity(MealCapacity);
            }
        }
    }

    void HandleDrop()
    {
        Mealtimer += Time.deltaTime;

        if (Mealtimer >= DropInterval)
        {
            DropMeal();
            Mealtimer = 0f;
        }
    }

    void DropMeal()
    {
        // hasGivenMealがtrueのとき、ごはんを落とさない
        if (HasGivenMeal) return;

        // ごはん袋が動いていたら、落とさない
        if(updown.isMoving) return;

        // 袋のごはんが0gを下回ったら
        if (MealCapacity <= 0f)
        {
            //Debug.Log("ごはんが無いよ！");
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

        if(s_MealCapacity != null)
        {
            s_MealCapacity.value = MealCapacity;
        }

        // 今のグラムをGamemanager.csに伝えて表示してもらう
        GameManager.instance.Show_CatfoodCapacity(MealCapacity);
    }

    void RefillMealBag()
    {
        MealCapacity = 100.0f;

        if (s_MealCapacity != null)
        {
            s_MealCapacity.value = MealCapacity;
        }
    }

    public void ResetMealFlag()
    {
        HasGivenMeal = false;
        HasStartedDrop = false;
    }
}