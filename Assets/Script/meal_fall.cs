using UnityEngine;

public class meal_fall : MonoBehaviour
{
    // ���͂�̃v���n�u
    public GameObject Meal_prefab;
    // ���͂�1���̏d��
    public static float Meal_Weight = 2.5f;

    // Update is called once per frame
    void Update()
    {
        // �J�[�\���ʒu�̎擾
        Vector3 mousePos = Input.mousePosition;

        // ���N���b�N�����Ƃ�
        if (Input.GetMouseButton(0))
        {
            Debug.Log("���N���b�N����");
            // ���̎��Ԃ��o�߂��邲�Ƃ�
            if (Time.frameCount % 60 == 0)
            {
                //Instantiate(Meal_prefab, mousePos, Quaternion.identity);
            }
        }
    }
}
