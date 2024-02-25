using cs2.Config;
using cs2.Game.Objects;
using cs2.GameOverlay;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static cs2.Offsets.OffsetsLoader;

namespace cs2.Game.Features
{
    internal static class WorldEsp
    {
        public static void Start()
        {
            new Thread(() =>
            {
                for (; ; )
                {
                    Update();
                    Thread.Sleep(1);
                }
            }).Start();
        }

        public static void Update()
        {
            lock (_block)
            {
                _objects.Clear();
                if (!Configuration.Current.ESP_World_Weapons)
                {
                    Thread.Sleep(100);
                    return;
                }
                IntPtr entityListPtr = Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwEntityList);
                for (int i = 0; i < 1024; i++)
                {
                    IntPtr list_entry = Memory.Read<IntPtr>(entityListPtr + 8 * ((i & 0x7FFF) >> 9) + 0x10);
                    if (list_entry == IntPtr.Zero)
                        continue;
                    IntPtr baseEntity = Memory.Read<IntPtr>(list_entry + 120 * (i & 0x1FF));
                    if (baseEntity == IntPtr.Zero)
                        continue;

                    IntPtr entityIdentity = Memory.Read<IntPtr>(baseEntity + 0x10);
                    if (entityIdentity == IntPtr.Zero)
                        continue;
                    IntPtr classNameAddr = Memory.Read<IntPtr>(entityIdentity + 0x20);

                    string designerName = Memory.ReadString(classNameAddr, 64);
                    bool projectile = designerName.Contains("projectile");
                    if (designerName.StartsWith("weapon") || projectile)
                    {
                        IntPtr gameSceneNode = Memory.Read<IntPtr>(baseEntity + C_BaseEntity.m_pGameSceneNode);
                        Vector3 origin = Memory.Read<Vector3>(gameSceneNode + CGameSceneNode.m_vecAbsOrigin); //grenade position

                        // interno: -20992
                        //int one = (int)Memory.Read<short>(entityIdentity + 0x8);
                        // m_nCreationTick - 0x520
                        Weapon w = null!;
                        if (!projectile)
                        {
                            w = new Weapon(baseEntity) { Origin = origin, Name = designerName, };
                            w.State = Memory.Read<int>(baseEntity + C_CSWeaponBase.m_iState);
                            w.UpdateIndex();
                        }
                        else
                        {
                            w = new Projectile(baseEntity, designerName)
                            {
                                Origin = origin
                            };
                        }

                        _objects.Add(w);
                    }
                }
            }
        }

        public static void Draw(Graphics g)
        {
            if (!Configuration.Current.ESP_World_Weapons)
                return;
            lock (_block)
            {
                foreach (Weapon weapon in _objects)
                {
                    if (weapon.State != 0)
                        continue;
                    Vector3 pos2d = weapon.Origin.ToScreenPos();
                    if (pos2d.IsValidScreen() && pos2d != Vector3.Zero)
                    {
                        Brushes.Share.Color = Configuration.Current.ESP_World_Weapons_Color;
                        g.DrawText(Fonts.WeaponIcons, 16, weapon is Projectile ? Brushes.Red : Brushes.Share, pos2d.X, pos2d.Y, weapon.ToIcon().ToString());
                    }
                }
            }
        }

        private static List<Weapon> _objects = new List<Weapon>();
        private static readonly object _block = new();
        //private static readonly byte[] _buffer = "weap"u8.ToArray();
    }
}
