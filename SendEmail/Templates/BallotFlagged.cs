
using Newtonsoft.Json;

namespace StarApi.SendEmail.Templates
{
    public class Reason
    {
        public string Code { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
    }
    public class BallotFlagged : EmailTemplate
    {
        const string labelStyle = "style='width:100px;text-align:right;padding-right:8px;font-style:italic;padding-bottom:8px;'";
        const string valueStyle = "style='font-weight:bold;padding-bottom:8px;'";
        public override string TemplateName => "ballotFlagged";

        public override string ToName => "IPO Voter Support";

        public override string ToEmail => "support@equal.vote";

        // EMAIL SUBJECT
        public override string Subject => $"Flagged: {Reason.Caption} - {(Reason.Code == "duplicate" ? VoterId : StarId)} from {VoterEmail}";

        // PLAIN TEXT EMAIL
        public override string Text => $@"{Reason.Description}

StarId: {StarId}
Name: {FirstName} {LastName}
Email: {VoterEmail}
Phone: {VoterPhone}

View Ballot Information
{ReturnLink}
";

        // HTML EMAIL
        //public override string Html => $@"<!DOCTYPE HTML><html><head>{styles}</head><body>
        public override string Html => $@"
<h2>{Reason.Description}</h2>
<table><tbody>
<tr><td {labelStyle}>StarId:</td><td {valueStyle}>{StarId}</td></tr>
<tr><td {labelStyle}>VoterId:</td><td {valueStyle}>{VoterId}</td></tr>
<tr><td {labelStyle}>Name:</td><td {valueStyle}>{FirstName} {LastName}</td></tr>
<tr><td {labelStyle}>Email:</td><td {valueStyle}>{VoterEmail}</td></tr>
<tr><td {labelStyle}>Phone:</td><td {valueStyle}>{VoterPhone}</td></tr>
</tbody></table>
<p><a href='{ReturnLink}' target='tier2'>View Ballot Information</a></p>
";
        private Reason Reason { get; set; }
        private string StarId { get; set; }
        private string VoterId { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string VoterEmail { get; set; }
        private string VoterPhone { get; set; }
        private string ReturnLink { get; set; }

        public BallotFlagged(dynamic dynamicObject)
        {
            string json = JsonConvert.SerializeObject(dynamicObject);
            BallotFlaggedFields fields = JsonConvert.DeserializeObject<BallotFlaggedFields>(json);

            // Validate input
            if (string.IsNullOrWhiteSpace(fields.reason)) throw new System.InvalidOperationException($"Missing expected field: reason");
            Reason = Lookup(fields.reason);
            if (Reason == null) throw new System.InvalidOperationException($"Unknown reason: {fields.reason}");
            StarId = string.IsNullOrWhiteSpace(fields.starId) ? "UNKNOWN" : fields.starId.ToString();
            VoterId = string.IsNullOrWhiteSpace(fields.voterId) ? "UNKNOWN" : fields.voterId.ToString();
            FirstName = string.IsNullOrWhiteSpace(fields.firstName) ? "" : fields.firstName.ToString();
            LastName = string.IsNullOrWhiteSpace(fields.lastName) ? "" : fields.lastName.ToString();
            VoterEmail = string.IsNullOrWhiteSpace(fields.email) ? "" : fields.email.ToString();
            VoterPhone = string.IsNullOrWhiteSpace(fields.phone) ? "" : fields.phone.ToString();
            ReturnLink = string.IsNullOrWhiteSpace(fields.returnLink) ? "" : fields.returnLink.ToString();
        }

        public static Reason Lookup(string code)
        {
            switch (code.ToLower())
            {
                case "illegible":
                    return new Reason
                    {
                        Code = "illegible",
                        Caption = "Credential Illegible",
                        Description = "Can’t read name, date of birth, or address."
                    };
                case "mismatch":
                    return new Reason
                    {
                        Code = "mismatch",
                        Caption = "Data Mismatch",
                        Description = "Name, date of birth, or address don’t match voter file info listed."
                    };
                case "provisional":
                    return new Reason
                    {
                        Code = "provisional",
                        Caption = "Ballot Provisional",
                        Description = "Does not contain required ID or photo."
                    };
                case "duplicate":
                    return new Reason
                    {
                        Code = "duplicate",
                        Caption = "Duplicate Ballot",
                        Description = "Duplicate ballots submitted with the same voter ID."
                    };
                case "notfound":
                    return new Reason
                    {
                        Code = "notFound",
                        Caption = "Voter Not Found",
                        Description = "Birthdate provided by voter does not match My Star."
                    };

                default:
                    return null;

            }
        }
    }
}
