using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace StarApi
{
    public class Email
    {
        public string subject { get; private set; }
        public string text { get; private set; }
        public string html { get; private set; }

        public Email(string subject, string text, string html)
        {
            this.subject = subject;
            this.text = text;
            this.html = html;
        }
    }
    public static class RequestBallotFunctions
    {
        private static Email FormatEmail(dynamic voter)
        {
            string url = $"https://staging.ipo.vote/2020primary/{voter.starId}";
            string subject = $"CONFIDENTIAL: IPO Primary Ballot for {voter.firstName} {voter.lastName}";
            string mailtoSubject = $"RE:%20Ballot%20{voter.starId}";
            string mailto = $"mailto:support@equal.vote?subject={mailtoSubject}";
            string text = $@"Dear {voter.firstName},

Thank you for registering to vote in the Independent Party of Oregon’s 2020 Primary.

A link to your specific ballot is provided below.

Please keep this email secure as anyone with this link could cast a vote in your name.

At the time you cast your vote, you will be required to also upload two supporting documents:

1. A photo ID such as your passport or driver's license which includes your full name and date of birth.

2. A recent photo of yourself which we can match to the face on your photo ID.

Thank you in advance for your cooperation.  We take election security very seriously and appreciate the extra time it takes for you to gather these documents.

In most cases, it's easiest to take these photos with your phone and to then vote with your phone by clicking the ballot link in this email.

You don't need to worry about carefully cropping you images, but they must be high resolution and taken with good lighting so that images are clear enough to be verified by our credentialing team.

If the credentialing team cannot verify you from the information you provide, you will be contacted by email and/or phone and you may be requested to provide additional documentation.

Your vote is provisional until we have verified your identity and your ballot will be rejected unless we can verify your identity by Thursday, May 28th 2020.

FINAL DOCUMENTATION MUST BE PROVIDED NO LATER THAN 5 PM PDT MAY 12.

Please vote early so we have time to ensure your vote is counted. 

*************************************************************************************************
BALLOT FOR {voter.firstName} {voter.lastName}

{url}

*************************************************************************************************

Note that this primary will use an innovative voting method called STAR Voting.
Learn more about STAR Voting at https://www.starvoting.us/

If you have any problems voting, you can contact our voter support team by emailing us at
support@equal.vote or simply replying to this email.

Thanks again for voting!

The Equal Vote Coalition - https://equal.vote
The Independent Party of Oregon - https://indparty.com
";
            string html = $@"<div style='max-width:800px;'><p>Dear {voter.firstName},</p>
<p>Thank you for registering to vote in the Independent Party of Oregon’s 2020 Primary. 
A link to your ballot is provided below. 
Please keep this email secure as this link represents your authorization to vote in this election.</p>

<p>At the time you cast your vote, you will be required to upload two supporting documents:</p>
<ul>
<li>A photo ID such as your passport or driver's license or other ID which includes your full name, date of birth, and address<br/></li>
<li>A recent photo with a clear view of your face that we can match to the face on your photo ID</li>
</ul>

<p><b>NOTE</b>: If you do not have a photo ID you may upload the best proof of identity documentation you have
and you will be issued a provisional ballot pending review.</p>

<p>Thank you in advance for your cooperation.  We take election security very seriously.
In most cases, it's easiest to take these photos with your phone and to then vote with your phone by clicking the ballot link in this email.
Images must be high resolution and taken with good lighting so that images are clear enough to be verified by our credentialing team.</p>

<p>If the credentialing team cannot verify you from the information you provide, you will be contacted by email and/or phone and you may be requested to provide additional documentation. 
Your vote is provisional until we have verified your identity</p>

<p>Please cast your vote now, or as soon as possible.</p>

<p><b>ALL VOTES AND FINAL DOCUMENTATION MUST BE SUBMITTED<br/>NO LATER THAN 8 PM PDT MAY 12th.</b></p>

<p>Please vote early so we have time to ensure your vote is counted.</p>

<hr />
<h1><a href='{url}'>CLICK HERE TO CAST YOUR BALLOT</h1>
<hr />

<p>This primary will use STAR Voting.</p>
<p>STAR Voting eliminates vote splitting and the spoiler effect, 
ensuring that voters can vote their conscience, 
even in elections with large fields of candidates.</p>
<p>More information on STAR Voting is available on our <a href='https://www.starvoting.us/ipo_faq'>FAQ</a>
and will be linked from your ballot.</p>

<div style='width:100%;margin-top:24px;margin-bottom:24px;'>
<a href='https://www.starvoting.us'><img src='https://register.ipo.vote/star.png' width='100%'/></a>
</div>

<p>If you have any problems voting, you may contact our voter support team by emailing us at
<a href='{mailto}'>support@equal.vote</a> or simply replying to this email.</p>

<p>Thanks again for voting!</>

<p><b>STAR Vote Elections</b> - <a href='https://starvoting.us'>starvoting.us</a><br/>
<b>The Independent Party of Oregon</b> - <a href='https://indparty.com'>indparty.com</a></p>
</div>";

            return new Email(subject, text, html);
        } 

        [FunctionName(nameof(RequestBallot))]
        public static async Task<IActionResult> RequestBallot(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(RequestBallot)}");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
            dynamic voter = JsonConvert.DeserializeObject(requestBody);
            log.LogInformation(requestBody);

            Email email = FormatEmail(voter);

            string subject = email.subject;
            string text = email.text;
            string html = email.html;
            string voterEmail = voter.email;
            string voterName = $"{voter.firstName} {voter.lastName}";
            log.LogInformation($"{nameof(RequestBallot)}: {voterEmail}");

            try { 
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("support@equal.vote", "IPO / STAR Voting");
                var to = new EmailAddress(voterEmail, voterName);
                log.LogInformation($"{nameof(RequestBallot)} Before CreateSingleEmail");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, text, html);
                log.LogInformation($"{nameof(RequestBallot)} Before SendEmailAsync");
                var responseMessage = await client.SendEmailAsync(msg).ConfigureAwait(false);
                log.LogInformation($"{nameof(RequestBallot)} Succeeded");
                return new OkObjectResult($"{nameof(RequestBallot)}: Succeeded");
            }
            catch (Exception ex)
            {
                log.LogError($"{nameof(RequestBallot)} FAILED", ex.Message, ex.GetType().Name, ex.StackTrace.Length, ex.InnerException.ToString());
                return new BadRequestObjectResult(ex.ToString());
            }
        }
    }
}
