using Nickel;
using Nickel.Common;
using HarmonyLib;
// using DragonOfTruth01.ValksCCMod.Cards;
// using DragonOfTruth01.ValksCCMod.Artifacts;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonOfTruth01.ValksCCMod;

public sealed class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal IKokoroApi.IV2 KokoroApi { get; }
    internal readonly Harmony Harmony;
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

    // Card Frames
    internal ISpriteEntry ValksCCMod_Character_CardFrame { get; }

    // Character Panel
    internal ISpriteEntry ValksCCMod_Character_Panel { get; }

    // Custom Card Arts
    internal ISpriteEntry ValksCCMod_Character_DefaultCardBG { get; }

    // Artifact Arts

    // Animation Sprites
    internal ISpriteEntry ValksCCMod_Character_Neutral_0 { get; }
    internal ISpriteEntry ValksCCMod_Character_Neutral_1 { get; }
    internal ISpriteEntry ValksCCMod_Character_Neutral_2 { get; }

    internal ISpriteEntry ValksCCMod_Character_Mini_0 { get; }

    internal ISpriteEntry ValksCCMod_Character_Squint_0 { get; }
    internal ISpriteEntry ValksCCMod_Character_Squint_1 { get; }
    internal ISpriteEntry ValksCCMod_Character_Squint_2 { get; }

    // Custom Action Icons

    // Custom Status Icons

    // Custom Decks
    internal IDeckEntry ValksCCMod_Character_Deck { get; }

    // Custom Statuses

    // Card List Definitions
    internal static IReadOnlyList<Type> ValksCCMod_Character_CommonCard_Types { get; } = [
        
    ];

    internal static IReadOnlyList<Type> ValksCCMod_Character_UncommonCard_Types { get; } = [
        
    ];

    internal static IReadOnlyList<Type> ValksCCMod_Character_RareCard_Types { get; } = [
        
    ];

    internal static IReadOnlyList<Type> ValksCCMod_Character_ExeCard_Types { get; } = [
        
    ];

    // Combine all the lists into a single object for reference
    internal static IEnumerable<Type> ValksCCMod_AllCard_Types = [
        .. ValksCCMod_Character_CommonCard_Types,
        .. ValksCCMod_Character_UncommonCard_Types,
        .. ValksCCMod_Character_RareCard_Types,
        .. ValksCCMod_Character_ExeCard_Types
    ];

    // Define our artifact lists
    internal static IReadOnlyList<Type> ValksCCMod_CommonArtifact_Types { get; } = [
        
    ];

    internal static IReadOnlyList<Type> ValksCCMod_BossArtifact_Types { get; } = [
        
    ];

    // Combine all the artifacts into a single list
    internal static IEnumerable<Type> ValksCCMod_AllArtifact_Types = [
        .. ValksCCMod_CommonArtifact_Types,
        .. ValksCCMod_BossArtifact_Types
    ];


    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;

        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!.V2;

        Harmony = new Harmony("DragonOfTruth01.ValksCCMod");

        // This can be done in place of all Instance.Harmony.Patch() calls in class constructors.
        // However, this will case your IDE/text editor to think the function is unused (since 
        // the patch hasn't been given visibility via constructor). This is expected behavior.
        Harmony.PatchAll();

        // Setup localization support
        AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );

        // Card Frames
        ValksCCMod_Character_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/ValksCCMod_character_cardframe.png"));

        // Character Panel
        ValksCCMod_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/ValksCCMod_character_panel.png"));

        // Custom Card Arts
        ValksCCMod_Character_DefaultCardBG = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/CardBGs/ValksCCMod_character_cardbackground.png"));

        // Artifact Arts

        // Animation Sprites
        ValksCCMod_Character_Neutral_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/ValksCCMod_character_neutral_0.png"));
        ValksCCMod_Character_Neutral_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/ValksCCMod_character_neutral_1.png"));
        ValksCCMod_Character_Neutral_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/ValksCCMod_character_neutral_2.png"));

        ValksCCMod_Character_Mini_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/ValksCCMod_character_mini_0.png"));

        ValksCCMod_Character_Squint_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/ValksCCMod_character_squint_0.png"));
        ValksCCMod_Character_Squint_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/ValksCCMod_character_squint_1.png"));
        ValksCCMod_Character_Squint_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/ValksCCMod_character_squint_2.png"));

        // Custom Action Icons

        // Custom Status Icons

        // Register Custom Decks

        ValksCCMod_Character_Deck = helper.Content.Decks.RegisterDeck("ValksCCModCharacterDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = new Color("f4f7f0"),
                titleColor = new Color("000000")
            },

            DefaultCardArt = ValksCCMod_Character_DefaultCardBG.Sprite,
            BorderSprite = ValksCCMod_Character_CardFrame.Sprite,

            Name = AnyLocalizations.Bind(["character", "ValksCCMod_Character", "name"]).Localize,
        });

        // Register the alt starters for the MoreDifficulties mod
        helper.ModRegistry.AwaitApi<IMoreDifficultiesApi>(
            "TheJazMaster.MoreDifficulties",
            new SemanticVersion(1, 3, 0),
            api => api.RegisterAltStarters(
                deck: ValksCCMod_Character_Deck.Deck,
                starterDeck: new StarterDeck
                {
                    cards = [
                        // Alt starters go here
                    ]
                }

            )
        );  

        // Register Animations
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = ValksCCMod_Character_Deck.Deck.Key(),

            LoopTag = "neutral",

            Frames = new[]
            {
                ValksCCMod_Character_Neutral_0.Sprite,
                ValksCCMod_Character_Neutral_1.Sprite,
                ValksCCMod_Character_Neutral_2.Sprite,
                ValksCCMod_Character_Neutral_0.Sprite,
                ValksCCMod_Character_Neutral_1.Sprite,
                ValksCCMod_Character_Neutral_2.Sprite
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = ValksCCMod_Character_Deck.Deck.Key(),
            LoopTag = "mini",
            Frames = new[]
            {
                ValksCCMod_Character_Mini_0.Sprite
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = ValksCCMod_Character_Deck.Deck.Key(),
            LoopTag = "squint",
            Frames = new[]
            {
                ValksCCMod_Character_Squint_0.Sprite,
                ValksCCMod_Character_Squint_1.Sprite,
                ValksCCMod_Character_Squint_2.Sprite,
                ValksCCMod_Character_Squint_0.Sprite,
                ValksCCMod_Character_Squint_1.Sprite,
                ValksCCMod_Character_Squint_2.Sprite
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = ValksCCMod_Character_Deck.Deck.Key(),
            LoopTag = "gameover",
            Frames = new[]
            {
                // The squint sprite is okay to use here...
                ValksCCMod_Character_Squint_0.Sprite,
            }
        });
        
        // Register the mod character as a playable character
        helper.Content.Characters.V2.RegisterPlayableCharacter("ValksCCMod", new PlayableCharacterConfigurationV2()
        {
            Deck = ValksCCMod_Character_Deck.Deck,

            Starters = new()
            {
                cards = [
                    // Starter cards go here
                ]
            },

            Description = AnyLocalizations.Bind(["character", "ValksCCMod_Character", "description"]).Localize,

            BorderSprite = ValksCCMod_Character_Panel.Sprite
        });

        // Register Cards
        foreach (var cardType in ValksCCMod_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(IValksCCModCard.Register))?.Invoke(null, [helper]);

        // Register Artifacts
        foreach (var artifactType in ValksCCMod_AllArtifact_Types)
            AccessTools.DeclaredMethod(artifactType, nameof(IValksCCModArtifact.Register))?.Invoke(null, [helper]);

        // Register Custom Statuses
        
        _ = new StatusManager();
    }
}
