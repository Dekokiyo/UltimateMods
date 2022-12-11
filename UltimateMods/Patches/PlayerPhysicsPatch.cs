namespace UltimateMods.Patches;

[HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.Awake))]
public static class PlayerPhysics_Awake_Patch
{
    [HarmonyPostfix]
    public static void Postfix(PlayerPhysics __instance)
    {
        if (!__instance.body) return;
        __instance.body.interpolation = RigidbodyInterpolation2D.Interpolate;
    }
}