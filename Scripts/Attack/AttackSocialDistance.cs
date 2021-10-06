using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collision;

namespace Attack
{
    /// <summary>
    /// 「ソーシャルディスタンス」攻撃コリジョン
    /// </summary>
    public class AttackSocialDistance : CollisionObject
    {
        /// <summary>
        /// Prefab
        /// </summary>
        private static GameObject prefab = null;

        /// <summary>
        /// Prefabのパス
        /// </summary>
        private static readonly string PrefabPath = "Prefabs/Attack/SocialDistance";

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="parentTransform">親のTransform</param>
        public static void Create(Transform parentTransform)
        {
            if (prefab == null)
            {
                prefab = Resources.Load<GameObject>(PrefabPath);
                Debug.Assert(prefab != null);
            }

            var obj = Instantiate<GameObject>(prefab);
            obj.transform.position = parentTransform.position;
            obj.transform.eulerAngles = parentTransform.eulerAngles;
        }

        /// <summary>
        /// 移動速度
        /// </summary>
        private static readonly float moveSpeed = 15.0f;

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void OnInitialize()
        {
            LifeTime = 1.5f;
            Radius = 10.0f;
        }

        /// <summary>
        /// 更新
        /// </summary>
        protected override void OnUpdate()
        {
            transform.position += transform.forward * (moveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 当たった
        /// </summary>
        /// <param name="applyable">当たったオブジェクト</param>
        protected override void OnHit(ICollisionApplyable applyable)
        {
            CollisionHitInfo info = new CollisionHitInfo();
            info.CollisionPosition = transform.position;
            info.PowerRate = 1.5f;

            applyable.ApplyCollision(ECollisionType.Attack, info);
        }
    }
}
