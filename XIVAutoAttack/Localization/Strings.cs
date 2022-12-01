using System.Resources;
using System.Reflection;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Lumina.Excel.GeneratedSheets;
using System.Collections.Generic;
using XIVAutoAttack.Combos.Script.Actions;
using XIVAutoAttack.Combos.CustomCombo;
using System;

namespace XIVAutoAttack.Localization;

internal class Strings
{
    #region ConfigWindow
    public string ConfigWindow_Header { get; set; } = "Auto Attack Settings v";
    public string ConfigWindow_AboutItem { get; set; } = "About";
    public string ConfigWindow_AttackItem { get; set; } = "Attack";
    public string ConfigWindow_ParamItem { get; set; } = "Param";
    public string ConfigWindow_EventsItem { get; set; } = "Events";
    public string ConfigWindow_ActionsItem { get; set; } = "Actions";
    public string ConfigWindow_HelpItem { get; set; } = "Help";

    public string ConfigWindow_ActionItem_Description { get; set; }
        = "In this window, you can modify the conditions of use for each action.";

    public string ConfigWindow_HelpItem_Description { get; set; }
        = "In this window, you can see all XIVAutoAttack built-in commands for combat. ";

    public string Configwindow_HelpItem_AttackSmart { get; set; }
        = "Start attacking in smart mode(auto-targeting) when out of combat, otherwise switch the target according to the conditions.";

    public string Configwindow_HelpItem_AttackManual { get; set; }
        = "Start attacking in manual mode.";

    public string Configwindow_HelpItem_AttackCancel { get; set; }
        = "Stop attacking. Remember to turn it off when not in use!";

    public string Configwindow_HelpItem_HealArea { get; set; }
        = "Open a window to use one or more AoE heal.";

    public string Configwindow_HelpItem_HealSingle { get; set; }
        = "Open a window to use one or more single heal.";

    public string Configwindow_HelpItem_DefenseArea { get; set; }
        = "Open a window to use one or more AoE defense.";

    public string Configwindow_HelpItem_DefenseSingle { get; set; }
        = "Open a window to use one or more single defense.";

    public string Configwindow_HelpItem_EsunaShield { get; set; }
        = "Open a window to use Esuna,tank stance actions or True North.";

    public string Configwindow_HelpItem_RaiseShirk { get; set; }
        = "Open a window to use Raise or Shirk.";

    public string Configwindow_HelpItem_AntiRepulsion { get; set; }
        = "Open a window to use knockback-penalty actions.";

    public string Configwindow_HelpItem_BreakProvoke { get; set; }
        = "Open a window to use break-combo or provoke action.";

    public string Configwindow_HelpItem_Move { get; set; }
        = "Open a window to move.";

    public string Configwindow_Helper_SwitchAuthor { get; set; } = "Click to switch authors";
    public string Configwindow_Helper_GameVersion { get; set; } = "Game Version";
    public string Configwindow_Helper_EditCombo { get; set; } = "Click to edit this custom Combo.";
    public string Configwindow_Helper_AddCombo { get; set; } = "Add a custom Combo.";
    public string Configwindow_Helper_OpenSource { get; set; } = "Open the source code URL";
    public string Configwindow_Helper_RunCommand { get; set; } = "Click to execute the command";
    public string Configwindow_Helper_InsertCommand { get; set; } = "Insert \"{0}\" first in 5s";
    public string Configwindow_AttackItem_Description { get; set; } = "You can enable the function for each job you want and configure the setting about how to use actions.\nOnly player-current-job specific commands will be prompted.";
    public string Configwindow_AttackItem_ScriptFolder { get; set; } = "Custom Opener Storage Path";
    public string Configwindow_AttackItem_LoadScript { get; set; } = "Click to load a custom opener";
    public string Configwindow_AttackItem_ScriptFolderError { get; set; } = "Please set a storage path to use the custom opener properly!";
    public string Configwindow_AttackItem_KeyName { get; set; } = "The key name is";
    public string Configwindow_Events_AddEvent { get; set; } = "AddEvents";
    public string Configwindow_Events_Description { get; set; } = "In this window, you can set what macro will be trigger after using an action.";
    public string Configwindow_Events_ActionName { get; set; } = "Action Name";
    public string Configwindow_Events_MacroIndex { get; set; } = "Macro No.";
    public string Configwindow_Events_ShareMacro { get; set; } = "Shared Macro No.";
    public string Configwindow_Events_RemoveEvent { get; set; } = "Delete Event";
    public string Configwindow_Params_Description { get; set; } = "In this window, you can set the parameters about the using way of actions.";
    public string Configwindow_Params_NeverReplaceIcon { get; set; } = "Never Replace Icons";
    public string Configwindow_Params_UseOverlayWindow { get; set; } = "Display top overlay";
    public string Configwindow_Params_UseOverlayWindowDesc { get; set; } = "This window is currently used to cue the body position in advance.";
    public string Configwindow_Params_BasicSettings { get; set; } = "Basic settings";
    public string Configwindow_Params_WeaponDelay { get; set; } = "Set the random interval between GCD (seconds)";
    public string Configwindow_Params_WeaponFaster { get; set; } = "Set the time advance of use actions";
    public string Configwindow_Params_WeaponInterval { get; set; } = "Set the interval between abilities using";
    public string Configwindow_Params_InterruptibleTime { get; set; } = "Set the delay of interrupting";
    public string Configwindow_Params_SpecialDuration { get; set; } = "Set the duration of special windows set by commands";
    public string Configwindow_Params_AddDotGCDCount { get; set; } = "Set GCD advance of DOT refresh";
    public string Configwindow_Params_DisplayEnhancement { get; set; } = "Display enhancement";
    public string Configwindow_Params_PoslockCasting { get; set; } = "Lock the movement when casting";
    public string Configwindow_Params_PoslockModifier { get; set; } = "Set the modifier key to unlock the movement temporary";
    public string Configwindow_Params_PoslockDescription { get; set; } = "LT is for gamepad player";
    public string Configwindow_Params_CheckForCasting { get; set; } = "Enhance castbar with casting status";
    public string Configwindow_Params_TeachingMode { get; set; } = "Teaching mode";
    public string Configwindow_Params_TeachingModeColor { get; set; } = "Prompt box color of teaching mode";
    public string Configwindow_Params_KeyBoardNoise { get; set; } = "Simulate the effect of pressing";
    public string Configwindow_Params_VoiceVolume { get; set; } = "Voice volume";
    public string Configwindow_Params_ShowLocation { get; set; } = "Hint positional anticipation by flytext";
    public string Configwindow_Params_SayingLocation { get; set; } = "Hint positional anticipation by shouting";
    public string Configwindow_Params_ShowLocationWrong { get; set; } = "Positional error feedback";
    public string Configwindow_Params_ShowLocationWrongDesc { get; set; } = "Attention: Positional anticipation is experimental, just for reference only.";
    public string Configwindow_Params_LocationWrongText { get; set; } = "Positional error prompt";
    public string Configwindow_Params_LocationWrongTextDesc { get; set; } = "How do you want to be scolded if you have a positional error ?!";
    public string Configwindow_Params_AutoSayingOut { get; set; } = "Saying the state changes out";
    public string Configwindow_Params_UseDtr { get; set; } = "Display attack mode on dtrbar";
    public string Configwindow_Params_UseToast { get; set; } = "Display attack mode changes on toast";
    public string Configwindow_Params_Actions { get; set; } = "Use of actions";
    public string Configwindow_Params_UseAOEWhenManual { get; set; } = "Use AOE actions in manual mode";
    public string Configwindow_Params_AutoBreak { get; set; } = "Automatic breaking";
    public string Configwindow_Params_OnlyGCD { get; set; } = "Only use GCD action without abilities";
    public string Configwindow_Params_AttackSafeMode { get; set; } = "Safe mode (absolute single target)";
    public string Configwindow_Params_AttackSafeModeDesc { get; set; } = "Nerver use any AOE action with single target./nBut if the number of hostile enough, AOE action will still be used.";
    public string Configwindow_Params_NoDefenceAbility { get; set; } = "Nerver use defence abilities";
    public string Configwindow_Params_NoDefenceAbilityDesc { get; set; } = "It is recommended to check this option if you are playing Raids./nPlan the heal and defense by yourself.";
    public string Configwindow_Params_AutoDefenseForTank { get; set; } = "Auto tank defense (the timing of use is not quite accurate)";
    public string Configwindow_Params_AutoDefenseForTankDesc { get; set; } = "Maybe misled by fake aoe.";
    public string Configwindow_Params_AutoShield { get; set; } = "Auto tank stance";
    public string Configwindow_Params_AutoProvokeForTank { get; set; } = "Auto Provoke (Tank)";
    public string Configwindow_Params_AutoProvokeForTankDesc { get; set; } = "When a hostile is hitting the non-Tank member of party, it will automatically use the Provoke.";
    public string Configwindow_Params_AutoUseTrueNorth { get; set; } = "Auto TrueNorth (Melee)";
    public string Configwindow_Params_RaisePlayerBySwift { get; set; } = "Raise player by swift";
    public string Configwindow_Params_UseAreaAbilityFriendly { get; set; } = "Use beneficaial ground-targeted actions";
    public string Configwindow_Params_RaisePlayerByCasting { get; set; } = "Raise player by casting when swift is in cooldown";
    public string Configwindow_Params_UseHealWhenNotAHealer { get; set; } = "Use heal when not-healer";
    public string Configwindow_Params_LessMPNoRaise { get; set; } = "Nerver raise player if MP is less than the set value";
    public string Configwindow_Params_UseItem { get; set; } = "Use items";
    public string Configwindow_Params_UseItemDesc { get; set; } = "Use poison, WIP";
    public string Configwindow_Params_Conditons { get; set; } = "Trigger conditions";
    public string Configwindow_Params_AutoStartCountdown { get; set; } = "Turn on auto-attack on countdown";
    public string Configwindow_Params_HealthDifference { get; set; } = "Set the HP standard deviation threshold for using AOE heal (ability & spell)";
    public string Configwindow_Params_HealthAreaAbility { get; set; } = "Set the HP threshold for using AOE healing ability";
    public string Configwindow_Params_HealthAreafSpell { get; set; } = "Set the HP threshold for using AOE healing spell";
    public string Configwindow_Params_HealingOfTimeSubstactArea { get; set; } = "Set the HP threshold reduce with hot effect(AOE)";
    public string Configwindow_Params_HealthSingleAbility { get; set; } = "Set the HP threshold for using single healing ability";
    public string Configwindow_Params_HealthSingleSpell { get; set; } = "Set the HP threshold for using single healing spell";
    public string Configwindow_Params_HealingOfTimeSubstactSingle { get; set; } = "Set the HP threshold reduce with hot effect(single)";
    public string Configwindow_Params_HealthForDyingTank { get; set; } = "Set the HP threshold for tank to use invincibility";
    public string Configwindow_Params_Targets { get; set; } = "Target selection";
    public string Configwindow_Params_RightNowTargetToHostileType { get; set; } = "Hostile target filtering condition";
    public string Configwindow_Params_TargetToHostileType1 { get; set; } = "All hostiles";
    public string Configwindow_Params_TargetToHostileType2 { get; set; } = "Enemies or all hostiles";
    public string Configwindow_Params_TargetToHostileType3 { get; set; } = "Hostiles targeting party members";
    public string Configwindow_Params_AddEnemyListToHostile { get; set; } = "Enemies";
    public string Configwindow_Params_ChooseAttackMark { get; set; } = "Priority attack targets with attack markers";
    public string Configwindow_Params_AttackMarkAOE { get; set; } = "Forced use of AOE";
    public string Configwindow_Params_AttackMarkAOEDesc { get; set; } = "Attention: Checking this option , AA will attack as many hostile targets as possible, while ignoring whether the attack will cover the marked target.";
    public string Configwindow_Params_FilterStopMark { get; set; } = "Never attack targets with stop markers";
    public string Configwindow_Params_ObjectMinRadius { get; set; } = "Set the minimum target circle threshold possessed by the attack target";
    public string Configwindow_Params_ChangeTargetForFate { get; set; } = "Select only Fate targets in Fate";
    public string Configwindow_Params_MoveTowardsScreen { get; set; } = "Using movement actions towards the object in the center of the screen";
    public string Configwindow_Params_MoveTowardsScreenDesc { get; set; } = "Using movement actions towards the object in the center of the screen, otherwise toward the facing object.";
    public string Configwindow_Params_RaiseAll { get; set; } = "Raise all (include passerby)";
    public string Configwindow_Params_RaiseBrinkofDeath { get; set; } = "Raise player even has Brink of Death";
    public string Configwindow_Params_Hostile { get; set; } = "Hostile target filtering options";
    public string Configwindow_Params_HostileDesc { get; set; } = "You can set the logic of hostile target selection to allow flexibility in switching the logic of selecting hostile in battle.";
    public string Configwindow_Params_AddHostileCondition { get; set; } = "Add selection condition";
    public string Configwindow_Params_HostileCondition { get; set; } = "Hostile target selection condition";
    public string Configwindow_Params_ConditionUp { get; set; } = "Up";
    public string Configwindow_Params_ConditionDown { get; set; } = "Down";
    public string Configwindow_Params_ConditionDelete { get; set; } = "Delete";
    #endregion

    #region ScriptWindow
    public string Scriptwindow_Header { get; set; } = "Custom Opener v";
    public string Scriptwindow_Author { get; set; } = "Author";
    public string Scriptwindow_OpenSourceFile { get; set; } = "Open Source File";
    public string Scriptwindow_Save { get; set; } = "Save";
    public string Scriptwindow_DragdropDescription { get; set; } = "Drag&drop to move，Ctrl+Alt+RightClick to delete.";
    public string Scriptwindow_SearchBar { get; set; } = "Search Bar";
    public string Scriptwindow_MustUse { get; set; } = "MustUse";
    public string Scriptwindow_MustUseDesc { get; set; } = "Skip AOE and Buff.";
    public string Scriptwindow_Empty { get; set; } = "UseUp";
    public string Scriptwindow_EmptyDesc { get; set; } = "UseUp or Skip Combo";
    public string Scriptwindow_Return { get; set; } = "Return condition";
    public string Scriptwindow_ActionConditionsDescription { get; set; } = "Description";
    public string Scriptwindow_AbilityRemain { get; set; } = "Ability Remains";
    public string Scriptwindow_AbilityRemainDesc { get; set; } = "Use this action while how many abilities remain. Set it to zero to ignore.";
    public string Scriptwindow_AdjustID { get; set; } = "AdjustID";
    public string Scriptwindow_NextGCD { get; set; } = "NextGCD";
    public string Scriptwindow_ActionSetDescription { get; set; } = "Input the actions into the box below. The higher the position, the higher the priority.";
    public string Scriptwindow_ActionSetGaurd { get; set; } = "ActionGaurd";
    public string Scriptwindow_AddActionDesc { get; set; } = "Add Action";
    public string Scriptwindow_AddFunctionDesc { get; set; } = "Add Function，total {0}.";
    public string Scriptwindow_ComboSetAuthorDefault { get; set; } = "Unknown";
    public string Scriptwindow_CountDown { get; set; } = "CountDown";
    public string Scriptwindow_CountDownDesc { get; set; } = "The actions need to use in the countdown.";
    public string Scriptwindow_CountDownSetDesc { get; set; } = "Input the actions need to use in the countdown into the box below.";
    public string Scriptwindow_Can { get; set; } = "Can";
    public string Scriptwindow_Cannot { get; set; } = "Cannot";
    public string Scriptwindow_Is { get; set; } = "Is";
    public string Scriptwindow_Isnot { get; set; } = "Isnot";
    public string Scriptwindow_Have { get; set; } = "Have";
    public string Scriptwindow_Havenot { get; set; } = "Havenot";
    public string Scriptwindow_Ability { get; set; } = "Ability";
    public string Scriptwindow_Charges { get; set; } = "Charges";
    public string Scriptwindow_OR { get; set; } = "OR";
    public string Scriptwindow_AND { get; set; } = "AND";
    public string Scriptwindow_ConditionSet { get; set; } = "ConditionSet";
    public string Scriptwindow_ActionCondition { get; set; } = "ActionCondition";
    public string Scriptwindow_TargetCondition { get; set; } = "TargetCondition";
    public string Scriptwindow_ComboCondition { get; set; } = "ComboCondition";
    public string Scriptwindow_ActionTarget { get; set; } = "{0}'s target";
    public string Scriptwindow_Target { get; set; } = "Target";
    public string Scriptwindow_Player { get; set; } = "Player";
    public string Scriptwindow_StatusSelf { get; set; } = "StatusSelf";
    public string Scriptwindow_StatusSelfDesc { get; set; } = "StatusSelf";
    #endregion

    #region ActionConditionType
    public string ActionConditionType_Elapsed { get; set; } = "Elapsed";
    public string ActionConditionType_ElapsedGCD { get; set; } = "ElapsedGCD ";
    public string ActionConditionType_Remain { get; set; } = "RemainTime";
    public string ActionConditionType_RemainGCD { get; set; } = "RemainGCD";
    public string ActionConditionType_ShouldUse { get; set; } = "ShouldUse";
    public string ActionConditionType_EnoughLevel { get; set; } = "EnoughLevel";
    public string ActionConditionType_IsCoolDown { get; set; } = "IsCoolDown";
    public string ActionConditionType_CurrentCharges { get; set; } = "CurrentCharges";
    public string ActionConditionType_MaxCharges { get; set; } = "MaxCharges";
    #endregion

    #region TargetConditionType
    public string TargetConditionType_HaveStatus { get; set; } = "HaveStatus";
    public string TargetConditionType_IsDying { get; set; } = "IsDying";
    public string TargetConditionType_IsBoss { get; set; } = "IsBoss";
    public string TargetConditionType_Distance { get; set; } = "Distance";
    public string TargetConditionType_StatusEnd { get; set; } = "StatusEnd";
    public string TargetConditionType_StatusEndGCD { get; set; } = "StatusEndGCD";

    #endregion

    #region DescType
    public string DescType_Description { get; set; } = "循环说明";
    public string DescType_BreakingAction { get; set; } = "爆发技能";
    public string DescType_HealArea { get; set; } = "范围治疗";
    public string DescType_HealSingle { get; set; } = "单体治疗";
    public string DescType_DefenseArea { get; set; } = "范围防御";
    public string DescType_DefenseSingle { get; set; } = "单体防御";
    public string DescType_MoveAction { get; set; } = "移动技能";

    #endregion

    public Dictionary<string, string> MemberInfoName { get; set; } = new Dictionary<string, string>()
    {
        #region Combo
        { nameof(ComboSet.EmergencyGCDSet), "EmergencyGCD"},
        { nameof(ComboSet.GeneralGCDSet), "GeneralGCD"},
        { nameof(ComboSet.DefenceAreaGCDSet), "DefenceAreaGCD"},
        { nameof(ComboSet.DefenceSingleGCDSet), "DefenceSingleGCD"},
        { nameof(ComboSet.HealAreaGCDSet), "HealAreaGCD"},
        { nameof(ComboSet.HealSingleGCDSet), "HealSingleGCD"},
        { nameof(ComboSet.MoveGCDSet), "MoveGCD"},
        { nameof(ComboSet.EmergencyAbilitySet), "EmergencyAbility"},
        { nameof(ComboSet.GeneralAbilitySet), "GeneralAbility"},
        { nameof(ComboSet.AttackAbilitySet), "AttackAbility"},
        { nameof(ComboSet.DefenceAreaAbilitySet), "DefenceAreaAbility"},
        { nameof(ComboSet.DefenceSingleAbilitySet), "DefenceSingleAbility"},
        { nameof(ComboSet.HealAreaAbilitySet), "HealAreaAbility"},
        { nameof(ComboSet.HealSingleAbilitySet), "HealSingleAbility"},
        { nameof(ComboSet.MoveAbilitySet), "MoveAbility"},
        { "IsMoving", "在移动"},
        { "HaveHostilesInRange", "范围内有敌人"},
        { "IsFullParty", "满编小队"},
        { "SettingBreak", "处于爆发"},
        { "Level", "玩家等级"},
        { "InCombat", "在战斗中"},
        { "IsLastGCD", "上一个GCD"},
        { "IsLastAbility", "上一个能力技"},
        { "IsLastAction", "上一个技能"},
        { "IsTargetDying", "目标将要死亡"},
        { "IsTargetBoss", "目标为Boss"},
        { "HaveSwift", "有即刻"},
        { "HaveShield", "有盾姿"},
        #endregion

        #region AST
        { "PlayCard", "发卡"},
        #endregion

        #region BLM
        { "UmbralIceStacks", "冰状态层数"},
        { "AstralFireStacks", "火状态层数"},
        { "PolyglotStacks", "通晓层数"},
        { "UmbralHearts", "灵极心层数"},
        { "IsParadoxActive", "是否有悖论"},
        { "InUmbralIce", "在冰状态"},
        { "InAstralFire", "在火状态"},
        { "IsEnochianActive", "是否有天语状态"},
        { "EnchinaEndAfter", "下一个通晓还剩多少时间好"},
        { "EnchinaEndAfterGCD", "下一个通晓还剩多少时间好"},
        { "ElementTimeEndAfter", "天语剩余时间"},
        { "ElementTimeEndAfterGCD", "天语剩余时间"},
        { "HasFire", "有火苗"},
        { "HasThunder", "有雷云"},
        { "IsPolyglotStacksMaxed", "通晓是否已经达到当前等级下的最大层数"}, //这玩意儿太长了！
        #endregion
    };

    public Dictionary<string, string> MemberInfoDesc { get; set; } = new Dictionary<string, string>()
    {
        #region Combo
        { nameof(ComboSet.EmergencyGCDSet), "EmergencyGCD"},
        { nameof(ComboSet.GeneralGCDSet), "GeneralGCD"},
        { nameof(ComboSet.DefenceAreaGCDSet), "DefenceAreaGCD"},
        { nameof(ComboSet.DefenceSingleGCDSet), "DefenceSingleGCD"},
        { nameof(ComboSet.HealAreaGCDSet), "HealAreaGCD"},
        { nameof(ComboSet.HealSingleGCDSet), "HealSingleGCD "},
        { nameof(ComboSet.MoveGCDSet), "MoveGCD"},
        { nameof(ComboSet.EmergencyAbilitySet), "EmergencyAbility"},
        { nameof(ComboSet.GeneralAbilitySet), "GeneralAbility"},
        { nameof(ComboSet.AttackAbilitySet), "AttackAbility"},
        { nameof(ComboSet.DefenceAreaAbilitySet), "DefenceAreaAbility"},
        { nameof(ComboSet.DefenceSingleAbilitySet), "DefenceSingleAbility"},
        { nameof(ComboSet.HealAreaAbilitySet), "HealAreaAbility"},
        { nameof(ComboSet.HealSingleAbilitySet), "HealSingleAbility"},
        { nameof(ComboSet.MoveAbilitySet), "MoveAbility"},
        { "IsMoving", "玩家正在移动"},
        { "HaveHostilesInRange", "近战3米内有敌人，远程25米内有敌人。"},
        { "IsFullParty", "满编小队"},
        { "SettingBreak", "处于爆发"},
        { "Level", "玩家等级"},
        { "InCombat", "在战斗中"},
        { "IsLastGCD", "上一个GCD"},
        { "IsLastAbility", "上一个能力技"},
        { "IsLastAction", "上一个技能"},
        { "IsTargetDying", "目标将要死亡"},
        { "IsTargetBoss", "目标为Boss"},
        { "HaveSwift", "有即刻"},
        { "HaveShield", "有盾姿"},
        #endregion

        #region AST
        { "PlayCard", "发卡"},
        #endregion

        #region BLM
        { "UmbralIceStacks", "冰状态层数"},
        { "AstralFireStacks", "火状态层数"},
        { "PolyglotStacks", "通晓层数"},
        { "UmbralHearts", "灵极心层数"},
        { "IsParadoxActive", "是否有悖论"},
        { "InUmbralIce", "在冰状态"},
        { "InAstralFire", "在火状态"},
        { "IsEnochianActive", "是否有天语状态"},
        { "EnchinaEndAfter", "下一个通晓还剩多少时间好"},
        { "EnchinaEndAfterGCD", "下一个通晓还剩多少时间好"},
        { "ElementTimeEndAfter", "天语剩余时间"},
        { "ElementTimeEndAfterGCD", "天语剩余时间"},
        { "HasFire", "有火苗"},
        { "HasThunder", "有雷云"},
        { "IsPolyglotStacksMaxed", "通晓是否已经达到当前等级下的最大层数"},
        #endregion
    };
}
