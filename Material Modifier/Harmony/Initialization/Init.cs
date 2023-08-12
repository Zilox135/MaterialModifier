using System.Reflection;

namespace Harmony
{
    public class MaterialModifierInit : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GetType().ToString());
        }
    }
}
