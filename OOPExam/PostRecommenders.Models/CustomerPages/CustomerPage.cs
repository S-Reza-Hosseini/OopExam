using PostRecommender.Contracts;
using PostRecommenders.Models.Posts;

namespace PostRecommenders.Models.CustomerPages;

class CustomerPage
{
    public CustomerPage(string title, PageType pageType, int followerCount)
    {
        Title = title;
        Type = pageType;
        FollowerCount = followerCount;
    }

    public decimal Wallet { get; set; } = 1000;

    public PageType Type { get; set; }

    public int FollowerCount { get; set; }

    public string Title { get; set; }
    

    public List<Post> Posts = new();

    public void AddPost(Post post)
    {
        Posts.Add(post);
    }

}