using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using UnityEngine;

namespace Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Services
{
    public class TreeSwingService
    {
        public Quaternion GetSwing(SweengingTreeComponent treeSwinger)
        {
            return Quaternion.Euler(
                treeSwinger.MaxAngleX * 
                Mathf.Sin(Time.time * treeSwinger.SpeedX), 
                treeSwinger.EnableYAxisSwingingTree
                    ? treeSwinger.MaxAngleY * Mathf.Sin(Time.time * treeSwinger.SpeedY) 
                    : treeSwinger.Direction, 0f);
        }
    }
}