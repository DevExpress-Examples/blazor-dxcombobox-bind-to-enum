# ComboBox for Blazor - Bind to an enumeration

This example demonstrates how to create a wrapper class that obtains enumeration values and passes them to the component's `Data` property.

![Bind Combobox to Enum](images/BindComboboxToEnum.png)

## Overview

Create a wrapper class with two properties that specify an enumeration value and a text string that the ComboBox displays.

```cs
public class EducationDegree {
    // Specifies an enumeration value
    public EducationType Value { get; set; }
    // Specifies a text string
    public string DisplayName { get; set; }
}
```

Create a generic extension method that gets the [DisplayAttribute.Name](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.displayattribute.name?view=net-7.0) property value from the enumeration's member.

```cs
using System;
using System.Linq;
using System.Reflection;

public static class Extensions {
    public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute => enumValue.GetType()
                                            .GetMember(enumValue.ToString())
                                            .First()
                                            .GetCustomAttribute<TAttribute>();
    }
}
```

Add the ComboBox component to your project and override the [OnInitialized](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-7.0#component-initialization-oninitializedasync) lifecycle method. This method creates a match between integer and string values of the enumeration's item.

```razor
@using System.ComponentModel.DataAnnotations;
@using Education.Data

<DxComboBox Data="EducationDegrees" 
            TextFieldName="@nameof(EducationDegree.DisplayName)"
            ValueFieldName="@nameof(EducationDegree.Value)" 
            @bind-Value="@Value"
            AllowUserInput="true"></DxComboBox>

@code {
    public List<EducationDegree> EducationDegrees { get; set; } = new List<EducationDegree>();
    int Value { get; set; } = 0;

    protected override void OnInitialized() {
        //...
        EducationDegrees = Enum.GetValues(typeof(EducationType))
            .OfType<EducationType>()
            .Select(t => new EducationDegree()
            {
                Value = t,
                DisplayName = t.GetAttribute<DisplayAttribute>().Name
            }).ToList();
        base.OnInitialized();
    }
}
```

## Files to Review

- [Combobox.razor](CS/Shared/Combobox.razor)
- [Education.cs](CS/Data/Education.cs)

## Documentation

- [DxComboBox - Bind to an Enumeration](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxComboBox-2.Data#bind-to-an-enumeration)
