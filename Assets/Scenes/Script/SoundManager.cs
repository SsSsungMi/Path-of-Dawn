using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    public SoundComponent soundPrefab;
    public Queue<SoundComponent> soundPool = new Queue<SoundComponent>();
    public AudioSource audioSource;

    public AudioClip[] bgms;    // 배경음악
    public int bgmIndex;

    public AudioClip BackBtn;                 // 뒤로가기 버튼
    public AudioClip basicBtnSfx;             // 기본 버튼
    public AudioClip InfoBtnSfx;              // 정보창 캐릭터 선택 버튼
    public AudioClip deckSelectBtnSfx;        // 덱 선택 버튼
    public AudioClip deckBackSelectBtnSfx;    // 덱 선택 취소 버튼
    public AudioClip shopSelectBtnSfx;        // 상점 선택 버튼
    public AudioClip shopBuyBtnSfx;           // 상점 구매 버튼
    public AudioClip pveSelectBtnSfx;         // PVE 스테이지 선택 버튼
    public AudioClip pvpSelectBtnSfx;         // PVP 스테이지 선택 버튼
    public AudioClip stageSelectBtnSfx;       // 스테이지 선택 버튼

    [Header("Volume")]
    public Scrollbar bgmSlider;
    public Scrollbar sfxSlider;

    Dictionary<int , AudioClip> bgmDic = new Dictionary<int , AudioClip>();
    public new void Awake()
    {
        base.Awake();
        Init();
        audioSource = GetComponent<AudioSource>();

        bgmDic.Add(0, bgms[0]);
        bgmDic.Add(1, bgms[1]);
        bgmDic.Add(2, bgms[2]);
        bgmDic.Add(3, bgms[0]);
        bgmDic.Add(4, bgms[0]);
        bgmDic.Add(5, bgms[3]);
        bgmDic.Add(6, bgms[4]);
        bgmDic.Add(7, bgms[2]);
        bgmDic.Add(8, bgms[2]);

        audioSource.clip = bgms[0];
        audioSource.Play();
    }
    public void Init()
    {
        for (int i = 0; i < 100; i++)
        {
            SoundComponent sound = Instantiate(soundPrefab, transform);
            sound.gameObject.SetActive(false);
            soundPool.Enqueue(sound);
        }
    }
    private void Update()
    {
        audioSource.volume = bgmSlider.value;
    }
    public SoundComponent Pop()
    {
        SoundComponent sm = soundPool.Dequeue();
        sm.gameObject.SetActive(true);
        return sm.GetComponent<SoundComponent>();
    }

    public void ReturnPool(SoundComponent returnObj)
    {
        returnObj.gameObject.SetActive(false);
        returnObj.transform.SetParent(transform);
        soundPool.Enqueue(returnObj);
    }

    public void BgmPlay(int index)
    {
        if(audioSource.clip == bgmDic[index])
            return;

        audioSource.clip = bgmDic[index];
        audioSource.Play();
    }

    public void Play(AudioClip clip, Transform target = null)
    {
        SoundComponent sound = Pop();
        sound.transform.parent = target;
        sound.Play(clip);
    }
}
