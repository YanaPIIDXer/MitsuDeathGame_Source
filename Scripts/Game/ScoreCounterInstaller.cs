using UnityEngine;
using Zenject;

namespace Game
{
    public class ScoreCounterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IScoreEvent>()
                     .To<ScoreCounter>()
                     .FromComponentInHierarchy()
                     .AsCached();
        }
    }
}
