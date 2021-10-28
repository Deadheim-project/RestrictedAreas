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

        private List<Area> GetAreasPlayerIsInside(Vector3 vector3)
        {
            List<Area> areaList = BuildAreaList((int)vector3.z);
            return areaList.Where(area => Vector3.Distance(area.Vector, vector3) <= area.Radius).ToList();
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
        }
    }
}
