﻿using System;
using System.Collections.Generic;
using Fody;
using Mono.Cecil;

namespace SexyProxy.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield return "netstandard";
            yield return "mscorlib";
            yield return "System";
            yield return "System.Runtime";
            yield return "System.Core";
        }

        public override void Execute()
        {
            CecilExtensions.LogError = LogError;
            CecilExtensions.LogInfo = LogInfo;
            CecilExtensions.LogWarning = LogWarning;
            CecilExtensions.Initialize(this, ModuleDefinition);

            var propertyWeaver = new SexyProxyWeaver
            {
                ModuleWeaver = this,
                ModuleDefinition = ModuleDefinition,
                LogInfo = LogInfo,
                LogWarning = LogWarning,
                LogError = LogError
            };
            propertyWeaver.Execute();
        }
    }
}