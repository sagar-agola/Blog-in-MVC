using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class DataModel
    {
        public List<Temp> GetPosts
        {
            get
            {
                string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

                List<Temp> post = new List<Temp>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spGetposts", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Temp temp = new Temp()
                        {
                            PostID = int.Parse(reader["PostID"].ToString()),
                            PostName = reader["Name"].ToString(),
                            PostTime = DateTime.Parse(reader["Date"].ToString()),
                            PostDetails = reader["Details"].ToString()
                        };
                        CommentsModel commentsModel = new CommentsModel ();
                        List<Comment> cmt = commentsModel.GetCommentsByID (temp.PostID);

                        temp.CommentsList = cmt;
                        post.Add(temp);
                    }
                }
                return post;
            }
        }
        public void CreatePost(Post post)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con =new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAddPost", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramName    = new SqlParameter("@Name", post.Name);
                SqlParameter paramDate    = new SqlParameter("@Date", post.Time);
                SqlParameter paramDetail = new SqlParameter("@Detail", post.Detail);

                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramDate);
                cmd.Parameters.Add(paramDetail);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeletePost(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spDeletePost", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramID = new SqlParameter("@ID", id);
                cmd.Parameters.Add(paramID);

                con.Open();
                cmd.ExecuteNonQuery();
                SqlCommand command = new SqlCommand ("spDeleteComment", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter parameterID = new SqlParameter ("@paramID", id);
                command.Parameters.Add (parameterID);

                command.ExecuteNonQuery ();
            }
        }
        public void EditPost(Post post)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spEditPost", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramID     = new SqlParameter("@ID", post.ID);
                SqlParameter paramName   = new SqlParameter("@Name", post.Name);
                SqlParameter paramDate   = new SqlParameter("@Date", post.Time);
                SqlParameter paramDetail = new SqlParameter("@Detail", post.Detail);


                cmd.Parameters.Add(paramID);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramDate);
                cmd.Parameters.Add(paramDetail);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public Post GetPostDetailByID(int ID)
        {
            Post postDetail = new Post();

            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetPostDetailsByID", con)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                SqlParameter paramID = new SqlParameter("@ID", ID);
                cmd.Parameters.Add(paramID);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    postDetail.ID = int.Parse(reader["PostID"].ToString());
                    postDetail.Name = reader["Name"].ToString();
                    postDetail.Time = DateTime.Parse(reader["Date"].ToString());
                    postDetail.Detail = reader["Details"].ToString();
                }
            }
            return postDetail;
        }
    }
}