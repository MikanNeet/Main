using UnityEngine;
using System.Collections;

public class Main_Syuzinkou : MonoBehaviour {
    //主人公の座標/速度/物理演算
    float x;
    float y;
    private float speed = 10;
    Rigidbody rb;
    //マウスの位置
    private Vector3 mousepos;
    //ワールド座標
    private Vector3 W_mousepos;
    //今のマウスのワールド座標
    private Vector3 W_mousepos_now;
    //今の太平の位置
    private Vector3 nowposition;


    float bunkatu=50f;
    //変化量
    private float del_x = 0;
    private float del_y = 0;
    private static int money;
    GameObject area;
    private IEnumerator coroutine;


    //
    bool move = false;



    private bool Col_Start = true;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //今の太平の位置を格納
        nowposition = gameObject.transform.position;
        //Areaオブジェクト取得
        area = GameObject.Find("Area");
        //マウスの座標を格納
        mousepos = Input.mousePosition;
        //マウスのスクリーン座標
        W_mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        //奥行は3.5
        W_mousepos.z = 3.5f;
        //差分の１０分の1を移動距離に
        del_x = (W_mousepos.x - nowposition.x) / bunkatu;
        del_y = (W_mousepos.y - nowposition.y) / bunkatu;

        //差分の1/10をコルーチンの引数に
        coroutine = Col(del_x, del_y);

    }

    // Update is called once per frame
    void Update()
    {
        //マウスの座標を格納
        mousepos = Input.mousePosition;
        W_mousepos_now = Camera.main.ScreenToWorldPoint(mousepos);


        if ((area.transform.position.x - area.transform.lossyScale.x / 2f < W_mousepos_now.x) &&
            (area.transform.position.y - area.transform.lossyScale.y / 2f < W_mousepos_now.y) &&
            (area.transform.position.x + area.transform.lossyScale.x / 2f > W_mousepos_now.x) &&
            (area.transform.position.y + area.transform.lossyScale.y / 2f > W_mousepos_now.y))
        {
            if (!move) {
                StartCoroutine(coroutine);
                move = true;
            }
            if ((W_mousepos.x - 0.1f > W_mousepos_now.x || W_mousepos_now.x > W_mousepos.x + 0.1f) || (W_mousepos.y - 0.1f > W_mousepos_now.y || W_mousepos_now.y > W_mousepos.y + 0.1f))
            {
                StopCoroutine(coroutine);
                W_mousepos = W_mousepos_now;
                del_x = (W_mousepos.x - nowposition.x) / bunkatu;
                del_y = (W_mousepos.y - nowposition.y) / bunkatu;
                coroutine = Col(del_x, del_y);
                StartCoroutine(coroutine);
                Col_Start = true;
            }
            //コルーチンの作動
            if (Col_Start)
            {
                Col_Start = false;
                StartCoroutine(coroutine);
            }

            rb.transform.position = new Vector3(nowposition.x, nowposition.y, 3.5f);
            /*
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
            Vector2 direction = new Vector2(x, y).normalized;
            GetComponent<Rigidbody>().velocity = direction * speed;
            */
        }
        else {
            if (move) {
                StopCoroutine(coroutine);
                move = false;
            }
            /*
            if (area.transform.position.x - area.transform.lossyScale.x / 2f < this.gameObject.transform.position.x){

            }
            else {

            }
            if (area.transform.position.y - area.transform.lossyScale.y / 2f < this.gameObject.transform.position.y){

            }
            else {

            }
            */
        }
    }
    


    IEnumerator Col(float x, float y)
    {
        for (int i = 0; ; i++)
        {
            if ((W_mousepos.x - 0.1f < nowposition.x && nowposition.x < W_mousepos.x + 0.1f) || (W_mousepos.y - 0.1f < nowposition.y && nowposition.y < W_mousepos.y + 0.1f))
            {
                Col_Start = true;
                yield break;
            }
            nowposition += new Vector3(x, y, 0);
            // gameObject.transform.position += new Vector3(x, y);
            yield return new WaitForSeconds(0.00001f);
        }
    }
    void OnTriggerEnter(Collider c) {
        
            switch (c.gameObject.tag) {
                //うまるなら
                case "umr":
                    break;
                //シルフィンなら
                case "tsf":
                Application.LoadLevel("Block_Menu");
                    break;
                //海老菜ちゃんなら
                case "enn":
                Application.LoadLevel("Typing_Menu");
                    break;
                //きりえちゃんなら
                case "kre":
                //Application.LoadLevel("Load");
                break;
        }
    }
}
