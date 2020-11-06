using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogProject.Models;
using MySql.Data.MySqlClient;

namespace BlogProject.Controllers
{
    public class ArticleDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private BlogDbContext Blog = new BlogDbContext();

        //This Controller Will access the article table of blog database.
        /// <summary>
        /// Returns a list of Articles in the system
        /// </summary>
        /// <example>GET api/ArticleData/ListArticles</example>
        /// <returns>
        /// A list of Articles (article title)
        /// </returns>
        [HttpGet]
        public IEnumerable<string> ListArticles()
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Articles";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Article Titles
            List<String> ArticleTitles = new List<string> { };            

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                string ArticleTitle = ResultSet["articletitle"] as string;
                //Add the Author Name to the List
                ArticleTitles.Add(ArticleTitle);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of articles
            return ArticleTitles;
        }

    }
}