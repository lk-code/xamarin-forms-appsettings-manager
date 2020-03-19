using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace de.lkraemer.appsettingsmanager.test
{
    [TestClass]
    public class AppSettingsManagerTest
    {
        [TestMethod]
        public void TestClassInit()
        {
            try
            {
                AppSettingsManager.LoadSettings("XamarinAppExample.App", "appsettings.json");

                string test = AppSettingsManager.Settings["test"];
            }
            catch (InvalidOperationException exception)
            {
                Assert.IsTrue(false);
            }
            catch (Exception exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
