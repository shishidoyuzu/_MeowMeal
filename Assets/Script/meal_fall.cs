using Unity.VisualScripting;
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
        OnMouseOver();
    }

    private void OnMouseOver()
    {
        /*
        �{�^�����������Ƃ����N���b�N���Ă��܂�����ɂȂ邽�߁A
        �u�{�^���̏�ɃJ�[�\��������Ă��鎞�̓N���b�N�̔�������Ȃ��v���������I

        EventTrriger���Ƃ����W���Ƃ����邯�ǁA�����Ray���΂��Ĕ��肷��I
        */

        // ���N���b�N�����Ƃ�
        if (Input.GetMouseButton(0))
        {
            // Ray���΂�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            /*
            RaycastHit2D raycastHit2D�c���C�������ɓ��������ꍇ��
                                       ���i�ʒu�ⓖ�������I�u�W�F�N�g�Ȃǁj���i�[����ϐ��B
            
            Physics2D.Raycast()�cUnity��2D�����G���W����
                                 ���C�L���X�g���s���֐��B

            �`�����`
                (Vector2)ray.origin�c���C�̊J�n�ʒu�B
                (Vector2)ray.direction�c���C�̐i�ޕ����B
             */

            if (!raycastHit2D) { // Ray�ŉ����q�b�g���Ȃ��������ʃN���b�N�ƍl����
                Debug.Log("���N���b�N����");
                // ���̎��Ԃ��o�߂��邲�Ƃ�
                if (Time.frameCount % 200 == 0)
                    Instantiate(Meal_prefab, feedPoinit, Quaternion.identity);
            }
        }
    }
}
