//=======================================================================================
//! @file   EraseThread.cs
//!
//! @brief  オブジェクトに重力をつける
//!
//! @author 長尾、橋本奉武
//!
//! @date   ?月?日
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 型名省略
using Particle = ParticleManager.Particle;

public class EraseThread : MonoBehaviour
{
    //消す判定フラグ
    private bool m_flag = false;

    //糸を消す為の時間
    private float m_dTime;

    // 糸が消える最大時間
    private const float MAXTIME = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //本を倒す為にOnTriggerExitを行いたいから
        //消す判定になったら
        if (m_flag)
        {
            //消す時間開始
            m_dTime += Time.deltaTime;

            if(m_dTime > MAXTIME)
            {
                //削除
                Destroy();
            }
            
        }
    }


    /// <summary>
    /// 氷が溶けきった時に糸の削除
    /// </summary>
    void OnTriggerEnter(Collider collider)
    {
        //火に当たったら
        if (collider.gameObject.tag == "Fire")
        {
            // 当たった位置
            Vector3 hitPos = collider.ClosestPointOnBounds(this.transform.position);

            // 当たった位置に爆発させる
            ParticleManager.PlayParticle(Particle.ExplosionEF, hitPos);

            //消える前に大きさの変更
            this.transform.localScale = new Vector3(0, 0, 0);

            m_flag = true;
        }
    }


    /// <summary>
    /// 氷が溶けきった時に糸の削除
    /// </summary>
    void OnTriggerExit(Collider collider)
    {
        //氷がなくなったら
        if (collider.gameObject.tag == "Ice")
        {
            // 消える前に大きさの変更
            this.transform.localScale = new Vector3(0, 0, 0);

            m_flag = true;
        }
    }


    void Destroy()
    {
        //糸の削除
        Destroy(this.gameObject);
    }
}
