using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.CompositionRoot
{
    public class MonoInstaller : LifetimeScope
    {
        // Autorun gameobjects
        protected override void Configure(IContainerBuilder builder)
        {
        }
    }
}