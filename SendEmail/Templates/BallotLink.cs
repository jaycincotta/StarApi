
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

Thank you for registering to vote in the Independent Party of Oregon's 2020 Primary.

A link to your ballot is provided below. Please keep this email secure, as this link provides access to vote in this election in your name.

At the time you cast your vote, you will be asked to upload two supporting documents:

* A government-issued photo ID, such as a driver's license, a passport, or an ID card from any state.

* A recent photo with a clear view of your face that we can match to the face on your photo ID.

NOTE: For most people it is easiest to upload photos of your documents and vote using a smartphone, but you can also use a computer. If you are unable to upload the documents listed above, please see the note at the bottom of this email for more options.

Please cast your vote now, or as soon as possible.

ALL BALLOTS MUST BE SUBMITTED NO LATER THAN 8 PM PDT MAY 12th.

Thanks again for voting!

STAR Vote Elections - https://www.starvoting.us
The Independent Party of Oregon - https://www.indparty.com

*********************************************************
BALLOT FOR {voter.firstName} {voter.lastName}

{url}

*********************************************************

This primary will use STAR Voting.
https://www.starvoting.us/how_to_vote

STAR Voting eliminates vote splitting and the spoiler effect, ensuring that voters can vote their conscience, even in elections with large fields of candidates.

STAR Voting is simple. 5-Best, 0-Worst.

As a rule of thumb, it's a good idea to give your favorite or favorites a full five stars, give your least favorite candidate or candidates zero stars, and arrange the others in between. With STAR it's easy to show your preference order and also your level of support for each candidate.

It's safe to vote your conscience, and you don't have to worry about voting for the lesser of two evils' or wasting your vote. If your favorite can't win, your vote automatically goes to the finalist you prefer.

If this is your first election using STAR Voting, please take a moment to go over:

The How to Vote with STAR Voting Guide...
https://www.starvoting.us/how_to_vote

...and watch this VIDEO
https://www.youtube.com/watch?v=NuVSn2rAFVU&amp;list=PLdi1cwRPPnuJT7vsg8qwbO5vPFuO9yr2q

NOTES ON ADDITIONAL VOTER IDENTITY DOCUMENTATION

If you do not have a valid photo ID or can&rsquo;t upload a recent photo of yourself, you can still vote, but your ballot will be considered provisional pending verification.

To vote with a provisional ballot please submit the following documentation when you cast your ballot:

* Any photo ID which includes your date of birth. (This does not need to be government-issued or from Oregon.

* Another document confirming the address where you are currently registered to vote and your full name as spelled in your voter registration. (Acceptable examples include: a utility bill, bank statement, pay stub, government document, or voter registration card.)

Thank you in advance for your cooperation. We take election security very seriously.

In most cases, it's easiest to take these photos with your phone and to then vote with your phone by clicking the ballot link below. A webcam or digital camera can also be used. Images must be high resolution and taken with good lighting so that images are legible.

If the credentialing team cannot verify your identity from the information you provide, you will be contacted by email for additional documentation.

If you have any problems voting, you may contact our voter support team by emailing us at support@equal.vote or simply replying to this email.

*********************************************************
BALLOT FOR {voter.firstName} {voter.lastName}

{url}

*********************************************************
";

        // HTML EMAIL
        public override string Html => $@"
<div style='max-width:800px;'>
<p>Dear {voter.firstName},</p>

<p>Thank you for registering to vote in the Independent Party of Oregon&rsquo;s 2020 Primary.
   A link to your ballot is provided below. Please keep this email secure, as this link provides
   access to vote in this election in your name.</p>

<p>At the time you cast your vote, you will be asked to upload two supporting documents:</p>

<ul>
<li>A government-issued photo ID, such as a driver's license, a passport, or an ID card from any state.</li>
<li>A recent photo with a clear view of your face that we can match to the face on your photo ID.</li>
</ul>

<p><strong>NOTE: For most people it is easiest to upload photos of your documents and vote using
   a smartphone, but you can also use a computer. If you are unable to upload the documents listed above,
   please see the note at the bottom of this email for more options.</strong></p>

<p>Please cast your vote now, or as soon as possible.</p>

<p><strong>ALL BALLOTS MUST BE SUBMITTED NO LATER THAN 8&nbsp;PM&nbsp;PDT&nbsp;MAY&nbsp;12<sup>th</sup>.</strong></p>

<p>Thanks again for voting!</p>

<p><strong>STAR Vote Elections</strong> - <a href='https://www.starvoting.us'>starvoting.us</a><br/>
<strong>The Independent Party of Oregon</strong> - <a href='https://www.indparty.com'>indparty.com</a></p>

<hr />
<h1><a href='{url}'>CLICK HERE TO CAST YOUR BALLOT</h1>
<hr />

<p>This primary will use <a href='https://www.starvoting.us/how_to_vote'>STAR Voting</a>.</p>

<p>STAR Voting eliminates vote splitting and the spoiler effect, 
   ensuring that voters can vote their conscience, even in elections with large fields of candidates.</p>

<div style='width:100%;max-width:400px;margin-top:24px;margin-bottom:24px;'>
<a href='https://www.starvoting.us'><img src='https://register.ipo.vote/star.png' 
width='100%' max-width:'400px' /></a>
</div>

<h2>STAR Voting is simple. 5-Best, 0-Worst.</h2>

<p>As a rule of thumb, it's a good idea to give your favorite or favorites a full five stars,
   give your least favorite candidate or candidates zero stars, and arrange the others in between.
   With STAR it's easy to show your preference order and also your level of support for each candidate.</p>

<p>It's safe to vote your conscience, and you don't have to worry about voting for the 
   'lesser of two evils' or wasting your vote. If your favorite can't win,
   your vote automatically goes to the finalist you prefer.</p>

<p>If this is your first election using STAR Voting, please take a moment to go over the
   <a href='https://www.starvoting.us/how_to_vote'>&ldquo;How to vote with STAR Voting&rdquo;</a>
   guide and watch this <a href='https://www.youtube.com/watch?v=NuVSn2rAFVU&amp;list=PLdi1cwRPPnuJT7vsg8qwbO5vPFuO9yr2q'>video</a>.</p>

<h2>NOTES ON ADDITIONAL VOTER IDENTITY DOCUMENTATION</h2>

<p>If you do not have a valid photo ID or can&rsquo;t upload a recent photo of yourself,
   you can still vote, but your ballot will be considered provisional pending verification.</p>

<p><strong>To vote with a provisional ballot please submit the following documentation when you cast your ballot:</strong></p>

<ul>
<li>Any photo ID which includes your date of birth. (This does not need to be government-issued or from Oregon.)<br/><br/></li>

<li>Another document confirming the address where you are currently registered to vote and your full name
    as spelled in your voter registration. (Acceptable examples include: a utility bill, bank statement,
    pay stub, government document, or voter registration card.)</li>
</ul>

<p>Thank you in advance for your cooperation. We take election security very seriously.&nbsp;</p>

<p><strong>In most cases, it's easiest to take these photos with your phone and to then vote with your phone
   by clicking the ballot link below. A webcam or digital camera can also be used.
   Images must be high resolution and taken with good lighting so that images are legible.</strong>&nbsp;</p>

<p>If the credentialing team cannot verify your identity from the information you provide,
   you will be contacted by email for additional documentation.</p>

<p>If you have any problems voting, you may contact our voter support team by emailing us at
   <a href='{mailto}'>support@equal.vote</a> or simply replying to this email.</p>

<hr />
<h1><a href='{url}'>CLICK HERE TO CAST YOUR BALLOT</h1>
<hr />
</div>
";

        public BallotLink(dynamic fields)
        {
            this.voter = fields;
        }
    }
}
