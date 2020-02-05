using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TextColor : MonoBehaviour
{
    private float speed = 1.0f;

    // 文字の色
    [SerializeField]
    private Color color;
         
    //α値
    private float alpha = 1.0f;

    //時間
    private float time;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        // Textコンポーネントを取得
        Text text = this.GetComponent<Text>();

        time += Time.deltaTime * 5.0f * speed;
        //α値にSin関数を代入する
        alpha = Mathf.Sin(time) * 0.5f + 0.5f;

        // 色を指定
        text.color = new Color(color.r, color.b, color.g, alpha);
    }
}
