using UnityEngine;

public class meal_fall : MonoBehaviour
{
    // ���͂�̃v���n�u
    public GameObject Meal_prefab;
    // ���͂�1���̏d��
    public static float Meal_Weight = 2.5f;

    // ���͂�o���ʒu
    Vector3 feedPoinit;

    void Start()
    {
        feedPoinit = transform.Find("meal_pos").localPosition;
    }

    void Update()
    {

        // ���N���b�N�����Ƃ�
        if (Input.GetMouseButton(0))
        {
            Debug.Log("���N���b�N����");
            // ���̎��Ԃ��o�߂��邲�Ƃ�
            if (Time.frameCount % 200 == 0)
            {
                Instantiate(Meal_prefab);
                // Instantiate(Meal_prefab, transform.position + feedPoinit, Quaternion.identity);
            }
        }
    }
}
