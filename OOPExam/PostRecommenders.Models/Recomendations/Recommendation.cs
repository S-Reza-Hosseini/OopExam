using PostRecommenders.Models.Bank;

namespace PostRecommenders.Models.Recomendations;
using PostRecommenders.Models.Posts;

class Recommendation
{
    public Recommendation(Post post, Follower follower)
    {
        Post = post;
        Follower = follower;
    }
    public Post Post { get; set; }
    public Follower Follower { get; set; }
}