using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.gameplay.Helpers
{
    public class SinTrajectory : Trajectory
    {
        public float amplitude;
        public float phase;
        public float coef;


        public SinTrajectory(float _amplitude, float _phase, float _coef)
        {
            amplitude = _amplitude;
            phase = _phase;
            coef = _coef;
        }
        public override float compute(float x)
        {
            return (float)Math.Sin((x + phase) * coef) * amplitude + 0.5f;
        }

    }
}
