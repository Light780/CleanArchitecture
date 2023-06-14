using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System.IO;

StreamerDbContext dbContext = new();


await MultipleEntitiesQuery();

async Task MultipleEntitiesQuery()
{
    //var videoWithActors = await dbContext.Videos!.Include(x => x.Actors).FirstOrDefaultAsync(q => q.Id == 1);

    //var actors = await dbContext.Actors!.Select(q => q.Name ).ToListAsync();

    var videosWithDirector = await dbContext.Videos!
                            .Where(x => x.Director != null)
                            .Include(x => x.Director)
                            .Select(x => new {
                                Director = $"{x.Director!.Name} {x.Director!.LastName}",
                                Movie = x.Name
                            }).ToListAsync();

    foreach (var video in videosWithDirector) Console.WriteLine($"{video.Director} - {video.Movie}");
}

async Task AddNewDirectorWithVideos()
{
    var director = new Director()
    {
        Name = "Will",
        LastName = "Smith",
    };

    var videos = new List<Video>()
    {
        new Video
        {
            Name = "I'm Legend",
            StreamerId = 1,
            Director = director,
        },
        new Video
        {
            Name = "Bel Air",
            StreamerId = 2,
            Director = director
        }
    };

    await dbContext.AddRangeAsync(videos);
    await dbContext.SaveChangesAsync();

}

async Task AddNewActorWithVideo()
{
    var actor = new Actor()
    {
        Name = "Brad",
        LastName = "Pitt"
    };

    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var videoActor = new VideoActor()
    {
        ActorId = actor.Id,
        VideoId = 1
    };

    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideo()
{
    var streamer = new Streamer()
    {
        Name = "Pantaya"
    };

    var director = new Director()
    {
        Name = "Quentin",
        LastName = "Tarantino"
    };

    var video = new Video()
    {
        Name = "Hunger Games",
        Streamer = streamer,
        Director = director
    };

    await dbContext.Videos!.AddAsync(video);
    await dbContext.SaveChangesAsync();
}

async Task TrackingAndNoTracking()
{
    var streamerWithTracking = await dbContext.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);
    var streamerWithNoTracking = await dbContext.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    streamerWithTracking!.Name = "Netflix Super";
    streamerWithNoTracking!.Name = "Amazon Plus";

    await dbContext.SaveChangesAsync();
}

async Task QueryLinq()
{
    Console.WriteLine("Enter a streaming company's name: ");
    var streamerName = Console.ReadLine();

    var streamers = await (from s in dbContext.Streamers!
                           where EF.Functions.Like(s.Name!, $"%{streamerName}%")
                           select s).ToListAsync();

    foreach (var streamer in streamers) Console.WriteLine($"{streamer.Id} - {streamer.Name}");
}

async Task QueryMethods()
{
    var streamer = await dbContext.Streamers!.FirstOrDefaultAsync(x => x.Name!.Contains("a"));
    Console.WriteLine($"{streamer.Id} - {streamer.Name}");
}

async Task QueryFilter()
{

    Console.WriteLine("Enter a streaming company's name: ");
    var streamerName = Console.ReadLine();

    var streamers = await dbContext.Streamers!.Where(x => x.Name == streamerName).ToListAsync();

    foreach (var streamer in streamers) Console.WriteLine($"{streamer.Id} - {streamer.Name}");
}

void QueryStreaming()
{
    var streamers = dbContext.Streamers!.ToList();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }
}

async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Name = "Disney",
        Url = "https://www.disney.com"
    };

    dbContext.Streamers!.Add(streamer);

    await dbContext.SaveChangesAsync();


    var movies = new List<Video>()
    {
        new Video
        {
            Name = "Avengers",
            StreamerId = streamer.Id,
        },
        new Video
        {
            Name = "Avengers 2",
            StreamerId = streamer.Id,
        }
    };

    dbContext.Videos!.AddRange(movies);

    await dbContext.SaveChangesAsync();

}