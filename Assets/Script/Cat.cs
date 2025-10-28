using TMPro;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public string catName;           // �l�R�̖��O
    public int targetMeal;           // �l�R�̋K�育�͂��
    public SpriteRenderer catSprite; // �l�R�̌�����
    public TextMeshProUGUI catNameText;         // ���O��\������UI�i�C�Ӂj

    // �\����i�C���X�y�N�^�ŃZ�b�g�j
    public Sprite happyFace;
    public Sprite sadFace;
    public Sprite normalFace;

    void Start()
    {
        // �������F���O��UI�ɕ\��
        if (catNameText != null)
            catNameText.text = catName;

        // �ŏ��̊�́u���ʁv
        if (catSprite != null)
            catSprite.sprite = normalFace;
    }

    // GameManager����Ă΂��F�f�[�^�ݒ�
    public void SetCatData(string name, int food)
    {
        catName = name;
        targetMeal = food;

        if (catNameText != null)
            catNameText.text = name;
    }

    // Plate �� GameManager �� �����ɔ��茋�ʂ��͂�
    public void ReactToMeal(float diff, float cat_margin)
    {
        if (Mathf.Abs(diff) <= cat_margin)
        {
            catSprite.sprite = happyFace;
            Debug.Log($"{catName} �͖������Ă�I");
        }
        else if (diff < 0)
        {
            catSprite.sprite = sadFace;
            Debug.Log($"{catName} �́w�܂����������Ă�c�x���Ċ�����Ă�B");
        }
        else
        {
            catSprite.sprite = sadFace;
            Debug.Log($"{catName} �́w�H�ׂ������ɂ�c�x���Ċ�����Ă�B");
        }
    }
}