using UnityEngine;

public class meal_fall : MonoBehaviour
{
    // ごはんのプレハブ
    public GameObject Meal_prefab;
    // ごはん1粒の重さ
    public static float Meal_Weight = 2.5f;

    // Update is called once per frame
    void Update()
    {
        // カーソル位置の取得
        Vector3 mousePos = Input.mousePosition;

        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            Debug.Log("左クリックした");
            // 一定の時間が経過するごとに
            if (Time.frameCount % 60 == 0)
            {
                //Instantiate(Meal_prefab, mousePos, Quaternion.identity);
            }
        }
    }
}
