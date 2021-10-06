using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using System;
using Game;
using Cysharp.Threading.Tasks;

namespace UI.Game
{
    /// <summary>
    /// 「３、２、１、START!!」や「FINISH!!」の表示
    /// </summary>
    public class AnnounceText : MonoBehaviour
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
        /// GameTiemEventのInject
        /// </summary>
        /// <param name="timeEvent">GameTimeEventインタフェース</param>
        [Inject]
        public void InjectGameTimeEvent(IGameTimeEvent timeEvent)
        {
            timeEvent.OnStartCountDown.Subscribe(value => text.text = value.ToString())
                                      .AddTo(gameObject);

            timeEvent.OnStart.Subscribe(async _ =>
            {
                text.text = "START!!";
                await UniTask.Delay(1000);
                text.text = "";
            }).AddTo(gameObject);

            timeEvent.OnFinish.Subscribe(async _ =>
            {
                text.text = "FINISH!!";
                await UniTask.Delay(1000);
                text.text = "";
            }).AddTo(gameObject);
        }
    }
}
