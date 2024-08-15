using PostRecommender.Contracts;
using PostRecommenders.Models.Bank;
using PostRecommenders.Models.Recomendations;
using PostRecommenders.Models.CustomerPages;
using PostRecommenders.Models.Posts;

namespace PostRecommenders.Models.InstagramPageServise;

public class Manage : IInstagramPageService
{
    private readonly List<CustomerPage> _customerPages = new();
    private readonly List<Follower> _folowers = new();
    private readonly List<Recommendation> _recommendation = new();
    
    public void RegisterCustomerPage(RegisterCustomerPageDto registerCustomerPageDto)
    {
        var customerPage = new CustomerPage(
             registerCustomerPageDto.Title,
            registerCustomerPageDto.PageType,
            registerCustomerPageDto.FollowerCount
        );

        if (customerPage.FollowerCount < 10)
        {
            throw new Exception("each page must be at least 10 follower count ");
        }
        _customerPages.Add(customerPage);
        
    }

    public List<ShowCustomerPageDto> ShowCustomersPage()
    {
        return _customerPages.Select((customerPage, index) => new ShowCustomerPageDto()
        {
        Id = index+1,
        Title = customerPage.Title,
        PageType = customerPage.Type,
        FollowerCount = customerPage.FollowerCount,
        WalletBalance = customerPage.Wallet
        }).ToList();
    }

    public void UpdateCustomerFollowerCount(UpdateFollowerCountDto updateFollowerCountDto)
    {
        var customerPage = _customerPages[updateFollowerCountDto.CustomerId -1];
        customerPage.FollowerCount = updateFollowerCountDto.NewFollowerCount;
    }

    public void RechargeCustomerWallet(WalletRechargeDto walletRechargeDto)
    {
        var customerPage = _customerPages[walletRechargeDto.CustomerId -1];
        customerPage.Wallet = walletRechargeDto.Amount;
    }

    public void RegisterFollower(RegisterFollowerDto registerFollowerDto)
    {
        var follower = new Follower(
            registerFollowerDto.Title,
            registerFollowerDto.PageAddress
        );
        
        _folowers.Add(follower);
    }

    public List<ShowFollowerDto> ShowFollowers()
    {
        return _folowers.Select((folower, index) => new ShowFollowerDto()
        {
            FollowerId = index+1,
            Title = folower.PageAddress,
            PageAddress = folower.PageAddress
        }).ToList();
    }

    public void RegisterFollowerLikedPost(RegisterFollowerLikedPostDto registerFollowerLikedPostDto)
    {
        var folower = _folowers[registerFollowerLikedPostDto.FollowerId-1];
        folower.Like(
            registerFollowerLikedPostDto.LikedPageType,
            registerFollowerLikedPostDto.PostHashtags);

    }

    public void RegisterCustomerPost(RegisterCustomerPostDto registerCustomerPostDto)
    {
        var customer = _customerPages[registerCustomerPostDto.CustomerId - 1];

        var post = new Post(
            registerCustomerPostDto.PostAddress,
            registerCustomerPostDto.LikeCount,
            registerCustomerPostDto.Hashtags 
        );
        if (post.LikeCount < 5 && registerCustomerPostDto.Hashtags.Count == 0)
        {
            throw new Exception("cant add this !! count Like very lowing");
        }
        customer.AddPost(post);
        
    }

    public void RecommendCustomerPosts(RecommendCustomerPostsDto recommendCustomerPostsDto)
    {
        var customer = _customerPages[recommendCustomerPostsDto.CustomerId - 1];

        _folowers.ForEach(follower =>
        {
            if (follower.InterestedPageType != customer.Type) return;
            var posts = customer.Posts.Where(p => p.Tags.Any(c =>
                follower.InterestedTags.Contains(c))).ToList();
            posts.ForEach(post =>
            {

                _recommendation.Add(new Recommendation(post, follower));
            });
        });
        
            customer = _customerPages[recommendCustomerPostsDto.CustomerId - 1];

        _folowers.ForEach(follower =>
        {
            if (follower.InterestedPageType != customer.Type) return;
            var posts = customer.Posts.Where(p =>
                p.Tags.Any(c => follower.InterestedTags.Contains(c))).ToList();
            posts.ForEach(post => 
            {
                _recommendation.Add(new Recommendation(post, follower)) ;
            });
        });
    }

    public List<RecommendationDto> ShowCustomerRecommendations(RecommendationRequestDto recommendationRequestDto)
    {
        _customerPages[recommendationRequestDto.CustomerId -1 ].Posts.Select(_ => new RecommendationDto()
        {
            Hashtags = _.Tags
                
        }).ToList();
    }

    public ShowTotalIncomeDto ShowTotalIncome()
    {
        throw new NotImplementedException();
    }
}