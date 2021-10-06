using UnityEngine;
using Zenject;

namespace Game
{
    public class GameTimeControlInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameTimeEvent>()
                     .To<GameTimeControl>()
                     .FromComponentInNewPrefabResource("Prefabs/Zenject/Instances/GameTimeControl")
                     .AsCached();
        }
    }
}
