using UnityEngine;
using UnityEngine.EventSystems;

public class meal_fall : MonoBehaviour
{
    // ���͂�̃v���n�u
    public GameObject Meal_prefab;
    // �J�b�v�̈ʒu
    public Transform CupPoint_pos;

    public int mealCount = 10; // ��x�ɗ��Ƃ����͂�̐�
    public float dropInterval = 0.3f; // ���͂�𗎂Ƃ��Ԋu�i�b�j

    //private float timer = 0f;

    void Update()
    {
        // ���N���b�N�����Ƃ�
        if (Input.GetMouseButton(0))
        {
            // UI�̏�ɃJ�[�\��������Ă��Ȃ�������
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                /*
                timer += Time.deltaTime;

                if (timer >= dropInterval)
                {
                    DropMeal();
                    timer = 0f;
                }
            }
        }
        else
        {
            timer = 0f; // �N���b�N���ĂȂ��Ƃ��̓^�C�}�[���Z�b�g
        }
                */

                // ���̎��Ԃ��o�߂��邲�Ƃ�
                if (Time.frameCount % 100 == 0)
                {               
                    Debug.Log("���N���b�N�I���͂񂪏o���I");
                    DropMeal();
                }
                else
                {
                    Debug.Log("UI�̏ゾ����N���b�N�����I");
                }
            }
        }


        void DropMeal()
        {
            for (int i = 0; i < mealCount; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-0.3f, 0.3f),
                    Random.Range(-0.1f, 0.1f),
                    0
                );

                Vector3 spawnPos = CupPoint_pos.position + randomOffset;

                GameObject meal = Instantiate(Meal_prefab, spawnPos, Quaternion.identity);

                Rigidbody2D rb = meal.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 force = new Vector2(Random.Range(-1f, 1f), Random.Range(-2f, -5f));
                    rb.AddForce(force, ForceMode2D.Impulse);
                }
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
