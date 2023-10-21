using UnityEngine;

namespace GearFactory
{
    /// <summary>
    /// Copy rotation of other gear. The amount (and sign)
    /// of source rotation can be tailored by 
    /// rotationMultiplier.
    /// 
    /// It can be both connected to one of the gears or
    /// used as a separate object.
    /// </summary>
    public class CopyInverseRotation : MonoBehaviour
    {
        public GearBase masterGear;
        public GearBase slaveGear;
        public float rotationMulitplier = 1.0f;
        public bool IsUpdating = true;

        private float previousGearARotation;

        void Start()
        {
            previousGearARotation = GetMasterZRotation();
        }

        void FixedUpdate()
        {
            if (IsUpdating && CheckGears())
            {
                float axisValue = GetMasterZRotation();
                float deltaAngle = axisValue - previousGearARotation;
                deltaAngle *= rotationMulitplier;

                slaveGear.transform.Rotate(Vector3.forward, -deltaAngle);

                //save rotation in this frame
                previousGearARotation = GetMasterZRotation();
            }
        }

        //shortcut
        private float GetMasterZRotation()
        {
            return masterGear.transform.rotation.eulerAngles.z;
        }

        //check gears for null
        public bool CheckGears()
        {
            return masterGear != null && slaveGear != null;
        }

        void OnDrawGizmos()
        {
            if (CheckGears())
            {
                GearHelper.DrawArrowGizmos(masterGear.transform, slaveGear.transform, Color.blue);
            }
        }
    }
}
