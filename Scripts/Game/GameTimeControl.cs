using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Cysharp.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// ゲーム時間イベントインタフェース
    /// </summary>
    public interface IGameTimeEvent
    {
        /// <summary>
        /// 開始までのカウントダウン
        /// </summary>
        IObservable<int> OnStartCountDown { get; }

        /// <summary>
        /// 開始
        /// </summary>
        IObservable<Unit> OnStart { get; }

        /// <summary>
        /// 終了
        /// </summary>
        IObservable<Unit> OnFinish { get; }
    }

    /// <summary>
    /// ゲーム時間管理
    /// </summary>
    public class GameTimeControl : MonoBehaviour, IGameTimeEvent
    {
        /// <summary>
        /// 開始までのカウントダウンSubject
        /// </summary>
        private Subject<int> onStartCountDownSubject = new Subject<int>();

        /// <summary>
        /// 開始までのカウントダウン
        /// </summary>
        public IObservable<int> OnStartCountDown => onStartCountDownSubject;

        /// <summary>
        /// 開始Subject
        /// </summary>
        private Subject<Unit> onStartSubject = new Subject<Unit>();

        /// <summary>
        /// 開始
        /// </summary>
        public IObservable<Unit> OnStart => onStartSubject;

        /// <summary>
        /// 終了Subject
        /// </summary>
        private Subject<Unit> onFinishSubject = new Subject<Unit>();

        /// <summary>
        /// 終了
        /// </summary>
        /// <returns></returns>
        public IObservable<Unit> OnFinish => onFinishSubject;

        async void Start()
        {
            for (int count = 3; count > 0; count--)
            {
                onStartCountDownSubject.OnNext(count);
                await UniTask.Delay(1000);
            }
            onStartSubject.OnNext(Unit.Default);

            await UniTask.Delay(GameConfig.GamePlayLimitTime * 1000);

            onFinishSubject.OnNext(Unit.Default);
        }
    }
}
