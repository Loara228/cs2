﻿using cs2.GameOverlay.UI.Controls;
using GameOverlay.Drawing;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace cs2.Config
{
    [Serializable]
    public class Configuration
    {
        public Configuration()
        {
        }

        public static bool Save()
        {
            File.Delete(_path);
            StreamWriter? writer = null;
            try
            {
                writer = new StreamWriter(_path);
                XmlSerializer serializer = new(typeof(Configuration));
                serializer.Serialize(writer, Current);
                writer.Close();
            }
            catch (Exception exc)
            {
                Program.Log(exc.ToString(), ConsoleColor.Red);
                writer?.Close();
                return false;
            }
            return true;
        }

        public static bool Load()
        {
            StreamReader? reader = null;
            try
            {
                reader = new StreamReader(_path);
                XmlSerializer serializer = new(typeof(Configuration));
                Current = (Configuration)serializer.Deserialize(reader)!;
                if (Current == null)
                    throw new NullReferenceException($"{nameof(Current)} file is null");

                reader.Close();
            }
            catch (Exception exc)
            {
                reader?.Close();
                Program.Log(exc.ToString(), ConsoleColor.Red);
                return false;
            }
            return true;
        }

        public WeaponConfig GetWeaponFromType(WeaponConfigType type)
        {
            if (type == WeaponConfigType.PISTOL)
                return Pistols;
            else if (type == WeaponConfigType.SMG)
                return SMGs;
            else if (type == WeaponConfigType.RIFLE)
                return Rifles;
            else if (type == WeaponConfigType.SNIPER_RIFLE)
                return SniperRifles;
            else if (type == WeaponConfigType.SHOTGUN)
                return Shotguns;
            throw new NotImplementedException($"Config.GetWeaponFromType({type})");
        }

        #region UI

        public Vec2i FormRadarPos { get; set; } = new Vec2i(330, 10);
        public Vec2i FormBombTimePos { get; set; } = new Vec2i(330, 350);
        public Vec2i FormScoreboardPos { get; set; } = new Vec2i(800, 10);
        public Vec2i FormRadarSize { get; set; } = new Vec2i(200, 200 + UIForm.HEADER_SIZE);
        public Vec2i FormSpectatorsPos { get; set; } = new Vec2i(15, 360);

        public bool EnableSpectators { get; set; }
        public bool EnableRadar { get; set; }
        public float RadarScale { get; set; } = 12;
        public float RadarEnemyRadius { get; set; } = 3;

        #endregion

        #region ESP

        public ESP_Box_Type ESP_Box_Type { get; set; } = 0;
        public bool ESP_Boxes { get; set; }
        public bool ESP_Skeleton { get; set; }
        public bool ESP_Weapon { get; set; }
        public bool ESP_Health { get; set; }
        public bool ESP_Flash { get; set; }
        public bool ESP_Alerts { get; set; }
        public bool ESP_Ammo { get; set; }
        public bool ESP_World_Weapons { get; set; }

        public float ESP_Boxes_Stroke { get; set; } = 1f;

        public float ESP_Bone_Stroke { get; set; } = 1.5f;

        public float ESP_Weapon_Font_Size { get; set; } = 12f;

        public Color ESP_Boxes_Color { get; set; } = new Color(255, 0, 0);

        public Color ESP_Skeleton_Color { get; set; } = new Color(255, 0, 0);

        public Color ESP_Boxes_Spotted_Color { get; set; } = new Color(0, 255, 0);

        public Color ESP_Skeleton_Spotted_Color { get; set; } = new Color(0, 255, 0);

        public Color ESP_Weapon_Color { get; set; } = new Color(255, 255, 255);

        public Color ESP_World_Weapons_Color { get; set; } = new Color(255, 255, 255, 220);

        #endregion

        #region AimAssist

        public WeaponConfig Pistols { get; set; } = new WeaponConfig(WeaponConfigType.PISTOL);
        public WeaponConfig SMGs { get; set; } = new WeaponConfig(WeaponConfigType.SMG);
        public WeaponConfig Rifles { get; set; } = new WeaponConfig(WeaponConfigType.RIFLE);
        public WeaponConfig SniperRifles { get; set; } = new WeaponConfig(WeaponConfigType.SNIPER_RIFLE);
        public WeaponConfig Shotguns { get; set; } = new WeaponConfig(WeaponConfigType.SHOTGUN);
        public int AimDelay { get; set; } = 10;

        #endregion

        #region Misc

        public bool Misc_Scoreboard { get; set; }
        public bool Misc_BombTimer { get; set; }
        public bool DM_Mode_Enabled { get; set; }
        public bool HitMarker { get; set; }
        public bool Bhop { get; set; }

        #region perf

        public int FPS_Max = 40;
        public int THR_DELAY_AIM = 10;
        public int THR_DELAY_BHOP = 25;
        public int THR_DELAY_TB = 25;

        #endregion

        #endregion

        #region Crosshair

        public bool Misc_Crosshair { get; set; }
        public bool Crosshair_Sniper_Only { get; set; } = false;

        public float Crosshair_Length { get; set; } = 8f;
        public float Crosshair_Thickness { get; set; } = 2f;
        public float Crosshair_Gap { get; set; } = 2f;
        public float Crosshair_Outline { get; set; } = 0.5f;
        public Color Crosshair_Fill { get; set; } = new Color(255, 255, 255);
        public Color Crosshair_Stroke { get; set; } = new Color(0, 0, 0);

        #endregion

        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "configs", "0.xml");
        internal static Configuration Current { get; set; } = new Configuration();
    }
}
