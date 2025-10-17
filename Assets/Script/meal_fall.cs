using UnityEngine;

public class meal_fall : MonoBehaviour
{
    // ごはんのプレハブ
    public GameObject Meal_prefab;
    // ごはん1粒の重さ
    public static float Meal_Weight = 2.5f;

    // ごはん出現位置
    Vector2 feedPoinit;

    // カーソル位置の取得
    Vector2 mousePos;

    void Start()
    {
        feedPoinit = transform.Find("meal_pos").localPosition;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));

        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            Debug.Log("左クリックした");
            // Instantiate(Meal_prefab, mousePos, Quaternion.identity);
            // 一定の時間が経過するごとに
            if (Time.frameCount % 200 == 0)
            {
                Instantiate(Meal_prefab, feedPoinit, Quaternion.identity);
            }
        }
    }
}
