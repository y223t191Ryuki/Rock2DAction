using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アクター操作・制御クラス
/// </summary>
public class ActorController : MonoBehaviour
{
    // 変数宣言部

    // オブジェクト・コンポーネント参照
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private ActorGroundSensor groundSensor;
    private ActorSprite actorSprite;
    public CameraController cameraController; // カメラ制御クラスの参照

    // 移動関連関数
    [HideInInspector] public float xSpeed; // X方向の移動速度
    [HideInInspector] public bool rightFacing; // 向いている方向(trueが右向き)
    private float remainJumpTime; // ジャンプの残り時間

    // Start (オブジェクト有効化時に1度実行)
    void Start()
    {
        // コンポーネント参照の取得
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundSensor = GetComponentInChildren<ActorGroundSensor>();
        actorSprite = GetComponent<ActorSprite>();

        // 配下コンポーネントの初期化
        actorSprite.Init(this);

        // カメラ初期位置
        cameraController.SetPosition(transform.position);

        // 変数の初期化
        rightFacing = true; // 初期状態は右向き
    }

    // Update (1フレームごとに1度ずつ実行)
    void Update()
    {
        MoveUpdate();
        JumpUpdate();

        // 坂道での滑り防止
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation; // 回転を固定
        if (groundSensor.isGround && !Input.GetKey(KeyCode.UpArrow))
        {
            // 坂道を登る際の上昇防止
            if (rigidbody2D.linearVelocity.y > 0.0f)
            {
                rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 0.0f);
            }

            // 坂道に立っている際の滑り防止
            if (Mathf.Abs (xSpeed) < 0.1f)
            {
                // 移動速度が小さい場合は全ての移動を固定
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }

        // カメラに自身の位置を渡す
        cameraController.SetPosition(transform.position);
    }

    /// <summary>
    /// Updateから呼び出される左右移動入力処理
    /// </summary>
    private void MoveUpdate()
    {
        // X方向移動入力
        if (Input.GetKey(KeyCode.RightArrow))
        {// 右方向の移動入力
            //　X方向移動速度をプラスに設定
            xSpeed = 6.0f;
            rightFacing = true; // 右向きに設定
            spriteRenderer.flipX = false; // スプライトの反転を解除
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {// 左方向の移動入力
            // X方向移動速度をマイナスに設定
            xSpeed = -6.0f;
            rightFacing = false; // 左向きに設定
            spriteRenderer.flipX = true; // スプライトの反転を設定
        }
        else
        {// 入力なし
            // X方向の移動を停止
            xSpeed = 0.0f;
        }
    }

    /// <summary>
    /// Updateから呼び出されるジャンプ入力処理
    /// </summary>
    private void JumpUpdate()
    {
        // ジャンプの残り時間を減らす
        if (remainJumpTime > 0.0f)
            remainJumpTime -= Time.deltaTime;

        // ジャンプ操作
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {// ジャンプ開始
            // 接地していないなら終了
            if (!groundSensor.isGround)
                return;
            
            // ジャンプ力を計算
            float jumpPower = 10.0f; // ジャンプ力の大きさ
            // ジャンプ力を適用
            rigidbody2D.linearVelocity = new Vector2 (rigidbody2D.linearVelocity.x, jumpPower);

            // ジャンプの残り時間を設定
            remainJumpTime = 0.25f;
        }
        else if (Input.GetKey (KeyCode.UpArrow))
        {// ジャンプ中
            // ジャンプの残り時間がないなら終了
            if (remainJumpTime <= 0.0f)
                return;
            // 接地しているなら終了
            if (groundSensor.isGround)
                return;
            
            // ジャンプ力を計算
            float jumpAddPower = 30.0f * Time.deltaTime; // ジャンプ力の大きさ
            // ジャンプ力を適用
            rigidbody2D.linearVelocity += new Vector2 (0.0f, jumpAddPower);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {// ジャンプ終了
                // ジャンプの残り時間を0に設定
                remainJumpTime = -1.0f;
        }
    }

    private void FixedUpdate()
    {
        // 移動速度ベクトルを現在地から取得
        Vector2 velocity = rigidbody2D.linearVelocity;
        // X方向の速度を入力から決定
        velocity.x = xSpeed;

        // 計算した移動速度ベクトルをRigidbody2Dコンポーネントに設定
        rigidbody2D.linearVelocity = velocity;
    }
}