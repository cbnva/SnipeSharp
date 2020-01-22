---
external help file: SnipeSharp.PowerShell.dll-Help.xml
Module Name: SnipeSharp.PowerShell
online version:
schema: 2.0.0
---

# Set-SnipeUser

## SYNOPSIS
Changes the properties of an existing Snipe-IT user.

## SYNTAX

### ByUserName
```
Set-SnipeUser -UserName <String> [-AvatarUrl <Uri>] [-FirstName <String>] [-LastName <String>]
 [-NewUserName <String>] [-Password <String>] [-EmployeeNumber <String>] [-Manager <UserBinding>]
 [-JobTitle <String>] [-PhoneNumber <String>] [-Address <String>] [-City <String>] [-Country <String>]
 [-State <String>] [-ZipCode <String>] [-NewEmailAddress <String>]
 [-Department <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Department]>]
 [-Location <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Location]>]
 [-Company <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Company]>]
 [-Groups <ObjectBinding`1[]>] [-PassThru] [-Overwrite] [<CommonParameters>]
```

### ByEmailAddress
```
Set-SnipeUser -EmailAddress <String> [-AvatarUrl <Uri>] [-FirstName <String>] [-LastName <String>]
 [-NewUserName <String>] [-Password <String>] [-EmployeeNumber <String>] [-Manager <UserBinding>]
 [-JobTitle <String>] [-PhoneNumber <String>] [-Address <String>] [-City <String>] [-Country <String>]
 [-State <String>] [-ZipCode <String>] [-NewEmailAddress <String>]
 [-Department <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Department]>]
 [-Location <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Location]>]
 [-Company <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Company]>]
 [-Groups <ObjectBinding`1[]>] [-PassThru] [-Overwrite] [<CommonParameters>]
```

### ByIdentity
```
Set-SnipeUser [-AvatarUrl <Uri>] [-FirstName <String>] [-LastName <String>] [-NewUserName <String>]
 [-Password <String>] [-EmployeeNumber <String>] [-Manager <UserBinding>] [-JobTitle <String>]
 [-PhoneNumber <String>] [-Address <String>] [-City <String>] [-Country <String>] [-State <String>]
 [-ZipCode <String>] [-NewEmailAddress <String>]
 [-Department <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Department]>]
 [-Location <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Location]>]
 [-Company <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Company]>]
 [-Groups <ObjectBinding`1[]>] [-Identity] <UserBinding> [-PassThru] [-Overwrite] [<CommonParameters>]
```

### ByName
```
Set-SnipeUser [-AvatarUrl <Uri>] [-FirstName <String>] [-LastName <String>] [-NewUserName <String>]
 [-Password <String>] [-EmployeeNumber <String>] [-Manager <UserBinding>] [-JobTitle <String>]
 [-PhoneNumber <String>] [-Address <String>] [-City <String>] [-Country <String>] [-State <String>]
 [-ZipCode <String>] [-NewEmailAddress <String>]
 [-Department <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Department]>]
 [-Location <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Location]>]
 [-Company <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Company]>]
 [-Groups <ObjectBinding`1[]>] -Name <String> [-PassThru] [-Overwrite] [<CommonParameters>]
```

### ByInternalId
```
Set-SnipeUser [-AvatarUrl <Uri>] [-FirstName <String>] [-LastName <String>] [-NewUserName <String>]
 [-Password <String>] [-EmployeeNumber <String>] [-Manager <UserBinding>] [-JobTitle <String>]
 [-PhoneNumber <String>] [-Address <String>] [-City <String>] [-Country <String>] [-State <String>]
 [-ZipCode <String>] [-NewEmailAddress <String>]
 [-Department <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Department]>]
 [-Location <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Location]>]
 [-Company <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Company]>]
 [-Groups <ObjectBinding`1[]>] -Id <Int32> [-PassThru] [-Overwrite] [<CommonParameters>]
```

## DESCRIPTION
The Set-User cmdlet changes the properties of an existing Snipe-IT user object.

## EXAMPLES

### Example 1
```powershell
PS C:\> Set-User -Identity "atuber" -LastName 'Spud'
```

Changes the last name of user "atuber" to "Spud" without updating their username.

## PARAMETERS

### -Address
The user's updated street address.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AvatarUrl
The updated uri of the image for the user's avatar.

```yaml
Type: Uri
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -City
The user's updated address city.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Company
The updated company the user works for.

```yaml
Type: SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Company]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Country
The user's updated address country.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Department
The updated department the user works for.

```yaml
Type: SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Department]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EmailAddress
The updated email address for the user.

```yaml
Type: String
Parameter Sets: ByEmailAddress
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EmployeeNumber
The updated employee number for the user.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FirstName
The user's new first name.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Groups
The updated list of groups the user is a member of.

```yaml
Type: ObjectBinding`1[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
The id of the item to update.

```yaml
Type: Int32
Parameter Sets: ByInternalId
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The identity of the item to update.

```yaml
Type: UserBinding
Parameter Sets: ByIdentity
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -JobTitle
The updated position of the user.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LastName
The user's new surname.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Location
The updated location the user works at.

```yaml
Type: SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Location]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Manager
The new manager for the user.

```yaml
Type: UserBinding
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name of the item to update.

```yaml
Type: String
Parameter Sets: ByName
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NewEmailAddress
The updated email address for the user.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NewUserName
The updated unique username for the user.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
The updated password for the user.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PhoneNumber
The updated phone number to contact the user.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -State
The user's updated address state.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserName
The updated unique username for the user.

```yaml
Type: String
Parameter Sets: ByUserName
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ZipCode
The user's updated address zip code.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Overwrite
If present, completely overwrite all properties the remote object with the current or provided values.

The provided object will be fetched, its values updated with the ones provided to the cmdlet, then all values given to the API.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
If present, write the response from the Api to the pipeline.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[[SnipeSharp.Models.User, SnipeSharp, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null]]

## OUTPUTS

### SnipeSharp.Models.User

## NOTES

## RELATED LINKS
