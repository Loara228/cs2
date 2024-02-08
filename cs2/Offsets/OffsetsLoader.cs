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
        public static void Initialize()
        {
            C_BaseEntity = new C_BaseEntity();
            Client = new Interfaces.Client();
            CBasePlayerController = new CBasePlayerController();
            C_BasePlayerPawn = new C_BasePlayerPawn();
            CPlayer_ObserverServices = new CPlayer_ObserverServices();
            C_CSPlayerPawnBase = new C_CSPlayerPawnBase();
            CGameSceneNode = new CGameSceneNode();

            if (!InitFromDir)
            {
                using (WebClient wc = new WebClient())
                {
                    const string clientDllUrl = "https://raw.githubusercontent.com/a2x/cs2-dumper/main/generated/client.dll.cs";
                    const string offseltsUrl = "https://raw.githubusercontent.com/a2x/cs2-dumper/main/generated/offsets.cs";

                    string clientDllData = wc.DownloadString(clientDllUrl);
                    string offsetsData = wc.DownloadString(offseltsUrl);

                    Load(C_BaseEntity, clientDllUrl);
                    Load(Client, offseltsUrl);
                    Load(CBasePlayerController, clientDllUrl);
                    Load(C_BasePlayerPawn, clientDllUrl);
                    Load(CPlayer_ObserverServices, clientDllUrl);
                    Load(C_CSPlayerPawnBase, clientDllUrl);
                    Load(CGameSceneNode, clientDllUrl);
                }
            }
            else
            {
            }
        }

        private static void Load(InterfaceBase @interface, string fileData)
        {
            @interface.ParseInterface(fileData);
            Program.Log($"{@interface.Name} loaded", ConsoleColor.Green);
        }

        public static C_BaseEntity C_BaseEntity
        {
            get; private set;
        } = null!;

        public static Interfaces.Client Client
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

        public static bool InitFromDir = false;

    }
}
