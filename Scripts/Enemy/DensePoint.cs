using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Zenject;

namespace Enemy
{
    /// <summary>
    /// 密ポイント
    /// こいつが親になってEnemyを生み出し、
    /// Enemyはこいつに群がってくるように動く
    /// </summary>
    public class DensePoint : MonoBehaviour
    {
        /// <summary>
        /// 生成できる最大数
        /// </summary>
        private static readonly int MaxCount = 10;

        /// <summary>
        /// 現在の生成数
        /// </summary>
        private int currentCount = 0;

        /// <summary>
        /// エネミーファクトリ
        /// </summary>
        [Inject]
        private EnemyFactory enemyFactory = null;

        void Awake()
        {
            Observable.Interval(TimeSpan.FromSeconds(10))
                      .Subscribe(_ =>
                      {
                          int count = UnityEngine.Random.Range(3, 5);
                          if (currentCount + count > MaxCount)
                          {
                              count = MaxCount - currentCount;
                          }
                          for (int i = 0; i < count; i++)
                          {
                              var enemy = Enemy.Spawn(this, enemyFactory);
                          }
                          currentCount += count;
                      });
        }

        /// <summary>
        /// EventObservableの注入
        /// </summary>
        /// <param name="observable">Observable</param>
        [Inject]
        public void InjectEventObservable(IEnemyEventObservable observable)
        {
            observable.OnDead
                      .Where(info => info.DeadEnemy.ParentPoint == this)
                      .Subscribe(_ => currentCount--)
                      .AddTo(gameObject);
        }
    }
}
