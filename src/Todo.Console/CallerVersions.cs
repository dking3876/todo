using System;
using System.Collections.Generic;
using System.Text;
using Brafton.Http.RestClient;
using System.Reflection;

namespace Todo.Console
{
    public class CallerVersions : ICallerVersions
    {
        public string ApplicationName { get; private set; }

        public string ApplicationVersion { get; private set; }

        public string ClientLibraryName { get; private set; }

        public string ClientLibraryVersion { get; private set; }

        public static ICallerVersions GetVersions(Type callingApplication, Type clientLibrary)
        {
            var applicationAssembly = callingApplication.GetTypeInfo().Assembly;
            var applicationName = applicationAssembly.GetName().Name;
            var applicationVersion = applicationAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            var clientLibraryAssembly = clientLibrary.GetTypeInfo().Assembly;
            var clientLibraryName = clientLibraryAssembly.GetName().Name;
            var clientLibraryVersion = clientLibraryAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            return new CallerVersions() { ApplicationName = applicationName, ApplicationVersion = applicationVersion, ClientLibraryName = clientLibraryName, ClientLibraryVersion = clientLibraryVersion };
        }
    }
}