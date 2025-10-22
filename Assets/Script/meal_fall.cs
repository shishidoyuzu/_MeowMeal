using UnityEngine;
using UnityEngine.EventSystems;

public class meal_fall : MonoBehaviour
{
    // ごはんのプレハブ
    public GameObject Meal_prefab;
    // カップの位置
    public Transform CupPoint_pos;

    public int mealCount = 10; // 一度に落とすごはんの数
    public float dropInterval = 0.3f; // ごはんを落とす間隔（秒）

    //private float timer = 0f;

    void Update()
    {
        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            // UIの上にカーソルが乗っていなかったら
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                /*
                timer += Time.deltaTime;

                if (timer >= dropInterval)
                {
                    DropMeal();
                    timer = 0f;
                }
            }
        }
        else
        {
            timer = 0f; // クリックしてないときはタイマーリセット
        }
                */

                // 一定の時間が経過するごとに
                if (Time.frameCount % 100 == 0)
                {               
                    Debug.Log("左クリック！ごはんが出るよ！");
                    DropMeal();
                }
                else
                {
                    Debug.Log("UIの上だからクリック無効！");
                }
            }
        }


        void DropMeal()
        {
            for (int i = 0; i < mealCount; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-0.3f, 0.3f),
                    Random.Range(-0.1f, 0.1f),
                    0
                );

                Vector3 spawnPos = CupPoint_pos.position + randomOffset;

                GameObject meal = Instantiate(Meal_prefab, spawnPos, Quaternion.identity);

                Rigidbody2D rb = meal.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 force = new Vector2(Random.Range(-1f, 1f), Random.Range(-2f, -5f));
                    rb.AddForce(force, ForceMode2D.Impulse);
                }
            }
        }
    }
}

/*
    うまくいかなかったので今回は不使用
    RaycastHit2D raycastHit2D…レイが何かに当たった場合の
                               情報（位置や当たったオブジェクトなど）を格納する変数。

    Physics2D.Raycast()…Unityの2D物理エンジンでレイキャストを行う関数。

    ～引数～
        (Vector2)ray.origin…レイの開始位置。
        (Vector2)ray.direction…レイの進む方向。
 */
