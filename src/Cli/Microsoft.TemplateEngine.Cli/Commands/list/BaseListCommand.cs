﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.CommandLine;
using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Edge.Settings;

namespace Microsoft.TemplateEngine.Cli.Commands
{
    internal class BaseListCommand : BaseCommand<ListCommandArgs>, IFilterableCommand, ITabularOutputCommand
    {
        internal static readonly IReadOnlyList<FilterOptionDefinition> SupportedFilters = new List<FilterOptionDefinition>()
        {
            FilterOptionDefinition.AuthorFilter,
            FilterOptionDefinition.BaselineFilter,
            FilterOptionDefinition.LanguageFilter,
            FilterOptionDefinition.TypeFilter,
            FilterOptionDefinition.TagFilter
        };

        internal BaseListCommand(
            NewCommand parentCommand,
            Func<ParseResult, ITemplateEngineHost> hostBuilder,
            string commandName)
            : base(hostBuilder, commandName, SymbolStrings.Command_List_Description)
        {
            ParentCommand = parentCommand;
            Filters = SetupFilterOptions(SupportedFilters);

            this.Arguments.Add(NameArgument);
            this.Options.Add(IgnoreConstraintsOption);
            this.Options.Add(SharedOptions.OutputOption);
            this.Options.Add(SharedOptions.ProjectPathOption);
            SetupTabularOutputOptions(this);
        }

        public virtual CliOption<bool> ColumnsAllOption { get; } = SharedOptionsFactory.CreateColumnsAllOption();

        public virtual CliOption<string[]> ColumnsOption { get; } = SharedOptionsFactory.CreateColumnsOption();

        public IReadOnlyDictionary<FilterOptionDefinition, CliOption> Filters { get; protected set; }

        internal static CliOption<bool> IgnoreConstraintsOption { get; } = new("--ignore-constraints")
        {
            Description = SymbolStrings.ListCommand_Option_IgnoreConstraints,
            Arity = new ArgumentArity(0, 1)
        };

        internal static CliArgument<string> NameArgument { get; } = new("template-name")
        {
            Description = SymbolStrings.Command_List_Argument_Name,
            Arity = new ArgumentArity(0, 1)
        };

        internal NewCommand ParentCommand { get; }

        protected override Task<NewCommandStatus> ExecuteAsync(
            ListCommandArgs args,
            IEngineEnvironmentSettings environmentSettings,
            TemplatePackageManager templatePackageManager,
            ParseResult parseResult,
            CancellationToken cancellationToken)
        {
            TemplateListCoordinator templateListCoordinator = new TemplateListCoordinator(
                environmentSettings,
                templatePackageManager,
                new HostSpecificDataLoader(environmentSettings));

            return templateListCoordinator.DisplayTemplateGroupListAsync(args, cancellationToken);
        }

        protected override ListCommandArgs ParseContext(ParseResult parseResult) => new(this, parseResult);

    }
}
