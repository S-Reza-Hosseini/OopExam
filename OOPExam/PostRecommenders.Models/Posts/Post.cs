namespace PostRecommenders.Models.Posts;

class Post
{
   public Post(string url, int likeCount, List<string> hashtag)
   {
      Url = url;
      LikeCount = likeCount;
      Tags.Union(hashtag);
   }
   public HashSet<string> Tags = new();
   public string Url { get; set; }
   public int LikeCount { get; set; }

   
}