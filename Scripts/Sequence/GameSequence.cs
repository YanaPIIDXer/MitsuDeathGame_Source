using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Fade;
using Zenject;
using UI.Game;
using UnityEngine.SceneManagement;
using UniRx;

namespace Sequence
{
    /// <summary>
    /// ゲームシーケンス
    /// </summary>
    public class GameSequence : MonoBehaviour
    {
        /// <summary>
        /// フェードインタフェース
        /// </summary>
        [Inject]
        private IFade fade = null;

        /// <summary>
        /// メインUI
        /// </summary>
        [SerializeField]
        private MainUI mainUI = null;

        void Awake()
        {
            fade.FadeIn();
            mainUI.OnBackToTitle.Subscribe(_ =>
            {
                fade.OnFadeFinish.Subscribe(__ => SceneManager.LoadScene("Title"))
                                 .AddTo(gameObject);
                fade.FadeOut();
            }).AddTo(gameObject);
        }
    }
}
