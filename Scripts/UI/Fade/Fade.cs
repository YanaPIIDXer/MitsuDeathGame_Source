using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace UI.Fade
{
    /// <summary>
    /// フェードインタフェース
    /// </summary>
    public interface IFade
    {
        /// <summary>
        /// フェードイン
        /// </summary>
        void FadeIn();

        /// <summary>
        /// フェードアウト
        /// </summary>
        void FadeOut();

        /// <summary>
        /// フェードが完了した
        /// </summary>
        IObservable<Unit> OnFadeFinish { get; }
    }

    /// <summary>
    /// フェード
    /// </summary>
    public class Fade : MonoBehaviour, IFade
    {
        /// <summary>
        /// Image
        /// </summary>
        private Image fadeImage = null;

        /// <summary>
        /// フェードにかかる時間
        /// </summary>
        private static readonly float FadeTime = 1.0f;

        /// <summary>
        /// 経過した時間
        /// </summary>
        private float elapsedTime = 0.0f;

        /// <summary>
        /// モード
        /// </summary>
        private enum EMode
        {
            None,
            FadeIn,
            FadeOut,
        }

        /// <summary>
        /// モード
        /// </summary>
        private EMode mode = EMode.None;

        /// <summary>
        /// フェード完了Subject
        /// </summary>
        private Subject<Unit> onFadeFinishSubject = new Subject<Unit>();

        /// <summary>
        /// フェードが完了した
        /// </summary>
        public IObservable<Unit> OnFadeFinish => onFadeFinishSubject;

        void Awake()
        {
            fadeImage = GetComponent<Image>();
        }

        void Update()
        {
            if (mode == EMode.None) { return; }

            elapsedTime = Mathf.Min(elapsedTime + Time.deltaTime, FadeTime);
            float alpha = elapsedTime / FadeTime;
            if (mode == EMode.FadeIn)
            {
                alpha = 1.0f - alpha;
            }
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            if (elapsedTime >= FadeTime)
            {
                mode = EMode.None;
                onFadeFinishSubject.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        public void FadeIn()
        {
            elapsedTime = 0.0f;
            mode = EMode.FadeIn;
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        public void FadeOut()
        {
            elapsedTime = 0.0f;
            mode = EMode.FadeOut;
        }
    }
}
