using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class CommentsModel
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public List<Comment> GetCommentsByID(int id)
        {
            List<Comment> comments = new List<Comment>();

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetComments", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramID = new SqlParameter("@ID", id);
                cmd.Parameters.Add(paramID);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Comment comment = new Comment()
                    {
                        UserName = reader["UserName"].ToString(),
                        CommentBody = reader["CommentBody"].ToString(),
                        Time = DateTime.Parse(reader["Time"].ToString())
                    };
                    comments.Add(comment);
                }
            }
            if(comments.Count ==0)
            {
                comments.Add (new Comment());
            }
            return comments;
        }
        public void AddComment(Comment comment)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAddComment", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramUserName = new SqlParameter("@UserName", comment.UserName);
                SqlParameter paramCommentBody = new SqlParameter("@CommentBody", comment.CommentBody);
                SqlParameter paramTime = new SqlParameter("@Time", comment.Time);
                SqlParameter paramPostID = new SqlParameter("@PostID", comment.PostId);

                cmd.Parameters.Add(paramUserName);
                cmd.Parameters.Add(paramCommentBody);
                cmd.Parameters.Add(paramTime);
                cmd.Parameters.Add(paramPostID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteComment(int id)
        {
            using (SqlConnection con = new SqlConnection (CS))
            {
                SqlCommand cmd = new SqlCommand ("spDeletePerticularComment", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter paramID = new SqlParameter ("@ID", id);
                cmd.Parameters.Add (paramID);

                con.Open ();
                cmd.ExecuteNonQuery ();
            }
        }
    }
}