// SimpleMoudleEditor.Target.cs

using UnrealBuildTool;
using System.Collections.Generic;

public class SimpleModuleTarget : TargetRules
{
	public SimpleModuleTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Editor;
		DefaultBuildSettings = BuildSettingsVersion.V5;
		IncludeOrderVersion = EngineIncludeOrderVersion.Unreal5_5;
		ExtraModuleNames.Add("SimpleModule");
		ExtraModuleNames.Add("MySpartaLog");
	}
}
