using cs2.Game.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Config
{
    [Serializable]
    public class WeaponConfig
    {
        public WeaponConfig()
        {

        }

        public WeaponConfig(WeaponConfigType type)
        {
            this.type = type;
        }

        #region tb

        public bool EnableTriggerbot { get; set; }
        public int DelayAfterShot { get; set; } = 500;

        #endregion

        #region aim

        public float FOV { get; set; } = 90;
        public bool EnableAimAssist { get; set; }
        public float Smoothing { get; set; } = 1.5f;
        public int ShotsFired { get; set; } = 1;
        public bool RCS { get; set; } = false;
        public bool VelocityPrediction { get; set; } = false;

        public AimAssist.HitboxBone bones = AimAssist.HitboxBone.head |
                                        AimAssist.HitboxBone.neck_0 |
                                        AimAssist.HitboxBone.pelvis |
                                        AimAssist.HitboxBone.spine_1 |
                                        AimAssist.HitboxBone.spine_2;

        #endregion

        public WeaponConfigType type;

    }
}
