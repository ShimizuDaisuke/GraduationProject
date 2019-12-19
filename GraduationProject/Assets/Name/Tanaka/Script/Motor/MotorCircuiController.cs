//=======================================================================================
//! @file   MotorCircuiController.cs
//! @brief  モーター回路の処理
//! @author 田中歩夢
//! @date   12月04日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//モーター回路のクラス
public class MotorCircuiController : MonoBehaviour
{
    //回路クラス
    [SerializeField]
    private CircuitController m_circuitCon = default;

    //橋
    [SerializeField]
    private GameObject m_BridgeObj = null;

    //モーターの回転する部分のオブジェクト
    [SerializeField]
    private GameObject m_rotateMotorObj = null;

    //橋からモーターまでの線
    [SerializeField]
    private GameObject m_lineObj = null;

    //橋の１秒当たりの回転量
    [SerializeField]
    private Vector3 m_rotateBridge = new Vector3(0.0f, 11.0f, 0.0f);

    //モーターの回転する部分の１秒当たりの回転量
    [SerializeField]
    private Vector3 m_rotateMotor = new Vector3(0.0f, 0.0f, 11.0f);

    //線の移動量
    [SerializeField]
    private Vector3 m_velLine = new Vector3(-1.0f, -0.4f, 0.0f);

    //線のサイズ変更量
    [SerializeField]
    private Vector3 m_scaleLine = new Vector3(-1.0f, 0.0f, 0.0f);

    //線の速度
    [SerializeField]
    private float m_speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //回路がつながってる時
        if(m_circuitCon.ConnectFlag)
        {

            if(m_BridgeObj.transform.eulerAngles.x < 180.0f)
            {
                //橋の回転
                m_BridgeObj.transform.Rotate(m_rotateBridge * Time.deltaTime);
                //モーターの回転する部分の回転
                m_rotateMotorObj.transform.Rotate(m_rotateMotor * Time.deltaTime);
                //線の移動
                m_lineObj.transform.position += m_velLine * m_speed * Time.deltaTime;
                //線のサイズ変更
                m_lineObj.transform.localScale += m_scaleLine * m_speed * Time.deltaTime;
                
            }
            
        }
    }
}
