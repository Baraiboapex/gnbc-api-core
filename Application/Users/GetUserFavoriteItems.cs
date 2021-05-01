using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

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
                    var user = await _context.UserFavorites
                    .Include(uf => uf.BlogPosts)
                    .Include(uf => uf.ChurchEvents)
                    .Include(uf => uf.BibleStudies)
                    .Include(uf => uf.Sermons)
                    .SingleOrDefaultAsync(u => u.UserId == request.UserId);

                    bool userDoesNotExist = user == null;

                    if(!userDoesNotExist)
                    {
                        var favoriteBlogPosts = user.BlogPosts;
                        var favoriteSermons = user.Sermons;
                        var favoriteBibleStudies = user.BibleStudies;
                        var markedChurchEvents = user.ChurchEvents;

                        var loadBibleStudies = new List<Hashtable>();
                        var loadSermons = new List<Hashtable>();
                        var loadBlogPosts = new List<Hashtable>();
                        var loadChurchEvents = new List<Hashtable>();

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

                        if(markedChurchEvents.Count > 0)
                        {
                            foreach(var churchEvent in markedChurchEvents)
                            {
                                var outboundItemData = new OutboundDTO();

                                outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventId", Value = churchEvent.Id });
                                outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventName", Value = churchEvent.ChurchEventName });
                                outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventDescription", Value = churchEvent.ChurchEventDescription });
                                outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventImage", Value = churchEvent.ChurchEventImage });

                                loadChurchEvents.Add(outboundItemData.GetPayload());
                            }
                        }

                        outboundParentItem.AddField(new DictionaryEntry { Key = "FavoriteBibleStudies", Value = loadBibleStudies });
                        outboundParentItem.AddField(new DictionaryEntry { Key = "FavoriteSermons", Value = loadSermons });
                        outboundParentItem.AddField(new DictionaryEntry { Key = "BlogPosts", Value = loadBlogPosts });
                        outboundParentItem.AddField(new DictionaryEntry { Key = "ChurchEvents", Value = loadChurchEvents });
                        
                        return outboundParentItem.GetPayload();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "User does not exist.");
                            
                    throw newError;
                }
            }
        }
    }
}