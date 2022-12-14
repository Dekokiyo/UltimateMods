namespace UltimateMods.Roles.Patches
{
    [HarmonyPatch]
    public static class BakeryPatch
    {
        [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
        public static class BombPatch
        {
            private static TextMeshPro BakeryText;
            public static bool Bomb = false;
            public static bool BakeryIsDead = false;

            public static void Postfix(ExileController __instance)
            {
                if (BakeryIsDead || !(Bakery.allPlayers.Count > 0)) return;

                int Probability = rnd.Next(1, 101);

                BakeryText = UnityEngine.Object.Instantiate(__instance.ImpostorText, __instance.Text.transform);

                if (Probability >= 100 - Bakery.BombRate && !(Bakery.BombRate == 0f)) Bomb = true; // Bomb Bread

                BakeryText.text = Bomb ? LocalizationManager.GetString(TransKey.BombBakeryText) : LocalizationManager.GetString(TransKey.MakeBreadText);
                BakeryText.gameObject.SetActive(true);

                if (GameManager.Instance.LogicOptions.currentGameOptions.GetBool(BoolOptionNames.ConfirmImpostor))
                {
                    BakeryText.transform.localPosition -= new Vector3(0f, 0.4f, 0f);
                }
                else
                {
                    BakeryText.transform.localPosition -= new Vector3(0f, 0.2f, 0f);
                }
                BakeryText.gameObject.SetActive(true);

                foreach (var bakery in Bakery.allPlayers)
                {
                    if (Bomb)
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.BakeryBomb, Hazel.SendOption.Reliable, -1);
                        writer.Write(bakery.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.BakeryBomb(bakery.PlayerId);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
        public static class SetBakeryDeadPatch
        {
            public static void Prefix()
            {
                if (BombPatch.Bomb)
                {
                    BombPatch.Bomb = false;
                    BombPatch.BakeryIsDead = true;
                }
            }
        }

        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
        public static class Reset
        {
            public static void Prefix()
            {
                BombPatch.BakeryIsDead = false;
            }
        }
    }
}