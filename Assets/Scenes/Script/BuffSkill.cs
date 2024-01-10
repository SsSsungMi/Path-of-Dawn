using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUFF_TYPE
{
    ADDATKBUF,
    ADDDEFBUF,
    HEALBUF,
    TURNBUF
}


public class BuffSkill : Skill
{
    public BUFF_TYPE type;

    public new void Awake()
    {
        base.Awake();
    }


    public override void Cast()
    {
        if(type == BUFF_TYPE.ADDATKBUF)
        {
            //모든 플레이어블 캐릭터 공격력 증가
        }
        if(type == BUFF_TYPE.ADDDEFBUF)
        {
            //모든 플레이어블 캐릭터 방어력 증가
        }
        if(type == BUFF_TYPE.HEALBUF)
        {
            //모든 플레이어블 캐릭터 힐
        }
        if(type == BUFF_TYPE.TURNBUF)
        {
            //턴 맨 앞으로..
        }
    }
}
