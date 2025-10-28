using UnityEngine;
using UnityEngine.EventSystems;

public class Meal_Fall : MonoBehaviour
{
    // ごはんのプレハブ
    public GameObject Meal_prefab;
    // カップの位置
    public Transform Cup_pos;
    // ごはんを落とす間隔（秒）
    public float dropInterval = 0.1f;

    public GameObject Decide_Button;

    // 時間を測るタイマー
    private float timer = 0f;

    void Update()
    {
        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            // UIの上にカーソルが乗っていなかったら
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // マウスの左ボタンを離したとき「これくらいにする」ボタンを非表示に
                Decide_Button.SetActive(false);

                // 経過時間を足していく
                timer += Time.deltaTime;
                // dropIntervalの値になると、ごはんを落としタイマーをリセット
                if (timer >= dropInterval)
                {
                    DropMeal(); // ごはんを落とす
                    Debug.Log("左クリック！ごはんが出るよ！");
                    timer = 0f; // 経過時間のリセット
                }
            }
        }
        else
        {
            //「これくらいにする」ボタンを表示
            Decide_Button.SetActive(true);

            // クリックしてないときはタイマーリセット
            timer = 0f;
        }
    }

    void DropMeal()
    {
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
