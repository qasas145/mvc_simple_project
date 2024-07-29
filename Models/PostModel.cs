namespace firstApp.Models;


public class Post
{   
    public  int Id { get; set; }

    public String Content{get;set;}
    public  String Image{get;set;}
    public  int Likes{get;set;}
    public List<Comment> comments = new List<Comment>();
    public  int Shares{get;set;}

}
