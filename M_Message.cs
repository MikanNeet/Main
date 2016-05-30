using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class M_Message : MonoBehaviour
{

    //メッセージを表示するかどうか
    private static bool msg_flg = false;


    //表示する名前
    private static List<string> msg_name = new List<string>();

    //表示する文字
    private static List<string> msg = new List<string>();

    //表示する文字数
    private static int chara_len = 0;

    //選択支をつけるか
    private static List<bool> choice_flg = new List<bool>();

    //表示する回数
    private static int count = 0;

    private static int end_cnt = -1;

    //カウンタ
    private static int f_cnt = 0;

    private static int show_cnt = 0;

    //選択結果
    private static int choice_r = -1;

    private static bool click_flg = false;

    private static GameObject yes_button;
    private static GameObject no_button;

    private const int READ_MAX = 100;

    void Awake()
    {
        yes_button = GameObject.Find("Yes");
        no_button = GameObject.Find("No");
    }

    void Start()
    {

    }

    void Update()
    {
        
        if (msg_flg)
        {
            this.gameObject.GetComponent<Canvas>().enabled = true;

            if (f_cnt % 5 == 0)
            {
                if (show_cnt <= msg.Count - 1)
                {

                    if (chara_len < msg[show_cnt].Length) chara_len++;
                }
            }
            f_cnt++;
        }
        else
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;

        }

        if (show_cnt <= msg.Count - 1)
        {



            if (choice_flg[show_cnt])
            {
                if (chara_len == msg[show_cnt].Length)
                {

                    yes_button.SetActive(true);
                    no_button.SetActive(true);
                }
            }
            else
            {
                yes_button.SetActive(false);
                no_button.SetActive(false);
            }

        }

        if (end_cnt == show_cnt) msg_flg = false;
    }

    void OnGUI()
    {

        if (msg_flg)
        {

            GameObject text_Obj = GameObject.Find("msgText").gameObject;
            if (show_cnt <= msg.Count - 1)
            {
                text_Obj.GetComponent<Text>().text = msg[show_cnt].Substring(0, chara_len);
                GameObject.Find("nameText").gameObject.GetComponent<Text>().text = msg_name[show_cnt];
            }

        }
    }

    public void on_Click()
    {
        if (msg_flg)
        {
            if (show_cnt <= msg.Count - 1)
            {

                if (chara_len == msg[show_cnt].Length)
                {
                    if (!choice_flg[show_cnt])
                    {
                        next_Text();
                        choice_r = -1;
                    }
                }
                else
                {
                    chara_len = msg[show_cnt].Length;
                }
            }
        }
    }

    void next_Text()
    {
        f_cnt = 0;
        chara_len = 0;
        show_cnt++;
        click_flg = true;
        yes_button.SetActive(false);
        no_button.SetActive(false);

    }

    public void yes_Click()
    {
        if (show_cnt <= msg.Count - 1)
        {

            if (choice_flg[show_cnt])
            {
                next_Text();
                choice_r = 1;
            }
        }
    }

    public void no_Click()
    {
        if (show_cnt <= msg.Count - 1)
        {
            if (choice_flg[show_cnt])
            {
                next_Text();
                choice_r = 0;
            }
        }
    }

    static int choice_Result()
    {
        int r = choice_r;
        //choice_r = -1;
        return r;
    }

    static bool show_flg()
    {
        return msg_flg;
    }

    List<string> str = new List<string>();

    int pc = 0;
    int length = 0;

    //メッセージ読み込みのメイン
    public IEnumerator main_Message(string pass)
    {
        StreamReader reader = new StreamReader(pass);

        pc = 0;
        length = 0;

        while (reader.Peek() >= 0)
        {
            string s = reader.ReadLine().TrimStart();


            str.Add(s);
            length++;
        }

        int last_pc = 0;

        while (pc < length)
        {
            string nowstr = str[pc];
            int div = nowstr.IndexOf(":");
            bool choice = false;


            if (nowstr.IndexOf(" -if") != -1)    //選択肢のとき
            {
                choice = true;
                nowstr = nowstr.Replace(" -if", "");
            }

            string s = nowstr.Substring(div + 1);

            while (s.IndexOf("/n") != -1)
            {
                int d = s.IndexOf("/n");
                s = s.Substring(0, d) + "\n" + s.Substring(d + 2);
            }

            if (div != -1)
            {
                yield return StartCoroutine(M_Message.show_Message(nowstr.Substring(0, div), s, choice));
                last_pc = pc;
            }

            next_Value(choice);
        }

        yield return StartCoroutine(M_Message.End_Message());

        yield return last_pc + 1;

    }

    public IEnumerator load_Message(string pass)
    {
        StreamReader reader = new StreamReader(pass);

        pc = 1;
        length = 0;

        while (reader.Peek() >= 0)
        {
            string s = reader.ReadLine().TrimStart();


            str.Add(s);
            length++;
        }

        reader.Close();

        bool[] choiceflg = new bool[READ_MAX];
        int[,] read_pos = new int[READ_MAX, 2];
        string[,] msg_str = new string[3, READ_MAX];

        int choice_num = -1;

        while (pc < length)
        {
            string s = str[pc];

            if (choice_num == -1 && int.Parse(s.Substring(0, 1)) == 1) choice_num = pc - 1;
            read_pos[pc - 1, 0] = int.Parse(s.Substring(2, 1));
            read_pos[pc - 1, 1] = int.Parse(s.Substring(4, 1));

            msg_str[read_pos[pc - 1, 0], read_pos[pc - 1, 1]] = s;
            pc++;
        }

        int i, j, k;
        i = 0;
        j = 0;
        k = 0;

        string last_msg = "";


        while (true)
        {
            bool flg = false;

            String s = msg_str[i, j];

            if (i == 0 && j == choice_num)
            {
                flg = true;
                k = j + 1;
            }
            if (s == null)
            {
                if (i != 0)
                {
                    i = 0;
                    j = k;
                }
                else
                {
                    break;
                }
                continue;
            }

            string s1 = "";
            string s2 = "";

            int cnt = 0;

            //人物名の取得
            do
            {
                if(s.Substring(cnt + 5, 1) == "　")
                {
                    cnt++;
                }
                else
                {
                    int cnt2 = 0;
                    do
                    {
                        cnt2++;
                        if(s.Substring(cnt + cnt2 + 5, 1) == "　")
                        {
                            s1 = s.Substring(cnt + 5, cnt2);
                            cnt += cnt2;
                            break;
                        }
                            
                    } while (true);
                }
            } while (s1 == "");

            if (s1.Length > 6) s1 = s1.Substring(0, 6);

            //台詞の取得
            do
            {
                if (s.Substring(cnt + 5, 1) == "　")
                {
                    cnt++;
                }
                else
                {
                    s2 = s.Substring(cnt + 5);
                }
            } while (s2 == "");

            int rank = 0;
            last_msg = s2;

            while (s2.IndexOf("　") != -1)
            {
                int d = s2.IndexOf("　");
                if (rank < 4) s2 = s2.Substring(0, d) + "\n" + s2.Substring(d + 1);
                else s2 = s2.Substring(0, d);
                rank++;
            }

            yield return StartCoroutine(M_Message.show_Message(s1, s2, flg));

            if (choice_Result() == 1)
            {
                i = 1;
                j = 0;
            }
            else if (choice_Result() == 0)
            {
                i = 2;
                j = 0;
            }
            else
            {
                j++;
            }

        }



        yield return StartCoroutine(M_Message.End_Message());

        yield return last_msg;

    }

    bool refresh()
    {
        pc++;
        if (pc >= length - 1) return false;
        return true;
    }

    //次に読み込む行を決める
    void next_Value(bool choice)
    {

        if (!choice)
        {


            if (!refresh()) return;

            if (str[pc].IndexOf("}") != -1)     //}を見つけたら
            {
                int c = -1;
                do
                {

                    if (!refresh()) return;

                    if (str[pc].IndexOf("[") != -1)
                    {
                        c++;
                    }
                    else if (str[pc].IndexOf("]") != -1)
                    {
                        c--;
                    }



                } while (c != -1 || str[pc].IndexOf("]") == -1);   //]をさがす。


            }
            else if (str[pc].IndexOf("-warp") != -1)
            {
                pc = int.Parse(str[pc].Substring(6).TrimStart()) - 1;
                return;
            }

            else if (str[pc].IndexOf("-end") != -1)
            {
                pc = length;
                return;
            }

        }
        else
        {
            if (choice_Result() == 1)     //Yes
            {
                do
                {

                    if (!refresh()) return;


                } while (str[pc].IndexOf("{") == -1);   //{をさがす。


                //do
                //{

                //    if (!refresh()) return;

                //    if (str[pc].IndexOf("}") != -1)     //}を見つけたら
                //    {
                //        int c = -1;
                //        do
                //        {

                //            if (!refresh()) return;

                //            if (str[pc].IndexOf("[") != -1)
                //            {
                //                c++;
                //            }
                //            else if (str[pc].IndexOf("]") != -1)
                //            {
                //                c--;
                //            }


                //        } while (c != -1 || str[pc].IndexOf("]") == -1);   //]をさがす。

                //    }


                //} while (str[pc].IndexOf(":") == -1);   //{の下のメッセージをさがす。

            }
            else
            {
                int c = -1;
                do
                {

                    if (!refresh()) return;

                    if (str[pc].IndexOf("[") != -1)
                    {
                        c--;
                    }
                    else if (str[pc].IndexOf("{") != -1)
                    {
                        c++;
                    }

                } while (c != -1 || str[pc].IndexOf("[") == -1);   //[をさがす。

                //do
                //{

                //    if (!refresh()) return;

                //} while (str[pc].IndexOf(":") == -1);   //[の下のメッセージをさがす。
            }

        }

    }


    static IEnumerator show_Message(string name_message, string message, bool choice)
    {
        if (!msg_flg)
        {
            msg_flg = true;
            end_cnt = -1;
            msg_name = new List<string>();
            msg = new List<string>();
            choice_flg = new List<bool>();
            count = 0;
            show_cnt = 0;


        }

        msg_name.Add(name_message);
        msg.Add(message);
        choice_flg.Add(choice);
        count++;
        click_flg = false;

        while (!click_flg)
        {
            yield return new WaitForEndOfFrame();
        }
    }


    static IEnumerator End_Message()
    {
        if (end_cnt == -1)
        {
            end_cnt = count;
        }

        while (msg_flg)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
