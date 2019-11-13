using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//シーン遷移に必要なためSceneManager追加
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    // プレイヤーが落ちる高さ
    [SerializeField]
    float height = -4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       // プレイヤーがある程度下へ落ちたら、リザルトシーンへ遷移する
        if(transform.position.y < height)
        {
            SceneManager.LoadScene("Result");
        }

    }
}
