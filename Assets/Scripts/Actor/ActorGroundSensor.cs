using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorGroundSensor : MonoBehaviour
{
    // コンポーネント参照
    private ActorController actorCtrl;

    // 接地判定用変数
    // 接地中はtrueが入る
    [HideInInspector] public bool isGround = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // コンポーネント参照取得
        actorCtrl = GetComponentInParent<ActorController> ();
    }

    // 各トリガー呼び出し処理
    // トリガー滞在時に呼び出し
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 接地判定オン
        if (collision.tag == "Ground")
            isGround = true;
    }
    // トリガー離脱時に呼び出し
    private void OnTriggerExit2D(Collider2D collision)
    {        
        // 接地判定オフ
        if (collision.tag == "Ground")
            isGround = false;
    }
}
