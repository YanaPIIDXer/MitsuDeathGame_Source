using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Fade;
using Zenject;
using UniRx;
using System;
using UI.Title;
using UnityEngine.SceneManagement;

namespace Sequence
{
    /// <summary>
    /// タイトルシーケンス
    /// </summary>
    public class TitleSequence : MonoBehaviour
    {
        [Inject]
        private IFade fade = null;

        /// <summary>
        /// スタートボタン
        /// </summary>
        [SerializeField]
        private StartButton startButton = null;

        void Awake()
        {
            fade.FadeIn();
            startButton.OnClick.Subscribe(_ =>
            {
                fade.OnFadeFinish.Subscribe(__ => SceneManager.LoadScene("Game"))
                                 .AddTo(gameObject);
                fade.FadeOut();
            }).AddTo(gameObject);
        }
    }
}
