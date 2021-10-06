using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

namespace UI.Game
{
    public class ScoreText : MonoBehaviour
    {
        /// <summary>
        /// テキスト
        /// </summary>
        private Text text = null;

        void Awake()
        {
            text = GetComponent<Text>();
        }

        /// <summary>
        /// ScoreEventインタフェースのInject
        /// </summary>
        /// <param name="scoreEvent">ScoreEventインタフェース</param>
        [Inject]
        public void InjectScoreEvent(IScoreEvent scoreEvent)
        {
            scoreEvent.OnScoreUpdated.Subscribe(value => text.text = string.Format("Score:{0}", value))
                                     .AddTo(gameObject);
        }
    }
}
