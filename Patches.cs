using HarmonyLib;
using Steamworks;
using UnityEngine;

namespace RestrictedAreas
{
    class Patches
    {
        [HarmonyPatch(typeof(Player), "PlacePiece")]
        public static class PlacePiece
        {
            private static bool Prefix(Piece piece)
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (piece.gameObject.name == "guard_stone" && !service.isWardAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString()))
                {
                    Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                    return false;
                }

                if (service.IsBuildAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                return false;
            }
        }

        [HarmonyPatch(typeof(WearNTear), "Damage")]
        public static class WearNTearDamage
        {
            private static bool Prefix(WearNTear __instance, HitData hit)
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (service.IsBuildAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                return false;
            }
        }


        [HarmonyPatch(typeof(Player), "RemovePiece")]
        public static class RemovePiece
        {
            private static bool Prefix(Player __instance)
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (service.IsBuildAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                return false;
            }
        }

        [HarmonyPatch(typeof(Attack), "SpawnOnHitTerrain")]
        public static class SpawnOnHitTerrain
        {
            private static bool Prefix(Vector3 hitPoint)
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (service.IsPickaxeAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                return false;
            }
        }

        [HarmonyPatch(typeof(TerrainOp), "Awake")]
        public static class TerrainOpAwake
        {
            private static bool Prefix(TerrainOp __instance)
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (service.IsTerrainAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                return false;
            }
        }

        [HarmonyPatch(typeof(TreeBase), "Damage")]
        public static class TreeBaseDamage
        {
            public static bool Prefix(TreeBase __instance, HitData hit)
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (service.isTreeDamageAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                return false;
            }
        }

        [HarmonyPatch(typeof(Pickable), "Interact")]
        public static class PickableInteract
        {
            private static bool Prefix(ItemDrop __instance)
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (service.IsInteractAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                return false;
            }
        }

        [HarmonyPatch(typeof(ItemStand), "Interact")]
        public static class ItemStandInteract
        {
            private static bool Prefix()
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (service.IsInteractAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                return false;
            }
        }


        //[HarmonyPostfix]
        //[HarmonyPatch(typeof(Game), "Update")]
        //private static void GameUpdate()
        //{
        //    if (Player.m_localPlayer)
        //    {
        //        var service = new RestrictedAreasService();

        //        if (service.isPvpAwaysOn(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString()))
        //        {
        //            InventoryGui.instance.m_pvp.isOn =  true;
        //            Player.m_localPlayer.SetPVP(true    );
        //            InventoryGui.instance.m_pvp.interactable = false;
        //            return;
        //        }

        //        if (service.isPvpAwaysOff(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString()))
        //        {
        //            InventoryGui.instance.m_pvp.isOn = false;
        //            Player.m_localPlayer.SetPVP(false);
        //            InventoryGui.instance.m_pvp.interactable = false;
        //            return;
        //        }
        //    }
        //}
    }
}
