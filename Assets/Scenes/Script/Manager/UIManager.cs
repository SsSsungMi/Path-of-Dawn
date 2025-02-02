using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    // ���� �� UI
    public GameObject mainSceneCanvas;
    public GameObject lobbyStage;
    public GameObject infoStage;

    public UICamController uICamController;
    public InfoUIController infoUIController;
    public DeckSelectController deckSelectController;
    public ShopController shopController;

    // ��Ʋ �� UI
    public PlayerableUIController playerableUIController;
    public MonsterUIController monsterUIController;
    public BehaviorUIController behaviorUIController;
    public TurnTableUIController turnTableUIController;
    public BuffController buffController;

    public event Action<int> setBtnDel;
    public BtnViewController[] btnViewControllers;

    private void Start()
    {
        btnViewControllers[0].Init();
        btnViewControllers[1].Init();
    }

    public void SetBtn()
    {
        if (setBtnDel != null)
            setBtnDel(User.instance.MyCharacters.Count);
    }

    public void MainSceneUI(bool isShow)
    {
        mainSceneCanvas.SetActive(isShow);
        lobbyStage.SetActive(isShow);
        infoStage.SetActive(isShow);
    }

    public void ExitBattleUI()
    {
        playerableUIController.gameObject.SetActive(false);
        monsterUIController.gameObject.SetActive(false);
        behaviorUIController.gameObject.SetActive(false);
        turnTableUIController.gameObject.SetActive(false);
        turnTableUIController.Clear();
    }
}
