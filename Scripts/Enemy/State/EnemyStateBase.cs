using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.State
{
    /// <summary>
    /// エネミーステート基底クラス
    /// </summary>
    public abstract class EnemyStateBase
    {
        /// <summary>
        /// 親
        /// </summary>
        protected Enemy Parent { get; private set; }

        /// <summary>
        /// ステートを切り替えてもいい状態か？
        /// </summary>
        public virtual bool IsStateChangeable { get { return true; } }

        /// <summary>
        /// ダメージを与えられるか？
        /// </summary>
        public virtual bool IsDamageApplyable { get { return true; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親</param>
        public EnemyStateBase(Enemy parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// 更新処理
        /// </summary>
        public virtual void Update() { }
    }
}
