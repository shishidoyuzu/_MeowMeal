using System.Collections;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [SerializeField] float moveHeight = 1.5f; // 上に移動する高さ
    [SerializeField] float moveTime   = 0.3f; // 上下にかかる時間

    Vector3 StartPos;
    
    public bool isMoving = false;

    Meal_Fall m_fall;

    // Start is called before the first frame update
    void Start()
    {
        m_fall = FindObjectOfType<Meal_Fall>();
        // 初期位置の設定
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 右クリックしたら、袋が上へ移動する
        if (Input.GetMouseButtonDown(1))
        {
            TryMoveBag();
        }
    }

    public void TryMoveBag()
    {
        if (isMoving) return;
        StartCoroutine(MoveUPDOWN());
    }

    IEnumerator MoveUPDOWN()
    {
        isMoving = true;

        // 上に移動した時の座標
        Vector3 upPos = StartPos + Vector3.up * moveHeight;

        // 上に移動
        yield return Move(StartPos, upPos);
        // 少しの間止める
        yield return new WaitForSeconds(1.6f);
        // 元の位置に戻る
        yield return Move(upPos, StartPos);

        isMoving = false;
    }

    IEnumerator Move(Vector3 from, Vector3 to)
    {
        // 経過時間
        float time = 0f;

        while (time < moveTime)
        {
            // 正規化されたパーセンテージ
            float t = time / moveTime;
            // スムーズに移動させる
            t = Mathf.SmoothStep(0f, 1f, t);
            // 線形補間　毎フレームやるから動いて見える
            transform.position = Vector3.Lerp(from, to, t);
            // 何秒経過したか
            time += Time.deltaTime;
            // 今フレームの処理はここまで
            yield return null;
        }
        // 小数点の誤差で到達しないのを防ぐ　保険
        transform.position = to;
    }

}