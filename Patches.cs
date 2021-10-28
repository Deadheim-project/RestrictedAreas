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
            private static bool Prefix()
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (service.IsBuildAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                return false;
            }
        }

        [HarmonyPatch(typeof(WearNTear), "Damage")]
        public static class Building_Wear_N_Tear_Patch
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
        public static class Attack_Patch
        {
            private static bool Prefix(Vector3 hitPoint)
            {
                if (!Player.m_localPlayer) return true;

                var service = new RestrictedAreasService();

                if (service.IsTerrainAllowed(Player.m_localPlayer.transform.position, SteamUser.GetSteamID().ToString())) return true;

                Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You can't do that in restricted areas", 0, null);
                return false;
            }
        }

        [HarmonyPatch(typeof(TerrainOp), "Awake")]
        public static class TerrainComp_Patch
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
        public static class TreeBase_Modifier
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
    }
}
