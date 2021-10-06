using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using Game;

namespace UI.Game
{
    /// <summary>
    /// コンボカウント表示
    /// </summary>
    public class ComboCount : MonoBehaviour
    {
        /// <summary>
        /// テキスト
        /// </summary>
        private Text text = null;

        void Awake()
        {
            text = GetComponent<Text>();
            text.enabled = false;
        }

        /// <summary>
        /// IComboEventのInject
        /// </summary>
        /// <param name="observable">IComboEvent</param>
        [Inject]
        public void InjectComboEvent(IComboEvent observable)
        {
            observable.OnCombo
                      .Where(count => count >= 5)
                      .Subscribe(count =>
                      {
                          text.text = string.Format("{0} Combo!!", count);
                          text.enabled = true;
                      }).AddTo(gameObject);

            observable.OnComboEnd
                      .Subscribe(_ => text.enabled = false)
                      .AddTo(gameObject);
        }
    }
}
