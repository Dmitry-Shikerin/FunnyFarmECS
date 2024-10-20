using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Configs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.SwingingTrees.Domain.Configs
{
    public class TreeSwingerConfig : ScriptableObject
    {
        [Header("Speed settings")]
        [Tooltip("How fast do the trees swing in the X axis")]
        [Range(0.001f,3f)]
        [SerializeField] private float _swingSpeedX;
        [Tooltip("The difference in swing speed of each tree in the X axis")]
        [Range(0,1f)]
        [SerializeField] private float _swingSpeedRandomnessX;
		
        [Tooltip("How fast do the trees swing in the Y axis")]
        [Range(0.001f,3f)]
        [SerializeField] private float _swingSpeedY;
		
        [Tooltip("The difference in swing speed of each tree in the Y axis")]
        [Range(0,1f)]
        [SerializeField] private float _swingSpeedRandomnessY;
		
        [Header("Angle settings")]
        [Tooltip("How far do the trees swing in the X axis")]
        [Range(0.001f,20f)]
        [SerializeField] private float _swingMaxAngleX;
        [Tooltip("The difference in how far does each trees swing in the X axis")]
        [Range(0.001f,5f)]
        [SerializeField] private float _swingMaxAngleRandomnessX;
		
        [Tooltip("How far do the trees swing in the Y axis")]
        [Range(0.001f,180f)]
        [SerializeField] private float _swingMaxAngleY;
        [Tooltip("The difference in how far does each trees swing in the Y axis")]
        [Range(0.001f,15f)]
        [SerializeField] private float _swingMaxAngleRandomnessY;

        [Header("Direction settings")]
        [Tooltip("The \"wind\" direction in angles from standard X axis")]
        [Range(0f,180f)]
        [SerializeField] private float _direction;
        [Tooltip("The \"wind\" direction randomness")]
        [Range(0f,180f)]
        [SerializeField] private float _directionRandomness;
        [SerializeField] private bool _enableYAxisSwinging;
        
        [HideInInspector]
        public TreeSwingerCollector Parent;
        [HideInInspector]
        public string Id;
        
        public float SwingSpeedX => _swingSpeedX;
		public float SwingSpeedRandomnessX => _swingSpeedRandomnessX;
		public float SwingSpeedY => _swingSpeedY;
		public float SwingSpeedRandomnessY => _swingSpeedRandomnessY;
		public float SwingMaxAngleX => _swingMaxAngleX;
		public float SwingMaxAngleRandomnessX => _swingMaxAngleRandomnessX;
		public float SwingMaxAngleY => _swingMaxAngleY;
		public float SwingMaxAngleRandomnessY => _swingMaxAngleRandomnessY;
		public float Direction => _direction;
		public float DirectionRandomness => _directionRandomness;
		public bool EnableYAxisSwinging => _enableYAxisSwinging;
    }
}