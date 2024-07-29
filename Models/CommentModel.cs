namespace firstApp.Models;


public class Comment {
    public int Id{get;set;}
    public String comment_content{get;set;}
    // Foreign key for the relationship
    public int PId { get; set; }

    // Navigation property for the relationship
    public Post Post { get; set; }
}
