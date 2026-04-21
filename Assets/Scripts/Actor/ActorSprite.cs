using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アクタースプライト制御クラス
/// </summary>
public class ActorSprite : MonoBehaviour
{
    private ActorController actorController; // アクター操作・制御クラスの参照
    private SpriteRenderer spriteRenderer; // アクターのスプライトレンダラー

    // 画像素材参照
    public List<Sprite> walkAnimationRes; // 歩行アニメーション用スプライトリスト

    // 各種変数
    private float walkAnimationTime; // 歩行アニメーションの経過時間
    private int walkAnimationFrame; // 歩行アニメーションの現在のフレーム

    private const int walkAnimationNum = 3; // 歩行アニメーションの枚数
    private const float walkAnimationSpan = 0.3f; // 歩行アニメーションの切り替え間隔
    
    public void Init(ActorController _actorController)
    {
        // 引数からアクター操作・制御クラスの参照を取得
        actorController = _actorController;
        // コンポーネント参照の取得
        spriteRenderer = actorController.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 歩行アニメーション時間の経過
        if(Mathf.Abs(actorController.xSpeed) > 0.0f)
            walkAnimationTime += Time.deltaTime;
        // 歩行アニメーションのコマ数の計算
        if (walkAnimationTime >= walkAnimationSpan)
        {
            walkAnimationTime -= walkAnimationSpan;
            // コマ数の増加
            walkAnimationFrame++;
            // コマ数が最大値を超えたら0に戻す
            if (walkAnimationFrame >= walkAnimationNum)
                walkAnimationFrame = 0;
        }
        
        // 歩行アニメーションの更新
        spriteRenderer.sprite = walkAnimationRes[walkAnimationFrame];
    }
}
