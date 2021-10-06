using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Game;
using Zenject;

namespace Input
{
    /// <summary>
    /// キー入力インタフェース
    /// </summary>
    public class KeyInput : MonoBehaviour, IInput
    {
        /// <summary>
        /// 移動
        /// </summary>
        public IObservable<Vector2> Move => InGameUpdate
                                                .Select(_ =>
                                                {
                                                    var y = UnityEngine.Input.GetAxisRaw("Vertical");
                                                    var x = UnityEngine.Input.GetAxisRaw("Horizontal");
                                                    return new Vector2(x, y);
                                                });

        public IObservable<float> Rotate => InGameUpdate
                                                .Select(_ =>
                                                {
                                                    float value = 0.0f;
                                                    if (UnityEngine.Input.GetKey(KeyCode.Q))
                                                    {
                                                        value = -1.0f;
                                                    }
                                                    else if (UnityEngine.Input.GetKey(KeyCode.E))
                                                    {
                                                        value = 1.0f;
                                                    }

                                                    return value;
                                                });


        /// <summary>
        /// 密ですボタン
        /// </summary>
        public IObservable<Unit> Mitsu => InGameUpdate
                                            .Where(_ => (!UnityEngine.Input.GetKey(KeyCode.LeftShift) && !UnityEngine.Input.GetKey(KeyCode.RightShift)) &&
                                                            UnityEngine.Input.GetKeyDown(KeyCode.Space))
                                            .Select(_ => Unit.Default);
        /// <summary>
        /// ソーシャルディスタンスボタン
        /// </summary>
        public IObservable<Unit> SocialDistance => InGameUpdate
                                                    .Where(_ => (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)) &&
                                                                 UnityEngine.Input.GetKeyDown(KeyCode.Space))
                                                    .Select(_ => Unit.Default);

        /// <summary>
        /// キー操作を受け付けるか？
        /// </summary>
        private bool bEnableInput = false;

        /// <summary>
        /// ゲーム中のアップデート
        /// キー操作を受け付けるかどうかを制御するためのもの
        /// </summary>
        private IObservable<Unit> InGameUpdate => Observable.EveryUpdate()
                                                            .Where(_ => bEnableInput)
                                                            .Select(_ => Unit.Default);

        /// <summary>
        /// 構築
        /// </summary>
        /// <param name="gameTimeEvent">ゲームタイムイベント</param>
        [Inject]
        public void Configure(IGameTimeEvent gameTimeEvent)
        {
            gameTimeEvent.OnStart
                         .Subscribe(_ => bEnableInput = true)
                         .AddTo(gameObject);

            gameTimeEvent.OnFinish
                         .Subscribe(_ => bEnableInput = false)
                         .AddTo(gameObject);
        }
    }
}
