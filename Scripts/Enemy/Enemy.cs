using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collision;
using Enemy.State;
using UniRx;
using System;
using Zenject;

namespace Enemy
{
    /// <summary>
    /// 敵キャラクラス
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour, ICollisionApplyable
    {
        /// <summary>
        /// ダメージの種類
        /// </summary>
        public enum EDamageType
        {
            Attacked,
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="parentPoint">親となるポイント</param>
        /// <param name="factory">ファクトリ</param>
        /// <returns>生成したインスタンス</returns>
        public static Enemy Spawn(DensePoint parentPoint, EnemyFactory factory)
        {
            var pos = parentPoint.transform.position + new Vector3(UnityEngine.Random.Range(-20.0f, 20.0f), 0.0f, UnityEngine.Random.Range(-20.0f, 20.0f));
            var enemy = factory.Create();
            enemy.transform.position = pos;
            enemy.ParentPoint = parentPoint;
            return enemy;
        }

        /// <summary>
        /// エネミーイベントObserver
        /// </summary>
        [Inject]
        private IEnemyEventObserver enemyEventObserver = null;

        /// <summary>
        /// 親となる密ポイント
        /// </summary>
        public DensePoint ParentPoint { get; private set; }

        /// <summary>
        /// 現在のState
        /// </summary>
        private EnemyStateBase currentState = null;

        /// <summary>
        /// 最大HP
        /// </summary>
        public static readonly int MaxHp = 3;

        /// <summary>
        /// HP
        /// </summary>
        public int Hp
        {
            get { return hp; }
            private set
            {
                hp = Math.Max(value, 0);
            }
        }
        private int hp = MaxHp;

        /// <summary>
        /// 次のState
        /// </summary>
        public EnemyStateBase NextState
        {
            set
            {
                if (nextState == null || nextState.IsStateChangeable)
                {
                    nextState = value;
                }
            }
            private get
            {
                return nextState;
            }
        }
        private EnemyStateBase nextState = null;

        /// <summary>
        /// 剛体
        /// </summary>
        public Rigidbody Rigidbody { get { return rigidBody; } }
        private Rigidbody rigidBody = null;

        /// <summary>
        /// ボイス
        /// </summary>
        private EnemyVoice voice = null;

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            currentState = new EnemyStateMoveToDense(this);
        }

        /// <summary>
        /// EnemyVoiceの構築
        /// </summary>
        /// <param name="observable">イベントObservable</param>
        [Inject]
        public void ConfigureEnemyVoice(IEnemyEventObservable observable)
        {
            voice = new EnemyVoice(GetComponent<AudioSource>());
            voice.Configure(observable);
        }

        void Update()
        {
            currentState.Update();
            if (NextState != null && currentState.IsStateChangeable)
            {
                currentState = NextState;
                currentState.Initialize();
                NextState = null;
            }
        }

        /// <summary>
        /// ダメージを与える
        /// </summary>
        /// <param name="value">ダメージ量</param>
        /// <param name="damageType">ダメージの種類</param>
        public void ApplyDamage(int value, EDamageType damageType)
        {
            if (Hp <= 0) { return; }

            EnemyDamageInfo damageInfo = new EnemyDamageInfo();
            damageInfo.PrevHp = Hp;
            damageInfo.DamagedEnemy = this;
            Hp -= value;
            enemyEventObserver.Damaged.OnNext(damageInfo);

            if (Hp <= 0)
            {
                // 死亡
                BeRadgoll();        // 死んだらラグドールになる
                NextState = new EnemyStateDead(this);

                EnemyDeadInfo deadInfo = new EnemyDeadInfo();
                deadInfo.DeadEnemy = this;
                enemyEventObserver.Dead.OnNext(deadInfo);
            }
        }

        /// <summary>
        /// コリジョンがヒットした
        /// </summary>
        /// <param name="type">コリジョンの種類</param>
        /// <param name="hitInfo">ヒット情報</param>
        public void ApplyCollision(ECollisionType type, CollisionHitInfo hitInfo)
        {
            switch (type)
            {
                case ECollisionType.Attack:

                    if (!currentState.IsDamageApplyable) { return; }

                    ApplyDamage(1, EDamageType.Attacked);
                    Vector3 blowPower = (transform.position - hitInfo.CollisionPosition);
                    blowPower.y = UnityEngine.Random.Range(1.0f, 2.0f);
                    blowPower = blowPower.normalized * UnityEngine.Random.Range(500.0f, 1000.0f) * hitInfo.PowerRate;
                    rigidBody.AddForce(blowPower, ForceMode.Force);
                    NextState = new EnemyStateBlow(this);
                    break;
            }
        }

        /// <summary>
        /// ラグドール化
        /// </summary>
        private void BeRadgoll()
        {
            var animator = GetComponent<Animator>();
            animator.enabled = false;

            var bodies = GetComponentsInChildren<Rigidbody>();
            foreach (var body in bodies)
            {
                if (body != rigidBody)      // 「InChildren」と銘打ちながら自分自身からも取得する罠がある
                {
                    // 子オブジェクトの座標を固定しないと、オブジェクトの実態とは離れた座標に吹っ飛ぶ可能性がある
                    body.constraints = RigidbodyConstraints.FreezePosition;
                }
                body.isKinematic = false;
            }
        }
    }
}
