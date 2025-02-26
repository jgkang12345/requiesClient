using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatViewController : MonoBehaviour
{
    struct Chat
    {
        public int chatType;
        public string msg;
    }

    List<Chat> chat = new List<Chat>();
    int _maxSize = 50;
    int _type = 0;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);


        if (_type == 0)
        {
            foreach (var msg in chat)
            {
                GameObject textGo = Managers.Resource.Instantiate("UI/Chatting", transform);
                textGo.GetComponent<TMP_Text>().text = msg.msg;

                switch (msg.chatType)
                {
                    case 0:
                        textGo.GetComponent<TMP_Text>().color = Color.white;
                        break;
                    case 1:
                        textGo.GetComponent<TMP_Text>().color = Color.red;
                        break;
                    case 2:
                        textGo.GetComponent<TMP_Text>().color = Color.green;
                        break;
                }
            }
        }

        else if (_type == 1)
        {
            foreach (var msg in chat)
            {
                if (msg.chatType == 1) continue;

                GameObject textGo = Managers.Resource.Instantiate("UI/Chatting", transform);
                textGo.GetComponent<TMP_Text>().text = msg.msg;

                switch (msg.chatType)
                {
                    case 0:
                        textGo.GetComponent<TMP_Text>().color = Color.white;
                        break;
                    case 1:
                        textGo.GetComponent<TMP_Text>().color = Color.red;
                        break;
                    case 2:
                        textGo.GetComponent<TMP_Text>().color = Color.green;
                        break;
                }
            }
        }
    }

    public void All() 
    {
        //for (int i = 0; i < transform.childCount; i++) 
        //{
        //    transform.GetChild(i).gameObject.SetActive(true);
        //}
        _type = 0;
    }

    public void Modoo() 
    {
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    if (transform.GetChild(i).GetComponent<TMP_Text>().color != Color.red)
        //        transform.GetChild(i).gameObject.SetActive(false);
        //}
        _type = 1;
    }

    internal void Push(string msg, int chatType)
    {
        if (chat.Count >= _maxSize)
            chat.RemoveAt(chat.Count - 1);

        Chat c = new Chat();
        c.chatType = chatType;
        c.msg = msg;

        chat.Insert(0, c);
    }
}
