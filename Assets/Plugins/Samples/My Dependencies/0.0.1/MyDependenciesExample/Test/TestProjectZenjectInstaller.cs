using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using MyDependencies.Sources.Lifetimes;

namespace MyDependencies.Sources.Test
{
    public class TestProjectZenjectInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ITestClass, TestClass>(LifeTime.Single);
        }
    }
}