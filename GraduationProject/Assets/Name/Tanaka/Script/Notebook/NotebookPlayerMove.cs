//=======================================================================================
//! @file   NotebookPlayerMove.cs
//! @brief  ノートブックを消すプレイヤーの動きの処理
//! @author 田中歩夢
//! @date   10月07日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ノートブックを消すプレイヤーの動きのクラス
public class NotebookPlayerMove : MonoBehaviour
{
   //幅
    private float m_width;

    //高さ
    private float m_height;

    //左上の座標
    private Vector3 m_leftUp;
    //左下の座標
    private Vector3 m_leftDown;
    //右上の座標
    private Vector3 m_rightUp;
    //右下の座標
    private Vector3 m_rightDown;
    //左をサイズ3で割った座標
    private Vector3 m_left3;
    //右をサイズ3で割った座標
    private Vector3 m_right3;

    // Start is called before the first frame update
    void Start()
    {
        //幅を取得
        m_width = gameObject.GetComponent<Renderer>().bounds.size.z;
        //高さを取得
        m_height = gameObject.GetComponent<Renderer>().bounds.size.x;

        //座標の設定
        m_leftUp   = new Vector3(transform.position.z - (m_width / 2.0f),transform.position.y,transform.position.x - (m_height / 2.0f));
        m_leftDown = new Vector3(transform.position.z - (m_width / 2.0f), transform.position.y, transform.position.x + (m_height / 2.0f));
        m_rightUp  = new Vector3(transform.position.z + (m_width / 2.0f), transform.position.y, transform.position.x - (m_height / 2.0f));
        m_rightDown= new Vector3(transform.position.z + (m_width / 2.0f), transform.position.y, transform.position.x + (m_height / 2.0f));
        m_left3    = new Vector3(transform.position.z - (m_width / 6.0f), transform.position.y, transform.position.z);
        m_right3   = new Vector3(transform.position.z + (m_width / 6.0f), transform.position.y, transform.position.z);

        Debug.Log("幅"+m_width);
        Debug.Log("高さ"+m_height);
        Debug.Log("左上" + m_leftUp);
        Debug.Log("左下" + m_leftDown);
        Debug.Log("右上" + m_rightUp);
        Debug.Log("右下" + m_rightDown);
        Debug.Log("左３" + m_left3);
        Debug.Log("右３" + m_right3);


    }

    // Update is called once per frame
    void Update()
    {


        
    }


    private void EraseMove()
    {

    }
}
