using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace cs2
{
    [Serializable]
    internal class Configuration
    {
        public Configuration()
        {

        }

        #region UI

        public Vec2i FormRadarPos { get; set; } = new Vec2i(15, -2);
        public Vec2i FormSpectatorsPos { get; set; } = new Vec2i(15, 360);

        public bool EnableSpectators { get; set; }
        public bool EnableRadar { get; set; }
        public float RadarScale { get; set; } = 12;
        public float RadarEnemyRadius { get; set; } = 3;

        #endregion

        #region ESP

        public bool ESP_Boxes { get; set; }
        public bool ESP_Skeleton { get; set; }
        public bool ESP_Weapon { get; set; }
        public bool ESP_Health { get; set; }
        public bool ESP_Flash { get; set; }
        public bool ESP_Alerts { get; set; }
        public bool ESP_Ammo { get; set; }

        #endregion

        #region AimAssist

        public float FOV_Radius { get; set; } = 80;
        public bool EnableAimAssist { get; set; }
        public bool EnableTriggerbot { get; set; }
        public float AimAssistMult { get; set; } = 10;

        #endregion

        #region other

        public bool Misc_Crosshair { get; set; }
        public bool Misc_Scoreboard { get; set; }

        #endregion

        public static Configuration Current { get; set; } = new Configuration();
    }

    [Serializable]
    public struct Vec2i
    {
        public Vec2i() { }

        public Vec2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x, y;
    }
}
