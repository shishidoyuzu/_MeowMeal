using TMPro;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public string catName;           // ネコの名前
    public int targetMeal;           // ネコの規定ごはん量
    public SpriteRenderer catSprite; // ネコの見た目
    public TextMeshProUGUI catNameText;         // 名前を表示するUI（任意）

    // 表情差分（インスペクタでセット）
    public Sprite happyFace;
    public Sprite sadFace;
    public Sprite normalFace;

    void Start()
    {
        // 初期化：名前をUIに表示
        if (catNameText != null)
            catNameText.text = catName;

        // 最初の顔は「普通」
        if (catSprite != null)
            catSprite.sprite = normalFace;
    }

    // GameManagerから呼ばれる：データ設定
    public void SetCatData(string name, int food)
    {
        catName = name;
        targetMeal = food;

        if (catNameText != null)
            catNameText.text = name;
    }

    // Plate → GameManager → ここに判定結果が届く
    public void ReactToMeal(float diff, float cat_margin)
    {
        if (Mathf.Abs(diff) <= cat_margin)
        {
            catSprite.sprite = happyFace;
            Debug.Log($"{catName} は満足してる！");
        }
        else if (diff < 0)
        {
            catSprite.sprite = sadFace;
            Debug.Log($"{catName} は『まだお腹すいてる…』って顔をしてる。");
        }
        else
        {
            catSprite.sprite = sadFace;
            Debug.Log($"{catName} は『食べすぎたにゃ…』って顔をしてる。");
        }
    }
}