using UnityEngine;
using Zenject;

namespace Game
{
    public class GameFinalizerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameFinalizer>()
                     .To<GameFinalizer>()
                     .FromComponentInHierarchy()
                     .AsCached();
        }
    }
}
