var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/day1-1", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day1.txt");
    var depthReadings = File.ReadLines(filePath).Select(row => int.Parse(row)).ToList();
    var comparisons = depthReadings.Zip(depthReadings.Skip(1), (first, second) => second - first);
    var deeper = comparisons.Where(difference => difference > 0);
    var deeperCount = deeper.Count();
    return deeperCount; //1215
}
);

app.MapGet("/day1-2", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day1.txt");
    var depthReadings = File.ReadLines(filePath).Select(row => int.Parse(row)).ToList();
    var tripleSums = depthReadings.Zip(depthReadings.Skip(1), (first, second) => first + second).Zip(depthReadings.Skip(2), (firstTwo, third) => firstTwo + third);
    var comparisons = tripleSums.Zip(tripleSums.Skip(1), (first, second) => second - first);
    var deeper = comparisons.Where(difference => difference > 0);
    var deeperCount = deeper.Count();
    return deeperCount; //1150
}
);

app.MapGet("/day2-1", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day2.txt");
    var moveCommandTuples = File.ReadLines(filePath).Select(commandText => (command: commandText.Split(" ").First(), distance: int.Parse(commandText.Split(" ").Last()))).ToList();
    var x = 0;
    var y = 0;
    moveCommandTuples.ForEach(pairing => {
        _ = pairing switch {
            {command:"forward"} => x+= pairing.distance,
            {command:"up", distance: var toofar} when toofar > y  => y = 0,
            {command:"up"} => y -= pairing.distance,
            {command:"down"} => y += pairing.distance,
            _ => 0,
        };
    }
    );
    return x * y; //1693300
}
);

app.MapGet("/day2-2", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day2.txt");
    var moveCommandTuples = File.ReadLines(filePath).Select(commandText => (command: commandText.Split(" ").First(), distance: int.Parse(commandText.Split(" ").Last()))).ToList();
    var x = 0;
    var y = 0;
    var aim = 0;
    moveCommandTuples.ForEach(pairing => {
        _ = pairing switch {
            {command:"forward", distance: var howFar} when aim * howFar + y < 0 => (x+= pairing.distance, y = 0),
            {command:"forward"} => (x+= pairing.distance, y += aim * pairing.distance),
            {command:"up"} => (aim -= pairing.distance, 0),
            {command:"down"} => (aim += pairing.distance, 0),
            _ => (0,0),
        };
    }
    );
    return x * y; //1857958050
}
);

app.MapGet("/day3-1", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day3.txt");
    var binaryStrings = File.ReadLines(filePath).ToList();
    var listLength = binaryStrings.Count;
    var bitCount = binaryStrings.First().Length;
    var oneCounts = new int[bitCount];
    binaryStrings.ForEach(item => {
        for (var i = 0; i < bitCount; i++) {
            if(item[i] == '1') {
                oneCounts[i]++;
            }
        }
    });
    var mostCommon = oneCounts.Select(count => count > listLength/2 ? "1" : "0");
    var leastCommon = mostCommon.Select(item => item == "1" ? "0" : "1");

    var gamma = Convert.ToInt64(string.Join("", mostCommon), 2);
    var epsilon = Convert.ToInt64(string.Join("", leastCommon), 2);

    return gamma * epsilon; //3923414
    
}
);





app.Run();