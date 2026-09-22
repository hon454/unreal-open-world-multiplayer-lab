using UnrealBuildTool;

public class OpenWorldMultiLabServerTarget : TargetRules
{
	public OpenWorldMultiLabServerTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Server;
		DefaultBuildSettings = BuildSettingsVersion.V7;
		IncludeOrderVersion = EngineIncludeOrderVersion.Unreal5_8;
		ExtraModuleNames.Add("OpenWorldMultiLab");
	}
}
