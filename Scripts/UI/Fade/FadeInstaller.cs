using UnityEngine;
using Zenject;

namespace UI.Fade
{
    public class FadeInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFade>()
                     .To<Fade>()
                     .FromComponentInHierarchy()
                     .AsCached();
        }
    }
}
