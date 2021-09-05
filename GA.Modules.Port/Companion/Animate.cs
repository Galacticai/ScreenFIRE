
//using Transitions;
using FluentTransitions;
using System;

namespace GeekAssistant.Modules.General.Companion {
    internal static class Animate {
        /// <summary>
        /// Animate a property of an object with critical damping
        /// </summary>
        /// <param name="target">Parent object to work with</param>
        /// <param name="propName">Property of object to animate</param>
        /// <param name="destination">Final value of prop</param>
        /// <param name="AnimationTime">Optional animation time (ms) (Default: 500ms)</param>
        public static void Run<ObjType, destinationType>(ObjType target, string propName, destinationType destination, int AnimationTime = 500) {
            if (c.S.PerformAnimations)
                Transition.With(target, propName, destination).CriticalDamp(TimeSpan.FromMilliseconds(AnimationTime));
            else {
                var propInfo = target.GetType().GetProperty(propName);
                propInfo.SetValue(target, destination);
            }
        }
        public static void KillAnimation(Transition transition) {
            return;
        }
    }
}
