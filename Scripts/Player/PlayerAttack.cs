using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using Input;
using Attack;

namespace Player
{
    /// <summary>
    /// プレイヤー攻撃Component
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        /// <summary>
        /// 構築
        /// </summary>
        /// <param name="input">入力インタフェース</param>
        [Inject]
        public void Construct(IInput input)
        {
            input.Mitsu.Subscribe(_ => AttackMitsu.Create(transform.position + (transform.forward * 10.0f)))
                       .AddTo(gameObject);

            input.SocialDistance.Subscribe(_ => AttackSocialDistance.Create(transform))
                                .AddTo(gameObject);
        }
    }
}
