using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Enemy
{
    /// <summary>
    /// エネミーダメージ情報
    /// </summary>
    public class EnemyDamageInfo
    {
        /// <summary>
        /// 前のHP
        /// </summary>
        public int PrevHp = 0;

        /// <summary>
        /// ダメージを受けたエネミー
        /// </summary>
        public Enemy DamagedEnemy = null;
    }

    /// <summary>
    /// エネミー死亡情報
    /// </summary>
    public class EnemyDeadInfo
    {
        /// <summary>
        /// 死んだエネミー
        /// </summary>
        public Enemy DeadEnemy = null;
    }

    /// <summary>
    /// エネミーイベントのObserver
    /// </summary>
    public interface IEnemyEventObserver
    {
        /// <summary>
        /// ダメージ
        /// </summary>
        IObserver<EnemyDamageInfo> Damaged { get; }

        /// <summary>
        /// 死亡
        /// </summary>
        IObserver<EnemyDeadInfo> Dead { get; }
    }

    /// <summary>
    /// エネミーイベントのObservable
    /// </summary>
    public interface IEnemyEventObservable
    {
        /// <summary>
        /// ダメージ
        /// </summary>
        IObservable<EnemyDamageInfo> OnDamaged { get; }

        /// <summary>
        /// 死亡
        /// </summary>
        IObservable<EnemyDeadInfo> OnDead { get; }
    }

    /// <summary>
    /// エネミーイベントSubject
    /// </summary>
    public class EnemyEventSubject : MonoBehaviour, IEnemyEventObserver, IEnemyEventObservable
    {
        /// <summary>
        /// 死亡Subject
        /// </summary>
        private Subject<EnemyDeadInfo> onDeadSubject = new Subject<EnemyDeadInfo>();

        /// <summary>
        /// ダメージSubject
        /// </summary>
        private Subject<EnemyDamageInfo> onDamagedSubject = new Subject<EnemyDamageInfo>();

        /// <summary>
        /// 死亡
        /// </summary>
        public IObservable<EnemyDeadInfo> OnDead => onDeadSubject;
        public IObserver<EnemyDeadInfo> Dead => onDeadSubject;

        /// <summary>
        /// ダメージ
        /// </summary>
        public IObservable<EnemyDamageInfo> OnDamaged => onDamagedSubject;
        public IObserver<EnemyDamageInfo> Damaged => onDamagedSubject;

    }
}
