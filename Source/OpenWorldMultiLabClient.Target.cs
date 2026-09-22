using UnrealBuildTool;

public class OpenWorldMultiLabClientTarget : TargetRules
{
	public OpenWorldMultiLabClientTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Client;
		DefaultBuildSettings = BuildSettingsVersion.V7;
		IncludeOrderVersion = EngineIncludeOrderVersion.Unreal5_8;
		ExtraModuleNames.Add("OpenWorldMultiLab");
	}
}
