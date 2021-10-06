using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using System;
using Game;
using Enemy;

namespace UI.Game
{
    /// <summary>
    /// ゲームプレイ中のメインUI
    /// </summary>
    public class MainUI : MonoBehaviour
    {
        /// <summary>
        /// リザルト画面のタイトルに戻るボタンが押された時のSubject
        /// </summary>
        private Subject<Unit> onBackToTitleSubject = new Subject<Unit>();

        /// <summary>
        /// リザルト画面のタイトルに戻るボタンが押された
        /// </summary>
        public IObservable<Unit> OnBackToTitle => onBackToTitleSubject;

        /// <summary>
        /// EnemyEventObservableのInject
        /// </summary>
        /// <param name="observable">EnemyEventObservableインタフェース</param>
        [Inject]
        public void InjectEnemyEventObservable(IEnemyEventObservable observable)
        {
            observable.OnDamaged
                      .Subscribe(info =>
                      {
                          var gauge = EnemyHpGauge.Create(info);
                          gauge.transform.parent = transform.parent;    // Canvasを親にする
                      }).AddTo(gameObject);
        }

        /// <summary>
        /// GameFinalizerインタフェースのInject
        /// </summary>
        /// <param name="gameFinalizer">GameFinalizerインタフェース</param>
        [Inject]
        public void InjectGameFinalizer(IGameFinalizer gameFinalizer)
        {
            gameFinalizer.OnFinish
                         .Subscribe(info =>
                         {
                             ResultScreen prefab = Resources.Load<ResultScreen>("Prefabs/UI/Result");
                             Debug.Assert(prefab != null);

                             ResultScreen screen = Instantiate<ResultScreen>(prefab, transform);
                             screen.SetResult(info);
                             screen.OnBackToTitle.Subscribe(_ => onBackToTitleSubject.OnNext(Unit.Default))
                                                 .AddTo(gameObject);
                         }).AddTo(gameObject);
        }
    }
}
