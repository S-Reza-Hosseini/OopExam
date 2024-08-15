using PostRecommender.Contracts;

namespace PostRecommenders.Models.Bank;
using PostRecommenders.Models.Posts;

class Follower
{
    public Follower(string address,string pageAddress)
    {
        Address = address;
        PageAddress = pageAddress;
    }

    public string PageAddress { get; set; }
    public string Address { get;}
    public PageType InterestedPageType { get; set; }
    
    public readonly HashSet<string> InterestedTags = new();

    public void Like(PageType interstedPageType,List<string> tags )
    {
        InterestedPageType = interstedPageType;
        InterestedTags.UnionWith(tags);
    }
}