using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    // ���͂�1���̏d��
    public static float Meal_weight = 0.25f;

    // �����݂̂��͂�ʁi�\������j
    private float Now_gram;
    // ���͂�ʃe�L�X�g
    [SerializeField] TextMeshProUGUI meal_g;

    // ���͂�̌덷
    public float Cat_margin = 3.0f;
    // ���͂�̌덷�e�L�X�g
    [SerializeField] TextMeshProUGUI Cat_margin_Text;

    // �˂��̗��z���͂��
    private int Target_Meal;
    // �˂��̗��z���͂�ʃe�L�X�g
    [SerializeField] TextMeshProUGUI Target_Meal_Text;

    // �˂��̊���e�L�X�g
    [SerializeField] TextMeshProUGUI Cat_emotion_Text;

    private GameManager gameManager;

    void Start()
    {
        // �e�X�g�p�ɌŒ�i����̓n�`�����j
        Target_Meal = Cat_DataBase.Instance.GetFoodAmount("�n�`����");

        Target_Meal_Text.text = "�ڕW�O�����F" + Target_Meal + "g";
        Cat_margin_Text.text = "�덷�F" + Cat_margin + "g";

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

    public void Reset_Meal()
    {
        Now_gram = 0.0f;
    }

    public void Meal_35()
    {
        Now_gram = 35.0f;
    }

    public void DecideMeal()
    {
        // �Q�[�����ł́A�u���̂��炢�ɂ���I�v�{�^�������������_��
        // �l�R�����͂��H�ׂ�`�ʂ�����A�l�R�̊���\���ɂȂ�B

        // ���͂�ʂ̃Y��
        float diff = Now_gram - Target_Meal;

        if (Now_gram == Target_Meal)
        {
            // �҂�����
            Cat_emotion_Text.text = "�҂�����̂��͂�I������I";
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

        // ����`�悪�I���Ύ��̃l�R��

        // �l�R�ɔ���������
        if (gameManager != null)
        {
            gameManager.DecideCatMeal(diff, Cat_margin);
        }
    }
}
