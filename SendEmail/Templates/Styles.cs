using System;
using System.Collections.Generic;
using System.Text;

namespace StarApi.SendEmail.Templates
{
    public static class Styles
    {
        public const string VoterReceipt = @"
<meta charset=utf-8>
<style type='text/css'>
div.bigContainer {
  font-family: verdana, tahoma, helvetica, sans-serif;
  box-shadow: 4px 4px 4px #cccccc;
  border-radius: 10px;
  margin-top: 20px;
  background-color: #ffffff;
  width: 100%;
  max-width: 400px;
}
div.bigContainerTitle {
  font-size: 13pt;
  font-weight: bold;
  border: none;
  background-color: #e7e7e7;
  border-radius: 10px 10px 0px 0px;
  padding: 10px;
  margin-top: 10px;
}
div.bigContainerInner, #indexCreatePollContainer {
    border: solid 2px #e7e7e7;
    border-radius: 0px 0px 10px 10px;
    padding: 10px;
}
.clear {
    clear: both;
}
div.startEndString {
    float: left;
    font-size: 11pt;
    margin-bottom: 5px;
    border: solid 1px #38c;
    background-color: #e7e7e7;
    border-radius: 7px;
    padding: 5px;
    margin-bottom: 12px; //JJC
}
div#yourVoterID {
    float: left;
    padding-left: 3px;
    border-bottom: solid 1px #e7e7e7;
    margin-bottom: 10px;
    font-size: 10pt;
}
span#yourVoterIDActual {
    border-radius: 7px 7px 0px 0px;
    border: solid 1px #e7e7e7;
    background-color: #e7e7e7;
    padding: 0px 5px 0px 5px;
}
div#yourVoteTime {
    font-size: 12px;
    margin-bottom: 20px; //JJC
}
table {
    display: table;
    border-collapse: separate;
    border-spacing: 2px;
    border-color: grey;
    margin-top: 12px; //JJC
}
table.yourVoteTable {
    border-collapse: collapse;
    min-width: 300px; //JJC
}
th.pollHeader {
    text-align: center;
    background-color: #e7e7e7;
    border: solid 1px #e7e7e7;
}
th {
    font-size: 12px;
    text-align: left;
    display: table-cell;
    vertical-align: inherit;
    font-weight: bold;
    text-align: -internal-center;
}
td.number {
    font-family: monospace;
    text-align: right;
    font-size: 14pt;
}
td {
    padding: 4px; //JJC
}
.pollSubtext {
    font-size: 10pt;
}
</style>
";
        public const string SampleVote = @"
<div class='bigContainer'><div class='bigContainerTitle'>Your vote for 'IPO 2020 Primary'</div><div class='bigContainerInner'><div class='startEndString'>Ends: 2020-04-24 00:00:00</div><div class='clear'></div><div id='voteInput'><div id='yourVoterID'>Your voter ID: 
<span id='yourVoterIDActual'>2ydwrpwdr4</span></div><div class='clear'></div><div id='yourVoteTime'>Voted: 2020-04-20 16:43:40</div><table class='yourVoteTable'><tbody><tr><th colspan='3' class='pollHeader'>President</th></tr><tr><th>#</th><th>Option</th><th>Vote</th></tr><tr><td class='orderCell'>1</td><td>Joe Biden
<br><div class='pollSubtext'>Democrat</div></td><td class='number'>4</td></tr><tr><td class='orderCell'>2</td><td>Bernie Sanders
<br><div class='pollSubtext'>Democrat</div></td><td class='number'>5</td></tr><tr><td class='orderCell'>3</td><td>Donald Trump
<br><div class='pollSubtext'>Republican</div></td><td class='number'>2</td></tr></tbody></table><table class='yourVoteTable'><tbody><tr><th colspan='3' class='pollHeader'>Secretary of State</th></tr><tr><th>#</th><th>Option</th><th>Vote</th></tr><tr><td class='orderCell'>1</td><td>Shemia Fagan
<br><div class='pollSubtext'>Democrat</div></td><td class='number'>0</td></tr><tr><td class='orderCell'>2</td><td>Mark Hass
<br><div class='pollSubtext'>Democrat</div></td><td class='number'>0</td></tr><tr><td class='orderCell'>3</td><td>Jamie McLeod-Skinner
<br><div class='pollSubtext'>Democrat</div></td><td class='number'>0</td></tr><tr><td class='orderCell'>4</td><td>Ken Smith
<br><div class='pollSubtext'>Independent</div></td><td class='number'>3</td></tr><tr><td class='orderCell'>5</td><td>Kim Thatcher
<br><div class='pollSubtext'>Republican</div></td><td class='number'>0</td></tr><tr><td class='orderCell'>6</td><td>Armand 'Rich' Vial
<br><div class='pollSubtext'>Non-Affiliated</div></td><td class='number'>0</td></tr></tbody></table><table class='yourVoteTable'><tbody><tr><th colspan='3' class='pollHeader'>US Senate</th></tr><tr><th>#</th><th>Option</th><th>Vote</th></tr><tr><td class='orderCell'>1</td><td>Jeff Merkley
<br><div class='pollSubtext'>Democrat</div></td><td class='number'>5</td></tr><tr><td class='orderCell'>2</td><td>Nobody
<br><div class='pollSubtext'>N/A</div></td><td class='number'>0</td></tr></tbody></table><table class='yourVoteTable'><tbody><tr><th colspan='3' class='pollHeader'>Attorney General</th></tr><tr><th>#</th><th>Option</th><th>Vote</th></tr><tr><td class='orderCell'>1</td><td>Ellen Rosenblum
<br><div class='pollSubtext'>Democrat</div></td><td class='number'>5</td></tr><tr><td class='orderCell'>2</td><td>Nobody
<br><div class='pollSubtext'>N/A</div></td><td class='number'>0</td></tr></tbody></table><table class='yourVoteTable'><tbody><tr><th colspan='3' class='pollHeader'>State Treasurer</th></tr><tr><th>#</th><th>Option</th><th>Vote</th></tr><tr><td class='orderCell'>1</td><td>Jeff Gudman
<br><div class='pollSubtext'>Republican</div></td><td class='number'>0</td></tr><tr><td class='orderCell'>2</td><td>Chris Henry
<br><div class='pollSubtext'>Independent</div></td><td class='number'>5</td></tr><tr><td class='orderCell'>3</td><td>Tobias Read
<br><div class='pollSubtext'>Democrat</div></td><td class='number'>3</td></tr></tbody></table><img src='https://star.ipo.vote/web/images/qr_voterid/2ydwrpwdr4.png' alt='2ydwrpwdr4'></div></div></div>";
    }
}
