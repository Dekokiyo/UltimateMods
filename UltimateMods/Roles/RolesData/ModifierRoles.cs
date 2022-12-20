namespace UltimateMods.Roles;

public static class ModifierRoles
{
    public class Opportunist : ModifierBase<Opportunist>
    {
        public override string ModifierName() { return "Opportunist"; }
        public override string ModifierPostfix() { return "OP"; }
        public override Color ModifierColor() { return OpportunistGreen; }

        public static List<PlayerControl> Candidates
        {
            get
            {
                List<PlayerControl> validPlayers = new();

                foreach (var player in PlayerControl.AllPlayerControls) if (!player.HasModifier(ModifierId.Opportunist)) validPlayers.Add(player);

                return validPlayers;
            }
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public override void Clear()
        {
            players = new();
        }
    }

    public class Sunglasses : ModifierBase<Sunglasses>
    {
        public override string ModifierName() { return "Sunglasses"; }
        public override string ModifierPostfix() { return "SG"; }
        public override Color ModifierColor() { return SunglassesGray; }
        public static int Vision { get { return Mathf.RoundToInt(CustomRolesH.Sunglass.getFloat()); } }

        public static List<PlayerControl> Candidates
        {
            get
            {
                List<PlayerControl> validPlayers = new();

                foreach (var player in PlayerControl.AllPlayerControls) if (!player.HasModifier(ModifierId.Sunglasses)) validPlayers.Add(player);

                return validPlayers;
            }
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public override void Clear()
        {
            players = new();
        }
    }

    public class Watcher : ModifierBase<Watcher>
    {
        public override string ModifierName() { return "Watcher"; }
        public override string ModifierPostfix() { return "WT"; }
        public override Color ModifierColor() { return WatcherPurple; }

        public static List<PlayerControl> Candidates
        {
            get
            {
                List<PlayerControl> validPlayers = new();

                foreach (var player in PlayerControl.AllPlayerControls) if (!player.HasModifier(ModifierId.Watcher)) validPlayers.Add(player);

                return validPlayers;
            }
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public override void Clear()
        {
            players = new();
        }
    }
}