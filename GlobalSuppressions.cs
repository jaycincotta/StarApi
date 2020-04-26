// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>", Scope = "module" )]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.RequestBallotFunctions.RequestBallot(Microsoft.AspNetCore.Http.HttpRequest,Microsoft.Extensions.Logging.ILogger)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.VoterFunctions.Ping(Microsoft.AspNetCore.Http.HttpRequest,Microsoft.Extensions.Logging.ILogger)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.VoterFunctions.Ping(Microsoft.AspNetCore.Http.HttpRequest,Microsoft.Extensions.Logging.ILogger)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.FindVoterFunctions.FindVoter(Microsoft.AspNetCore.Http.HttpRequest,Microsoft.Extensions.Logging.ILogger)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Usage", "CA2234:Pass system uri objects instead of strings", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.PollGetNextEmail.Run(Microsoft.Azure.WebJobs.TimerInfo,Microsoft.Extensions.Logging.ILogger)")]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.SendEmailFunctions.SendEmail(Microsoft.AspNetCore.Http.HttpRequest,Microsoft.Extensions.Logging.ILogger)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.SendEmailFunctions.GetEmailTemplate(System.String,)~StarApi.SendEmail.EmailTemplate")]
[assembly: SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.SendEmail.Templates.BallotFlagged.Lookup(System.String)~StarApi.SendEmail.Templates.Reason")]
[assembly: SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.SendEmailFunctions.GetEmailTemplate(System.String,)~StarApi.SendEmail.EmailTemplate")]
[assembly: SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "<Pending>", Scope = "member", Target = "~M:StarApi.SendEmailFunctions.GetEmailTemplate(System.String,)~StarApi.SendEmail.EmailTemplate")]
