using Unity.VisualScripting;
using UnityEngine;

public class meal_fall : MonoBehaviour
{
    // ごはんのプレハブ
    public GameObject Meal_prefab;
    // ごはん出現位置
    Vector3 feedPoinit;

    void Start()
    {
        feedPoinit = new Vector3(0,2,0);
    }

    void Update()
    {
        OnMouseOver();
    }

    private void OnMouseOver()
    {
        /*
        ボタンを押したときもクリックしてしまう判定になるため、
        「ボタンの上にカーソルが乗っている時はクリックの判定を取らない」をしたい！

        EventTrrigerだとか座標だとかあるけど、今回はRayを飛ばして判定する！
        */

        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            // Rayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            /*
            RaycastHit2D raycastHit2D…レイが何かに当たった場合の
                                       情報（位置や当たったオブジェクトなど）を格納する変数。
            
            Physics2D.Raycast()…Unityの2D物理エンジンで
                                 レイキャストを行う関数。

            ～引数～
                (Vector2)ray.origin…レイの開始位置。
                (Vector2)ray.direction…レイの進む方向。
             */

            if (!raycastHit2D) { // Rayで何もヒットしなかったら画面クリックと考える
                Debug.Log("左クリックした");
                // 一定の時間が経過するごとに
                if (Time.frameCount % 200 == 0)
                    Instantiate(Meal_prefab, feedPoinit, Quaternion.identity);
            }
        }
    }
}
