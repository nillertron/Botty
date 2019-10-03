using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Data.SqlClient;
using Botty.DBCommands;



namespace Botty
{
    public class DbConnect : ModuleBase<SocketCommandContext>
    {
        const string connectionString = "Data Source=TOKEN;Initial Catalog=Botty;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        #region Poll db
        public async Task test()
        {
            dbObtainer db = new dbObtainer();
            
        }
        //Indsæt poll question
        public async Task indsaetPollQ(string PollQ)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("Insert into [Question] ([Q], [IsActive]) values ('" + PollQ + "', '1')"))
                        {
                            command.Connection = conn;
                            command.ExecuteNonQuery();

                        }

                        conn.Close();

                    }
                });
            }
            catch (SqlException e)
            {
                await Context.Channel.SendMessageAsync(e.ToString());

            }
        }
        //Indsætter til Answer i database
        public async Task indsaetPollA(List<PollAnswers> poll)
        {
            int i = 1;
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        foreach (var pollAnswer in poll)
                        {
                            using (SqlCommand command = new SqlCommand("Insert into [Answer] ([A], [votes], [Qnum], [Number]) values ('" + pollAnswer.Answer + "', '0', '" + pollAnswer.QId + "','" + i + "')"))
                            {
                                command.Connection = conn;
                                command.ExecuteNonQuery();
                                i++;
                            }
                        }
                        conn.Close();

                    }
                });
            }
            catch (SqlException e)
            {
                await Context.Channel.SendMessageAsync(e.ToString());

            }
        }
        //Henter nuværende aktive pollid
        public int getPollId()
        {
            int id = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataReader dataReader;


                    using (SqlCommand command = new SqlCommand("Select Qnum from question where IsActive = '1'", conn))
                    {
                        dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            id = ((int)dataReader.GetValue(0));
                        }

                        conn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Context.Channel.SendMessageAsync(e.ToString());

            }

            return id;

        }
        public int getActive()
        {
            int id = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataReader dataReader;


                    using (SqlCommand command = new SqlCommand("Select IsActive from question where IsActive = '1'", conn))
                    {
                        dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            id = (Convert.ToInt32(dataReader.GetValue(0)));
                        }


                    }
                }

            }
            catch (SqlException e)
            {
                Context.Channel.SendMessageAsync(e.ToString());

            }
            return id;

        }
        public string CheckUser(string user)
        {
            int qId = getPollId();
            var testuser = "";

            try
            {
                //Først henter Qnum for at identificere hvilket spørgsmål
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataReader dataReader;

                    using (SqlCommand command = new SqlCommand("Select distinct [user] from vote where Qnum='" + qId + "' and [user]='" + user + "'", conn))
                    {
                        dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            testuser = (dataReader.GetValue(0).ToString());
                        }
                        conn.Close();
                    }
                }
                return testuser;
            }
            catch (SqlException e)
            {
                Context.Channel.SendMessageAsync(e.ToString());
                return e.ToString();
            }





        }

        public string GetQuestion(string question)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataReader dataReader;


                    using (SqlCommand command = new SqlCommand("Select Q from question where IsActive = '1'", conn))
                    {
                        dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            return question = dataReader.GetValue(0).ToString();
                        }


                    }
                }
            }

            catch (SqlException e)
            {
                Context.Channel.SendMessageAsync(e.ToString());

            }

            return question;


        }
        public List<PollAnswersFromDb> GetAnswer(List<PollAnswersFromDb> poll)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataReader dataReader;




                    using (SqlCommand command = new SqlCommand("Select Anum, A, Number, Votes from Answer " +
                        "inner join question on question.Qnum = Answer.Qnum AND question.IsActive = 1 ", conn))
                    {
                        dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            poll.Add(new PollAnswersFromDb(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(),
                                Convert.ToInt32(dataReader.GetValue(2)), Convert.ToInt32(dataReader.GetValue(3))));
                        }


                    }
                }
            }

            catch (SqlException e)
            {
                Context.Channel.SendMessageAsync(e.ToString());

            }



            return poll;
        }
        public async Task indsaetVote(int qnum, string user, int anum)
        {

            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand("Insert into [Vote] ([Qnum], [user], [anum]) " +
                            "values " +
                            "('" + qnum + "', '" + user + "', '" + anum + "')" +
                            "update answer SET votes = votes + 1 where qnum = '" + qnum + "' AND number = '" + anum + "'"))
                        {
                            command.Connection = conn;
                            command.ExecuteNonQuery();

                        }

                        conn.Close();

                    }
                });
            }
            catch (SqlException e)
            {
                await Context.Channel.SendMessageAsync(e.ToString());

            }
        }

        public async Task StopPoll()
        {
            await Task.Run(() =>
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("Update Question set IsActive = '0' ", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }

            });
        }

        #endregion
    }
}
