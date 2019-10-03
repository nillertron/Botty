using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Botty
{
    public class Poll : ModuleBase<SocketCommandContext>
    {
        public Polls newPoll = Polls.newPoll;


        //Starter en poll (sætter newPollStarted = true, så vi kan hoppe ned i vores næste metode)
        [Command("StartPoll")]
        public async Task StartPoll()
        {
            //tjekker om der er aktiv poll i db
            TjekActive();

            if (newPoll.newPollStarted || newPoll.PollActive)
            {
                await Context.Channel.SendMessageAsync("A poll is active already!");
            }
            else
            {
                //sætter newpoll til startet så vi kan komme videre i med at oprette en poll
                newPoll.newPollStarted = true;
                await Context.Channel.SendMessageAsync("Poll started! Type !PollQ QUESTION TO ASK IN POLL");
            }


        }


        //tilføjer question til databasen
        [Command("PollQ")]
        public async Task OpretPollQ([Remainder]string pollQ)
        {
            //Tjek om der er startet ny poll
            if (!newPoll.newPollStarted)
            {
                await Context.Channel.SendMessageAsync("No new poll is started");
            }
            else//Hvis ny poll er startet indsætter vi spørgsmålet i databasen og stter poll add answers til true / newPollStarted til false
            {
                DbConnect db = new DbConnect();
                await db.indsaetPollQ(pollQ);
                await Context.Channel.SendMessageAsync("Question registered, start adding answer options with !PollA ");
                newPoll.newPollStarted = false;
                newPoll.PollAddA = true;
            }
        }
        //Skaber linjer til listen med svar
        [Command("PollA")]
        public async Task OpretPollA([Remainder]string pollA)
        {
            if (!newPoll.PollAddA)
            {
                await Context.Channel.SendMessageAsync("Create a poll question first or wait for current poll to finish!");
            }
            else
            {


                DbConnect db = new DbConnect();
                int id = db.getPollId();
                newPoll.poll.Add(new PollAnswers(id, pollA));
                await Context.Channel.SendMessageAsync("Answer registered, continue adding answers or post the poll !PollPost ");

            }
        }
        // Uploader pollens svar til db
        [Command("PollPost")]
        public async Task PollPost()
        {
            if (!newPoll.PollAddA)
            {
                await Context.Channel.SendMessageAsync("Create a poll question first or wait for current poll to finish!");
            }
            else
            {
                DbConnect db = new DbConnect();
                await db.indsaetPollA(newPoll.poll);

                await Context.Channel.SendMessageAsync("Poll is active! ");
                newPoll.poll.Clear();
                newPoll.PollAddA = false;
                newPoll.PollActive = true;
            }
        }
        //Udskriver den nuværende Poll
        [Command("ActivePoll")]
        public async Task PollActive()
        {
            TjekActive();

            if (newPoll.PollActive)
            {
                List<PollAnswersFromDb> pollAnswer = new List<PollAnswersFromDb>();

                DbConnect db = new DbConnect();
                var question = "";
                question = db.GetQuestion(question);
                pollAnswer = db.GetAnswer(pollAnswer);
                await Context.Channel.SendMessageAsync("***" + question + "***");
                await Context.Channel.SendMessageAsync("-------------");

                foreach (var answer in pollAnswer)
                {
                    await Context.Channel.SendMessageAsync($"Valg :***{answer.Nummer}*** \n {answer.Answer} - ***{answer.Votes}***\n-------------");


                }
                await Context.Channel.SendMessageAsync("Add your !Vote now :)");

            }
            else
            {
                await Context.Channel.SendMessageAsync("No poll active, make one by typing !Startpoll");
            }
        }
        [Command("vote")]
        public async Task Vote(int vote)
        {//videre herfra
            var user = Context.User.Username.ToString();

            bool harStemt = TjekUser(user);
            TjekActive();
            if (harStemt)
            {
                await Context.Channel.SendMessageAsync(user + " you have already voted, shame on you!");

            }

            else if (newPoll.PollActive)
            {
                DbConnect db = new DbConnect();
                int qnr = db.getPollId();
                await db.indsaetVote(qnr, user, vote);
                await Context.Channel.SendMessageAsync("You voted on number " + vote + " gj !");



            }
            else
            {
                await Context.Channel.SendMessageAsync("No poll active");
            }
        }
        [Command("StopPoll")]
        public async Task StopPoll()
        {
            if (newPoll.PollActive)
            {
                DbConnect db = new DbConnect();
                await db.StopPoll();
                newPoll.PollActive = false;
                await Context.Channel.SendMessageAsync("Poll stopped");
            }
            else
            {
                await Context.Channel.SendMessageAsync("No poll to stop");
            }
        }

        private void TjekActive()
        {
            //Tjek om der er en poll active i db:
            DbConnect db = new DbConnect();
            int i = 0;


            i = db.getActive();

            if (i > 0)
            {
                newPoll.PollActive = true;

            }


        }
        private bool TjekUser(string user)
        {
            DbConnect db = new DbConnect();
            string testUser = "";
            testUser = db.CheckUser(user);




            //findes brugeren return true
            if (testUser == user)
            {
                return true;
            }//ellers return false
            else
            {
                return false;
            }
        }
    }



}

public class Polls
{


    public bool newPollStarted { get; set; } = false;
    public bool PollAddA { get; set; } = false;
    public bool PollActive { get; set; } = false;
    //En instaniseret liste til at gemme svar i
    public List<PollAnswers> poll = new List<PollAnswers>();


    public static Polls newPoll { get; } = new Polls();
    public Polls()
    {
    }

}
//Gemmer i listen til DB
public class PollAnswers
{
    public int QId { get; }
    public string Answer { get; }


    public PollAnswers(int qId, string answer)
    {
        QId = qId;
        Answer = answer;

    }
}
//Skal bruges til listen fra DB (var lige nemmere end at ændre koden :D)
public class PollAnswersFromDb
{
    public int AId { get; }
    public string Answer { get; }

    public int Nummer { get; }
    public int Votes { get; }

    public PollAnswersFromDb(int aId, string answer, int nummer, int votes)
    {
        AId = aId;
        Answer = answer;
        Nummer = nummer;
        Votes = votes;

    }
}

