﻿using Microsoft.VisualBasic;
using RotationSolver.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RotationSolver.Configuration.RotationConfig
{
    internal class RotationConfigSet
    {
        ClassJobID _job;
        string _rotationName;
        public HashSet<IRotationConfig> Configs { get; } = new HashSet<IRotationConfig>(new RotationConfigComparer());

        public RotationConfigSet(ClassJobID job, string rotationName)
        {
            _job = job;
            _rotationName = rotationName;
        }

        #region Set
        public RotationConfigSet SetFloat(string name, float value, string displayName, float min = 0, float max = 1, float speed = 0.002f)
        {
            Configs.Add(new RotationConfigFloat(name, value, displayName, min, max, speed));
            return this;
        }

        public RotationConfigSet SetString(string name, string value, string displayName)
        {
            Configs.Add(new RotationConfigString(name, value, displayName));
            return this;
        }

        public RotationConfigSet SetBool(string name, bool value, string displayName)
        {
            Configs.Add(new RotationConfigBoolean(name, value, displayName));
            return this;
        }

        public RotationConfigSet SetCombo(string name, int value, string displayName, params string[] items)
        {
            Configs.Add(new RotationConfigCombo(name, value, displayName, items));
            return this;
        }

        public void SetValue(string name, string value)
        {
            var config = Configs.FirstOrDefault(config => config.Name == name);
            if (config == null) return;
            config.SetValue(_job, _rotationName, value);
        }
        #endregion

        #region Get
        public int GetCombo(string name)
        {
            var result = GetString(name);
            if (int.TryParse(result, out var f)) return f;
            return 0;
        }

        public bool GetBool(string name)
        {
            var result = GetString(name);
            if (bool.TryParse(result, out var f)) return f;
            return false;
        }

        public float GetFloat(string name)
        {
            var result = GetString(name);
            if(float.TryParse(result, out var f)) return f;
            return float.NaN;
        }

        public string GetString(string name)
        {
            var config = Configs.FirstOrDefault(config => config.Name == name);
            if (config == null) return string.Empty;
            return config.GetValue(_job, _rotationName);
        }
        #endregion

        public void Draw(bool canAddButton)
        {
            foreach (var config in Configs)
            {
                config.Draw(this, canAddButton);
            }
        }
    }
}
