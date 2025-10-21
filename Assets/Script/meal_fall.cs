using UnityEngine;

public class meal_fall : MonoBehaviour
{
    // ごはんのプレハブ
    public GameObject Meal_prefab;
    // ごはん1粒の重さ
    public static float Meal_Weight = 2.5f;

    // ごはん出現位置
    Vector3 feedPoinit;

    void Start()
    {
        feedPoinit = transform.Find("meal_pos").localPosition;
    }

    void Update()
    {

        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            Debug.Log("左クリックした");
            // 一定の時間が経過するごとに
            if (Time.frameCount % 200 == 0)
            {
                Instantiate(Meal_prefab);
                // Instantiate(Meal_prefab, transform.position + feedPoinit, Quaternion.identity);
            }
        }
    }
}
