using UnityEngine;

public class meal_fall : MonoBehaviour
{
    // ���͂�̃v���n�u
    public GameObject Meal_prefab;
    // ���͂�o���ʒu
    Vector3 feedPoinit;

    void Start()
    {
        feedPoinit = new Vector3(0,2,0);
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
                Instantiate(Meal_prefab, feedPoinit, Quaternion.identity);
            }
        }
    }
}
