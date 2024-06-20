using cs2.Offsets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets
{
    internal static class OffsetsLoader
    {
        public static bool Initialize(LoadType type)
        {
            Program.Log($"Load.{type}");

            C_BaseEntity = new C_BaseEntity();
            ClientOffsets = new Interfaces.ClientOffsets();
            CBasePlayerController = new CBasePlayerController();
            C_BasePlayerPawn = new C_BasePlayerPawn();
            CPlayer_ObserverServices = new CPlayer_ObserverServices();
            C_CSPlayerPawnBase = new C_CSPlayerPawnBase();
            CGameSceneNode = new CGameSceneNode();
            CCSPlayerController = new CCSPlayerController();
            CCSPlayerController_InGameMoneyServices = new CCSPlayerController_InGameMoneyServices();
            C_BaseModelEntity = new C_BaseModelEntity();
            C_CSPlayerPawn = new C_CSPlayerPawn();
            EntitySpottedState_t = new EntitySpottedState_t();
            C_CSWeaponBase = new C_CSWeaponBase();
            C_BasePlayerWeapon = new C_BasePlayerWeapon();
            C_PlantedC4 = new C_PlantedC4();
            C_EconEntity = new C_EconEntity();
            C_EconItemView = new C_EconItemView();
            CSkeletonInstance = new CSkeletonInstance();

            if (type == LoadType.FROM_GIT)
            {
                using (WebClient wc = new WebClient())
                {
                    const string clientDllUrl = "https://raw.githubusercontent.com/a2x/cs2-dumper/main/output/client.dll.cs";
                    const string offseltsUrl = "https://raw.githubusercontent.com/a2x/cs2-dumper/main/output/offsets.cs";

                    string clientDllData = wc.DownloadString(clientDllUrl);
                    string offsetsData = wc.DownloadString(offseltsUrl);

                    ParseData(clientDllData, offsetsData);
                    return true;
                }
            }
            else if (type == LoadType.FROM_DIR)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "generated");
                string clientDllPath = Path.Combine(path, "client.dll.cs");
                string offsetsPath = Path.Combine(path, "offsets.cs");

                if (!File.Exists(clientDllPath))
                {
                    Program.Log($"\"{clientDllPath}\" not found", ConsoleColor.Red);
                    return false;
                }
                if (!File.Exists(offsetsPath))
                {
                    Program.Log($"\"{offsetsPath}\" not found", ConsoleColor.Red);
                    return false;
                }

                string clientDllData = "";
                string offsetsData = "";

                using (StreamReader reader = new StreamReader(clientDllPath))
                    clientDllData = reader.ReadToEnd();

                using (StreamReader reader = new StreamReader(offsetsPath))
                    offsetsData = reader.ReadToEnd();

                ParseData(clientDllData, offsetsData);
                return true;
            }
            return false;
        }

        private static void ParseData(string clientDllData, string offsetsData)
        {
            Load(ClientOffsets, offsetsData);

            DumpTime = clientDllData.Substring(0, clientDllData.IndexOf("//"));

            Load(C_BaseEntity, clientDllData);
            Load(CBasePlayerController, clientDllData);
            Load(C_BasePlayerPawn, clientDllData);
            Load(CPlayer_ObserverServices, clientDllData);
            Load(C_CSPlayerPawnBase, clientDllData);
            Load(CGameSceneNode, clientDllData);
            Load(CCSPlayerController, clientDllData);
            Load(CCSPlayerController_InGameMoneyServices, clientDllData);
            Load(C_BaseModelEntity, clientDllData);
            Load(C_CSPlayerPawn, clientDllData);
            Load(EntitySpottedState_t, clientDllData);
            Load(C_CSWeaponBase, clientDllData);
            Load(C_BasePlayerWeapon, clientDllData);
            Load(C_EconEntity, clientDllData);
            Load(C_EconItemView, clientDllData);
            Load(CSkeletonInstance, clientDllData);
        }

        private static void Load(InterfaceBase @interface, string fileData)
        {
            @interface.ParseInterface(fileData);
            Program.Log($"{@interface.Name}", ConsoleColor.DarkGray);
        }

        public static string DumpTime { get; private set; }

        public static CSkeletonInstance CSkeletonInstance
        {
            get; private set;
        } = null!;

        public static C_PlantedC4 C_PlantedC4
        {
            get; private set;
        } = null!;

        public static C_EconItemView C_EconItemView
        {
            get; private set;
        } = null!;

        public static C_BaseEntity C_BaseEntity
        {
            get; private set;
        } = null!;

        public static C_EconEntity C_EconEntity
        {
            get; private set;
        } = null!;

        public static ClientOffsets ClientOffsets
        {
            get; private set;
        } = null!;

        public static CBasePlayerController CBasePlayerController
        {
            get; private set;
        } = null!;

        public static C_BasePlayerPawn C_BasePlayerPawn
        {
            get; private set;
        } = null!;

        public static CPlayer_ObserverServices CPlayer_ObserverServices
        {
            get; private set;
        } = null!;

        public static C_CSPlayerPawnBase C_CSPlayerPawnBase
        {
            get; private set;
        } = null!;

        public static CGameSceneNode CGameSceneNode
        {
            get; private set;
        } = null!;

        public static CCSPlayerController CCSPlayerController
        {
            get; private set;
        } = null!;

        public static CCSPlayerController_InGameMoneyServices CCSPlayerController_InGameMoneyServices
        {
            get; private set;
        } = null!;

        public static C_BaseModelEntity C_BaseModelEntity
        {
            get; private set;
        } = null!;

        public static C_CSPlayerPawn C_CSPlayerPawn
        {
            get; private set;
        } = null!;

        public static EntitySpottedState_t EntitySpottedState_t
        {
            get; private set;
        } = null!;

        public static C_CSWeaponBase C_CSWeaponBase
        {
            get; private set;
        } = null!;

        public static C_BasePlayerWeapon C_BasePlayerWeapon
        {
            get; private set;
        } = null!;
    }
}
