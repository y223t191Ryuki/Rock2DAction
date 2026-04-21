using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの可動範囲指定クラス
/// </summary>
public class CameraMovingLimitter : MonoBehaviour
{
    // オブジェクト・コンポーネント参照
    private SpriteRenderer spriteRenderer; // スプライトレンダラーの参照
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 参照の取得
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.clear; // スプライトを透明にする
        
    }

    public Rect GetSpriteRect()
    {
        Rect result = new Rect();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        Sprite sprite = spriteRenderer.sprite;

        // 範囲計算
        // スプライトの半分を取得
        float halfSizeX = sprite.bounds.extents.x;
        float halfSizeY = sprite.bounds.extents.y;

        // スプライトの中心から見た左上と右下の座標を取得
        Vector3 topLeft = new Vector3(-halfSizeX, halfSizeY, 0.0f);
        topLeft = spriteRenderer.transform.TransformPoint(topLeft);

        Vector3 bottomRight = new Vector3(halfSizeX, -halfSizeY, 0.0f);
        bottomRight = spriteRenderer.transform.TransformPoint(bottomRight);

        // - Rectで呼び出し元に返す
        result.xMin = topLeft.x;
        result.yMin = topLeft.y;
        result.xMax = bottomRight.x;
        result.yMax = bottomRight.y;

        return result;
    }
    
}
