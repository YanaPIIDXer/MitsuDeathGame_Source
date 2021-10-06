using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace UI.Title
{
    /// <summary>
    /// スタートボタン
    /// </summary>
    public class StartButton : MonoBehaviour
    {
        /// <summary>
        /// 押された
        /// </summary>
        public IObservable<Unit> OnClick
        {
            get
            {
                var button = GetComponent<Button>();
                return button.OnClickAsObservable()
                             .First();
            }
        }
    }
}
