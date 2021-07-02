@{
    RootModule = 'SnipeSharp.PowerShell.dll'

    # Version number of this module.
    ModuleVersion = '0.1.0'
    CompatiblePSEditions = @(
        'Desktop'
        'Core'
    )

    # ID used to uniquely identify this module
    GUID = 'd16d3f92-561f-4c81-8cb2-73e11cbbff51'
    Author = 'cofl'

    # Company or vendor of this module
    CompanyName = 'Unknown'
    # Copyright statement for this module
    Copyright = '(c) 2020 cofl. MIT License.'

    # Description of the functionality provided by this module
    Description = 'The SnipeSharp.PowerShell module allows the management of SnipeIT with PowerShell using the SnipeSharp library.'

    # Minimum version of the Windows PowerShell engine required by this module
    PowerShellVersion = '5.1'

    # Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
    DotNetFrameworkVersion = '4.6'

    # Assemblies that must be loaded prior to importing this module
    # RequiredAssemblies = @()

    TypesToProcess = 'SnipeSharp.PowerShell.dll-types.ps1xml'
    FormatsToProcess = 'SnipeSharp.PowerShell.dll-format.ps1xml'
    CmdletsToExport = @(
        '*' # TODO
    )

    # Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
    PrivateData = @{
        PSData = @{
            # Tags applied to this module. These help with module discovery in online galleries.
            # Tags = @()

            # A URL to the license for this module.
            # LicenseUri = ''

            # A URL to the main website for this project.
            # ProjectUri = ''

            # A URL to an icon representing this module.
            # IconUri = ''

            # ReleaseNotes of this module
            # ReleaseNotes = ''
        }
    }

    # HelpInfo URI of this module
    # HelpInfoURI = ''
}
