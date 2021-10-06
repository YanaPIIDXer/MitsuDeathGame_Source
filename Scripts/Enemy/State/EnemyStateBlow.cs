using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.State
{
    /// <summary>
    /// エネミーステート：吹っ飛び
    /// </summary>
    public class EnemyStateBlow : EnemyStateBase
    {
        /// <summary>
        /// ダメージを与えられるか？
        /// </summary>
        public override bool IsDamageApplyable => false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親</param>
        public EnemyStateBlow(Enemy parent)
            : base(parent)
        {
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            var animator = Parent.GetComponent<Animator>();
            animator.SetBool("IsDown", true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void Update()
        {
            if (Parent.Rigidbody.velocity.sqrMagnitude < 10.0f)
            {
                Parent.NextState = new EnemyStateMoveToDense(Parent);
            }
        }
    }
}
