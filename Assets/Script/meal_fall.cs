using UnityEngine;

public class meal_fall : MonoBehaviour
{
    // ���͂�̃v���n�u
    public GameObject Meal_prefab;
    // ���͂�1���̏d��
    public static float Meal_Weight = 2.5f;

    // ���͂�o���ʒu
    Vector2 feedPoinit;

    // �J�[�\���ʒu�̎擾
    Vector2 mousePos;

    void Start()
    {
        feedPoinit = transform.Find("meal_pos").localPosition;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));

        // ���N���b�N�����Ƃ�
        if (Input.GetMouseButton(0))
        {
            Debug.Log("���N���b�N����");
            // Instantiate(Meal_prefab, mousePos, Quaternion.identity);
            // ���̎��Ԃ��o�߂��邲�Ƃ�
            if (Time.frameCount % 200 == 0)
            {
                Instantiate(Meal_prefab, feedPoinit, Quaternion.identity);
            }
        }
    }
}
