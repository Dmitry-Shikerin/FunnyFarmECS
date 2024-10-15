using MyDependencies.Sources.Containers;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;

namespace Sources.Frameworks.Utils.Injects
{
    public static class InjectUtils
    {
        public static void InjectFsm(this FSMOwner fsmOwner, DiContainer container)
        {
            foreach (FSMState state in fsmOwner.behaviour.GetAllNodesOfType<FSMState>())
                container.Inject(state);
            
            foreach (ConditionTask task in fsmOwner.behaviour.GetAllTasksOfType<ConditionTask>())
                container.Inject(task);
        }
    }
}