using UnityEngine;
using Zenject;

namespace Game
{
    public class ComboCounterInstalller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IComboEvent>()
                     .To<ComboCounter>()
                     .FromComponentInHierarchy()
                     .AsCached();
        }
    }
}
