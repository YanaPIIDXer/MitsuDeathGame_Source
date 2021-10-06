using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Game;
using UniRx;
using System;

namespace UI.Game
{
    /// <summary>
    /// リザルト画面
    /// </summary>
    public class ResultScreen : MonoBehaviour
    {
        /// <summary>
        /// スコアテキスト
        /// </summary>
        [SerializeField]
        private Text scoreText = null;

        /// <summary>
        /// 最大コンボテキスト
        /// </summary>
        [SerializeField]
        private Text maxComboText = null;

        /// <summary>
        /// タイトルに戻るボタン
        /// </summary>
        [SerializeField]
        private Button backToTitleButton = null;

        /// <summary>
        /// タイトルに戻るボタンが押された
        /// </summary>
        public IObservable<Unit> OnBackToTitle
        {
            get
            {
                return backToTitleButton.OnClickAsObservable()
                                        .First();
            }
        }

        /// <summary>
        /// リザルト情報をセット
        /// </summary>
        /// <param name="info">リザルト情報</param>
        public void SetResult(GameResultInfo info)
        {
            scoreText.text = info.Score.ToString();
            maxComboText.text = info.MaxCombo.ToString();
        }
    }
}
