using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    /// <summary>
    /// コリジョンタイプ
    /// </summary>
    public enum ECollisionType
    {
        Attack,
    }

    /// <summary>
    /// コリジョンが当たった時の情報
    /// </summary>
    public class CollisionHitInfo
    {
        /// <summary>
        /// コリジョンの座標
        /// </summary>
        public Vector3 CollisionPosition = Vector3.zero;

        public float PowerRate = 1.0f;
    }

    /// <summary>
    /// コリジョンによる影響を受けさせる為のインタフェース
    /// </summary>
    public interface ICollisionApplyable
    {
        /// <summary>
        /// コリジョンが当たった
        /// </summary>
        /// <param name="type">コリジョンの種類</param>
        /// <param name="hitInfo"ヒット時の情報</param>
        void ApplyCollision(ECollisionType type, CollisionHitInfo hitInfo);
    }
}
