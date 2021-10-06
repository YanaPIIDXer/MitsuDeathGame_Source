using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Zenject;

namespace Game
{
    /// <summary>
    /// ゲーム初期化クラス
    /// </summary>
    public class GameInitializer : MonoBehaviour
    {
        /// <summary>
        /// 密ポイント生成
        /// </summary>
        /// <param name="factory">ファクトリ</param>
        [Inject]
        public void SpawnDensePoints(DensePointFactory factory)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-25.0f, 25.0f), 0.0f, Random.Range(-25.0f, 25.0f));
                var obj = factory.Create();
                obj.transform.position = pos;
            }
        }
    }
}
