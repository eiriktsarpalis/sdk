// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.CommandLine;
using Microsoft.DotNet.Workloads.Workload;
using Microsoft.DotNet.Workloads.Workload.Install;
using LocalizableStrings = Microsoft.DotNet.Workloads.Workload.Install.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class WorkloadInstallCommandParser
    {
        public static readonly CliArgument<IEnumerable<string>> WorkloadIdArgument = new("workloadId")
        {
            HelpName = LocalizableStrings.WorkloadIdArgumentName,
            Arity = ArgumentArity.OneOrMore,
            Description = LocalizableStrings.WorkloadIdArgumentDescription
        };

        public static readonly CliOption<bool> SkipSignCheckOption = new("--skip-sign-check")
        {
            Description = LocalizableStrings.SkipSignCheckOptionDescription,
            Hidden = true
        };

        public static readonly CliOption<bool> SkipManifestUpdateOption = new("--skip-manifest-update")
        {
            Description = LocalizableStrings.SkipManifestUpdateOptionDescription
        };

        public static readonly CliOption<string> TempDirOption = new("--temp-dir")
        {
            Description = LocalizableStrings.TempDirOptionDescription
        };

        private static readonly CliCommand Command = ConstructCommand();

        public static CliCommand GetCommand()
        {
            return Command;
        }

        private static CliCommand ConstructCommand()
        {
            CliCommand command = new("install", LocalizableStrings.CommandDescription);

            command.Arguments.Add(WorkloadIdArgument);
            AddWorkloadInstallCommandOptions(command);

            command.SetAction((parseResult) => new WorkloadInstallCommand(parseResult).Execute());

            return command;
        }

        internal static void AddWorkloadInstallCommandOptions(CliCommand command)
        {
            InstallingWorkloadCommandParser.AddWorkloadInstallCommandOptions(command);

            command.Options.Add(SkipManifestUpdateOption);
            command.Options.Add(TempDirOption);
            command.AddWorkloadCommandNuGetRestoreActionConfigOptions();
            command.Options.Add(CommonOptions.VerbosityOption);
            command.Options.Add(SkipSignCheckOption);
        }
    }
}
