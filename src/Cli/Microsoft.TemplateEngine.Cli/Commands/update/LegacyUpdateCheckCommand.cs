﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.CommandLine;
using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Edge.Settings;

namespace Microsoft.TemplateEngine.Cli.Commands
{
    internal class LegacyUpdateCheckCommand : BaseUpdateCommand
    {
        public LegacyUpdateCheckCommand(
            NewCommand parentCommand,
            Func<ParseResult, ITemplateEngineHost> hostBuilder)
            : base(parentCommand, hostBuilder, "--update-check", SymbolStrings.Command_Update_Description)
        {
            this.Hidden = true;
            parentCommand.AddNoLegacyUsageValidators(this, except: new CliOption[] { InteractiveOption, AddSourceOption });
        }

        internal override CliOption<bool> InteractiveOption => ParentCommand.InteractiveOption;

        internal override CliOption<string[]> AddSourceOption => ParentCommand.AddSourceOption;

        protected override Task<NewCommandStatus> ExecuteAsync(UpdateCommandArgs args, IEngineEnvironmentSettings environmentSettings, TemplatePackageManager templatePackageManager, ParseResult parseResult, CancellationToken cancellationToken)
        {
            PrintDeprecationMessage<LegacyUpdateCheckCommand, UpdateCommand>(args.ParseResult, additionalOption: UpdateCommand.CheckOnlyOption);

            return base.ExecuteAsync(args, environmentSettings, templatePackageManager, parseResult, cancellationToken);
        }
    }
}
