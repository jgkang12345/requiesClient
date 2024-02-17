using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    TMP_Text _skilllQName;
    [SerializeField]
    TMP_Text _skilllEName;
    [SerializeField]
    Image _skill1CollTimeImg;
    [SerializeField]
    Image _skill2CollTimeImg;
    bool skill1CollTime;
    bool skill2CollTime;

    private float skill1CoolTime;
    private float skill2CoolTime;
    public void Init(Type.CharacterType type)
    {
        switch (type) 
        {
            case Type.CharacterType.Warrior:
                _skilllQName.text = "���̽��ʵ�(Q)";
                _skilllEName.text = "�Ŀ���Ʈ����ũ(E)";
                break;

            case Type.CharacterType.Archer:
                _skilllQName.text = "���׿�(Q)";
                _skilllEName.text = "�ռ�(E)";
                break;
        }
    }

    public void SetQCoolTime(float cool) 
    {
        StartCoroutine(QCoolTime(cool));
    }

    public void SetECoolTime(float cool)
    {
        StartCoroutine(ECoolTime(cool));
    }

    IEnumerator QCoolTime(float cool) 
    {
        float coolTimeMax = cool;
        while (cool > 0.0f)
        { 
            cool -= Time.deltaTime;
            _skill1CollTimeImg.fillAmount = cool / coolTimeMax;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ECoolTime(float cool)
    {
        float coolTimeMax = cool;
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            _skill2CollTimeImg.fillAmount = cool / coolTimeMax;
            yield return new WaitForFixedUpdate();
        }
    }

}
