using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Enemy;

namespace UI.Game
{
    /// <summary>
    /// 敵のHPゲージ
    /// </summary>
    public class EnemyHpGauge : MonoBehaviour
    {
        /// <summary>
        /// Prefabのパス
        /// </summary>
        private static readonly string PrefabPath = "Prefabs/UI/EnemyHpGauge";

        /// <summary>
        /// Prefab
        /// </summary>
        private static EnemyHpGauge prefab = null;

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="info">ダメージ情報</param>
        /// <returns>生成したゲージ</returns>
        public static EnemyHpGauge Create(EnemyDamageInfo info)
        {
            if (prefab == null)
            {
                prefab = Resources.Load<EnemyHpGauge>(PrefabPath);
                Debug.Assert(prefab != null);
            }

            var gauge = Instantiate<EnemyHpGauge>(prefab);
            gauge.prevHp = info.PrevHp;
            gauge.currentHp = info.DamagedEnemy.Hp;
            gauge.enemyTransform = info.DamagedEnemy.transform;
            return gauge;
        }

        /// <summary>
        /// HPゲージのトランスフォーム
        /// </summary>
        [SerializeField]
        private RectTransform gaugeTransform = null;

        /// <summary>
        /// 前のHP
        /// </summary>
        private int prevHp = 0;

        /// <summary>
        /// 現在のＨＰ
        /// </summary>
        private int currentHp = 0;

        /// <summary>
        /// エネミーのTransform
        /// </summary>
        private Transform enemyTransform = null;

        /// <summary>
        /// 表示時間
        /// </summary>
        private static readonly float DisplayTime = 1.5f;

        /// <summary>
        /// 経過時間
        /// </summary>
        private float elapsedTime = 0.0f;

        void Update()
        {
            gaugeTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, enemyTransform.position + Vector3.up);

            elapsedTime = Mathf.Min(elapsedTime + Time.deltaTime, DisplayTime);
            UpdateValue(Mathf.Lerp(prevHp, currentHp, elapsedTime / DisplayTime));
            if (elapsedTime >= DisplayTime)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 値をセット
        /// </summary>
        /// <param name="Value">値</param>
        private void UpdateValue(float value)
        {
            float rate = (Enemy.Enemy.MaxHp - value) / Enemy.Enemy.MaxHp * 100.0f;
            gaugeTransform.localPosition = gaugeTransform.localPosition + new Vector3(-rate, 0.0f, 0.0f);
            gaugeTransform.sizeDelta = new Vector2(100.0f - rate, 10.0f);
        }
    }
}
