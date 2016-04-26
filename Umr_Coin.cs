using UnityEngine;
using System.Collections;

public class Umr_Coin : MonoBehaviour { 
    private int umrcoin = 0;


    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);//壊れないように

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.Log(umrcoin);
        }
    }
    public int getMoney() {
        return umrcoin;
    }
    //まとめて加算用
    public void Add(int add)
    {
        umrcoin += add;
    }

    //****************************************************大石くんが持ってないUMR_Coinデータ
    //１ずつ増加用
    public void Inc(int inc) {
            StartCoroutine(Cor(inc, true));
    }
    //1ずつ減少用
    public void Dec(int dec) {
        StartCoroutine(Cor(dec, false));
    }
    IEnumerator Cor(int point,bool mode) {
        if (mode)//true:加算
        {//加算
            for (int i = 0; i < point; i++)
            {
                umrcoin++;
                yield return new WaitForSeconds(0.1f/point);
            }
        }
        else {//false:減算
            for (int i = 0; i < point; i++)
            {
                umrcoin--;
                yield return new WaitForSeconds(0.1f/point);
            }
        }
    }
    
    //*****************************************************
    //まとめて加算用
    public void Sub(int sub)
    {
        umrcoin -= sub;
        if (umrcoin <= 0)
        {
            umrcoin = 0;
        }
    }

}
