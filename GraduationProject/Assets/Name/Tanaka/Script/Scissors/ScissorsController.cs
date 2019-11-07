﻿//=======================================================================================
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
    private EventDirector m_event;

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
    private Vector3 m_directionBlade;

    // 初期の刃の角度
    private Vector3 m_upperBladeOnceDegree;
    private Vector3 m_lowerBladeOnceDegree;

    //回転終了フラグ
    private bool m_rotationFinishFlag = false;

    //上の刃をどれだけ動かした値
    private Vector3 m_upperBladeMove;

    //毎フレーム角度を保存する
    private Vector3 m_updeateAngle;

    // Start is called before the first frame update
    void Start()
    {
        // 角度の範囲を制限する(0<=Θ<=180)
        //上の刃
        //if (m_upperBlade.transform.localEulerAngles.y > 180) // 〇〇〇 190 -> 10  370→　10
        //{
        //    m_upperBlade.transform.localEulerAngles -= new Vector3(0.0f, 180.0f, 0.0f);
        //}
        //if (m_upperBlade.transform.localEulerAngles.y < 0)    // 〇〇〇 -10 -> 170  -190→　170
        //{
        //    m_upperBlade.transform.localEulerAngles += new Vector3(0.0f, 180.0f, 0.0f);
        //}
        ////下の刃
        //if (m_lowerBlade.transform.localEulerAngles.y > 180) // 〇〇〇 190 -> 10  370→　10
        //{
        //    m_lowerBlade.transform.localEulerAngles -= new Vector3(0.0f, 180.0f, 0.0f);
        //}
        //if (m_lowerBlade.transform.localEulerAngles.y < 0)    // 〇〇〇 -10 -> 170  -190→　170
        //{
        //    m_lowerBlade.transform.localEulerAngles += new Vector3(0.0f, 180.0f, 0.0f);
        //}

        // 上の刃と下の刃の差を求める
        m_directionBlade = (m_lowerBlade.transform.localEulerAngles - m_upperBlade.transform.localEulerAngles) / 2.0f;
        
        m_upperBladeOnceDegree = m_upperBlade.transform.localEulerAngles;
        m_lowerBladeOnceDegree = m_lowerBlade.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_event.IsEventKIND == EventDirector.EventKIND.SCISSORS_CUT)
        {
            //プレイヤーがまっすぐ移動
            m_player.transform.position = new Vector3(m_player.transform.position.x + m_playerSpeedX, m_player.transform.position.y, m_player.transform.position.z);


            //切る動き
            UpperBladeMove();
            LowerBladeMove();

            //どれだけ動いたか求める
            m_upperBladeMove = m_updeateAngle - m_upperBlade.transform.localEulerAngles;

            //角度を更新
            m_updeateAngle = m_upperBlade.transform.localEulerAngles;
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
