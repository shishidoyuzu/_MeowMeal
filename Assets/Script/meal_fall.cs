using UnityEngine;
using UnityEngine.EventSystems;

public class Meal_Fall : MonoBehaviour
{
    // ���͂�̃v���n�u
    public GameObject Meal_prefab;
    // �J�b�v�̈ʒu
    public Transform CupPoint_pos;

    public float dropInterval = 0.3f; // ���͂�𗎂Ƃ��Ԋu�i�b�j

    private float timer = 0f;

    void Update()
    {
        // ���N���b�N�����Ƃ�
        if (Input.GetMouseButton(0))
        {
            // UI�̏�ɃJ�[�\��������Ă��Ȃ�������
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // �o�ߎ��Ԃ𑫂��Ă���
                timer += Time.deltaTime;
                // dropInterval�̒l�ɂȂ�ƁA���͂�𗎂Ƃ��^�C�}�[�����Z�b�g
                if (timer >= dropInterval)
                {
                    DropMeal(); // ���͂�𗎂Ƃ�
                    Debug.Log("���N���b�N�I���͂񂪏o���I");
                    timer = 0f; // �o�ߎ��Ԃ̃��Z�b�g
                }
            }
            else
            {
                Debug.Log("UI�̏ゾ����N���b�N�����I");
            }
        }
        else
        {
            // �N���b�N���ĂȂ��Ƃ��̓^�C�}�[���Z�b�g
            timer = 0f;
        }            
    }

    void DropMeal()
    {
        // �����_���ɂ��͂�𗎂Ƃ����W�����߂�
        Vector3 randomOffset = new Vector3(
            Random.Range(-0.3f, 0.3f),
            Random.Range(-0.1f, 0.1f),
            0
        );

        // ���߂����W���A���͂�����Ƃ����W�ɂ���
        Vector3 spawnPos = CupPoint_pos.position + randomOffset;

        // ���͂�v���n�u�𐶐�
        GameObject meal = Instantiate(Meal_prefab, spawnPos, Quaternion.identity);

        // ���͂�v���n�u��Rigidbody2D������
        Rigidbody2D rb = meal.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // ���͂�ɉ�����͂������_���Ɍ��߂�
            Vector2 force = new Vector2(Random.Range(-1f, 1f), Random.Range(-2f, -5f));
            // ���߂��͂����͂�ɉ�����
            rb.AddForce(force, ForceMode2D.Impulse);
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
