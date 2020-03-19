# de.lkraemer.appsettingsmanager

this library contains a simple appsettings manager for xamarin forms (.net standard) <br />

[![Downloads](https://img.shields.io/nuget/dt/de.lkraemer.appsettingsmanager.svg?style=flat-square)](http://www.nuget.org/packages/de.lkraemer.appsettingsmanager/) [![NuGet](https://img.shields.io/nuget/v/de.lkraemer.appsettingsmanager.svg?style=flat-square)](http://nuget.org/packages/de.lkraemer.appsettingsmanager) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/626994bfa7fd4b7497251a0b6c40ea6c)](https://www.codacy.com/manual/lk-code/xamarin-forms-appsettings-manager?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=lk-code/xamarin-forms-appsettings-manager&amp;utm_campaign=Badge_Grade) [![License](https://img.shields.io/github/license/lk-code/xamarin-forms-appsettings-manager.svg?style=flat-square)](https://github.com/lk-code/xamarin-forms-appsettings-manager/blob/master/LICENSE)

## installation

install the lib from nuget: https://www.nuget.org/packages/de.lkraemer.appsettingsmanager

## usage

    Assembly assembly = Assembly.GetAssembly(typeof(XamarinAppExample.App));
    AppSettingsManager.LoadSettings(assembly, "XamarinAppExample", "appsettings.json");

    string value = AppSettingsManager.Settings["Secrets:AndroidSecret"];

