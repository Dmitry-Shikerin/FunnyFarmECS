﻿using NodeCanvas.Framework;
using ParadoxNotion.Design;

using NodeCanvas.BehaviourTrees;
using NodeCanvas.StateMachines;
using NodeCanvas.DialogueTrees;

namespace FlowCanvas.Nodes
{

    [Name("Sub Tree")]
    [DropReferenceType(typeof(BehaviourTree))]
    [ParadoxNotion.Design.Icon("BT")]
    public class FlowNestedBT : FlowNestedBase<BehaviourTree>
    {
        protected override void RegisterPorts() {
            base.RegisterPorts();
            AddValueOutput<Status>("Status", () => currentInstance.rootStatus);
        }
    }

    [Name("Sub FSM")]
    [DropReferenceType(typeof(FSM))]
    [ParadoxNotion.Design.Icon("FSM")]
    public class FlowNestedFSM : FlowNestedBase<FSM>
    {
        protected override void RegisterPorts() {
            base.RegisterPorts();
            AddValueOutput<IState>("State", () => currentInstance.currentState);
        }
    }

    [Name("Sub Dialogue")]
    [DropReferenceType(typeof(DialogueTree))]
    [ParadoxNotion.Design.Icon("Dialogue")]
    public class FlowNestedDT : FlowNestedBase<DialogueTree>
    {
        protected override void RegisterPorts() {
            base.RegisterPorts();
            AddValueOutput<IDialogueActor>("Actor", () => currentInstance.currentNode?.finalActor);
        }
    }
}