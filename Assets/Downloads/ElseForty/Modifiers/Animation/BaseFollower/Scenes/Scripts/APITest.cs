
using System.Linq;
using ElseForty.splineplus;
using ElseForty.splineplus.animation;
using ElseForty.splineplus.animation.api;
using ElseForty.splineplus.animation.model;
using ElseForty.splineplus.core.api;
using ElseForty.splineplus.core.models;

using UnityEngine;

public class APITest : MonoBehaviour
{
     public void Start()
     {
          var splinePlus = SplinePlusClassSetup();
          BaseFollowerClassSetup(splinePlus);
     }

     SplinePlusClass SplinePlusClassSetup()
     {
          //create a spline plus gameObject  
          var splinePlus = SplinePlusAPI.CreateSplinePlus(new Vector3(0, 0, 0));
          var spaceType = SpaceType_Enum.World;
          //create nodes 
          var node1 = splinePlus.CreateNode(new Vector3(-20, 0, 0), new Vector3(-15, 0, 0), new Vector3(-25, 0, 0), spaceType);
          var node2 = splinePlus.CreateNode(new Vector3(10, 0, 0), new Vector3(15, 0, 0), new Vector3(5, 0, 0), spaceType);
          var node3 = splinePlus.CreateNode(new Vector3(30, 0, 10), new Vector3(35, 0, 10), new Vector3(25, 0, 10), spaceType);
          var node4 = splinePlus.CreateNode(new Vector3(80, 0, 10), new Vector3(100, 0, 10), new Vector3(60, 0, 10), spaceType);
          var node6 = splinePlus.CreateNode(new Vector3(30, 0, -10), new Vector3(25, 0, -10), new Vector3(35, 0, -10), spaceType);
          var node5 = splinePlus.CreateNode(new Vector3(80, 0, -10), new Vector3(60, 0, -10), new Vector3(100, 0, -10), spaceType);

          //create a branches, catch the branch key  
          var branchKey1 = splinePlus.CreateBranch();
          var branch1 = splinePlus.GetBranch(branchKey1);
          var branchKey2 = splinePlus.CreateBranch();
          var branch2 = splinePlus.GetBranch(branchKey2);
          var branchKey3 = splinePlus.CreateBranch();
          var branch3 = splinePlus.GetBranch(branchKey3);
          var branchKey4 = splinePlus.CreateBranch();
          var branch4 = splinePlus.GetBranch(branchKey4);

          //add nodes to branches 
          branch1.AddNodeAtEnd(node1, splinePlus, false);
          branch1.AddNodeAtEnd(node2, splinePlus, false);

          branch2.AddNodeAtEnd(node2, splinePlus, false);
          branch2.AddNodeAtEnd(node3, splinePlus, false);

          branch3.AddNodeAtEnd(node3, splinePlus, false);
          branch3.AddNodeAtEnd(node4, splinePlus, false);
          branch3.AddNodeAtEnd(node5, splinePlus, false);
          branch3.AddNodeAtEnd(node6, splinePlus, false);

          branch4.AddNodeAtEnd(node6, splinePlus, false);
          branch4.AddNodeAtEnd(node2, splinePlus, false);


          //flip the last handle to correct the curvature 
          var lastNodeIndex = branch4.GetBranchNodes().Count() - 1;
          branch4.FlipNodeHandlesAtIndex(lastNodeIndex, splinePlus);

          //update spline plus
          splinePlus.UpdateSplinePlus();

          return splinePlus;
     }

     void BaseFollowerClassSetup(SplinePlusClass splinePlus)
     {
          //add baseFollowers modifier
          var baseFollowerClass = (BaseFollowerClass)splinePlus.AddModifier(Modifiers_Enum.BaseFollowers);

          //add new baseFollower
          var go = GameObject.CreatePrimitive(PrimitiveType.Cube); ;
          var newBaseFollower = baseFollowerClass.Create(go);

          //get the first available branch key in the branches dictionnary
          var newBaseFollowerBranchKey = splinePlus.GetDictionary().FirstOrDefault().Key;

          newBaseFollower.SetBranch(splinePlus, newBaseFollowerBranchKey);
          newBaseFollower.SetDistance(0);
          newBaseFollower.SetSpeed(8);
          newBaseFollower.SetLerpFactor(4);
          newBaseFollower.SetLocalRotation(new Vector3(0, 0, 90));
          newBaseFollower.SetControlType(ControlType_Enum.Auto);
          newBaseFollower.Animate();

          //update spline plus
          splinePlus.UpdateSplinePlus();
     }
}

