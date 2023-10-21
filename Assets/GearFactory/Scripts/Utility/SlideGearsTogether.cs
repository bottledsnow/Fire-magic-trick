using UnityEngine;

namespace GearFactory
{
    /// <summary>
    /// Utility for making the two gears' teeth 
    /// engage into one another, modifying the
    /// distance between them.
    /// 
    /// Note 1:
    ///     gearA is stationary
    ///     gearB is moved to gearA
    /// 
    /// Note 2:
    ///     Remember that when SlideGears method
    ///     is invoked, the original distance
    ///     between the wheels is lost.
    /// 
    /// </summary>
    public class SlideGearsTogether : MonoBehaviour
    {
        public Gear gearA;
        public Gear gearB;

        [Range(0f, 1f)]
        public float slideFactor = 0.25f;
        public bool liveUpdate = false;

        //make gears snap into each other
        public static void SlideGears(Gear gearA, Gear gearB, float slideFactor)
        {
            Vector3 dir = gearA.transform.position - gearB.transform.position;
            float maxA = GetMaxDistance(gearA, slideFactor);
            float maxB = GetMaxDistance(gearB, slideFactor);

            float distanceDelta = dir.magnitude - maxA - maxB;
            gearB.transform.position += dir.normalized * distanceDelta;
        }

        //distance is based on cog length
        private static float GetMaxDistance(Gear gear, float slideFactor)
        {
            if (gear.transform.localScale.x != gear.transform.localScale.y)
            {
                Debug.LogWarning("Performing SlideGearsTogether::GetMaxDistance on gear " +
                                 "with non-uniform scale, the result may be ugly.");
            }

            GearStructure gs = gear.GetStructure();
            return Mathf.LerpUnclamped(
                gs.outerRadius * gear.transform.localScale.x,
                gs.tipRadius * gear.transform.localScale.x,
                slideFactor);
        }

        //check gears for null
        public bool CheckGears()
        {
            return gearA != null && gearB != null;
        }

        void OnDrawGizmos()
        {
            if (CheckGears())
            {
                GearHelper.DrawArrowGizmos(gearB.transform, gearA.transform, Color.green);
            }
        }
    }
}
