using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Input
{
    /// <summary>
    /// 入力インタフェース
    /// </summary>
    public interface IInput
    {
        /// <summary>
        /// 移動
        /// </summary>
        IObservable<Vector2> Move { get; }

        /// <summary>
        /// 回転
        /// </summary>
        IObservable<float> Rotate { get; }

        /// <summary>
        /// 密ですボタン
        /// </summary>
        IObservable<Unit> Mitsu { get; }

        /// <summary>
        /// ソーシャルディスタンスボタン
        /// </summary>
        IObservable<Unit> SocialDistance { get; }
    }
}
