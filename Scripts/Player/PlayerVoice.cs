using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input;
using Zenject;
using System;
using UniRx;

namespace Player
{
    /// <summary>
    /// プレイヤーボイス
    /// </summary>
    public class PlayerVoice : MonoBehaviour
    {
        /// <summary>
        /// 「密です」のオーディオソース
        /// </summary>
        [SerializeField]
        private AudioSource mitsuSource = null;

        /// <summary>
        /// 「ソーシャルディスタンス」のオーディオソース
        /// </summary>
        [SerializeField]
        private AudioSource socialDistanceSource = null;

        /// <summary>
        /// 入力のインジェクション
        /// </summary>
        /// <param name="input">入力インタフェース</param>
        [Inject]
        public void Construct(IInput input)
        {
            input.Mitsu.Subscribe(_ => mitsuSource.Play())
                       .AddTo(gameObject);

            input.SocialDistance.Subscribe(_ => socialDistanceSource.Play())
                                .AddTo(gameObject);
        }
    }
}
