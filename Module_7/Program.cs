using Module_7;

var saveSetting = new ConfigurationComponent("test.json")
{
    C1 = 123.ToString(),
    C2 = 1.23.ToString(),
    C3 = "test1",
    C4 = TimeSpan.FromSeconds(123).ToString(),
    F1 = 456.ToString(),
    F2 = 4.56.ToString(),
    F3 = "test2",
    F4 = TimeSpan.FromSeconds(456).ToString(),
};
saveSetting.SaveSettings();

var loadSetting = new ConfigurationComponent("test.json");
loadSetting.LoadSettings();

Console.WriteLine(loadSetting.C1);
Console.WriteLine(loadSetting.C2);
Console.WriteLine(loadSetting.C3);
Console.WriteLine(loadSetting.C4);
Console.WriteLine(loadSetting.F1);
Console.WriteLine(loadSetting.F2);
Console.WriteLine(loadSetting.F3);
Console.WriteLine(loadSetting.F4);