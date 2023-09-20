// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "CQRS", Scope = "namespaceanddescendants", Target = "~N:Mt.ChangeLog.Logic.Features")]
[assembly: SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates", Justification = "Logger", Scope = "namespaceanddescendants", Target = "~N:Mt.ChangeLog.Logic")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SX1101:Do not prefix local calls with 'this.'", Justification = "this", Scope = "namespaceanddescendants", Target = "~N:Mt.ChangeLog.Logic")]