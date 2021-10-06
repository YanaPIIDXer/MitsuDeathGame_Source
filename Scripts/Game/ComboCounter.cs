using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Zenject;
using UniRx;
using System;

namespace Game
{
    /// <summary>
    /// コンボ関係イベントインタフェース
    /// </summary>
    public interface IComboEvent
    {
        /// <summary>
        /// コンボが増えた
        /// </summary>
        IObservable<int> OnCombo { get; }

        /// <summary>
        /// コンボが切れた
        /// </summary>
        IObservable<Unit> OnComboEnd { get; }

        /// <summary>
        /// 最大コンボが更新された
        /// </summary>
        IObservable<int> OnMaxComboUpdated { get; }
    }

    /// <summary>
    /// コンボのカウント
    /// </summary>
    public class ComboCounter : MonoBehaviour, IComboEvent
    {
        /// <summary>
        /// 現在のコンボ数
        /// </summary>
        private int currentCombo = 0;

        /// <summary>
        /// 最大コンボ
        /// </summary>
        private int maxCombo = 0;

        /// <summary>
        /// コンボカウントが増えた時のSubject
        /// </summary>
        private Subject<int> onComboSubject = new Subject<int>();
        public IObservable<int> OnCombo => onComboSubject;

        /// <summary>
        /// コンボが切れた時のSubject
        /// </summary>
        private Subject<Unit> onComboEndSubject = new Subject<Unit>();
        public IObservable<Unit> OnComboEnd => onComboEndSubject;

        /// <summary>
        /// 最大コンボが更新された時のSubject
        /// </summary>
        private Subject<int> onMaxComboUpdatedSubject = new Subject<int>();
        public IObservable<int> OnMaxComboUpdated => onMaxComboUpdatedSubject;

        /// <summary>
        /// コンボ継続時間
        /// </summary>
        private static readonly float ComboEndInterval = 2.0f;

        /// <summary>
        /// コンボが切れるまでの時間
        /// </summary>
        private float comboTimer = 0.0f;

        /// <summary>
        /// エネミーイベントObservableの注入
        /// </summary>
        /// <param name="observable">エネミーイベントObsdervable</param>
        [Inject]
        public void InjectEnemyEventObservablw(IEnemyEventObservable observable)
        {
            observable.OnDamaged
                      .Where(info => info.PrevHp > 0)
                      .Subscribe(_ =>
                      {

                          currentCombo++;
                          onComboSubject.OnNext(currentCombo);
                          comboTimer = ComboEndInterval;
                      })
                      .AddTo(gameObject);
        }

        void Update()
        {
            if (comboTimer <= 0.0f) { return; }

            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0.0f)
            {
                if (maxCombo < currentCombo)
                {
                    maxCombo = currentCombo;
                    onMaxComboUpdatedSubject.OnNext(maxCombo);
                }
                currentCombo = 0;
                onComboEndSubject.OnNext(Unit.Default);
            }
        }
    }
}
