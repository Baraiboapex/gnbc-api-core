using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(GNBCContext context)
        {
            if (!context.BibleStudies.Any())
            {
                var bibleStudies = new List<BibleStudy>
                {
                    new BibleStudy
                    {
                        BibleStudyName = "Bible Study 2",
                        BibleStudyDescription = "Bible study 1 is about Jesus",
                        BibleStudyVideoLink = "#"
                    },
                    new BibleStudy
                    {
                        BibleStudyName = "Bible Study 2",
                        BibleStudyDescription = "Bible study 2 is about the Trinity",
                        BibleStudyVideoLink = "#"
                    },
                    new BibleStudy
                    {
                        BibleStudyName = "Bible Study 3",
                        BibleStudyDescription = "Bible study 3 is about Jesus and God",
                        BibleStudyVideoLink = "#"
                    },
                    new BibleStudy
                    {
                        BibleStudyName = "Bible Study 4",
                        BibleStudyDescription = "Bible study 4 is about Jesus and God and the Holy Spirit",
                        BibleStudyVideoLink = "#"
                    }
                };

                await context.BibleStudies.AddRangeAsync(bibleStudies);
                await context.SaveChangesAsync();
            }

            if (!context.Sermons.Any())
            {
                var sermons = new List<Sermon>
                {
                    new Sermon
                    {
                        SermonName = "Sermon 1",
                        SermonDescription = "Sermon 1 is about Jesus",
                        SermonVideoLink = "#"
                    },
                    new Sermon
                    {
                        SermonName = "Sermon 2",
                        SermonDescription = "Sermon 2 is about God",
                        SermonVideoLink = "#"
                    },
                    new Sermon
                    {
                        SermonName = "Sermon 3",
                        SermonDescription = "Sermon 3 is about the Trinity",
                        SermonVideoLink = "#"
                    },
                    new Sermon
                    {
                        SermonName = "Sermon 4",
                        SermonDescription = "Sermon 4 is about Jesus and God",
                        SermonVideoLink = "#"
                    }
                };

                await context.Sermons.AddRangeAsync(sermons);
                await context.SaveChangesAsync();
            }

             if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        FirstName = "Matthew",
                        LastName = "Bailey",
                        Email = "matthewpbaileydesigns@gmail.com",
                        Password="Welcome1",
                        CanBlog = true,
                        UserFavorite = new UserFavorite()
                    },
                    new User
                    {
                        FirstName = "Steve",
                        LastName = "Bailey",
                        Email = "pastorsteve1998@gmail.com",
                        Password="Welcome1",
                        CanBlog = true,
                        UserFavorite = new UserFavorite()
                    },
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }

            if (!context.ChurchEvents.Any())
            {
                var churchEvents = new List<ChurchEvent>
                {
                    new ChurchEvent
                    {
                        ChurchEventName = "A church event",
                        ChurchEventDescription ="This is an event",
                        ChurchEventDate ="12-13-2022",
                        ChurchEventFacebookLink ="#", 
                        ChurchEventImage ="#"
                    },
                    new ChurchEvent
                    {
                        ChurchEventName = "Another church event",
                        ChurchEventDescription ="This is another event",
                        ChurchEventDate ="12-14-2022",
                        ChurchEventFacebookLink ="#", 
                        ChurchEventImage ="#"
                    },
                    new ChurchEvent
                    {
                        ChurchEventName = "Yet another church event",
                        ChurchEventDescription ="This is yet another event",
                        ChurchEventDate ="12-14-2022",
                        ChurchEventFacebookLink ="#", 
                        ChurchEventImage ="#"
                    },
                };

                await context.ChurchEvents.AddRangeAsync(churchEvents);
                await context.SaveChangesAsync();
            }
        }
    }
}