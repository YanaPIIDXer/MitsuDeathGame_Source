using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collision;

namespace Attack
{
    /// <summary>
    /// 「密です」攻撃コリジョン
    /// </summary>
    public class AttackMitsu : CollisionObject
    {
        /// <summary>
        /// Prefab
        /// </summary>
        private static GameObject prefab = null;

        /// <summary>
        /// Prefabのパス
        /// </summary>
        private static readonly string PrefabPath = "Prefabs/Attack/Mitsu";

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="position">座標</param>
        public static void Create(Vector3 position)
        {
            if (prefab == null)
            {
                prefab = Resources.Load<GameObject>(PrefabPath);
                Debug.Assert(prefab != null);
            }

            var obj = Instantiate<GameObject>(prefab);
            obj.transform.position = position;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void OnInitialize()
        {
            LifeTime = 1.0f;
            Radius = 5.0f;
        }

        /// <summary>
        /// ヒットした
        /// </summary>
        /// <param name="applyable">ヒットしたオブジェクト</param>
        protected override void OnHit(ICollisionApplyable applyable)
        {
            CollisionHitInfo info = new CollisionHitInfo();
            info.CollisionPosition = transform.position;

            applyable.ApplyCollision(ECollisionType.Attack, info);
        }
    }
}
