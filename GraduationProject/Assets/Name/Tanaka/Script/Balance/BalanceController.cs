//=======================================================================================
//! @file   BalanceController.cs
//! @brief  天秤の処理
//! @author 田中歩夢
//! @date   12月16日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//天秤の処理
public class BalanceController : MonoBehaviour
{
    [SerializeField]
    private CameraDirector m_cameraDirector = default;

    //モータークラス
    [SerializeField]
    private MotorController m_motorCon = default;

    //鉄側の板の当たり判定クラス
    [SerializeField]
    private BoardIronSideController m_boradIronSideCon = default;

    //鉄の塊
    [SerializeField]
    private GameObject m_IronObj = null;

    //鉄側の板
    [SerializeField]
    private GameObject m_board_IronSide = null;

    //ビー玉側の板
    [SerializeField]
    private GameObject m_board_MarbleSide = null;

    [SerializeField]
    private GameObject m_boardMarble = null;

    //ビー玉の当たっている数
    [SerializeField]
    private MarbleHitNum m_marbleHitNum = default;

    //ビー玉を乗せる数
    [SerializeField]
    private int m_marblePutNum = 3;

    //鉄側の板の子供
    private GameObject m_childIronSide = null;

    //鉄側の板の孫
    private GameObject m_grandchildIronSide = null;

    //ビー玉の板の子供
    private GameObject m_childMarbleSide = null;

    //ビー玉の板の孫
    private GameObject m_grandchildMarbleSide = null;

    //Sclaeの差を求める
    private float m_scaleDifference = 0.0f;

    //鉄の塊とヒットフラグ
    private bool m_ironHitFlag = false;

    //鉄の板のScaleを保存
    private Vector3 m_ironScale = Vector3.zero;

    //鉄の板のScaleを保存
    private Vector3 m_marbleScale = Vector3.zero;

    //鉄の板が戻る最小のサイズ
    const float IRON_SMALLSCALESIZE = 0.8f;

    //変わるサイズ
    private float m_changeSize = 0.0f;

    //現在のサイズ

    private Vector3 m_ironStartPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_childIronSide = m_board_IronSide.transform.GetChild(0).gameObject;
        m_grandchildIronSide = m_childIronSide.transform.GetChild(0).gameObject;

        m_childMarbleSide = m_board_MarbleSide.transform.GetChild(0).gameObject;
        m_grandchildMarbleSide = m_childMarbleSide.transform.GetChild(0).gameObject;

        m_ironStartPos = m_IronObj.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(m_cameraDirector.IsAppearCamera3D)
        {
            m_IronObj.transform.position = new Vector3(m_IronObj.transform.position.x, m_IronObj.transform.position.y, 14.0f);
        }
        else
        {
            if(!m_ironHitFlag)
            {
                m_IronObj.transform.position = new Vector3(m_IronObj.transform.position.x, m_IronObj.transform.position.y, 14.0f);
            }
            else
            {
                m_IronObj.transform.position = new Vector3(m_IronObj.transform.position.x,m_IronObj.transform.position.y,0.0f);
            }
        }


        //回路がつながってる時
        if (m_motorCon.MotorConConnectFlag)
        {
            m_IronObj.GetComponent<Rigidbody>().useGravity = false;
            if (!m_boradIronSideCon.HitFlag)
            {
                m_board_IronSide.transform.localScale = new Vector3(m_board_IronSide.transform.localScale.x, m_board_IronSide.transform.localScale.y + 0.01f, m_board_IronSide.transform.localScale.z);
                m_board_MarbleSide.transform.localScale = new Vector3(m_board_MarbleSide.transform.localScale.x, m_board_MarbleSide.transform.localScale.y - 0.01f, m_board_MarbleSide.transform.localScale.z);
            }
            else
            {
                if(!m_ironHitFlag)
                {
                    m_ironScale = m_board_IronSide.transform.localScale;
                    m_marbleScale = m_board_MarbleSide.transform.localScale;
                    //戻す距離を求める
                    m_scaleDifference = Mathf.Abs(m_ironScale.y) - Mathf.Abs(IRON_SMALLSCALESIZE);
                    //ビー玉を乗せる数で割って動く距離を求める
                    m_changeSize = m_scaleDifference / (m_marblePutNum);

                    m_ironHitFlag = true;
                }
                
                if(m_board_IronSide.transform.localScale.y > m_ironScale.y - (m_changeSize * m_marbleHitNum.HitNum))
                {
                    m_board_IronSide.transform.localScale = new Vector3(m_board_IronSide.transform.localScale.x, m_board_IronSide.transform.localScale.y - 0.01f, m_board_IronSide.transform.localScale.z);
                    m_board_MarbleSide.transform.localScale = new Vector3(m_board_MarbleSide.transform.localScale.x, m_board_MarbleSide.transform.localScale.y + 0.01f, m_board_MarbleSide.transform.localScale.z);

                }

                //鉄を板の下の座標につける
                m_IronObj.transform.position = new Vector3(m_grandchildIronSide.transform.position.x, m_grandchildIronSide.transform.position.y - 0.4f, m_grandchildIronSide.transform.position.z);
                //m_IronObj.GetComponent<Obj_OneAxitMove>().enabled = true;
            }
        }
        else
        {
            m_IronObj.GetComponent<Rigidbody>().useGravity = true;
        }
       
        //ビー玉側の板を常に維持する
        m_boardMarble.transform.position = m_grandchildMarbleSide.transform.position;
    }
}
