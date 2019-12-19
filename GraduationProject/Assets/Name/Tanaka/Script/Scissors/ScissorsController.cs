//=======================================================================================
//! @file   ScissorsController.cs
//! @brief  ハサミの処理
//! @author 田中歩夢
//! @date   10月21日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ハサミのクラス
public class ScissorsController : MonoBehaviour
{
    //イベントクラス
    [SerializeField]
    private EventDirector m_event = default;

    //プレイヤーのゲームオブジェクト
    [SerializeField]
    private GameObject m_player = null;

    //上の刃
    [SerializeField]
    private GameObject m_upperBlade = null;
    //下の刃
    [SerializeField]
    private GameObject m_lowerBlade = null;

    //プレイヤーのスピード
    [SerializeField]
    private float m_playerSpeedX = 0.01f;

    // 上の刃と下の刃の差
    private Vector3 m_directionBlade = Vector3.zero;

    // 初期の刃の角度
    private Vector3 m_upperBladeOnceDegree = Vector3.zero;
    private Vector3 m_lowerBladeOnceDegree = Vector3.zero;

    //回転終了フラグ
    private bool m_rotationFinishFlag = false;

    //上の刃をどれだけ動かした値
    private Vector3 m_upperBladeMove = Vector3.zero;

    //毎フレーム角度を保存する
    private Vector3 m_updeateAngle = Vector3.zero;

    //ターゲット座標
    [SerializeField]
    private GameObject m_targetPos = null;

    //ターゲット座標に進む速度
    [SerializeField]
    private float m_targetMoveSpeed = 0.005f;

    //カウント
    private int m_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 上の刃と下の刃の差を求める
        m_directionBlade = (m_lowerBlade.transform.localEulerAngles - m_upperBlade.transform.localEulerAngles) / 2.0f;
        
        m_upperBladeOnceDegree = m_upperBlade.transform.localEulerAngles;
        m_lowerBladeOnceDegree = m_lowerBlade.transform.localEulerAngles;

        m_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_event.IsEventKIND == EventDirector.EventKIND.SCISSORS_CUT)
        {
            if(m_count <= 120)
            {
                m_player.transform.position = Vector3.MoveTowards(m_player.transform.position, m_targetPos.transform.position, m_targetMoveSpeed);
                m_upperBlade.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                if(!m_rotationFinishFlag)
                {
                    //プレイヤーがまっすぐ移動
                    m_player.transform.position = new Vector3(m_player.transform.position.x + m_playerSpeedX, m_player.transform.position.y, m_player.transform.position.z);

                }

            }


            //切る動き
            UpperBladeMove();
            LowerBladeMove();

            //どれだけ動いたか求める
            m_upperBladeMove = m_updeateAngle - m_upperBlade.transform.localEulerAngles;

            //角度を更新
            m_updeateAngle = m_upperBlade.transform.localEulerAngles;

            m_count++;
        }
        
    }


    //下の刃の動き
    private void LowerBladeMove()
    {
        //上の刃の動きに合わせて動く
        m_lowerBlade.transform.localRotation = Quaternion.RotateTowards(m_lowerBlade.transform.localRotation, Quaternion.Euler(m_lowerBlade.transform.localEulerAngles + m_upperBladeMove), 2.0f);
       
    }


    //上の刃の動き
    private void UpperBladeMove()
    {
        //目的角度まで回転したか
        if(m_upperBlade.transform.localEulerAngles.y >= m_upperBladeOnceDegree.y + m_directionBlade.y)
        {
            //座標、回転Freeeeeeeeeze!!
            m_upperBlade.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            m_rotationFinishFlag = true;
        }
        
        //角度がマイナス方向に行ったら初期角度に戻す
        if(m_upperBlade.transform.localEulerAngles.y < m_upperBladeOnceDegree.y)
        {
            m_upperBlade.transform.localEulerAngles = m_upperBladeOnceDegree;
        }

    }

    public bool RotationFinishFlag { get { return m_rotationFinishFlag; } set { m_rotationFinishFlag = value; } }


}
