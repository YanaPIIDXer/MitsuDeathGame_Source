using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Enemy
{
    /// <summary>
    /// 敵ボイス
    /// </summary>
    public class EnemyVoice
    {
        /// <summary>
        /// ダメージボイス
        /// </summary>
        private static AudioClip damageVoice = null;

        /// <summary>
        /// 死亡ボイス
        /// </summary>
        private static AudioClip deadVoice = null;

        /// <summary>
        /// オーディオソース
        /// </summary>
        private AudioSource audioSource = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EnemyVoice(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        /// <summary>
        /// 構築
        /// </summary>
        /// <param name="observable">イベントObservable</param>
        public void Configure(IEnemyEventObservable observable)
        {
            if (damageVoice == null)
            {
                damageVoice = Resources.Load<AudioClip>("Voices/Enemy/damage");
                Debug.Assert(damageVoice != null);
            }

            if (deadVoice == null)
            {
                deadVoice = Resources.Load<AudioClip>("Voices/Enemy/dead");
                Debug.Assert(deadVoice != null);
            }

            observable.OnDamaged
                      .Where(info => info.DamagedEnemy.Hp > 0)
                      .Subscribe(_ =>
                      {
                          try
                          {
                              audioSource.PlayOneShot(damageVoice);
                          }
                          catch (ArgumentNullException) { }
                      });

            observable.OnDead
                      .Subscribe(_ =>
                      {
                          try
                          {
                              audioSource.PlayOneShot(deadVoice);
                          }
                          catch (ArgumentNullException) { }
                      });
        }
    }
}
