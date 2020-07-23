using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] Button retryButton;
    [SerializeField] Text stageNumText;

    public override void OnStart()
    {
        retryButton.onClick.AddListener(OnClickRetryButtton);

        base.SetScreenAction(thisScreen: ScreenState.Game);

        this.ObserveEveryValueChanged(currentSceneBuildIndex => Variables.currentSceneBuildIndex)
            .Subscribe(currentSceneBuildIndex => { ShowStageNumText(); })
            .AddTo(this.gameObject);

        gameObject.SetActive(true);

    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen()) { return; }

    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
    }

    protected override void OnClose()
    {
        // gameObject.SetActive(false);
    }

    void ShowStageNumText()
    {
        stageNumText.text = "LEVEL " + Variables.currentSceneBuildIndex;
    }

    void OnClickRetryButtton()
    {
        bool canClick = Variables.screenState == ScreenState.Game;
        if (!canClick) { return; }
        base.ReLoadScene();
    }
}
