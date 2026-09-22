// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class OpenWorldMultiLab : ModuleRules
{
	public OpenWorldMultiLab(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] {
			"Core",
			"CoreUObject",
			"Engine",
			"InputCore",
			"EnhancedInput",
			"AIModule",
			"StateTreeModule",
			"GameplayStateTreeModule",
			"UMG",
			"Slate"
		});

		PrivateDependencyModuleNames.AddRange(new string[] { });

		PublicIncludePaths.AddRange(new string[] {
			"OpenWorldMultiLab",
			"OpenWorldMultiLab/Variant_Platforming",
			"OpenWorldMultiLab/Variant_Platforming/Animation",
			"OpenWorldMultiLab/Variant_Combat",
			"OpenWorldMultiLab/Variant_Combat/AI",
			"OpenWorldMultiLab/Variant_Combat/Animation",
			"OpenWorldMultiLab/Variant_Combat/Gameplay",
			"OpenWorldMultiLab/Variant_Combat/Interfaces",
			"OpenWorldMultiLab/Variant_Combat/UI",
			"OpenWorldMultiLab/Variant_SideScrolling",
			"OpenWorldMultiLab/Variant_SideScrolling/AI",
			"OpenWorldMultiLab/Variant_SideScrolling/Gameplay",
			"OpenWorldMultiLab/Variant_SideScrolling/Interfaces",
			"OpenWorldMultiLab/Variant_SideScrolling/UI"
		});

		// Uncomment if you are using Slate UI
		// PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

		// Uncomment if you are using online features
		// PrivateDependencyModuleNames.Add("OnlineSubsystem");

		// To include OnlineSubsystemSteam, add it to the plugins section in your uproject file with the Enabled attribute set to true
	}
}
