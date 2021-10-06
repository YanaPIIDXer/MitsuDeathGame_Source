using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    /// <summary>
    /// コリジョンを持つオブジェクト
    /// </summary>
    [RequireComponent(typeof(SphereCollider))]
    public abstract class CollisionObject : MonoBehaviour
    {
        /// <summary>
        /// コリジョン
        /// </summary>
        private new SphereCollider collider = null;

        /// <summary>
        /// 寿命で消滅するか？
        /// </summary>
        private bool isDestroyByLifeTime = false;

        /// <summary>
        /// 残り生存時間
        /// </summary>
        public float LifeTime
        {
            set
            {
                lifeTime = value;
                isDestroyByLifeTime = true;
            }
        }
        private float lifeTime = 0.0f;

        /// <summary>
        /// 半径
        /// </summary>
        /// <value></value>
        public float Radius
        {
            set
            {
                collider.radius = value;
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// 更新
        /// </summary>
        protected virtual void OnUpdate() { }

        /// <summary>
        /// ヒットした
        /// </summary>
        /// <param name="applyable">ICollisionApplyable実装オブジェクト</param>
        protected abstract void OnHit(ICollisionApplyable applyable);

        void Awake()
        {
            collider = GetComponent<SphereCollider>();
            collider.isTrigger = true;
            OnInitialize();
        }

        void Update()
        {
            OnUpdate();

            if (isDestroyByLifeTime)
            {
                lifeTime -= Time.deltaTime;
                if (lifeTime <= 0.0f)
                {
                    Destroy(gameObject);
                }
            }
        }

        void OnTriggerEnter(Collider collision)
        {
            var applyable = collision.gameObject.GetComponent<ICollisionApplyable>();
            if (applyable != null)
            {
                OnHit(applyable);
            }
        }
    }
}
