﻿using System.CommandLine;

namespace CarbonAware.CLI.Common
{
    internal class CommonOptions
    {
        public static readonly Option<string[]> RequiredLocationOption = new Option<string[]>(
            new string[] { "--location", "-l" }, 
            CommonLocalizableStrings.LocationDescription)
            {
                IsRequired = true
            };
        public static readonly Option<DateTimeOffset?> StartTimeOption = new Option<DateTimeOffset?>("--startTime", CommonLocalizableStrings.StartTimeDescription);
        public static readonly Option<DateTimeOffset?> EndTimeOption = new Option<DateTimeOffset?>("--endTime", CommonLocalizableStrings.EndTimeDescription);
    }
}
