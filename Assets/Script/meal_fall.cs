using UnityEngine;
using UnityEngine.EventSystems;

public class meal_fall : MonoBehaviour
{
    // ���͂�̃v���n�u
    public GameObject Meal_prefab;
    // ���͂�o���ʒu
    Vector3 feedPoinit;

    void Start()
    {
        feedPoinit = new Vector3(0, 2, 0);
    }

    void Update()
    {
        // ���N���b�N�����Ƃ�
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("���N���b�N�I���͂񂪏o���I");
                // ���̎��Ԃ��o�߂��邲�Ƃ�
                if (Time.frameCount % 180 == 0)
                    Instantiate(Meal_prefab, feedPoinit, Quaternion.identity);
            }
            else
            {
                Debug.Log("UI�̏ゾ����N���b�N�����I");
            }
        }
    }
}
/*
    ���܂������Ȃ������̂ō���͕s�g�p
    RaycastHit2D raycastHit2D�c���C�������ɓ��������ꍇ��
                               ���i�ʒu�ⓖ�������I�u�W�F�N�g�Ȃǁj���i�[����ϐ��B

    Physics2D.Raycast()�cUnity��2D�����G���W���Ń��C�L���X�g���s���֐��B

    �`�����`
        (Vector2)ray.origin�c���C�̊J�n�ʒu�B
        (Vector2)ray.direction�c���C�̐i�ޕ����B
 */
