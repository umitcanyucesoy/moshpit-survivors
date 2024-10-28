using Datas;
using LevelScripts;
using PlayerScripts;
using SpawnScripts;
using UI_Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Weapons;

namespace Scopes
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private DamageNumberController damageNumberController;
        [SerializeField] private LevelController levelController;
        [SerializeField] private UIController uiController;
        
        protected override void Configure(IContainerBuilder builder)
        {
            
            // SERVICES
            builder.Register<WaveService>(Lifetime.Singleton).AsSelf();
            
            
            // COMPONENTS
            builder.RegisterComponent(playerController);
            builder.RegisterComponent(damageNumberController);
            builder.RegisterComponent(levelController);
            builder.RegisterComponent(uiController);
            
            
            // DATAS
            builder.RegisterInstance(playerData).AsSelf();
            

        }
    }
}