using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    // ���͂�ʃe�L�X�g
    [SerializeField] Text meal_g;

    // ���͂�1���̏d��
    public static float Meal_weight = 0.25f;

    // �����݂̂��͂�ʁi�\������j
    private float Now_gram;

    // ���͂�̌덷
    public float Cat_margin = 3.0f;

    // �o�Ă���l�R�̋K�育�͂��
    private int Target_Meal;

    // �˂��̊���e�L�X�g
    public Text Cat_emotion_Text;

    void Start()
    {
        // �e�X�g�p�ɌŒ�
        Target_Meal = Cat_DataBase.Instance.GetFoodAmount("�N���l�R");
    }


    void Update()
    {
        // �O��������\������
        meal_g.text = Now_gram.ToString("N2") + "��";
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
        // ���͂�ʂ̃Y��
        float diff = Now_gram - Target_Meal;

        if (Mathf.Abs(diff) <= Cat_margin)
        {
            // �҂�����
            Cat_emotion_Text.text = "���傤�ǂ����I";
            Debug.Log("���傤�ǂ����I�l�R�����ł�I");
        }
        else if (diff < 0)
        {
            // �����Ȃ�
            Cat_emotion_Text.text = "���Ȃ��I";
            Debug.Log("���Ȃ������݂����c");
        }
        else
        {
            // �������I
            Cat_emotion_Text.text = "�����I";
            Debug.Log("���������I");
        }
    }
}
