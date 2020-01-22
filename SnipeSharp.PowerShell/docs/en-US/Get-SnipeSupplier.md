---
external help file: SnipeSharp.PowerShell.dll-Help.xml
Module Name: SnipeSharp.PowerShell
online version:
schema: 2.0.0
---

# Get-SnipeSupplier

## SYNOPSIS
Gets a Snipe IT supplier.

## SYNTAX

### All (Default)
```
Get-SnipeSupplier [<CommonParameters>]
```

### ByInternalId
```
Get-SnipeSupplier -InternalId <Int32[]> [<CommonParameters>]
```

### ByName
```
Get-SnipeSupplier -Name <String[]> [<CommonParameters>]
```

### ByIdentity
```
Get-SnipeSupplier [-Identity] <ObjectBinding`1[]> [<CommonParameters>]
```

## DESCRIPTION
The Get-Supplier cmdlet gets one or more supplier objects by name or by Snipe IT internal id number.

Whatever identifier is used, both accept pipeline input.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-Supplier 14
```

Retrieve an supplier by its Internal Id.

### Example 2
```powershell
PS C:\> Get-Supplier Supplier4368
```

Retrieve an supplier explicitly by its Name.

### Example 3
```powershell
1..100 | Get-Supplier
```

Retrieve the first 100 suppliers by their Snipe IT internal Id numbers.

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

### SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[[SnipeSharp.Models.Supplier, SnipeSharp, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null]][]

## OUTPUTS

### SnipeSharp.Models.Supplier

## NOTES

## RELATED LINKS

[Find-SnipeSupplier](Find-SnipeSupplier.md)
