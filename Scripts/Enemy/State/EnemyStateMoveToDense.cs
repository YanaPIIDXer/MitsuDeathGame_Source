using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.State
{
    /// <summary>
    /// 密ポイントに向けて移動するステート
    /// </summary>
    public class EnemyStateMoveToDense : EnemyStateBase
    {
        /// <summary>
        /// 剛体
        /// </summary>
        private Rigidbody rigidBody = null;

        /// <summary>
        /// アニメーション制御
        /// </summary>
        private Animator animator = null;

        /// <summary>
        /// 移動速度
        /// </summary>
        private static readonly float moveSpeed = 10.0f;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親</param>
        public EnemyStateMoveToDense(Enemy parent)
            : base(parent)
        {
            rigidBody = parent.GetComponent<Rigidbody>();
            animator = parent.GetComponent<Animator>();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            animator.SetBool("IsDown", false);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void Update()
        {
            Parent.transform.LookAt(Parent.ParentPoint.transform, Vector3.up);
            Vector3 dist = Parent.ParentPoint.transform.position - Parent.transform.position;
            if (dist.sqrMagnitude > 40.0f)
            {
                rigidBody.velocity = dist.normalized * moveSpeed;
            }

            animator.SetFloat("MoveSpeed", rigidBody.velocity.sqrMagnitude);
        }
    }
}
