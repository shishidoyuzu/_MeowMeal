using TMPro;
using UnityEngine;

public class Plate : MonoBehaviour
{
    // ���͂�ʃe�L�X�g
    [SerializeField] TextMeshProUGUI meal_g;

    // ���͂�1���̏d��
    public static float Meal_weight = 0.25f;

    // �����݂̂��͂�ʁi�\������j
    private float Now_gram;

    // ���͂�̌덷
    public float Cat_margin = 3.0f;

    // �o�Ă���l�R�̋K�育�͂��
    private int Target_Meal = 45;

    // �˂��̊���e�L�X�g
    [SerializeField] TextMeshProUGUI Cat_emotion_Text;

    void Start()
    {
        // �e�X�g�p�ɌŒ�i����̓n�`�����j
        //Target_Meal = Cat_DataBase.Instance.GetFoodAmount("�n�`����");

        Debug.Log("�ڕW�O�����F" + Target_Meal);
    }

    void Update()
    {
        // �O��������\������
        meal_g.text = Now_gram.ToString("N2") + "g";
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ���͂�ɂ���������
        if (collision.gameObject.tag == "Meal")
        {
            // ���M�ɓ�����ƁA���̂��͂�ʂɂP���̃O�����𑫂��Ă���
            Now_gram += Meal_weight;

            // ���M�ɓ��������炲�т�������
            Destroy(collision.gameObject);
        }
    }


    public void DecideMeal()
    {
        // �Q�[�����̉L�y��ł́A�u���̂��炢�ɂ���I�v�{�^�������������_��
        // �l�R�����͂��H�ׂ�`�ʂ�����A�l�R�̊���\���ɂȂ�B
        // �����āA���̂܂܎��̃l�R�ւƐi��ł���


        // ���͂�ʂ̃Y��
        float diff = Now_gram - Target_Meal;

        if (Now_gram == Target_Meal)
        {
            // �҂�����
            Cat_emotion_Text.text = "�҂�����I�������I";
            Debug.Log("�s�b�^���I�l�R�����ł�I");
            // ���ʂ̂ɂ��
        }
        else if (Mathf.Abs(diff) <= Cat_margin)
        {
            // �덷�͈͓̔�
            Cat_emotion_Text.text = "���傤�ǂ������͂�̗ʁI";
            Debug.Log("���傤�ǂ����I�l�R�����ł�I");
            // �ɂ��ɂ�
        }
        else if (diff < 0)
        {
            // �����Ȃ�
            Cat_emotion_Text.text = "���͂񂪏��Ȃ��I";
            Debug.Log("���Ȃ������݂����c");
            // �����ڂ�
        }
        else
        {
            // �������I
            Cat_emotion_Text.text = "���͂񂪑����I";
            Debug.Log("���������I");
            // ���b����
        }
    }
}
