# Hubert's Toolkit

Infinite sprint and a flashlight battery that does not drain, for Lethal Company.

The sprint meter stays full. While you are using a battery flashlight, its charge is filled back in after the game drains it.

## Configuration

After one launch, edit:

`BepInEx/config/Huberts.Toolkit.cfg`

| Key | Default | Meaning |
|-----|---------|---------|
| `InfiniteSprint` | `true` | Keep the sprint meter full |
| `InfiniteFlashlight` | `true` | Refill a flashlight battery while it is in use |

Set either key to `false` to leave that part of the game vanilla. Both are on until you change them.

## Install

1. Use r2modman / Gale with [BepInExPack](https://thunderstore.io/c/lethal-company/p/BepInEx/BepInExPack/) for Lethal Company, **or** drop the built DLL into `BepInEx/plugins/`.
2. Launch Lethal Company from the mod manager.

Linux runs the Windows build under Proton. Proton skips BepInEx's `winhttp.dll`, so the plugin never loads. For a manual install, set the Steam launch options to:

```text
WINEDLLOVERRIDES="winhttp=n,b" %command%
```

r2modman and Gale usually set that override themselves. The BepInEx console often stays hidden on Proton. `BepInEx/LogOutput.log` should contain `Huberts.Toolkit has loaded successfully.`

## License

[MIT with attribution](https://github.com/fake-game-developers/HubertsToolkit/blob/master/LICENSE). Copyright (c) 2026 Fake Game Developers.

## Credits

- Original author: **urbecks**
- This mod belongs to **Fake Game Developers**

Work based on this mod must credit urbecks and Fake Game Developers.
