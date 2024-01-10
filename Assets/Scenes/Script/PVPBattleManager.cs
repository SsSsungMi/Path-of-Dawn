using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPBattleManager : MonoBehaviourPun
{
    public int OwnerCount
    {
        get => ownerCount;
        set
        {
            ownerCount = value;
        }
    }
    int ownerCount;

    public int OtherCount
    {
        get => otherCount;
        set
        {
            otherCount = value;
        }

    }
    int otherCount;

    int index;

    [SerializeField]
    Transform[] masterPlayerPos;
    [SerializeField]
    Transform[] otherPlayerPos;

    public Playerable[] playerablesArray;
    public List<Playerable> ownerPlayerableList = new List<Playerable>();
    public List<Playerable> otherPlayerableList = new List<Playerable>();
    public List<Playerable> battleList = new List<Playerable>();

    public Character CurTurnCharacter
    {
        get => curTurnCharacter;
        set
        {
            curTurnCharacter = value;
        }
    }
    [SerializeField] Character curTurnCharacter;
    public Character TargetCharacter
    {
        get => targetCharacter;

        set
        {
            targetCharacter = value;
        }
    }
    [SerializeField] Character targetCharacter;

    void Start()
    {
       InitPvP();
    }

    void InitPvP()
    {
        if(photonView.IsMine)
            photonView.RPC("SetPos", RpcTarget.AllBuffered);

        playerablesArray = FindObjectsOfType<Playerable>();
        Debug.Log(playerablesArray.Length);

        PlayerableListInit();

        index = 0;
        OwnerCount = ownerPlayerableList.Count;
        OtherCount = otherPlayerableList.Count;

        BattleStart();
    }

    void Update()
    {
       
    }

    void PlayerableListInit()
    {
        foreach (Playerable playerable in playerablesArray)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                ownerPlayerableList.Add(playerable);
                playerable.OnDie += () => { OwnerCount--; };
            }
            else
            {
                otherPlayerableList.Add(playerable);
                Debug.Log("other");
                playerable.OnDie += () => { OtherCount--; };
            }
        }
    }


    void BattleListInit()
    {
        foreach (Playerable playerable in ownerPlayerableList)
            battleList.Add(playerable);

        foreach (Playerable playerable in otherPlayerableList)
            battleList.Add(playerable);

        battleList.Sort();
    }

    void BattleStart()
    {
        BattleListInit();
        CurTurnCharacter = battleList[0];
    }

    public void NextOrder()
    {
        if (battleList.Count <= 0)
            return;

        index++;
        if (index >= battleList.Count)
            index = 0;


        if (battleList[index].isDie == true)
        {
            NextOrder();
            return;
        }
        CurTurnCharacter = battleList[index];
    }

    [PunRPC]
    public void SetPos(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            for (int i = 0; i < User.instance.Deck.Count; i++)
            {
                string[] ppp = User.instance.Deck[i].gameObject.name.Split("(");
                GameObject p = PhotonNetwork.Instantiate(ppp[0], transform.position, Quaternion.identity);
                p.GetComponent<Character>().DeepCopy(User.instance.Deck[i]);
                Transform[] playerPos = PhotonNetwork.IsMasterClient ? masterPlayerPos : otherPlayerPos;
                p.transform.position = playerPos[i].position;
                if (PhotonNetwork.IsMasterClient)
                    p.transform.LookAt(otherPlayerPos[i]);
                else
                    p.transform.LookAt(masterPlayerPos[i]);

                ownerPlayerableList.Add(p.GetComponent<Playerable>());
            }
        }
        else
        {
            foreach(Playerable playerable in ownerPlayerableList)
                otherPlayerableList.Add(playerable);
        }
    }
}
