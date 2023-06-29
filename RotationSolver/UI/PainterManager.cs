﻿using ECommons.DalamudServices;
using ECommons.GameHelpers;
using RotationSolver.Updaters;
using XIVPainter;
using XIVPainter.Element3D;
using XIVPainter.ElementSpecial;

namespace RotationSolver.UI;

internal static class PainterManager
{
    class PositionalDrawing : Drawing3DPoly
    {
        Drawing3DCircularSectorO _noneCir, _flankCir, _rearCir;

        public EnemyPositional Positional { get; set; }

        public GameObject Target { get; set; }

        public PositionalDrawing()
        {
            var right = ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.15f));
            var wrong = ImGui.ColorConvertFloat4ToU32(new Vector4(0.3f, 0.8f, 0.2f, 0.15f));

            _noneCir = new Drawing3DCircularSectorO(null, 3, wrong, 2);

            _flankCir = new Drawing3DCircularSectorO(null, 3, wrong, 2, XIVPainter.Enum.RadiusInclude.IncludeBoth,
                new Vector2(MathF.PI * 0.25f, MathF.PI / 2), new Vector2(MathF.PI * 1.25f, MathF.PI / 2));
            _rearCir = new Drawing3DCircularSectorO(null, 3, wrong, 2, XIVPainter.Enum.RadiusInclude.IncludeBoth,
                new Vector2(MathF.PI * 0.75f, MathF.PI / 2));

            _noneCir.InsideColor = _flankCir.InsideColor = _rearCir.InsideColor = right;

            SubItems = new IDrawing3D[] { _noneCir, _flankCir, _rearCir };
        }

        public override void UpdateOnFrame(XIVPainter.XIVPainter painter)
        {
            if (Target == null || !Target.IsNPCEnemy())
            {
                _flankCir.Target = null;
                _rearCir.Target = null;
                _noneCir.Target = null;
            }
            else
            {
                var pos = Positional;
                if (!Target.HasPositional() || Player.Available && Player.Object.HasStatus(true, CustomRotation.TrueNorth.StatusProvide))
                {
                    pos = EnemyPositional.None;
                }
                switch (pos)
                {
                    case EnemyPositional.Flank when Service.Config.DrawPositional:
                        _flankCir.Target = Target;
                        _rearCir.Target = null;
                        _noneCir.Target = null;
                        break;

                    case EnemyPositional.Rear when Service.Config.DrawPositional:
                        _flankCir.Target = null;
                        _rearCir.Target = Target;
                        _noneCir.Target = null;
                        break;

                    case EnemyPositional.None when Service.Config.DrawMeleeRange:
                        _flankCir.Target = null;
                        _rearCir.Target = null;
                        _noneCir.Target = Target;
                        break;

                    default:
                        _flankCir.Target = null;
                        _rearCir.Target = null;
                        _noneCir.Target = null;
                        break;
                }
            }

            base.UpdateOnFrame(painter);
        }
    }

    class TargetDrawing : Drawing3DPoly
    {
        Drawing3DCircularSector _target;
        Drawing3DImage _targetImage;

        public TargetDrawing()
        {
            var TColor = ImGui.GetColorU32(Service.Config.TargetColor);
            _target = new Drawing3DCircularSector(default, 0, TColor, 3)
            {
                IsFill = false,
            };
            _targetImage = new Drawing3DImage(null, default, 0);
        }

        const float targetRadius = 0.15f;
        public override void UpdateOnFrame(XIVPainter.XIVPainter painter)
        {
            SubItems = Array.Empty<IDrawing3D>();

            if (!Service.Config.ShowTarget) return;

            if (ActionUpdater.NextAction is not BaseAction act) return;

            if (act.Target == null) return;

            var d = DateTime.Now.Millisecond / 1000f;
            var ratio = (float)DrawingExtensions.EaseFuncRemap(EaseFuncType.None, EaseFuncType.Cubic)(d);
            List<IDrawing3D> subItems = new List<IDrawing3D>();

            if(Service.Config.TargetIconSize > 0)
            {
                _targetImage.Position = act.Target.Position;
                _targetImage.SetTexture(act.GetTexture(true), Service.Config.TargetIconSize);
                subItems.Add(_targetImage);
            }
            else
            {
                _target.Color = ImGui.GetColorU32(Service.Config.TargetColor);
                _target.Center = act.Target.Position;
                _target.Radius = targetRadius * ratio;
                subItems.Add(_target);
            }

            if (DataCenter.HostileTargets.Contains(act.Target) || act.Target == Player.Object && !act.IsFriendly)
            {
                var SColor = ImGui.GetColorU32(Service.Config.SubTargetColor);

                foreach (var t in DataCenter.HostileTargets)
                {
                    if (t == act.Target) continue;
                    if (act.CanGetTarget(act.Target, t))
                    {
                        subItems.Add(new Drawing3DCircularSector(t.Position, targetRadius * ratio, SColor, 3)
                        {
                            IsFill = false,
                        });
                    }
                }
            }

            SubItems = subItems.ToArray();

            base.UpdateOnFrame(painter);
        }
    }

    class TargetText : Drawing3DPoly
    {
        const int ItemsCount = 16;

        static readonly uint HealthRatioColor = ImGui.GetColorU32(new Vector4(0, 1, 0.8f, 1));
        public TargetText()
        {
            SubItems = new IDrawing3D[ItemsCount];
            for (int i = 0; i < ItemsCount; i++)
            {
                SubItems[i] = new Drawing3DText(string.Empty, default);
            }
        }

        public override void UpdateOnFrame(XIVPainter.XIVPainter painter)
        {
            for (int i = 0; i < ItemsCount; i++)
            {
                ((Drawing3DText)SubItems[i]).Text = string.Empty;
            }

            if (!Service.Config.ShowHealthRatio) return;

            var calHealth = (double)ObjectHelper.GetHealthFromMulty(1);

            int index = 0;
            foreach (GameObject t in DataCenter.AllTargets.OrderBy(ObjectHelper.DistanceToPlayer))
            {
                if (t is not BattleChara b) continue;

                var item = (Drawing3DText)SubItems[index++];

                item.Text = $"Health Ratio: {b.CurrentHp / calHealth:F2} / {b.MaxHp / calHealth:F2}";
                item.Color = HealthRatioColor;
                item.Position = b.Position;

                if(index >= ItemsCount) break;
            }
            base.UpdateOnFrame(painter);
        }
    }

    internal static XIVPainter.XIVPainter _painter;
    static PositionalDrawing _positional = new();
    static DrawingHighlightHotbar _highLight = new();

    public static HashSet<uint> ActionIds => _highLight.ActionIds;

    public static Vector4 HighlightColor
    {
        get => _highLight.Color;
        set => _highLight.Color = value;
    }

    public static void Init()
    {
        _painter = XIVPainter.XIVPainter.Create(Svc.PluginInterface, "RotationSolverOverlay");

        _positional = new();
        _highLight = new();

        _painter.DrawingHeight = Service.Config.DrawingHeight;
        _painter.SampleLength = Service.Config.SampleLength;
        _painter.Enable = Service.Config.UseOverlayWindow;
        HighlightColor = Service.Config.TeachingModeColor;

        var annulus = new Drawing3DAnnulusO(Player.Object, 3, 3 + Service.Config.MeleeRangeOffset, 0, 2);
        annulus.InsideColor = ImGui.ColorConvertFloat4ToU32(new Vector4(0.8f, 0.3f, 0.2f, 0.15f));

        annulus.UpdateEveryFrame = () =>
        {
            if (Player.Available && (Player.Object.IsJobCategory(JobRole.Tank) || Player.Object.IsJobCategory(JobRole.Melee)) && (Svc.Targets.Target?.IsNPCEnemy() ?? false) && Service.Config.DrawMeleeOffset
            && ActionUpdater.NextGCDAction == null)
            {
                annulus.Target = Svc.Targets.Target;
            }
            else
            {
                annulus.Target = null;
            }
        };

        var color = ImGui.GetColorU32(Service.Config.MovingTargetColor);
        var movingTarget = new Drawing3DHighlightLine(default, default, 0, color, 3);
        movingTarget.UpdateEveryFrame = () =>
        {
            var tar = CustomRotation.MoveTarget;

            if (!Service.Config.ShowMoveTarget || !Player.Available || !tar.HasValue || Vector3.Distance(tar.Value, Player.Object.Position) < 0.01f)
            {
                movingTarget.Radius = 0;
                return;
            }

            movingTarget.Radius = 0.5f;

            movingTarget.Color = ImGui.GetColorU32(Service.Config.MovingTargetColor);

            movingTarget.From = Player.Object.Position;
            movingTarget.To = tar.Value;
        };

        _painter.AddDrawings(_positional, _highLight, annulus, movingTarget, new TargetDrawing(), new TargetText());

#if DEBUG
        //try
        //{
        //    var deadTime = DateTime.Now.AddSeconds(10);
        //    var r = new Random();
        //    var col = ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 0.5f, 0.2f, 0.15f));
        //    var colIn = ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 0.5f, 0.2f, 0.5f));
        //    _painter.AddDrawings(
        //        new Drawing3DAnnulus(Player.Object.Position + new Vector3((float)r.NextDouble() * 3, 0, (float)r.NextDouble() * 3), 3, 5, col, 2)
        //        {
        //            DeadTime = deadTime,
        //            InsideColor = colIn,
        //            PolylineType = XIVPainter.Enum.PolylineType.ShouldGoOut,
        //        },

        //        new Drawing3DCircularSector(Player.Object.Position + new Vector3((float)r.NextDouble() * 3, 0, (float)r.NextDouble() * 3), 3, col, 2)
        //        {
        //            DeadTime = deadTime,
        //            InsideColor = colIn,
        //            PolylineType = XIVPainter.Enum.PolylineType.ShouldGoOut,
        //        }
        //        );

        //    _painter.AddDrawings(new DrawingHighlightHotbar(new(0f, 1f, 0.8f, 1f), 7411));

        //    _painter.AddDrawings(new Drawing3DCircularSectorO(Player.Object, 5, ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 0.5f, 0.4f, 0.15f)), 5));
        //}
        //catch
        //{

        //}
#endif
    }

    public static void UpdatePositional(EnemyPositional positional, GameObject target)
    {
        _positional.Target = target;
        _positional.Positional = positional;
    }

    public static void ClearPositional()
    {
        _positional.Target = null;
    }

    public static void Dispose()
    {
        _painter?.Dispose();
    }
}