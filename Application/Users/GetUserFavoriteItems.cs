using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class GetUserFavoriteItems
    {
        public class GetFavoriteUserItems : IRequest<Hashtable>
        {
            public Guid UserId { get; set; }
        }

        public class GetFavoriteUserItemsHandler : IRequestHandler<GetFavoriteUserItems, Hashtable>
        {
            private readonly GNBCContext _context;

            public GetFavoriteUserItemsHandler(GNBCContext context)
            {
                _context = context;
            }

                public async Task<Hashtable> Handle(GetFavoriteUserItems request, CancellationToken cancellationToken)
                {
                    var user = await _context.Users.FindAsync(request.UserId);
                    var userFavorite = user.UserFavorite;

                    bool userDoesNotExist = user == null;

                    if(!userDoesNotExist)
                    {
                        Task<Hashtable> createUser = Task<Hashtable>.Factory.StartNew(() =>
                        {
                            var favoriteBlogPosts = userFavorite.BlogPosts;
                            var favoriteSermons = userFavorite.Sermons;
                            var favoriteBibleStudies = userFavorite.BibleStudies;

                            var loadBibleStudies = new List<Hashtable>();
                            var loadSermons = new List<Hashtable>();
                            var loadBlogPosts = new List<Hashtable>();

                            var outboundParentItem = new OutboundDTO();

                            if(favoriteBlogPosts.Count > 0)
                            {
                                foreach(var blogPost in favoriteBlogPosts)
                                {
                                    var outboundItemData = new OutboundDTO();

                                    outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostId", Value = blogPost.Id });
                                    outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostTitle", Value = blogPost.BlogPostTitle });
                                    outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostContent", Value = blogPost.BlogPostContent });
                                    outboundItemData.AddField(new DictionaryEntry { Key = "SermonVideoLink", Value = blogPost.BlogPostImage });

                                    loadBlogPosts.Add(outboundItemData.GetPayload());
                                }
                            }
                            
                            if(favoriteSermons.Count > 0)
                            {
                                foreach(var sermon in favoriteSermons)
                                {
                                    var outboundItemData = new OutboundDTO();

                                    outboundItemData.AddField(new DictionaryEntry { Key = "SermonId", Value = sermon.Id });
                                    outboundItemData.AddField(new DictionaryEntry { Key = "SermonName", Value = sermon.SermonName });
                                    outboundItemData.AddField(new DictionaryEntry { Key = "SermonDescription", Value = sermon.SermonDescription });
                                    outboundItemData.AddField(new DictionaryEntry { Key = "SermonVideoLink", Value = sermon.SermonVideoLink });

                                    loadSermons.Add(outboundItemData.GetPayload());
                                }
                            }

                            if(favoriteBibleStudies.Count > 0)
                            {
                                foreach(var bibleStudy in favoriteBibleStudies)
                                {
                                    var outboundItemData = new OutboundDTO();

                                    outboundItemData.AddField(new DictionaryEntry { Key = "BibleStudyId", Value = bibleStudy.Id });
                                    outboundItemData.AddField(new DictionaryEntry { Key = "BibleStudyName", Value = bibleStudy.BibleStudyName });
                                    outboundItemData.AddField(new DictionaryEntry { Key = "BibleStudyDescription", Value = bibleStudy.BibleStudyDescription });
                                    outboundItemData.AddField(new DictionaryEntry { Key = "BibleStudyVideoLink", Value = bibleStudy.BibleStudyVideoLink });

                                    loadBibleStudies.Add(outboundItemData.GetPayload());
                                }
                            }
                            

                            outboundParentItem.AddField(new DictionaryEntry { Key = "FavoriteBibleStudies", Value = loadBibleStudies });
                            outboundParentItem.AddField(new DictionaryEntry { Key = "FavoriteSermons", Value = loadSermons });
                            outboundParentItem.AddField(new DictionaryEntry { Key = "FavoriteBibleStudies", Value = loadBlogPosts });
                            
                            return outboundParentItem.GetPayload();
                        }); 

                    return await createUser;
                }
                else
                {
                    throw new Exception("User does not exist");
                }
            }
        }
    }
}