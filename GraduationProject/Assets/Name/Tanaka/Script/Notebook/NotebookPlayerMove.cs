//=======================================================================================
//! @file   NotebookPlayerMove.cs
//! @brief  ノートブックを消すプレイヤーの動きの処理
//! @author 田中歩夢
//! @date   10月15日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ノートブックを消すプレイヤーの動きのクラス
public class NotebookPlayerMove : MonoBehaviour
{
    //イベントクラス
    [SerializeField]
    private EventDirector m_event;

    //プレイヤーのオブジェクト
    [SerializeField]
    private GameObject m_player;

    //消しカスを生成するクラス
    [SerializeField]
    private CreateEraserDust m_createED;

    // HitEraseEvent内
    [SerializeField]
    private HitEraseEvent m_script_HitEraseEvent;

    //速度
    [SerializeField]
    private float m_speed = 0.25f;

    //消しカスの出現のランダム幅
    private int m_randomRange;

    //追いかけるまでのカウント
    private float m_chaseCount;

    //追いかけるまでの時間
    private const float CHACE_MAX_COUNT = 1.0f;

    //使用フラグ
    private bool m_useFlag;

    //--------------------------------------------------------------

    //幅
    private float m_width;
    //高さ
    private float m_height;
   

    //左上の座標
    private Vector2 m_leftUp;
    //左下の座標
    private Vector2 m_leftDown;

    //右上の座標
    private Vector2 m_rightUp;
    //右下の座標
    private Vector2 m_rightDown;

    //左をサイズ3で割った座標
    private Vector2 m_left3;
    //左をサイズ3で割った上の座標
    private Vector2 m_left3Up;
    //左をサイズ3で割った下の座標
    private Vector2 m_left3Down;

    //右をサイズ3で割った座標
    private Vector2 m_right3;
    //右をサイズ3で割った上の座標
    private Vector2 m_right3Up;
    //右をサイズ3で割った下の座標
    private Vector2 m_right3Down;

    //真ん中の1番左
    private Vector2 m_centerLeft;
    //真ん中の１番右
    private Vector2 m_centerRight;

    //移動する数
    private const int MOVE_POINT_NUM = 6;

    //移動フラグ
    private bool[] m_moveFlag = new bool[MOVE_POINT_NUM];

    //移動座標
    private Vector2 m_movePos;

    //今のポイント
    private int m_nowPoint;

    //四角形
    private Vector2[] m_point = new Vector2[MOVE_POINT_NUM];
    //左上の四角形のランダム座標
    private Vector2 m_leftUpBox;
    //真ん中の上の四角形のランダム座標
    private Vector2 m_centerUpBox;
    //右上の四角形のランダム座標
    private Vector2 m_rightUpBox;
    //左下の四角形のランダム座標
    private Vector2 m_leftDownBox;
    //真ん中の下の四角形のランダム座標
    private Vector2 m_centerDownBox;
    //右下の四角形のランダム座標
    private Vector2 m_rightDownBox;

    

    // Start is called before the first frame update
    void Start()
    {
        //幅を取得
        m_width = gameObject.GetComponent<Renderer>().bounds.size.z;
        //高さを取得
        m_height = gameObject.GetComponent<Renderer>().bounds.size.x;

        //座標の設定
        //上の列
        m_leftUp     = new Vector2(transform.position.x - (m_height / 2.0f),transform.position.z - (m_width / 2.0f));
        m_left3Up    = new Vector2(transform.position.x - (m_height / 2.0f),transform.position.z - (m_width / 6.0f));
        m_right3Up   = new Vector2(transform.position.x - (m_height / 2.0f),transform.position.z + (m_width / 6.0f));
        m_rightUp    = new Vector2(transform.position.x - (m_height / 2.0f),transform.position.z + (m_width / 2.0f));
        //真ん中の列
        m_centerLeft = new Vector2(transform.position.x,transform.position.z - (m_width / 2.0f));
        m_left3      = new Vector2(transform.position.x,transform.position.z - (m_width / 6.0f));
        m_right3     = new Vector2(transform.position.x,transform.position.z + (m_width / 6.0f));
        m_centerRight= new Vector2(transform.position.x, transform.position.z + (m_width / 2.0f));
        //下の列
        m_leftDown   = new Vector2(transform.position.x + (m_height / 2.0f),transform.position.z - (m_width / 2.0f));
        m_left3Down  = new Vector2(transform.position.x + (m_height / 2.0f),transform.position.z - (m_width / 6.0f));
        m_right3Down = new Vector2(transform.position.x + (m_height / 2.0f),transform.position.z + (m_width / 6.0f));
        m_rightDown  = new Vector2(transform.position.x + (m_height / 2.0f),transform.position.z + (m_width / 2.0f));

        //四角形のランダム座標の設定
        //左上
        m_point[5] = new Vector2((m_leftUp.x + m_centerLeft.x) / 2.0f, (m_leftUp.y + m_left3Up.y) / 2.0f);
        //真ん中の上           
        m_point[4] = new Vector2((m_left3Up.x + m_left3.x) / 2.0f, (m_left3Up.y + m_right3Up.y) / 2.0f);
        //左下                 
        m_point[3] = new Vector2((m_centerLeft.x + m_leftDown.x) / 2.0f, (m_centerLeft.y + m_left3.y) / 2.0f);
        //右上                 
        m_point[2] = new Vector2((m_right3Up.x + m_right3.x) / 2.0f, (m_right3Up.y + m_rightUp.y) / 2.0f);
        //真ん中の下           
        m_point[1] = new Vector2((m_left3.x + m_left3Down.x) / 2.0f, (m_left3.y + m_right3.y) / 2.0f);
        //右下                 
        m_point[0] = new Vector2((m_right3.x + m_right3Down.x) / 2.0f, (m_right3.y + m_centerRight.y) / 2.0f);

        //初期化
        m_nowPoint = 0;
        m_randomRange = 1;
        m_useFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_useFlag)
        {
            //消す動き
            EraseMove();
            //消しカスの動きを追いかけるにする
            EraseMoveChase();

        }
        

    }

    //消す動き
    private void EraseMove()
    {
        //プレイヤーが落書きを消すイベントになっているとき
        if (m_script_HitEraseEvent.HitFlag==true)
        {
            //今のポイントが最大のポイントかどうか
            if (m_nowPoint < MOVE_POINT_NUM)
            {
                //ポイントに達していなかったら移動、達していたら次のポイントに切り替える
                if (m_player.transform.position.x != m_point[m_nowPoint].x || m_player.transform.position.z != m_point[m_nowPoint].y)
                {
                    m_movePos = Vector2.MoveTowards(new Vector2(m_player.transform.position.x, m_player.transform.position.z), m_point[m_nowPoint], m_speed);
                    m_player.transform.position = new Vector3(m_movePos.x, m_player.transform.position.y, m_movePos.y);
                }
                else
                {
                    m_nowPoint++;
                    //消しカスの生成
                    m_createED.Create(new Vector3(m_player.transform.position.x + Random.Range(-m_randomRange, m_randomRange), m_player.transform.position.y, m_player.transform.position.z + Random.Range(-m_randomRange, m_randomRange)));
                }
            }
            else
            {
                m_chaseCount += Time.deltaTime;
            }
        }
    }

    //消しカスの動きを追いかけるにする
    private void EraseMoveChase()
    {
        if(m_chaseCount >= CHACE_MAX_COUNT)
        {
            //シーン上のEraserDustをすべて取得
            GameObject[] eraser = GameObject.FindGameObjectsWithTag("EraserDust");

            foreach (GameObject i in eraser)
            {
                //LayerのCreatedだけ追いかけるようにする
                if (i.layer == LayerMask.NameToLayer("Created"))
                {
                    i.layer = LayerMask.NameToLayer("Default");
                    i.GetComponent<EraserDust>().IsEraserMove = EraserDust.EraserMove.CHASE;
                    i.GetComponent<EraserDustController>().IsTargetPos = m_player.transform.position;

                }
            }
            m_event.IsEventKIND = EventDirector.EventKIND.NONE;
            m_script_HitEraseEvent.HitFlag = false;
            Debug.Log("fdasfdsa");
            m_useFlag = true;
        }
    }

}
