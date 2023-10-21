using UnityEngine;

namespace GearFactory
{
    /// <summary>
    /// Loads default sprite and Standard materials.
    /// </summary>
    public static class DefaultMaterialProvider
    {
        private const string defaultMaterialPath = "Sprites/Default";
        private static Material cachedDefaultMaterial;

        private const string defaultMaterial3DPath = "Standard";
        private static Material cachedDefaultMaterial3D;

        public static Material GetDefaultMaterial(bool forceNew)
        {
            //just create new Material
            if (forceNew)
            {
                return CreateDefaultMaterial(defaultMaterialPath);
            }
            else
            {
                if (cachedDefaultMaterial == null)
                {
                    cachedDefaultMaterial = CreateDefaultMaterial(defaultMaterialPath);
                }
                return cachedDefaultMaterial;
            }
        }

        public static Material GetDefaultMaterial3D(bool forceNew)
        {
            //just create new Material
            if (forceNew)
            {
                return CreateDefaultMaterial(defaultMaterial3DPath);
            }
            else
            {
                return cachedDefaultMaterial3D ?? (cachedDefaultMaterial3D = CreateDefaultMaterial(defaultMaterial3DPath));
            }
        }
        
        private static Material CreateDefaultMaterial(string defaultPath)
        {
            return new Material(Shader.Find(defaultPath));
        }

    }
}
