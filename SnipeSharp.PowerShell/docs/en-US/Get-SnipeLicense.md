---
external help file: SnipeSharp.PowerShell.dll-Help.xml
Module Name: SnipeSharp.PowerShell
online version:
schema: 2.0.0
---

# Get-SnipeLicense

## SYNOPSIS
Gets a Snipe IT license.

## SYNTAX

### All (Default)
```
Get-SnipeLicense [<CommonParameters>]
```

### ByInternalId
```
Get-SnipeLicense -InternalId <Int32[]> [<CommonParameters>]
```

### ByName
```
Get-SnipeLicense -Name <String[]> [<CommonParameters>]
```

### ByIdentity
```
Get-SnipeLicense [-Identity] <ObjectBinding`1[]> [<CommonParameters>]
```

## DESCRIPTION
The Get-License cmdlet gets one or more license objects by name or by Snipe IT internal id number.

Whatever identifier is used, both accept pipeline input.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-License 14
```

Retrieve an license by its Internal Id.

### Example 2
```powershell
PS C:\> Get-License License4368
```

Retrieve an license explicitly by its Name.

### Example 3
```powershell
PS C:\> 1..100 | Get-License
```

Retrieve the first 100 licenses by their Snipe IT internal Id numbers.

## PARAMETERS

### -Identity
An identity for an object.

```yaml
Type: ObjectBinding`1[]
Parameter Sets: ByIdentity
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -InternalId
The internal Id of the Object.

```yaml
Type: Int32[]
Parameter Sets: ByInternalId
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name of the Object.

```yaml
Type: String[]
Parameter Sets: ByName
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Int32[]

### System.String[]

### SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[[SnipeSharp.Models.License, SnipeSharp, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null]][]

## OUTPUTS

### SnipeSharp.Models.License

## NOTES

## RELATED LINKS

[Find-SnipeLicense](Find-SnipeLicense.md)
