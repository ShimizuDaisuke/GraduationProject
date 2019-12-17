//=======================================================================================
//! @file   NotebookController.cs
//! @brief  落書きノートの処理
//! @author 田中歩夢
//! @date   12月04日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//落書きノートのクラス
public class NotebookController : MonoBehaviour
{
    //イベントクラス
    [SerializeField]
    private EventDirector m_event = default;

    //プレイヤーのオブジェクト
    [SerializeField]
    private GameObject m_player = null;

    //消しカスを生成するクラス
    [SerializeField]
    private CreateEraserDust m_createED = default;

    // ヒットフラグ
    private bool m_hitFlag = false;

    //動き出すまでのカウント
    private float m_startCount = 0;

    //動き出すまでの時間
    private const float START_MAX_COUNT = 0.5f;

    //追いかけるまでのカウント
    private float m_chaseCount = 0.0f;

    //追いかけるまでの時間
    private const float CHACE_MAX_COUNT = 1.0f;

    //３Dカメラオブジェクト
    [SerializeField]
    private GameObject m_camera3DObj;

    //カメラクラス
    private Camera m_camera3D;

    //Z軸の調整
    [SerializeField]
    private float z;

    //最初の座標
    private Vector2 m_startPos;

    //現在の座標
    private Vector2 m_nowPos = Vector2.zero;

    //最後の座標
    private Vector2 m_lastPos = Vector2.zero;

    //ゲージの最大値
    private const float NOTEBOOK_MAX_GAUGE = 300.0f;

    //ゲージの合計値
    private float m_notebookGauge = 0.0f;

    //消しカスの生成フラグ
    private bool[] m_createFlag = new bool[6];

    //生成数
    private int m_createNum = 0;

    //イベント終了時間
    private float EVENT_END_TIME = 2.0f;

    //イベント終了時間カウント
    private float m_eventEndCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

        m_camera3D = m_camera3DObj.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_event.IsEventKIND == EventDirector.EventKIND.NOTEBOOK_GRAFFITI_ERASE)
        {
            //今の座標を設定
            m_nowPos = new Vector2(m_player.transform.position.x, m_player.transform.position.z);

            //Zが＋　Xが＋
            if (m_nowPos.y > m_startPos.y && m_nowPos.x > m_startPos.x)
            {
                //今のZ座標が最後のZ座標より小さい時計算する
                if (m_nowPos.y < m_lastPos.y || m_nowPos.x < m_lastPos.x)
                {
                    //今の座標からスタート座標の差
                    Vector2 difference = m_nowPos - m_startPos;
                    
                    //平均
                    float average = (Mathf.Abs(difference.x) + Mathf.Abs(difference.y)) / 2.0f;

                    //ゲージに足す
                    m_notebookGauge += average;

                    //スタート座標をリセット
                    m_startPos = m_nowPos;

                }
            }
            //Zが＋　Xがー
            if (m_nowPos.y > m_startPos.y && m_nowPos.x < m_startPos.x)
            {
                //今のZ座標が最後のZ座標より小さい時計算する
                if (m_nowPos.y < m_lastPos.y || m_nowPos.x > m_lastPos.x)
                {
                    //今の座標からスタート座標の差
                    Vector2 difference = m_nowPos - m_startPos;
                    
                    //平均
                    float average = (Mathf.Abs(difference.x) + Mathf.Abs(difference.y)) / 2.0f;

                    //ゲージに足す
                    m_notebookGauge += average;

                    //スタート座標をリセット
                    m_startPos = m_nowPos;

                }
            }

            //Zがー　Xが＋
            if (m_nowPos.y < m_startPos.y && m_nowPos.x > m_startPos.x)
            {
                //今のZ座標が最後のZ座標より小さい時計算する
                if (m_nowPos.y > m_lastPos.y || m_nowPos.x < m_lastPos.x)
                {
                    //今の座標からスタート座標の差
                    Vector2 difference = m_nowPos - m_startPos;
                    
                    //平均
                    float average = (Mathf.Abs(difference.x) + Mathf.Abs(difference.y)) / 2.0f;
                    
                    //ゲージに足す
                    m_notebookGauge += average;

                    //スタート座標をリセット
                    m_startPos = m_nowPos;

                }
            }

            //Zがー　Xがー
            if (m_nowPos.y < m_startPos.y && m_nowPos.x < m_startPos.x)
            {
                //今のZ座標が最後のZ座標より小さい時計算する
                if (m_nowPos.y > m_lastPos.y || m_nowPos.x > m_lastPos.x)
                {
                    //今の座標からスタート座標の差
                    Vector2 difference = m_nowPos - m_startPos;
                    
                    //平均
                    float average = (Mathf.Abs(difference.x) + Mathf.Abs(difference.y)) / 2.0f;
                    
                    //ゲージに足す
                    m_notebookGauge += average;

                    //スタート座標をリセット
                    m_startPos = m_nowPos;

                }
            }

            //最大値を超えたら
            if(m_notebookGauge >= NOTEBOOK_MAX_GAUGE)
            {
                m_notebookGauge = NOTEBOOK_MAX_GAUGE;
                m_chaseCount += Time.deltaTime;

                EraseMoveChase();
            }
            else
            {
                //プレイヤーの移動
                PlayerMove();
            }

            if(m_notebookGauge > 50 * (m_createNum + 1))
            {
                if(!m_createFlag[m_createNum])
                {
                    m_createED.Create(m_player.transform.position);
                    m_createFlag[m_createNum] = true;
                    m_createNum++;
                }
                
            }           

            //最後座標を設定
            m_lastPos = m_nowPos;
        }
        

    }

    //プレイヤーの移動
    private void PlayerMove()
    {
        //カメラからレイを飛ばす
        Ray ray = m_camera3D.ScreenPointToRay(Input.mousePosition);
        //レイの当たり判定
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 12.0f))
        {
            if (hit.collider.tag == "Notebook")
            {
                //マウスの座標を2Dに変換
                Vector3 pos = m_camera3D.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
                //マウスのワールド座標にプレイヤーを移動
                m_player.transform.position = new Vector3(pos.x, m_player.transform.position.y, pos.z);

                //スタート座標が設定されていなかったら設定する
                if (m_startPos == Vector2.zero)
                {
                    m_startPos = new Vector2(pos.x, pos.z);
                }
                
            }

        }
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if (!m_hitFlag)
        {
            if (collider.gameObject.tag == "Player")
            {
                //直進移動のイベントに設定
                m_event.IsEventKIND = EventDirector.EventKIND.NOTEBOOK_GRAFFITI_ERASE;
                m_hitFlag = true;
            }
        }
    }


    //消しカスの動きを追いかけるにする
    private void EraseMoveChase()
    {
        if (m_chaseCount >= CHACE_MAX_COUNT)
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

            //イベント終了カウントする
            m_eventEndCount += Time.deltaTime;

            if (m_eventEndCount > EVENT_END_TIME)
            {
                m_startCount = 0;
                //イベント終了
                m_event.IsEventKIND = EventDirector.EventKIND.NONE;
            }
        }
    }


    //ゲージの合計値の取得・設定
    public float NoteBookGauge { get { return m_notebookGauge; } set { m_notebookGauge = value; } }

}
