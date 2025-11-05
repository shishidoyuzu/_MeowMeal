using TMPro;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public string catName;                      // ネコの名前
    public float targetMeal;                    // ネコの規定ごはん量
    public SpriteRenderer catSprite;            // ネコの見た目

    public Sprite normalFace;


    // GameManagerから呼ばれる：データ設定
    public void SetCatData(string name, float food)
    {
        catName = name;
        targetMeal = food;
    }

    // Plate → GameManager → ここに判定結果が届く
    public void ReactToMeal(float diff, float cat_margin)
    {
        if (Mathf.Abs(diff) <= cat_margin)
        {
            //catSprite.sprite = happyFace;
            Debug.Log($"{catName} は満足してる！");
        }
        else if (diff < 0)
        {
            //catSprite.sprite = sadFace;
            Debug.Log($"{catName} は『まだお腹すいてる…』って顔をしてる。");
        }
        else
        {
            //catSprite.sprite = sadFace;
            Debug.Log($"{catName} は『食べすぎたにゃ…』って顔をしてる。");
        }
    }
}