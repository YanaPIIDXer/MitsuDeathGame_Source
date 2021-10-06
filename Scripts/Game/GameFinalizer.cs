using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

namespace Game
{
    /// <summary>
    /// リザルト情報
    /// </summary>
    public class GameResultInfo
    {
        /// <summary>
        /// スコア
        /// </summary>
        public int Score = 0;

        /// <summary>
        /// 最大コンボ
        /// </summary>
        public int MaxCombo = 0;
    }

    /// <summary>
    /// ゲーム終了管理インタフェース
    /// </summary>
    public interface IGameFinalizer
    {
        /// <summary>
        /// 終了Observable
        /// </summary>
        IObservable<GameResultInfo> OnFinish { get; }
    }

    /// <summary>
    /// ゲーム終了管理
    /// </summary>
    public class GameFinalizer : MonoBehaviour, IGameFinalizer
    {
        /// <summary>
        /// リザルト情報
        /// </summary>
        private GameResultInfo resultInfo = new GameResultInfo();

        /// <summary>
        /// 終了Subject
        /// </summary>
        private Subject<GameResultInfo> onFinishSubject = new Subject<GameResultInfo>();

        /// <summary>
        /// 終了Observable
        /// </summary>
        public IObservable<GameResultInfo> OnFinish => onFinishSubject;

        /// <summary>
        /// IGameTimeEventの注入
        /// </summary>
        /// <param name="timeEvent">GameTimeEventインタフェース</param>
        [Inject]
        public void InjectGameTimeEvent(IGameTimeEvent timeEvent)
        {
            timeEvent.OnFinish.Subscribe(async _ =>
            {
                await UniTask.Delay(3000);
                onFinishSubject.OnNext(resultInfo);
            }).AddTo(gameObject);
        }

        /// <summary>
        /// IScoreEventの注入
        /// </summary>
        /// <param name="scoreEvent">ScoreEventインタフェース</param>
        [Inject]
        public void InjectScoreEvent(IScoreEvent scoreEvent)
        {
            scoreEvent.OnScoreUpdated
                      .Subscribe(score => resultInfo.Score = score)
                      .AddTo(gameObject);
        }

        /// <summary>
        /// IComboEventの注入
        /// </summary>
        /// <param name="comboEvent">ComboEventインタフェース</param>
        [Inject]
        public void InjectComboEvemt(IComboEvent comboEvent)
        {
            comboEvent.OnMaxComboUpdated
                      .Subscribe(maxCombo => resultInfo.MaxCombo = maxCombo)
                      .AddTo(gameObject);
        }
    }
}
