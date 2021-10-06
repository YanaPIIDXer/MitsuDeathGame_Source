using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
using Zenject;
using UniRx;
using System;
using Cysharp.Threading.Tasks;

namespace UI.Game
{
    /// <summary>
    /// タイマーＵＩ
    /// </summary>
    public class GameTimer : MonoBehaviour
    {
        private Text text = null;

        private int CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                currentTime = value;
                text.text = value.ToString();
            }
        }
        private int currentTime = 0;

        void Awake()
        {
            text = GetComponent<Text>();
            CurrentTime = GameConfig.GamePlayLimitTime;
        }

        /// <summary>
        /// タイムイベントのInject
        /// </summary>
        /// <param name="timeEvent">タイムイベント</param>
        [Inject]
        public void InjectTimeEvent(IGameTimeEvent timeEvent)
        {
            timeEvent.OnStart.Subscribe(async _ =>
            {
                while (CurrentTime > 0)
                {
                    await UniTask.Delay(1000);
                    CurrentTime--;
                }
            }).AddTo(gameObject);

            timeEvent.OnFinish.Subscribe(_ => text.text = "")
                              .AddTo(gameObject);
        }
    }
}
