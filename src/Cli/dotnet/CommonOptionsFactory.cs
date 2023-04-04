﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

#nullable enable

using System.CommandLine;

namespace Microsoft.DotNet.Cli
{
    /// <summary>
    /// Creates common options.
    /// </summary>
    internal static class CommonOptionsFactory
    {
        /// <summary>
        /// Creates common diagnositcs option (-d|--diagnostics).
        /// </summary>
        public static CliOption<bool> CreateDiagnosticsOption() => new("--diagnostics", "-d")
        {
            Description = Microsoft.DotNet.Tools.Help.LocalizableStrings.SDKDiagnosticsCommandDefinition,
            Recursive = true
        };
    }
}
