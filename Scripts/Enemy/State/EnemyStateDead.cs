using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Enemy.State
{
    /// <summary>
    /// エネミーステート：死亡
    /// </summary>
    public class EnemyStateDead : EnemyStateBase
    {
        /// <summary>
        /// 生存時間（ミリ秒）
        /// </summary>
        private static readonly int LifeTimeMilliSecs = 8000;

        /// <summary>
        /// ステートを切り替えられる状態か？
        /// ※死んでるのでこれ以上ステートが切り替わるのは困る
        /// </summary>
        public override bool IsStateChangeable => false;

        /// <summary>
        /// ダメージを与えられるか？
        /// </summary>
        public override bool IsDamageApplyable => true;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親</param>
        public EnemyStateDead(Enemy parent)
            : base(parent)
        {
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override async void Initialize()
        {
            await UniTask.Delay(LifeTimeMilliSecs);
            GameObject.Destroy(Parent.gameObject);
        }
    }
}
