//=======================================================================================
//! @file   Camera2DHitHightFlag.cs
//! @brief  カメラ２Dの高さを変える当たり判定の処理
//! @author 田中歩夢、橋本奉武
//! @date   01月08日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カメラ２Dの高さを変える当たり判定の処理
public class Camera2DHitHightFlag : MonoBehaviour
{
    //カメラがプレイヤーに追従する
    [SerializeField]
    private CameraFollowPlayer m_cameraFP = default;

    //２DカメラのY座標（当たってる時の）
    [SerializeField]
    private float m_cameraHitYPos = 0.0f;

    //3DカメラZ座標
    [SerializeField]
    private float m_camera3dHitYPos = 0.0f;

    //２Dカメラ
    [SerializeField]
    private GameObject m_camera2d = null;

    //３Dカメラ
    [SerializeField]
    private GameObject m_camera3d = null;

    // 現在、エリア内にプレイヤーが入っているか
    private bool m_hitNowHightFlag = false;

    // 過去、エリア内にプレイヤーが入ったか
    private bool m_hitOnceHightFlag = false;

    //プレイヤー
    private GameObject m_player = null;

    //2DカメラZ座標
    private float m_camera2DposZ = -14.0f;

    // プレイヤーとカメラの距離
    private Vector3 dir = Vector3.zero;

    // プレイヤーと3dカメラの距離
    private Vector3 dir3d = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // 現在と過去でフラグが異なっている場合
        if (m_hitNowHightFlag != m_hitOnceHightFlag)
        {
            // 現在、プレイヤーがエリア内にプレイヤーが入った場合
            if(m_hitNowHightFlag)
            {
                // 現在のプレイヤーとカメラの距離を計算
                dir = m_camera2d.transform.position - m_player.transform.position;

                // 現在のプレイヤーと3dカメラの距離を計算
                dir3d = m_camera3d.transform.position - m_player.transform.position;

                // 指定された位置にカメラを動かす
                m_cameraFP.ChangeCameraPos(new Vector3(m_player.transform.position.x, m_cameraHitYPos, m_camera2DposZ), false);
               // m_cameraFP.ChangeCameraPos(new Vector3(m_camera3d.transform.position.x, m_camera3dHitYPos, m_player.transform.position.z), true);

            }
            // エリア内にプレイヤーが入っていない場合
            else
            {
                // カメラの位置が下記の距離に戻す
                Vector3 camerabasepos = dir + m_player.transform.position;

                // カメラの位置が下記の距離に戻す
                Vector3 camera3dBasePos = dir3d + m_player.transform.position;

                //  元の位置に戻す
                m_cameraFP.ChangeCameraPos(camerabasepos, false);
                m_cameraFP.ChangeCameraPos(camera3dBasePos, true);
            }
        }

        // 常に過去にエリア内にプレイヤーが入ったか　更新する
        m_hitOnceHightFlag = m_hitNowHightFlag;
    }
    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        //電気が当たったら
        if (collider.gameObject.tag == "Player")
        {
            // エリア内にプレイヤーが入った
            m_hitNowHightFlag = true;
        }
    }

    //衝突判定
    void OnTriggerExit(Collider collider)
    {
        //電気が当たったら
        if (collider.gameObject.tag == "Player")
        {
            // エリア外へプレイヤーが出た
            m_hitNowHightFlag = false;
        }
    }

    // フラグ
    public bool HitHightFlag { get { return m_hitNowHightFlag; } set { m_hitNowHightFlag = value; } }

}
