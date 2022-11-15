﻿using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIVAutoAttack.Actions;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.Basic;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;
using XIVAutoAttack.Updaters;
using static XIVAutoAttack.Combos.RangedMagicial.BLMCombo_Default.BLMCombo_Default;


namespace XIVAutoAttack.Combos.RangedMagicial.BLMCombo_Default;

internal sealed partial class BLMCombo_Default : BLMCombo_Base<CommandType>
{
    
    /// <summary>
    /// 星灵移位
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseTranspose(byte abilityRemain, out IAction act)
    {
        if (!Transpose.ShouldUse(out act, mustUse: true)) return false;

        //标准循环
        if (StandardLoop)
        {
            if (JobGauge.InUmbralIce && HasFire && Player.CurrentMp >= 9600 && JobGauge.UmbralHearts == 3 && !JobGauge.IsParadoxActive) return true;
            return false;
        }

        //星灵转火
        if (JobGauge.InUmbralIce)
        {
            //标准循环改
            if (HasFire && Player.CurrentMp >= 9600 && JobGauge.UmbralHearts == 3 && !JobGauge.IsParadoxActive) return true;

            if (!JobGauge.IsParadoxActive && JobGauge.UmbralHearts != 3)
            {
                var time1 = ActionUpdater.WeaponElapsed + GCDTime / 1000;
                var time2 = ActionUpdater.MPUpdateElapsed + 3;
                //瞬双2,长双2(2-1)
                if (IsOldSpell(1, Paradox) && abilityRemain == 1 && !HasFire)
                {
                    if (DoubleTranspose && (!Swiftcast.IsCoolDown || HaveSwift)) return true;
                    if (FewBlizzard && Player.CurrentMp > 6000) return true;
                }
                //瞬双3,长双3(2/3-1)
                if (IsOldSpell(1, Paradox) && abilityRemain == 1 && !HasFire && time1 > time2)
                {
                    if (DoubleTranspose && (!Swiftcast.IsCoolDown || HaveSwift)) return true;
                    if (FewBlizzard && Player.CurrentMp > 6000) return true;
                }
                if (IsOldSpell(1, Paradox) && Player.CurrentMp >= 9600 && !HasFire)
                {
                    if (DoubleTranspose && (!Swiftcast.IsCoolDown || HaveSwift)) return true;
                    if (FewBlizzard && Player.CurrentMp > 6000) return true;
                }

                if (IsLastSpell(true, Paradox)) return false;

                //火双3
                if (Player.CurrentMp >= 5600 && HasFire && CanF4Number(3) && !CanF4Number(4) && !CanF4Number(5))
                {
                    return true;
                }
                //火双4
                if (Player.CurrentMp >= 7200 && HasFire && CanF4Number(4) && !CanF4Number(5))
                {
                    return true;
                }
                //火双5
                if (Player.CurrentMp >= 8800 && HasFire && CanF4Number(5))
                {
                    return true;
                }
            }
        }

        //星灵转冰
        if (JobGauge.InAstralFire && abilityRemain == 2 && (Manafont.ElapsedAfter(3) || !Manafont.IsCoolDown))
        {
            if (IsLastSpell(true, Despair) || IsOldSpell(1, Despair) && IsLastSpell(true, Xenoglossy, Thunder)) return true;
        }

        act = null;
        return false;
    }

    /// <summary>
    /// 醒梦
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseLucidDreaming(out IAction act)
    {
        if (!LucidDreaming.ShouldUse(out act)) return false;
        if (Blizzard2.ShouldUse(out _)) return false;
        if (StandardLoop) return false;
        if (JobGauge.InUmbralIce && JobGauge.UmbralIceStacks < 3 && !JobGauge.IsParadoxActive)
        {
            //if (IsLastSpell(true, Actions.Thunder) || IsOldSpell(1, Actions.Thunder3)) return false;

            if (HasFire || Player.HaveStatus(true, StatusID.LeyLines)) return false;
            if (Transpose.IsCoolDown && MPYuPanDouble >= 7900) return true;
            if (Transpose.IsCoolDown) return true;
        }

        //if (fireOpener && Actions.Leylines.IsCoolDown) return true;
        act = null;
        return false;
    }

    /// <summary>
    /// 即刻
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseSwiftcast(out IAction act)
    {
        if (!Swiftcast.ShouldUse(out act)) return false;
        if (StandardLoop) return false;
        if (JobGauge.InUmbralIce && JobGauge.UmbralIceStacks < 3 && !JobGauge.IsParadoxActive)
        {
            if (HasFire) return false;
            //if (IsOldSpell(1, Actions.Thunder3)) return false;
            if (Player.HaveStatus(true, StatusID.LucidDreaming)) return true;
            if (MPYuPanDouble >= 9400) return true;
        }

        act = null;
        return false;
    }

    /// <summary>
    /// 三连
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseTriplecast(out IAction act)
    {
        if (!Triplecast.ShouldUse(out act, emptyOrSkipCombo: true)) return false;

        if (StandardLoop)
        {
            if (IsLastSpell(true, Xenoglossy, Thunder) && Triplecast.ShouldUse(out act)) return true;
            return false;
        }

        if (JobGauge.InUmbralIce && JobGauge.UmbralIceStacks < 3 && !JobGauge.IsParadoxActive)
        {
            //if (IsOldSpell(1, Actions.Thunder3)) return false;
            //if (Player.HaveStatus(ObjectStatus.LucidDreaming) && !HasFire) return true;
        }

        if (!JobGauge.InAstralFire) return false;

        if (!JobGauge.IsParadoxActive)
        {
            if (fireOpener && Leylines.IsCoolDown && !Leylines.ElapsedAfterGCD(1) && !Manafont.IsCoolDown) return true;

            if (F4RemainingNumber() == 3 && Triplecast.CurrentCharges == 1 && Level == 90) return false;

            if (Player.CurrentMp == 0) return false;

            if (IsLastSpell(true, Xenoglossy, Thunder) && F4RemainingNumber() < 2) return true;
        }



        act = null;
        return false;
    }

    /// <summary>
    /// 激情咏唱
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseSharpcast(out IAction act)
    {
        if (!Sharpcast.ShouldUse(out act, emptyOrSkipCombo: true)) return false;

        if (StandardLoop)
        {
            if (JobGauge.InUmbralIce && !TargetThunderWillEnd(20)) return true;
            return false;
        }

        if (Triplecast.IsCoolDown && Triplecast.CurrentCharges == 1 && Player.HaveStatus(true, StatusID.Triplecast)) return false;
        //if (!IsLastSpell(true, Actions.Thunder)) return true;
        return true;

        //act = null;
        //return false;
    }

    /// <summary>
    /// 悖论
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseParadox(out IAction act)
    {
        if (!Paradox.ShouldUse(out act)) return false;

        //在冰
        if (JobGauge.InUmbralIce)
        {
            //星灵进冰
            if (Transpose.IsCoolDown && JobGauge.UmbralIceStacks >= 1) return true;

            //冰3进冰,冰4后
            if (JobGauge.UmbralIceStacks == 3 && JobGauge.UmbralHearts == 3) return true;
        }

        //在火
        if (JobGauge.InAstralFire)
        {
            //if (JobGauge.UmbralHearts == 0) return true;
            if (JobGauge.ElementTimeRemaining <= CalcSpellTime(2500) * 2) return true;
        }

        act = null;
        return false;
    }

    /// <summary>
    /// 火3
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseFire3(out IAction act)
    {
        if (!Fire3.ShouldUse(out act)) return false;
        if (IsLastSpell(true, Fire3)) return false;

        //冰阶段进火
        if (JobGauge.InUmbralIce)
        {
            if (Paradox.EnoughLevel && JobGauge.IsParadoxActive) return false;

            if (!Fire4.EnoughLevel && Player.CurrentMp == 10000) return true;

            //标准循环
            if (JobGauge.UmbralHearts == 3) return true;
            //没有火苗
            if (HasFire || StandardLoop) return false;

            //瞬单条件
            if (HaveSwift)
            {
                if (Player.CurrentMp >= 9600) return false;
                //瞬单3
                if (Player.CurrentMp >= 5600 && CanF4Number(3) && !CanF4Number(4)) return true;
                //瞬单4
                if (Player.CurrentMp >= 7200 && CanF4Number(4)) return true;
            }

            //长单3,4,5
            if (!HaveSwift)
            {
                if (JobGauge.PolyglotStacks == 0) return true;

                return true;
            }
        }

        if (JobGauge.InAstralFire)
        {
            if (Level < 90 && HasFire) return true;
            //进火后火3
            if (IsLastAction(true, Transpose) || JobGauge.AstralFireStacks < 3) return true;
        }

        //火起手
        if (Level == 90 && fireOpener && !JobGauge.IsEnochianActive && !JobGauge.InUmbralIce) return true;

        return false;
    }

    /// <summary>
    /// 火4
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseFire4(out IAction act)
    {
        if (!Fire4.ShouldUse(out act)) return false;
        if (Player.CurrentMp < 2400) return false;

        //能瞬发时判断
        if (HaveSwift && JobGauge.ElementTimeRemaining > CalcSpellTime(2500)) return true;
        //当前火状态还能打几个火4
        if (F4RemainingNumber() >= 1) return true;
        //悖论后
        //if (!JobGauge.IsParadoxActive && JobGauge.ElementTimeRemaining >= CalcSpellTime(3000) + CalcSpellTime(2800)) return true;

        return false;
    }

    /// <summary>
    /// 绝望
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseDespair(out IAction act)
    {
        if (!Despair.ShouldUse(out act)) return false;
        //有悖论不放
        if (JobGauge.IsParadoxActive) return false;
        //能放火4时不放
        if (CanUseFire4(out _)) return false;
        //能瞬发时
        //if (HaveSwift) return true;
        //正常判断,绝望收尾
        if (JobGauge.ElementTimeRemaining >= Despair.CastTime - 0.4) return true;

        return false;
    }

    /// <summary>
    /// 冰3
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseBlizzard3(out IAction act)
    {
        if (!Blizzard3.ShouldUse(out act)) return false;
        if (IsLastSpell(true, Blizzard3)) return false;

        if (Level < 90 && Player.CurrentMp < 1600) return true;

        //标准循环
        if (StandardLoop && (Player.CurrentMp == 0 || !CanUseFire4(out _) && !CanUseDespair(out _))) return true;

        //双星灵
        if (JobGauge.InAstralFire && !CanUseFire4(out _) && !CanUseDespair(out _) && (JobGauge.ElementTimeRemaining < CooldownHelper.CalcSpellTime(3000) - 0.5 || Player.CurrentMp <= 1200) && !JobGauge.IsParadoxActive && (Manafont.ElapsedAfter(3) || !Manafont.IsCoolDown)) return true;

        return false;
    }

    /// <summary>
    /// 雷3
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseThunder(out IAction act)
    {
        if (!Thunder.ShouldUse(out act)) return false;

        if (IsLastSpell(true, Thunder) || Thunder.Target.IsBoss()) return false;

        //在冰
        if (JobGauge.InUmbralIce)
        {
            if (JobGauge.UmbralIceStacks == 3 || StandardLoop)
            {
                if (!TargetHasThunder || TargetThunderWillEnd(3)) return true;
                if (HasThunder && Player.WillStatusEnd(3, true, StatusID.Thundercloud)) return true;
                return false;
            }
            if (TargetHasThunder && !TargetThunderWillEnd(10)) return false;
            //有悖论时不释放
            if (JobGauge.IsParadoxActive) return false;
            //没雷dot
            if (!TargetHasThunder) return true;

            if (IsLastSpell(true, Xenoglossy)) return false;
            if (HasThunder) return true;
        }

        //在火
        if (JobGauge.InAstralFire)
        {
            if (StandardLoop)
            {
                if (!TargetHasThunder || TargetHasThunder && TargetThunderWillEnd(3)) return true;
                return false;
            }
            //上个技能是异言pass
            if (IsLastSpell(true, Xenoglossy)) return false;
            //魔泉时
            if (!Manafont.ElapsedAfter(3) && Manafont.IsCoolDown) return false;

            if (TargetHasThunder && !TargetThunderWillEnd(8)) return false;

            if (HasThunder && JobGauge.IsParadoxActive && TargetHasThunder && TargetThunderWillEnd(5)) return true;

            //未来观测卡跳蓝(三连咏唱)
            if (BenignMp()) return true;

            //if (IsLastSpell(true, Actions.Despair) && Player.HaveStatus(ObjectStatus.Sharpcast) && HasThunder)
            //{
            //    return true;
            //}

            if (FewBlizzard && IsLastSpell(true, Despair) && Player.HaveStatus(true, StatusID.Sharpcast) && HasThunder && !MpBackGCDCanDouble(1) && HaveXeCounts(2) <= 1)
            {
                return true;
            }
            //if (IsLastSpell(true, Actions.Despair) && HasThunder && GeneralActions.Swiftcast.IsCoolDown && !GeneralActions.Swiftcast.WillHaveOneChargeGCD(1))
            //{
            //    return true;
            //}

            if (!HasThunder && (!TargetHasThunder || TargetHasThunder && TargetThunderWillEnd(3))) return true;

            //if (Player.HaveStatus(ObjectStatus.Sharpcast) && Player.WillStatusEnd(3, false)) return true;
        }

        return false;
    }

    /// <summary>
    /// 异言
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    private bool CanUseXenoglossy(out IAction act)
    {
        if (!Xenoglossy.ShouldUse(out act)) return false;

        //标准循环
        if (StandardLoop)
        {
            if (JobGauge.UmbralHearts != 3) return false;
            if (IsLastSpell(true, Thunder, Xenoglossy)) return false;
            if (JobGauge.EnochianTimer <= 5000 && IsPolyglotStacksMaxed) return true;
            if (!Manafont.IsCoolDown && IsLastSpell(true, Despair)) return true;

            return false;
        }

        //在冰
        if (JobGauge.InUmbralIce)
        {
            if (JobGauge.UmbralHearts == 3 || JobGauge.IsParadoxActive) return false;
            if (IsLastSpell(true, Thunder, Xenoglossy)) return false;

            if (FewBlizzard && MpTwoInIce) return true;
            if (IsOldSpell(1, Thunder3)) return false;
            if (JobGauge.PolyglotStacks == 2) return true;
            if (HasFire && !IsLastSpell(true, Thunder, Xenoglossy)) return true;
            if (!HasFire && (HaveSwift || !Swiftcast.IsCoolDown) && !Player.HaveStatus(true, StatusID.LeyLines)) return true;
        }

        //在火
        if (JobGauge.InAstralFire)
        {
            if (IsLastSpell(true, Xenoglossy) || HaveSwift) return false;
            //起手
            if (iceOpener && !JobGauge.IsParadoxActive && Player.CurrentMp <= 1200) return true;
            //魔泉时
            if (!Manafont.ElapsedAfter(3) && Manafont.IsCoolDown) return false;

            //未来观测卡跳蓝(三连咏唱)
            if (BenignMp()) return true;

            if (IsLastSpell(true, Despair))
            {
                //火双n
                if (HasFire) return true;

                //长瞬双
                if (FewBlizzard && JobGauge.PolyglotStacks >= 1) return true;

                if (DoubleTranspose && HaveXeCounts(2) == 2) return true;

                //看雷云满足条件吗
                if (TargetHasThunder && !TargetThunderWillEnd((float)GCDTime * 4 / 1000) || !TargetHasThunder)
                {
                    if (HaveXeCounts(2) == 2 || HaveXeCounts(2) == 1 && HaveT3InIce(2)) return true;
                    return false;
                }
            }

            if (JobGauge.ElementTimeRemaining < GCDTime + CooldownHelper.CalcSpellTime(3000)) return false;
            if (JobGauge.EnochianTimer <= 4000 && IsPolyglotStacksMaxed) return true;
        }

        act = null;
        return false;
    }
}