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

## Build

Requires [Lethal Company](https://store.steampowered.com/app/1966720/Lethal_Company/) installed (auto-detected under common Steam paths), or set `LETHAL_COMPANY_GAME_DIR` / `-p:LethalCompanyGameRootDir=`.

```bash
dotnet build src/HubertsToolkit/HubertsToolkit.csproj -c Release
```

`HubertsToolkit.slnx` is for Visual Studio. The `dotnet` CLI can build that file with the .NET 9 SDK or newer. .NET 8 stops on it (`MSB4068`, unrecognized `<Solution>`). The project file builds on .NET 8 and newer, which is what CI uses.

Optional deploy: `-p:DeployToLethalCompany=true -p:LethalCompanyPluginsDir="/path/to/BepInEx/plugins/HubertsToolkit"`  
Optional overrides: copy `Config.Build.user.props.example` → `Config.Build.user.props` (gitignored).

The DLL is `artifacts/bin/HubertsToolkit/release/HubertsToolkit.dll`.

## Thunderstore packaging (CI)

Every push to `master` runs [.github/workflows/thunderstore.yml](.github/workflows/thunderstore.yml):

1. Builds a Thunderstore ZIP (using stripped [LethalAPI.GameLibs](https://www.nuget.org/packages/LethalAPI.GameLibs) for compile references)
2. Uploads a workflow artifact named **`urbecks-HubertsToolkit`**

Every push still builds that zip. Thunderstore publish runs only when `<Version>` in `src/HubertsToolkit/HubertsToolkit.csproj` changes, and the repository secret `TCLI_AUTH_TOKEN` is set. The publish step copies the categories already on the package, so a new version keeps the same tags. A commit that leaves the version unchanged only builds the artifact.

The package namespace in `src/HubertsToolkit/thunderstore.toml` is `urbecks`. Change that to your Thunderstore namespace before the first publish.

### Local package build

```bash
./build.sh
# or: dotnet build src/HubertsToolkit/HubertsToolkit.csproj -c Release -target:PackTS
# zip lands in artifacts/thunderstore/
```

## License

[MIT](LICENSE). Copyright (c) 2024 urbecks.

## Credits

- Original author: **urbecks**
