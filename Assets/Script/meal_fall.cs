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

        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            Debug.Log("左クリックした");
            // 一定の時間が経過するごとに
            if (Time.frameCount % 200 == 0)
            {
                Instantiate(Meal_prefab, feedPoinit, Quaternion.identity);
            }
        }
    }
}
