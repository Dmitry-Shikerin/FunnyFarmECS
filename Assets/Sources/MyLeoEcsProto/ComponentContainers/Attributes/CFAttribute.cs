using System;

namespace Sources.MyLeoEcsProto.ComponentContainers.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CFAttribute : Attribute
    {
        public CFAttribute()
        {
        }
    }
}