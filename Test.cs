using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;


public class Test : MonoBehaviour {

    public GameObject Message_Object;

    string text_pass = "message_Data/data1.txt";

    IEnumerator Start()
    {
        var coroutine = Message_Object.GetComponent<M_Message>().load_Message(text_pass);

        yield return StartCoroutine(coroutine);

        string result = (string)coroutine.Current;    //最後に表示したメッセージデータの行数を取得

        if(result == "ねー　お金頂戴！　ねー　お金頂戴！　ねー　お金頂戴！")
        {
            Debug.Log("買った");
        }
    }

    // Update is called once per frame
    int flg = 0;

    void Update()
    {

        if(flg == 0)
        {
            flg = 1;
            
        }
    }

    IEnumerator a()
    {
        yield return new WaitForSeconds(10);
        Debug.Log(1);
        yield break;
    }

}
