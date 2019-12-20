using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMelt : MonoBehaviour
{
    //氷のモデル
    [SerializeField]
    private GameObject IceCube = default;

    //溶ける時間
    private float m_meltTime = 0.0f;

    //氷の大きさ
    [SerializeField]
    private Vector3 m_IceSize = new Vector3(1.0f, 1.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        //氷のモデルを取得
        GameObject IceCube = GameObject.Find("IceCube");

        IceCube.transform.localScale = m_IceSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        //火に当たり続けている間
        if(col.gameObject.tag == "Fire")
        {
            //時間のカウントアップ
            m_meltTime += (Time.deltaTime * 0.3f);


            //氷のY軸が0より大きいなら
            if (0 <= IceCube.transform.localScale.y)
            {
                //氷のY軸をだんだん小さくする
                IceCube.transform.localScale = new Vector3(m_IceSize.x, (m_IceSize.y - m_meltTime), m_IceSize.z);
            }
            else
            {
                //氷の削除
                Destroy(this.gameObject);
            }

        }
    }
}
