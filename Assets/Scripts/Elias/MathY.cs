using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YergoScripts
{
    /// <summary>
    /// Collection of hassle-driven problems resulted to the research of additional Math Functions.
    /// </summary>
    public struct MathY
    {
        /// <summary>
        /// Converts Vector2 to Angle. Source: https://math.stackexchange.com/questions/180874/convert-angle-radians-to-a-heading-vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Vector2ToDegree(Vector2 v)
        {
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

            return angle < 0 ? angle + 360 : angle;
        }
       
        /// <summary>
        /// Linearly interperlate between a and b by t.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            Vector3 result;

            result.x = Mathf.Lerp(a.x, b.x, t);
            result.y = Mathf.Lerp(a.y, b.y, t);
            result.z = Mathf.Lerp(a.z, b.z, t);

            return result;
        }

        /// <summary>
        /// Checks close approximation between 2 values.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Approximately(Vector3 a, Vector3 b)
        {
            bool result = Mathf.Approximately(a.x, b.x);
            result = result && Mathf.Approximately(a.y, b.y);
            result = result && Mathf.Approximately(a.z, b.z);

            return result;
        }

        /// <summary>
        /// Converts Radians to Vector2. Source: https://answers.unity.com/questions/823090/equivalent-of-degree-to-vector2-in-unity.html
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        /// <summary>
        /// Converts Degrees to Vector2. Source: https://answers.unity.com/questions/823090/equivalent-of-degree-to-vector2-in-unity.html 
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Finds the minimum value among the entered values.
        /// </summary>
        /// <param name="minValues"></param>
        /// <returns></returns>
        public static int Min(params int[] minValues)
        {
            int minVal = int.MaxValue;

            foreach(int val in minValues)
            {
                if(minVal > val)
                    minVal = val;
            }

            return minVal;
        }
        
        /// <summary>
        /// Finds the minimum value among the entered values.
        /// </summary>
        /// <param name="minValues"></param>
        /// <returns></returns>
        public static float Min(params float[] minValues)
        {
            float minVal = int.MaxValue;

            foreach(float val in minValues)
            {
                if(minVal > val)
                    minVal = val;
            }

            return minVal;
        }

        /// <summary>
        /// Finds the maximum value among the entered values.
        /// </summary>
        /// <param name="minValues"></param>
        /// <returns></returns>
        public static int Max(params int[] maxValues)
        {
            int maxVal = int.MaxValue;

            foreach(int val in maxValues)
            {
                if(maxVal < val)
                    maxVal = val;
            }

            return maxVal;
        }

        
        /// <summary>
        /// Finds the maximum value among the entered values.
        /// </summary>
        /// <param name="minValues"></param>
        /// <returns></returns>
        public static float Max(params float[] maxValues)
        {
            float maxVal = float.MaxValue;

            foreach(float val in maxValues)
            {
                if(maxVal < val)
                    maxVal = val;
            }

            return maxVal;
        }
    }
}