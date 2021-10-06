using UnityEngine;
using Zenject;

namespace Input
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInput>()
                     .To<KeyInput>()
                     .FromComponentInNewPrefabResource("Prefabs/Zenject/Instances/KeyInput")
                     .AsCached();
        }
    }
}
