using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインカメラ制御クラス
/// </summary>
public class CameraController : MonoBehaviour
{
    // オブジェクト・コンポーネント参照
    public Transform backgroundTransform; // 背景オブジェクトのTransformコンポーネントの参照
    public CameraMovingLimitter movingLimitter; // カメラの可動範囲指定クラスの参照

    // 各種変数
    private Vector2 basePos; // カメラの基準位置
    private Rect limitQuad; // カメラの可動範囲

    // 定数定義
    public const float BG_Scroll_Speed = 0.5f; // 背景のスクロール速度

    // Start
    void Start()
    {
        // カメラの可動範囲を取得
        limitQuad = movingLimitter.GetSpriteRect();
    }

    /// <summary>
    /// カメラの位置を設定する
    /// </summary>
    /// <param name="targetPos">カメラの目標位置</param>
    public void SetPosition(Vector2 targetPos)
    {
        basePos = targetPos;
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        // アクターの現在地より少し右上にカメラを配置
        pos.x = basePos.x + 2.5f;
        pos.y = basePos.y + 1.5f;
        // Z座標は現在値をそのまま使用

        pos.x = Mathf.Clamp(pos.x, limitQuad.xMin, limitQuad.xMax);
        pos.y = Mathf.Clamp(pos.y, limitQuad.yMin, limitQuad.yMax);

        // 計算後のカメラ位置を反映
        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 0.08f);

        // 背景スプライト移動
        Vector3 bgPos = transform.localPosition * BG_Scroll_Speed;
        bgPos.z = backgroundTransform.position.z; // Z座標は現在値を
        backgroundTransform.position = bgPos;
    }
    
}
