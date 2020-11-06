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
    public class TagsDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private BlogDbContext Blog = new BlogDbContext();

        //This Controller Will access the tags table of blog database.
        /// <summary>
        /// Returns a list of Tags in the system
        /// </summary>
        /// <example>GET api/TagsData/ListTags</example>
        /// <returns>
        /// A list of Tags 
        /// </returns>
        [HttpGet]
        public IEnumerable<string> ListTags()
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from tags";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Tag names
            List<String> Tags = new List<string> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                string tagname = ResultSet["tagname"] as string;
                //Add the Author Name to the List
                Tags.Add(tagname);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of tags
            return Tags;
        }

    }
}