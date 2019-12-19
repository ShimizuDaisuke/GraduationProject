using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPowerToOtherObject : MonoBehaviour
{
    // ★★★
    public float radius = 10.0f;
    public float power = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ★★★
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Marble")
        {

            // 力を発生させる場所
            Vector3 explosionPos = transform.position;

            // 中心点から設定した半径の中にあるcolliderの配列を取得する。
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach (Collider hit in colliders)
            {
                // 力を及ぼしたいオブジェクトにRigidbodyが付いていることが必要（ポイント）
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // 取得したRigidbodyに力を加える
                    // ３つの引数（加える力の強さ、力の中心点、力を及ぼす半径）
                    rb.AddExplosionForce(power, explosionPos, 5.0f);
                }
            }
        }
    }

}
