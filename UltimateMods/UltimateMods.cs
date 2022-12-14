namespace UltimateMods
{
    [HarmonyPatch]
    public static class UltimateMods
    {
        public static System.Random rnd = new((int)DateTime.Now.Ticks);

        public static void ClearAndReloadRoles()
        {
            Clear();
            Role.ClearAll();
        }

        public static void FixedUpdate(PlayerControl player)
        {
            Role.allRoles.DoIf(x => x.player == player, x => x.FixedUpdate());
            Modifier.allModifiers.DoIf(x => x.player == player, x => x.FixedUpdate());
        }

        public static void OnMeetingStart()
        {
            Role.allRoles.Do(x => x.OnMeetingStart());
            Modifier.allModifiers.Do(x => x.OnMeetingStart());
        }

        public static void OnMeetingEnd()
        {
            Role.allRoles.Do(x => x.OnMeetingEnd());
            Modifier.allModifiers.Do(x => x.OnMeetingEnd());

            // CustomOverlays.HideInfoOverlay();
        }

        public static void Clear()
        {
            Role.allRoles.Do(x => x.Clear());
            Modifier.allModifiers.Do(x => x.Clear());
        }

        [HarmonyPatch(typeof(GameData), nameof(GameData.HandleDisconnect), new Type[] { typeof(PlayerControl), typeof(DisconnectReasons) })]
        class HandleDisconnectPatch
        {
            public static void Postfix(GameData __instance, PlayerControl player, DisconnectReasons reason)
            {
                if (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started)
                {
                    Role.allRoles.Do(x => x.HandleDisconnect(player, reason));
                    Modifier.allModifiers.Do(x => x.HandleDisconnect(player, reason));
                    finalStatuses[player.PlayerId] = FinalStatus.Disconnected;
                }
            }
        }
    }
}