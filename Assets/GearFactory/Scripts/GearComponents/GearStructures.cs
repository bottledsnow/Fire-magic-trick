using System;

namespace GearFactory
{
    [Serializable]
    public struct GearStructure
    {
        public float innerRadius;
        public float rootRadius;
        public float rootAngleShiftMultiplier;
        public float outerRadius;
        public float outerAngleShiftMultiplier;
        public float tipRadius;
        public float tipAngleShiftMultiplier;
        public int sides;
        public float teethSlant;
    }

    [Serializable]
    public struct GearStructure3D
    {
        public float innerRadius;
        public float rootRadius;
        public float rootAngleShiftMultiplier;
        public float outerRadius;
        public float outerAngleShiftMultiplier;
        public float tipRadius;
        public float tipAngleShiftMultiplier;
        public int sides;
        public float teethSlant;
        public float thickness;
    }
}
