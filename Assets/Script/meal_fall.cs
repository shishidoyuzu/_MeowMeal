using UnityEngine;
using UnityEngine.EventSystems;

public class meal_fall : MonoBehaviour
{
    // ごはんのプレハブ
    public GameObject Meal_prefab;
    // ごはん出現位置
    Vector3 feedPoinit;

    void Start()
    {
        feedPoinit = new Vector3(0, 2, 0);
    }

    void Update()
    {
        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("左クリック！ごはんが出るよ！");
                // 一定の時間が経過するごとに
                if (Time.frameCount % 180 == 0)
                    Instantiate(Meal_prefab, feedPoinit, Quaternion.identity);
            }
            else
            {
                Debug.Log("UIの上だからクリック無効！");
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
