using System;
using System.Runtime.CompilerServices;

namespace RobotDashboard.Tests
{
    public static class TestEnvironment
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            Environment.SetEnvironmentVariable("FF_ADVANCED_STATS", "true");
        }
    }
}