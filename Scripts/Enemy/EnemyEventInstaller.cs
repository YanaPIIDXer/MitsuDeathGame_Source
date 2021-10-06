using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyFactory : PlaceholderFactory<Enemy> { }

    public class DensePointFactory : PlaceholderFactory<DensePoint> { }

    public class EnemyEventInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEnemyEventObservable>()
                     .To<EnemyEventSubject>()
                     .FromComponentInHierarchy()
                     .AsCached();

            Container.Bind<IEnemyEventObserver>()
                     .To<EnemyEventSubject>()
                    .FromComponentInHierarchy()
                     .AsCached();

            Container.BindFactory<Enemy, EnemyFactory>()
                    .FromComponentInNewPrefabResource("Enemy/Prefabs/Enemy");

            Container.BindFactory<DensePoint, DensePointFactory>()
                    .FromNewComponentOnNewGameObject();
        }
    }
}
