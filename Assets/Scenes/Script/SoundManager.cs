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

    public AudioClip[] bgms;    // �������
    public int bgmIndex;

    public AudioClip BackBtn;                 // �ڷΰ��� ��ư
    public AudioClip basicBtnSfx;             // �⺻ ��ư
    public AudioClip InfoBtnSfx;              // ����â ĳ���� ���� ��ư
    public AudioClip deckSelectBtnSfx;        // �� ���� ��ư
    public AudioClip deckBackSelectBtnSfx;    // �� ���� ��� ��ư
    public AudioClip shopSelectBtnSfx;        // ���� ���� ��ư
    public AudioClip shopBuyBtnSfx;           // ���� ���� ��ư
    public AudioClip pveSelectBtnSfx;         // PVE �������� ���� ��ư
    public AudioClip pvpSelectBtnSfx;         // PVP �������� ���� ��ư
    public AudioClip stageSelectBtnSfx;       // �������� ���� ��ư

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
