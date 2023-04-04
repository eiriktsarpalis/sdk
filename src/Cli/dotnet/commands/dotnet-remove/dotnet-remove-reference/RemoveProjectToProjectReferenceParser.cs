// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.CommandLine;
using Microsoft.DotNet.Tools;
using Microsoft.DotNet.Tools.Remove.ProjectToProjectReference;
using LocalizableStrings = Microsoft.DotNet.Tools.Remove.ProjectToProjectReference.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class RemoveProjectToProjectReferenceParser
    {
        public static readonly CliArgument<IEnumerable<string>> ProjectPathArgument = new CliArgument<IEnumerable<string>>(LocalizableStrings.ProjectPathArgumentName)
        {
            Description = LocalizableStrings.ProjectPathArgumentDescription,
            Arity = ArgumentArity.OneOrMore,
        }.AddCompletions(Complete.ProjectReferencesFromProjectFile);

        public static readonly CliOption<string> FrameworkOption = new("--framework", "-f")
        {
            Description = LocalizableStrings.CmdFrameworkDescription,
            HelpName = CommonLocalizableStrings.CmdFramework
        };

        private static readonly CliCommand Command = ConstructCommand();

        public static CliCommand GetCommand()
        {
            return Command;
        }

        private static CliCommand ConstructCommand()
        {
            var command = new CliCommand("reference", LocalizableStrings.AppFullName);

            command.Arguments.Add(ProjectPathArgument);
            command.Options.Add(FrameworkOption);

            command.SetAction((parseResult) => new RemoveProjectToProjectReferenceCommand(parseResult).Execute());

            return command;
        }
    }
}
