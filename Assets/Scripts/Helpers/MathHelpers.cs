using Unity.Mathematics;
using Unity.Transforms;

namespace Helpers
{
    public class MathHelpers
    {
        public static float3 GetDirection(float3 from, float3 to)
        {
            float3 direction = to - from;
            //math.normalize(direction);
            return math.normalizesafe(direction);
        }

        public static float3 GetDirectionFromForward(quaternion rotation)
        {
           return math.forward(rotation);
        }
        
    }
}