using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Zenject;
using Input;

namespace Player
{
    /// <summary>
    /// プレイヤー移動
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        /// <summary>
        /// 剛体
        /// </summary>
        private Rigidbody rigidBody = null;

        /// <summary>
        /// 移動ベクトル
        /// </summary>
        private Vector3 moveVec = Vector3.zero;

        /// <summary>
        /// 移動速度
        /// </summary>
        private static readonly float moveSpeed = 10.0f;

        /// <summary>
        /// 構築
        /// </summary>
        /// <param name="input">入力インタフェース</param>
        [Inject]
        public void Construct(IInput input)
        {
            input.Move.Subscribe(v => moveVec = new Vector3(v.x, 0.0f, v.y))
                      .AddTo(gameObject);

            input.Rotate.Subscribe(value => transform.Rotate(Vector3.up * value));
        }

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            rigidBody.velocity = (transform.rotation * moveVec) * moveSpeed;
            moveVec = Vector3.zero;
        }
    }
}
