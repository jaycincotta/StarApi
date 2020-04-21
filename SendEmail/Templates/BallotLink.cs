
namespace StarApi.SendEmail.Templates
{
    public class BallotLink : EmailTemplate
    {
        private dynamic voter { get; set; }
        private string url => $"https://star.ipo.vote/2020primary/{voter.starId}";
        string mailto => $"mailto:support@equal.vote?subject={mailtoSubject}";
        private string mailtoSubject => $"RE:%20Ballot%20{voter.starId}";


        public override string TemplateName => "BallotLink";

        public override string ToName => $"{voter.firstName} {voter.lastName}";

        public override string ToEmail => voter.email;

        // EMAIL SUBJECT
        public override string Subject => $"CONFIDENTIAL: IPO Primary Ballot for {voter.firstName} {voter.lastName}";

        // PLAIN TEXT EMAIL
        public override string Text => $@"Dear {voter.firstName},

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

        // HTML EMAIL
        public override string Html => $@"<div style='max-width:800px;'><p>Dear {voter.firstName},</p>
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

        public BallotLink(dynamic fields)
        {
            this.voter = fields;
        }
    }
}
