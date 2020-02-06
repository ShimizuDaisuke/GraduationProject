//=======================================================================================
//! @file   FallObject
//! @brief  落下物の動き
//! @author 志水大輔
//! @date   1/24
//! @note   カッターナイフの刃に使用
//=======================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{

    // 落とすオブジェクト
    [SerializeField]
    private GameObject FallingObject;

    // 落下させるかどうかのフラグ
    private bool fallColFlag = false;

    //落下位置
    [SerializeField]
    private float m_fallPos = 0.0f;

    // プレイヤーがエリアに入ってから物を落とすまでの時間
    [SerializeField]
    private float time_satrtfalling = 0.0f;

    // プレイヤーがエリアに入ってから掛かった時間
    private float time = 0.0f;

    //=======================================================================================
    //! @brief 更新処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void Update()
    {
        // ヌルチェック
        if(FallingObject != null)
        {
            // 判定内に入ったか
            if (fallColFlag)
            {
                // 常に時間を計る
                time += Time.deltaTime;

                // プレイヤーがエリアに入ってから物を落とすまでの時間になった場合
                if(time >= time_satrtfalling)
                {
                    // 下に移動させる
                    FallingObject.transform.position += new Vector3(0, -0.3f, 0);
                }
            }

            // 落下物のオブジェクトを消す処理
            if (FallingObject.transform.position.y < m_fallPos)
            {
                // 消す
                Destroy(FallingObject);
            }
        }

    }

    //=======================================================================================
    //! @brief 当たった時の処理
    //! @param[in] 当たり判定
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void OnTriggerEnter(Collider col)
    {
        // Playerに当たったら
        if (col.gameObject.tag == "Player")
        {
            // Flagの切り替え
            fallColFlag = true;
        }
    }
}
