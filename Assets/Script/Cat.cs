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
}