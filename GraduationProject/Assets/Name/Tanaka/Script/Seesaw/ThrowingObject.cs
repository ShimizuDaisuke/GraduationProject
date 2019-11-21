//=======================================================================================
//! @file   ThrowingObject.cs
//! @brief  オブジェクトを投げる処理
//! @author 田中歩夢
//! @date   10月10日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//オブジェクトを投げるクラス
public class ThrowingObject : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //投げられるオブジェクト
    [SerializeField]
    private GameObject m_throwObject = null;
    //着地地点のオブジェクト
    [SerializeField]
    private GameObject m_targetObject = null;
    //投げる角度
    [SerializeField]
    private float m_throwAngle = 0.0f;

    //たどり着いたかどうかのフラグ
    private bool m_arriveFlag = false;

    //SEフラグ
    private bool m_seFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //たどり着いたかどうか
        Arrive();
    }


    //投げる処理
    public void ThrowingObj()
    {
        //投げられるオブジェクトの座標
        Vector3 throwPos = m_throwObject.transform.position;
        //標的の座標
        Vector3 targetPos = m_targetObject.transform.position;
        //投げる角度
        float angle = m_throwAngle;

        //投げる角度を算出
        Vector3 vel = CalculateVelocity(throwPos, targetPos, angle);

        //投げる
        Rigidbody rigid = m_throwObject.GetComponent<Rigidbody>();
        rigid.AddForce(vel * rigid.mass, ForceMode.Impulse);
        if(!m_seFlag)
        {
            //SEの再生
            SoundManager.PlaySE(SoundManager.Sound.SE_SeesawJump);
            ParticleManager.PlayParticle(ParticleManager.Particle.JumpEF,m_throwObject.transform.position);
        }
        

    }


    /// <summary>
    /// 着地地点に投げる速度の計算
    /// </summary>
    /// <param name="throwPos">投げる時の座標</param>
    /// <param name="targetPos">着地地点の座標</param>
    /// <param name="angle">投げる角度</param>
    /// <returns>投げる速度</returns>
    private Vector3 CalculateVelocity(Vector3 throwPos,Vector3 targetPos,float angle)
    {
        //投げる角度をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        //水平方向の距離X
        float x = Vector2.Distance(new Vector2(throwPos.x, throwPos.z), new Vector2(targetPos.x, targetPos.z));

        //垂直方向の距離Y
        float y = throwPos.y - targetPos.y;

        //斜方投射の公式で初速度
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        //変な数字が入ってないかチェック
        if(float.IsNaN(speed))
        {
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(targetPos.x - throwPos.x, x * Mathf.Tan(rad), targetPos.z - throwPos.z).normalized * speed);
        }

    }

    //ターゲットにたどり着いたか
    private void Arrive()
    {
        //イベントが投げられたの時
        if(m_event.IsEventKIND == EventDirector.EventKIND.RULE_THOW)
        {
            //ターゲットにたどり着いたか
            if (m_throwObject.transform.position == m_targetObject.transform.position)
            {
                m_seFlag = false;
                //なにも起きていない状態に戻す
                m_event.IsEventKIND = EventDirector.EventKIND.NONE;
            }
        }
        
    }

    //たどり着いたかどうかのフラグを設定・取得
    public bool ArriveFlag { get { return m_arriveFlag; } set { m_arriveFlag = value; } }


}
