namespace UltimateMods
{
    enum CustomRPC
    {
        ResetVariables = 60,
        ShareOptions,
        DynamicMapOption,
        VersionHandshake,
        SetRole,
        AddModifier,
        UseAdminTime,
        UseCameraTime,
        UseVitalsTime,
        UncheckedMurderPlayer,
        SheriffKill = 70,
        EngineerFixLights,
        EngineerUsedRepair,
        UncheckedSetTasks,
        UncheckedEndGame,
        DragPlaceBody,
        CleanBody,
        BakeryBomb,
        TeleporterTeleport,
        JackalCreatesSidekick,
        SidekickPromotes = 80,
        ArsonistDouse,
        ArsonistWin,
        AltruistKill,
        AltruistRevive,
        UncheckedCmdReportDeadBody,
    }

    public static class RPCProcedure
    {
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
        class RPCHandlerPatch
        {
            static void Postfix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
            {
                byte packetId = callId;
                switch (packetId)
                {
                    // 60
                    case (byte)CustomRPC.ResetVariables:
                        RPCProcedure.ResetVariables();
                        break;
                    // 61
                    case (byte)CustomRPC.ShareOptions:
                        RPCProcedure.ShareOptions((int)reader.ReadPackedUInt32(), reader);
                        break;
                    // 62
                    case (byte)CustomRPC.DynamicMapOption:
                        byte mapId = reader.ReadByte();
                        RPCProcedure.DynamicMapOption(mapId);
                        break;
                    // 63
                    case (byte)CustomRPC.VersionHandshake:
                        int major = reader.ReadPackedInt32();
                        int minor = reader.ReadPackedInt32();
                        int patch = reader.ReadPackedInt32();
                        int versionOwnerId = reader.ReadPackedInt32();
                        byte revision = 0xFF;
                        Guid guid;
                        if (reader.Length - reader.Position >= 17)
                        { // enough bytes left to read
                            revision = reader.ReadByte();
                            // GUID
                            byte[] GBytes = reader.ReadBytes(16);
                            guid = new Guid(GBytes);
                        }
                        else
                        {
                            guid = new Guid(new byte[16]);
                        }
                        RPCProcedure.VersionHandshake(major, minor, patch, revision == 0xFF ? -1 : revision, guid, versionOwnerId);
                        break;
                    // 64
                    case (byte)CustomRPC.SetRole:
                        byte roleId = reader.ReadByte();
                        byte playerId = reader.ReadByte();
                        byte flag = reader.ReadByte();
                        RPCProcedure.SetRole(roleId, playerId, flag);
                        break;
                    // 65
                    case (byte)CustomRPC.AddModifier:
                        RPCProcedure.AddModifier(reader.ReadByte(), reader.ReadByte());
                        break;
                    // 66
                    case (byte)CustomRPC.UseAdminTime:
                        RPCProcedure.UseAdminTime(reader.ReadSingle());
                        break;
                    // 67
                    case (byte)CustomRPC.UseCameraTime:
                        RPCProcedure.UseCameraTime(reader.ReadSingle());
                        break;
                    // 68
                    case (byte)CustomRPC.UseVitalsTime:
                        RPCProcedure.UseVitalsTime(reader.ReadSingle());
                        break;
                    // 69
                    case (byte)CustomRPC.UncheckedMurderPlayer:
                        byte source = reader.ReadByte();
                        byte target = reader.ReadByte();
                        byte showAnimation = reader.ReadByte();
                        RPCProcedure.UncheckedMurderPlayer(source, target, showAnimation);
                        break;
                    // 70
                    case (byte)CustomRPC.SheriffKill:
                        RPCProcedure.SheriffKill(reader.ReadByte(), reader.ReadByte(), reader.ReadBoolean());
                        break;
                    // 71
                    case (byte)CustomRPC.EngineerFixLights:
                        RPCProcedure.EngineerFixLights();
                        break;
                    // 72
                    case (byte)CustomRPC.EngineerUsedRepair:
                        RPCProcedure.EngineerUsedRepair(reader.ReadByte());
                        break;
                    // 73
                    case (byte)CustomRPC.UncheckedSetTasks:
                        RPCProcedure.UncheckedSetTasks(reader.ReadByte(), reader.ReadBytesAndSize());
                        break;
                    // 74
                    case (byte)CustomRPC.UncheckedEndGame:
                        RPCProcedure.UncheckedEndGame(reader.ReadByte());
                        break;
                    // 75
                    case (byte)CustomRPC.DragPlaceBody:
                        RPCProcedure.DragPlaceBody(reader.ReadByte());
                        break;
                    // 76
                    case (byte)CustomRPC.CleanBody:
                        RPCProcedure.CleanBody(reader.ReadByte());
                        break;
                    // 77
                    case (byte)CustomRPC.BakeryBomb:
                        RPCProcedure.BakeryBomb(reader.ReadByte());
                        break;
                    // 78
                    case (byte)CustomRPC.TeleporterTeleport:
                        RPCProcedure.TeleporterTeleport(reader.ReadByte());
                        break;
                    // 79
                    case (byte)CustomRPC.JackalCreatesSidekick:
                        RPCProcedure.JackalCreatesSidekick(reader.ReadByte());
                        break;
                    // 80
                    case (byte)CustomRPC.SidekickPromotes:
                        RPCProcedure.SidekickPromotes(reader.ReadByte());
                        break;
                    // 81
                    case (byte)CustomRPC.ArsonistDouse:
                        RPCProcedure.ArsonistDouse(reader.ReadByte());
                        break;
                    // 82
                    case (byte)CustomRPC.ArsonistWin:
                        RPCProcedure.ArsonistWin();
                        break;
                    // 83
                    case (byte)CustomRPC.AltruistKill:
                        RPCProcedure.AltruistKill(reader.ReadByte());
                        break;
                    // 84
                    case (byte)CustomRPC.AltruistRevive:
                        byte parentId = reader.ReadByte();
                        byte AltruistId = reader.ReadByte();
                        RPCProcedure.AltruistRevive(parentId, AltruistId);
                        break;
                    // 85
                    case (byte)CustomRPC.UncheckedCmdReportDeadBody:
                        byte reportSource = reader.ReadByte();
                        byte reportTarget = reader.ReadByte();
                        RPCProcedure.UncheckedCmdReportDeadBody(reportSource, reportTarget);
                        break;
                }
            }
        }

        public static void ResetVariables()
        {
            Options.ClearAndReloadOptions();
            UltimateMods.ClearAndReloadRoles();
            GameHistory.clearGameHistory();
            AdminPatch.ResetData();
            CameraPatch.ResetData();
            VitalsPatch.ResetData();
            RolesButtons.SetButtonCooldowns();
            // CustomOverlays.ResetOverlays();
            MapBehaviorPatch.ResetIcons();
        }

        public static void ShareOptions(int NumberOfOptions, MessageReader reader)
        {
            try
            {
                for (int i = 0; i < NumberOfOptions; i++)
                {
                    uint optionId = reader.ReadPackedUInt32();
                    uint selection = reader.ReadPackedUInt32();
                    CustomOption option = CustomOption.options.FirstOrDefault(option => option.id == (int)optionId);
                    option.updateSelection((int)selection);
                }
            }
            catch (Exception e)
            {
                UltimateModsPlugin.Logger.LogError("Error while deserializing options: " + e.Message);
            }
        }

        public static void DynamicMapOption(byte mapId)
        {
            GameManager.Instance.LogicOptions.currentGameOptions.SetByte(ByteOptionNames.MapId, mapId);
        }

        public static void VersionHandshake(int major, int minor, int build, int revision, Guid guid, int clientId)
        {
            System.Version ver;
            if (revision < 0)
                ver = new System.Version(major, minor, build);
            else
                ver = new System.Version(major, minor, build, revision);
            GameStartManagerPatch.playerVersions[clientId] = new GameStartManagerPatch.PlayerVersion(ver, guid);
        }

        public static void SetRole(byte roleId, byte playerId, byte flag)
        {
            PlayerControl.AllPlayerControls.ToArray().DoIf(
                x => x.PlayerId == playerId,
                x => x.SetRole((RoleId)roleId)
            );
        }

        public static void AddModifier(byte modId, byte playerId)
        {
            PlayerControl.AllPlayerControls.ToArray().DoIf(
                x => x.PlayerId == playerId,
                x => x.AddModifier((ModifierId)modId)
            );
        }

        public static void UseAdminTime(float time)
        {
            Options.RestrictAdminTime -= time;
        }

        public static void UseCameraTime(float time)
        {
            Options.RestrictCamerasTime -= time;
        }

        public static void UseVitalsTime(float time)
        {
            Options.RestrictVitalsTime -= time;
        }

        public static void UncheckedMurderPlayer(byte sourceId, byte targetId, byte showAnimation)
        {
            PlayerControl source = Helpers.PlayerById(sourceId);
            PlayerControl target = Helpers.PlayerById(targetId);
            if (source != null && target != null)
            {
                if (showAnimation == 0) KillAnimationCoPerformKillPatch.hideNextAnimation = true;
                source.MurderPlayer(target);
            }
        }

        public static void SheriffKill(byte sheriffId, byte targetId, bool misfire)
        {
            PlayerControl sheriff = Helpers.PlayerById(sheriffId);
            PlayerControl target = Helpers.PlayerById(targetId);
            if (sheriff == null || target == null) return;

            if (sheriff != null) Sheriff.ReamingShots--;

            if (misfire)
            {
                sheriff.MurderPlayer(sheriff);
                finalStatuses[sheriffId] = FinalStatus.Misfire;

                if (!Sheriff.MisfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;
            }

            sheriff.MurderPlayer(target);
        }

        public static void UpdateMeeting(byte targetId, bool dead = true)
        {
            if (MeetingHud.Instance)
            {
                foreach (PlayerVoteArea pva in MeetingHud.Instance.playerStates)
                {
                    if (pva.TargetPlayerId == targetId)
                    {
                        pva.SetDead(pva.DidReport, dead);
                        pva.Overlay.gameObject.SetActive(dead);
                    }

                    // Give players back their vote if target is shot dead
                    if (Helpers.RefundVotes && dead)
                    {
                        if (pva.VotedFor != targetId) continue;
                        pva.UnsetVote();
                        var voteAreaPlayer = Helpers.PlayerById(pva.TargetPlayerId);
                        if (!voteAreaPlayer.AmOwner) continue;
                        MeetingHud.Instance.ClearVote();
                    }
                }

                if (AmongUsClient.Instance.AmHost)
                    MeetingHud.Instance.CheckForEndVoting();
            }
        }

        public static void EngineerFixLights()
        {
            SwitchSystem switchSystem = MapUtilities.Systems[SystemTypes.Electrical].CastFast<SwitchSystem>();
            switchSystem.ActualSwitches = switchSystem.ExpectedSwitches;
        }

        public static void EngineerUsedRepair(byte engineerId)
        {
            PlayerControl engineer = Helpers.PlayerById(engineerId);
            if (engineer != null) ProEngineer.ReamingCounts--;
        }

        public static void UncheckedSetTasks(byte playerId, byte[] taskTypeIds)
        {
            var player = Helpers.PlayerById(playerId);
            player.ClearAllTasks();

            GameData.Instance.SetTasks(playerId, taskTypeIds);
        }

        public static void UncheckedEndGame(byte GameOverReason)
        {
            OnGameEndPatch.EndGameNavigationPatch.EndGameManagerSetUpPatch.CheckEndCriteriaPatch.UncheckedEndGame((CustomGameOverReason)GameOverReason);
        }

        public static void DragPlaceBody(byte playerId)
        {
            DeadBody[] Array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < Array.Length; i++)
            {
                if (GameData.Instance.GetPlayerById(Array[i].ParentId).PlayerId == playerId)
                {
                    if (!UnderTaker.DraggingBody)
                    {
                        UnderTaker.DraggingBody = true;
                        UnderTaker.BodyId = playerId;
                        if (GameManager.Instance.LogicOptions.currentGameOptions.GetByte(ByteOptionNames.MapId) == 5)
                        {
                            GameObject vent = GameObject.Find("LowerCentralVent");
                            vent.GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        UnderTaker.DraggingBody = false;
                        UnderTaker.BodyId = 0;
                        foreach (var underTaker in UnderTaker.allPlayers)
                        {
                            var currentPosition = underTaker.GetTruePosition();
                            var velocity = underTaker.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
                            var newPos = ((Vector2)underTaker.GetTruePosition()) - (velocity / 3) + new Vector2(0.15f, 0.25f) + Array[i].myCollider.offset;
                            if (!PhysicsHelpers.AnythingBetween(
                                currentPosition,
                                newPos,
                                Constants.ShipAndObjectsMask,
                                false
                            ))
                            {
                                if (GameManager.Instance.LogicOptions.currentGameOptions.GetByte(ByteOptionNames.MapId) == 5)
                                {
                                    Array[i].transform.position = newPos;
                                    Array[i].transform.position += new Vector3(0, 0, -0.5f);
                                    GameObject vent = GameObject.Find("LowerCentralVent");
                                    vent.GetComponent<BoxCollider2D>().enabled = true;
                                }
                                else
                                {
                                    Array[i].transform.position = newPos;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void UnderTakerReSetValues()
        {
            // Restore UnderTaker values when rewind time
            if (PlayerControl.LocalPlayer.IsRole(RoleId.UnderTaker) && UnderTaker.DraggingBody)
            {
                UnderTaker.DraggingBody = false;
                UnderTaker.BodyId = 0;
            }
        }

        public static void CleanBody(byte playerId)
        {
            DeadBody[] Array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < Array.Length; i++)
            {
                if (GameData.Instance.GetPlayerById(Array[i].ParentId).PlayerId == playerId)
                {
                    UnityEngine.Object.Destroy(Array[i].gameObject);
                }
            }
        }

        public static void BakeryBomb(byte BakeryId)
        {
            var bakery = Helpers.PlayerById(BakeryId);

            bakery.Exiled();
            SoundManager.Instance.PlaySound(Bomb, false, 0.8f);
            finalStatuses[BakeryId] = FinalStatus.Bomb;
            UltimateModsPlugin.Logger.LogInfo("Bakery exploded!");
        }

        public static void TeleporterTeleport(byte playerId)
        {
            var p = Helpers.PlayerById(playerId);
            PlayerControl.LocalPlayer.transform.position = p.transform.position;
            new CustomMessage(string.Format(LocalizationManager.GetString(TransKey.TeleporterTeleported), p.cosmetics.nameText.text), 3);
            SoundManager.Instance.PlaySound(Teleport, false, 0.8f);
        }

        public static void ErasePlayerRoles(byte playerId, bool ClearNeutralTasks = true)
        {
            PlayerControl player = Helpers.PlayerById(playerId);
            if (player == null) return;

            // Don't give a former neutral role tasks because that destroys the balance.
            if (player.IsNeutral() && ClearNeutralTasks)
                player.ClearAllTasks();

            player.EraseAllRoles();
            player.EraseAllModifiers();
        }

        public static void JackalCreatesSidekick(byte targetId)
        {
            PlayerControl Player = Helpers.PlayerById(targetId);
            if (Player == null) return;

            FastDestroyableSingleton<RoleManager>.Instance.SetRole(Player, RoleTypes.Crewmate);
            ErasePlayerRoles(Player.PlayerId, true);
            Player.SetRole(RoleId.Sidekick);
            if (Player.PlayerId == PlayerControl.LocalPlayer.PlayerId) PlayerControl.LocalPlayer.moveable = true;

            Jackal.CanSidekick = false;
        }

        public static void SidekickPromotes(byte sidekickId)
        {
            PlayerControl sidekick = Helpers.PlayerById(sidekickId);
            ErasePlayerRoles(sidekickId);
            sidekick.SetRole(RoleId.Jackal);
            Jackal.CanSidekick = true;
        }

        public static void ArsonistDouse(byte playerId)
        {
            Arsonist.DousedPlayers.Add(Helpers.PlayerById(playerId));
        }

        public static void ArsonistWin()
        {
            UncheckedEndGame((byte)CustomGameOverReason.ArsonistWin);
            var livingPlayers = PlayerControl.AllPlayerControls.ToArray().Where(p => !p.IsRole(RoleId.Arsonist) && p.IsAlive());
            foreach (PlayerControl p in livingPlayers)
            {
                p.Exiled();
                finalStatuses[p.PlayerId] = FinalStatus.Torched;
            }
        }

        public static void AltruistKill(byte AltruistId)
        {
            UncheckedMurderPlayer(AltruistId, AltruistId, (byte)0);
            finalStatuses[AltruistId] = FinalStatus.Suicide;
        }

        public static void AltruistRevive(byte parentId, byte AltruistId)
        {
            PlayerControl Altruist = Helpers.PlayerById(AltruistId);
            PlayerControl TargetPlayer = Helpers.PlayerById(parentId);
            DeadBody Target = Helpers.DeadBodyById(parentId);

            if (Altruist || Target || TargetPlayer == null) return;

            var Position = Target.TruePosition;
            CleanBody(parentId);

            foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>()) if (deadBody.ParentId == AltruistId) CleanBody(AltruistId);
            TargetPlayer.Revive();
            FastDestroyableSingleton<RoleManager>.Instance.SetRole(TargetPlayer, TargetPlayer.IsImpostor() ? RoleTypes.Impostor : RoleTypes.Crewmate);
            finalStatuses[TargetPlayer.PlayerId] = FinalStatus.Revival;
            TargetPlayer.NetTransform.SnapTo(new(Position.x, Position.y + 0.3636f));
        }

        public static void UncheckedCmdReportDeadBody(byte sourceId, byte targetId)
        {
            PlayerControl source = Helpers.PlayerById(sourceId);
            var t = targetId == Byte.MaxValue ? null : Helpers.PlayerById(targetId).Data;
            if (source != null) source.ReportDeadBody(t);
        }
    }
}