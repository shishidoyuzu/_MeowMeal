using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    // ���͂�ʃe�L�X�g
    [SerializeField] Text meal_g;

    // ���͂�1���̏d��
    public static float Meal_Weight = 0.5f;

    // �����݂̂��͂�ʁi�\������j
    private float Now_gram;


    // Update is called once per frame
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
            Now_gram += Meal_Weight;

            // ���M�ɓ��������炲�т�������
            Destroy(collision.gameObject);
        }
    }

}
