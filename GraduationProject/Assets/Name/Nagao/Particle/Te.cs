using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 型名省略
using Particle = ParticleManager.Particle;

public class Te : MonoBehaviour
{
    //Vector3 m_pos = new Vector3(0.0f, 0.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // パーティクルを再生する
        if (Input.GetKey(KeyCode.Z))
        {
          
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //ゲームシーン以外でタブレット上でタップしたら、出現するエフェクト
            ParticleManager.PlayParticle(Particle.TouchEF, pos);

            Debug.Log(pos);
        }

    }
}
