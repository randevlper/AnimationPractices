using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gold {
    namespace Delegates {
        public delegate void ActionValue<T> (T value);
        //public delegate void Inform(); //System.Action is the equivelent
    }

    public class MathG {
        public static Vector2 RadianToVector2 (float radian) {
            return new Vector2 (Mathf.Cos (radian), Mathf.Sin (radian));
        }

        public static Vector2 DegreeToVector2 (float degree) {
            //if(degree > 180)
            return RadianToVector2 (degree * Mathf.Deg2Rad);
        }
    }
}