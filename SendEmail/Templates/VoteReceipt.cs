
using System.Text;

namespace StarApi.SendEmail.Templates
{
    public class VoteReceipt : EmailTemplate
    {
        private dynamic voter { get; set; }

        private string styles = Styles.VoterReceipt;
        private string ballotHtml { get; set; }
        public override string TemplateName => "voteReceipt";

        public override string ToName => $"{voter.firstName} {voter.lastName}";

        public override string ToEmail => voter.email;

        // EMAIL SUBJECT
        public override string Subject => $"CONFIDENTIAL: IPO Primary Vote Receipt for {voter.firstName} {voter.lastName}";

        // PLAIN TEXT EMAIL
        public override string Text => $@"Dear {voter.firstName},

Thank you for voting in the Independent Party of Oregon’s 2020 Primary.

If you have any problems voting, you can contact our voter support team by emailing us at
support@equal.vote or simply replying to this email.

Thanks again for voting!

The Equal Vote Coalition - https://equal.vote
The Independent Party of Oregon - https://indparty.com
";

        // HTML EMAIL
        //public override string Html => $@"<!DOCTYPE HTML><html><head>{styles}</head><body>
        public override string Html => $@"
<div style='max-width:800px;'><html><head>{styles}</head><body>
<p>Dear {voter.firstName},</p>

<p>Thank you for voting in the 2020 Independent Party of Oregon (IPO) primary.
   Your ballot has been received and is pending verification of your voter identity documentation.</p>

<p>Please print and save this email for auditing purposes. Vote receipt below.</p>

<p>Thanks again for voting!</p>

<p><strong>STAR Vote Elections</strong> - <a href='https://www.starvoting.us'>starvoting.us</a><br/>
<strong>The Independent Party of Oregon</strong> - <a href='https://www.indparty.com'>indparty.com</a></p>

<hr/>
<div style='margin-top=20px;margin-bottom:8px;'>
	<a href='http://starvoting.us/donate'>
        <input type='button' value='Donate to the STAR Elections IPO Primary fund' />
	</a>
</div>
<a href = 'https://www.youtube.com/watch?v=NuVSn2rAFVU&list=PLdi1cwRPPnuJT7vsg8qwbO5vPFuO9yr2q&index=2' >
    <img src='https://register.ipo.vote/simpsons.png' width='400px' alt='STAR: It beats the alternative!' />
</a>

<p>In Oregon, minor parties like the IPO do not qualify for any state funding or support in officiating their primary elections.
The Independent Party of Oregon has partnered with the STAR Elections team, who has generously donated their services at no cost.
Please click the link here to donate to the STAR Elections IPO Primary fund. 
100% of donations collected from this project will go to furthering the adoption of STAR Voting in governmental elections.</p>

<p><a href = 'http://star.vote'>Use STAR Voting for your own polling or elections</a></p>
<p><a href = 'http://starvoting.us'>Learn more about STAR Voting</a></p>

<div style='padding-bottom:16px;'>
	<a href='http://starvoting.us/donate'>
        <input type='button' value='Donate to the STAR Elections IPO Primary fund' />
	</a>
</div>
<hr/>
{ballotHtml}
</div></body></html>
";
        
        public VoteReceipt(dynamic fields)
        {
            voter = fields;
            string encodedHtml = fields.ballotHtml;
            byte[] htmlBytes = System.Convert.FromBase64String(encodedHtml);
            ballotHtml = Encoding.ASCII.GetString(htmlBytes);
        }
    }
}
