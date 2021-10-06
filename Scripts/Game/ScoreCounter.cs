using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Zenject;
using Enemy;

namespace Game
{
    /// <summary>
    /// スコアイベント
    /// </summary>
    public interface IScoreEvent
    {
        /// <summary>
        /// スコアが更新された
        /// </summary>
        IObservable<int> OnScoreUpdated { get; }
    }

    /// <summary>
    /// スコア計算
    /// </summary>
    public class ScoreCounter : MonoBehaviour, IScoreEvent
    {
        /// <summary>
        /// スコア更新Subject
        /// </summary>
        private Subject<int> onScoreUpdatedSubject = new Subject<int>();

        /// <summary>
        /// スコアが更新された
        /// </summary>
        public IObservable<int> OnScoreUpdated => onScoreUpdatedSubject;

        /// <summary>
        /// 現在のスコア
        /// </summary>
        public int CurrentScore
        {
            get { return currentScore; }
            private set
            {
                currentScore = value;
                onScoreUpdatedSubject.OnNext(currentScore);
            }
        }

        private int currentScore = 0;

        /// <summary>
        /// IEnemyEventObservableの注入
        /// </summary>
        /// <param name="observable">IEnemyEventObservable</param>
        [Inject]
        public void InjectEnemyEventObservable(IEnemyEventObservable observable)
        {
            observable.OnDead.Subscribe(_ => CurrentScore += 10)
                             .AddTo(gameObject);
        }

        /// <summary>
        /// IComboEventの注入
        /// </summary>
        /// <param name="observable">IComboEvent</param>
        [Inject]
        public void InjectComboEvent(IComboEvent observable)
        {
            observable.OnCombo
                      .Where(count => count >= 5)
                      .Subscribe(count => CurrentScore += count * 5)
                      .AddTo(gameObject);
        }
    }
}
