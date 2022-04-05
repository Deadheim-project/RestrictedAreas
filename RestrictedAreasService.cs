using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RestrictedAreas
{
    public class RestrictedAreasService
    {
        public bool IsBuildAllowed(Vector3 vector3, string steamId)
        {
            List<Area> areasPlayerIsInside = GetAreasPlayerIsInside(vector3);
            foreach (Area area in areasPlayerIsInside)
            {
                if (area.PermittedIds.Contains(steamId)) return true;

                if (area.NoBuild) return false;
            }


            return true;
        }

        public bool IsTerrainAllowed(Vector3 vector3, string steamId)
        {
            List<Area> areasPlayerIsInside = GetAreasPlayerIsInside(vector3);
            foreach (Area area in areasPlayerIsInside)
            {
                if (area.PermittedIds.Contains(steamId)) return true;

                if (area.NoTerrain) return false;
            }

            return true;
        }

        public bool IsPickaxeAllowed(Vector3 vector3, string steamId)
        {
            List<Area> areasPlayerIsInside = GetAreasPlayerIsInside(vector3);
            foreach (Area area in areasPlayerIsInside)
            {
                if (area.PermittedIds.Contains(steamId)) return true;

                if (area.NoPickaxe) return false;
            }

            return true;
        }

        public bool IsInteractAllowed(Vector3 vector3, string steamId)
        {
            List<Area> areasPlayerIsInside = GetAreasPlayerIsInside(vector3);
            foreach (Area area in areasPlayerIsInside)
            {
                if (area.PermittedIds.Contains(steamId)) return true;

                if (area.NoInteract) return false;
            }

            return true;
        }

        public bool isTreeDamageAllowed(Vector3 vector3, string steamId)
        {
            List<Area> areasPlayerIsInside = GetAreasPlayerIsInside(vector3);
            foreach (Area area in areasPlayerIsInside)
            {
                if (area.PermittedIds.Contains(steamId)) return true;

                if (area.NoTreeDamage) return false;
            }

            return true;
        }


        public bool isWardAllowed(Vector3 vector3, string steamId)
        {
            List<Area> areasPlayerIsInside = GetAreasPlayerIsInside(vector3);
            foreach (Area area in areasPlayerIsInside)
            {
                if (area.PermittedIds.Contains(steamId)) return true;

                if (area.NoWard) return false;
            }

            return true;
        }


        public bool isPvpAwaysOn(Vector3 vector3, string steamId)
        {
            List<Area> areasPlayerIsInside = GetAreasPlayerIsInside(vector3);
            foreach (Area area in areasPlayerIsInside)
            {
                if (area.PermittedIds.Contains(steamId)) return false;

                if (area.PvpAlwaysOn) return true;
            }

            return true;
        }


        public bool isPvpAwaysOff(Vector3 vector3, string steamId)
        {
            List<Area> areasPlayerIsInside = GetAreasPlayerIsInside(vector3);
            foreach (Area area in areasPlayerIsInside)
            {
                if (area.PermittedIds.Contains(steamId)) return false;

                if (area.PvpAlwaysOff) return true;
            }

            return true;
        }

        private List<Area> GetAreasPlayerIsInside(Vector3 vector3)
        {
            Vector2 testVector = new Vector2(vector3.x, vector3.z);
            List<Area> areaList = new List<Area>();

            foreach (Area area in BuildAreaList((int)vector3.z))
            {
                if (Vector3.Distance(testVector, area.Vector) <= area.Radius)
                {
                    areaList.Add(area);
                }

            }
            return areaList;
        }

        private List<Area> BuildAreaList(int z)
        {
            List<Area> areaList = new List<Area>();
            string[] values = RestrictedAreas.RestrictedAreasList.Value.Trim(' ').Split('|');

            foreach (string val in values)
            {
                string[] areaRaw = val.Split(';');
                int x = Convert.ToInt32(areaRaw[0].Split('=')[1]);
                int y = Convert.ToInt32(areaRaw[1].Split('=')[1]);
                int radius = Convert.ToInt32(areaRaw[2].Split('=')[1]);
                List<string> steamIdList = areaRaw[3].Split('=')[1].Split(',').ToList();
                List<string> permissions = areaRaw[4].Split('=')[1].Split(',').ToList();
                Area area = new Area(x, y, z, radius, steamIdList);

                foreach (string permission in permissions)
                {
                    if (permission.ToLower() == nameof(Area.NoBuild).ToLower()) area.NoBuild = true;
                    if (permission.ToLower() == nameof(Area.NoTerrain).ToLower()) area.NoTerrain = true;
                    if (permission.ToLower() == nameof(Area.NoInteract).ToLower()) area.NoInteract = true;
                    if (permission.ToLower() == nameof(Area.NoTreeDamage).ToLower()) area.NoTreeDamage = true;
                    if (permission.ToLower() == nameof(Area.PvpAlwaysOff).ToLower()) area.PvpAlwaysOff = true;
                    if (permission.ToLower() == nameof(Area.PvpAlwaysOn).ToLower()) area.PvpAlwaysOn = true;
                    if (permission.ToLower() == nameof(Area.NoWard).ToLower()) area.NoWard = true;
                    if (permission.ToLower() == nameof(Area.NoPickaxe).ToLower()) area.NoPickaxe = true;
                }

                areaList.Add(area);
            }

            return areaList;
        }

        private class Area
        {
            public Area(int x, int y, int z, int radius, List<string> permittedIds)
            {
                Vector = new Vector3(x, y, z);
                Radius = radius;
                PermittedIds = permittedIds;
            }

            public Vector3 Vector { get; set; }
            public int Radius { get; set; }
            public List<string> PermittedIds { get; set; }
            public bool NoBuild { get; set; }
            public bool NoTerrain { get; set; }
            public bool NoTreeDamage { get; set; }
            public bool NoInteract { get; set; }
            public bool PvpAlwaysOn { get; set; }
            public bool PvpAlwaysOff { get; set; }
            public bool NoWard { get; set; }
            public bool NoPickaxe { get; set; }
        }
    }
}
