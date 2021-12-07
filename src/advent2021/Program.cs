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


// Navigate to /swagger upon running


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

    var gamma = Convert.ToInt32(string.Join("", mostCommon), 2);
    var epsilon = Convert.ToInt32(string.Join("", leastCommon), 2);

    return gamma * epsilon; //3923414
    
}
);

app.MapGet("/day3-2", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day3.txt");
    var binaryStrings = File.ReadLines(filePath).ToList();
    var bitCount = binaryStrings.First().Length;

    var oxygenBinary = "";
    var currentList = binaryStrings;
    for (var i = 0; i < bitCount; i++) {
        if(!currentList.Any()) {
            throw new Exception("Uh oh");
        }

        var listLength = currentList.Count;
        if(listLength == 1) {
            oxygenBinary = currentList.Single();
            break;
        }

        // For oxygen, keep the rows with most common bit. Tie goes to 1.
        var onesCount = currentList.Where(item => item[i] == '1').Count();
        if (onesCount >= listLength / 2.0) {
            currentList = currentList.Where(item => item[i] == '1').ToList();
        } else {
            currentList = currentList.Where(item => item[i] == '0').ToList();
        }
    }
    if(string.IsNullOrWhiteSpace(oxygenBinary)) {
        oxygenBinary = currentList.Single();
    }

    var co2Binary = "";
    currentList = binaryStrings;
    for (var i = 0; i < bitCount; i++) {
        if(!currentList.Any()) {
            throw new Exception("Uh oh");
        }

        var listLength = currentList.Count;
        if(listLength == 1) {
            co2Binary = currentList.Single();
            break;
        }
        
        //For co2, keep the rows with least common bit. Tie goes to 0.
        var onesCount = currentList.Where(item => item[i] == '1').Count();
        if (onesCount > 0 && onesCount < listLength / 2.0) {
            currentList = currentList.Where(item => item[i] == '1').ToList();
        } else {
            currentList = currentList.Where(item => item[i] == '0').ToList();
        }
    }
    if(string.IsNullOrWhiteSpace(co2Binary)) {
        co2Binary = currentList.Single();
    }

    var oxygen = Convert.ToInt32(string.Join("", oxygenBinary), 2);
    var co2 = Convert.ToInt32(string.Join("", co2Binary), 2);

    return oxygen * co2; //No: 5805891. Yes: 5852595
    
}
);

app.MapGet("/day4-1", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day4.txt");
    var playsAndBoards = File.ReadLines(filePath).ToList();
    var plays = playsAndBoards.First().Split(',').Select(int.Parse).ToList();

    //Represent boards twice, as the original 5x5 grid split into rows and again as the transposed grid split into rows
    //Remove row elements when they are called
    //If a row is empty, that grid wins
    //The last called number plus the remaining elements in the grid go into the result

    var boardgroups = playsAndBoards
        .Skip(2)
        .Select((row, index) => new {row, index})
        .GroupBy(pairing => pairing.index / 6, el => el.row);
    
    var boardList = new List<(List<List<int>> original, List<List<int>> transposed)>();
    foreach(var boardlines in boardgroups) {
        var boardMatrixTransposed = new int[5,5];
        var originalLines = boardlines.Take(5).Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
        for (var i = 0; i < 5; i++) {
            for (var j = 0; j < 5; j++) {
                boardMatrixTransposed[j,i] = originalLines[i][j];
            }
        }
        var transposedLines = new List<List<int>>();
        for (var i = 0; i < 5; i++) {
            var newRow = new List<int>();
            for (var j = 0; j < 5; j++) {
                newRow.Add(boardMatrixTransposed[i,j]);
            }
            transposedLines.Add(newRow);
        }
        boardList.Add((original: originalLines, transposed: transposedLines));
    }
    var winningPlay = 0;
    var winningSum = 0;
    plays.ForEach(currentPlay => {
        boardList.ForEach(boardPair => {
            if(winningPlay == 0 && winningSum == 0) {
                boardPair.original.ForEach(boardRow => boardRow.RemoveAll(rowItem => rowItem == currentPlay));
                boardPair.transposed.ForEach(boardRow => boardRow.RemoveAll(rowItem => rowItem == currentPlay));

                if(boardPair.original.Any(boardRow => boardRow.Count == 0) || boardPair.transposed.Any(boardRow => boardRow.Count == 0)) {
                    var boardSum = boardPair.original.Sum(boardRow => boardRow.Sum());
                    winningSum = boardSum;
                    winningPlay = currentPlay;
                }
            }
        });
    });

    return winningSum * winningPlay; // 34506
}
);

app.MapGet("/day4-2", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day4.txt");
    var playsAndBoards = File.ReadLines(filePath).ToList();
    var plays = playsAndBoards.First().Split(',').Select(int.Parse).ToList();

    //Represent boards twice, as the original 5x5 grid split into rows and again as the transposed grid split into rows
    //Remove row elements when they are called
    //If a row is empty, that grid wins
    //The last called number plus the remaining elements in the grid calculate the result

    var boardgroups = playsAndBoards
        .Skip(2)
        .Select((row, index) => new {row, index})
        .GroupBy(pairing => pairing.index / 6, el => el.row);
    
    var boardList = new List<(List<List<int>> original, List<List<int>> transposed, List<bool> won)>();
    foreach(var boardlines in boardgroups) {
        var boardMatrixTransposed = new int[5,5];
        var originalLines = boardlines.Take(5).Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
        for (var i = 0; i < 5; i++) {
            for (var j = 0; j < 5; j++) {
                boardMatrixTransposed[j,i] = originalLines[i][j];
            }
        }
        var transposedLines = new List<List<int>>();
        for (var i = 0; i < 5; i++) {
            var newRow = new List<int>();
            for (var j = 0; j < 5; j++) {
                newRow.Add(boardMatrixTransposed[i,j]);
            }
            transposedLines.Add(newRow);
        }
        boardList.Add((original: originalLines, transposed: transposedLines, won: new List<bool>{false}));
    }
    var winningPlay = 0;
    var winningSum = 0;

    // Difference from 4-1: List<bool> in the board tuple. A list can be modified within the foreach, which works, but ew.
    plays.ForEach(currentPlay => {
        boardList.ForEach(boardPair => {
            if(!boardPair.won.Any(el => el == true)) {
                boardPair.original.ForEach(boardRow => boardRow.RemoveAll(rowItem => rowItem == currentPlay));
                boardPair.transposed.ForEach(boardRow => boardRow.RemoveAll(rowItem => rowItem == currentPlay));

                if(boardPair.original.Any(boardRow => boardRow.Count == 0) || boardPair.transposed.Any(boardRow => boardRow.Count == 0)) {
                    var boardSum = boardPair.original.Sum(boardRow => boardRow.Sum());
                    winningSum = boardSum;
                    winningPlay = currentPlay;
                    boardPair.won[0] = true;
                }
            }
        });
    });
    
    return winningSum * winningPlay; // 7686
}
);

app.MapGet("/day5-1", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day5.txt");
    var ventVectors = File
    .ReadLines(filePath)
    .Select(row => row
        .Split(" -> ", StringSplitOptions.RemoveEmptyEntries)
        .Select(coords => coords
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)))
    .ToList();

    var maxGridLength = 0;
    ventVectors.ForEach(vpair => {
        var max = Math.Max(vpair.First().Max(), vpair.Last().Max());
        if(maxGridLength <= max) {
            maxGridLength = max + 1;
        }
    });

    var grid = new int[maxGridLength,maxGridLength];
    
    ventVectors.ForEach(vpair => {
        // Both coordinates match on X
        if (vpair.First().First() == vpair.Last().First()) {
            var start = vpair.First().Last();
            var end = vpair.Last().Last();
            if (start > end) {
                end = Interlocked.Exchange(ref start,end);
            }
            for (var i = start; i <= end; i++) {
                grid[vpair.First().First(),i]++;
            }
        } else if (vpair.First().Last() == vpair.Last().Last()) {
            var start = vpair.First().First();
            var end = vpair.Last().First();
            if (start > end) {
                end = Interlocked.Exchange(ref start,end);
            }
            for (var i = start; i <= end; i++) {
                grid[i,vpair.First().Last()]++;
            }
        }
    });

    var twoPlusCount = 0;
    for (var i = 0; i < maxGridLength; i++) {
        for (var j = 0; j < maxGridLength; j++) {
            if(grid[i,j] > 1) {
                twoPlusCount++;
            }
        }
    }
    return twoPlusCount; //5167

});

app.MapGet("/day5-2", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day5.txt");
    var ventVectors = File
    .ReadLines(filePath)
    .Select(row => row
        .Split(" -> ", StringSplitOptions.RemoveEmptyEntries)
        .Select(coords => coords
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)))
    .ToList();

    var maxGridLength = 0;
    ventVectors.ForEach(vpair => {
        var max = Math.Max(vpair.First().Max(), vpair.Last().Max());
        if(maxGridLength <= max) {
            maxGridLength = max + 1;
        }
    });

    var grid = new int[maxGridLength,maxGridLength];
    
    ventVectors.ForEach(vpair => {
        // Both coordinates match on X
        if (vpair.First().First() == vpair.Last().First()) {
            var start = vpair.First().Last();
            var end = vpair.Last().Last();
            if (start > end) {
                end = Interlocked.Exchange(ref start,end);
            }
            for (var i = start; i <= end; i++) {
                grid[vpair.First().First(),i]++;
            }
        } else if (vpair.First().Last() == vpair.Last().Last()) {
            //Coordinates match on Y
            var start = vpair.First().First();
            var end = vpair.Last().First();
            if (start > end) {
                end = Interlocked.Exchange(ref start,end);
            }
            for (var i = start; i <= end; i++) {
                grid[i,vpair.First().Last()]++;
            }
        } else {
            var x1 = vpair.First().First(); 
            var x2 = vpair.Last().First();

            var y1 = vpair.First().Last();
            var y2 = vpair.Last().Last();

            if (x1 > x2) {
              x2 = Interlocked.Exchange(ref x1, x2);
              y2 = Interlocked.Exchange(ref y1, y2);
            }

            bool ascending = (y1 < y2);

            var j = y1;
            for(var i = x1; i <= x2; i++) {
                grid[i,j]++;
                if(ascending) {
                    j++; 
                } else {
                    j--;
                }
            }
        };
    });

    var twoPlusCount = 0;
    for (var i = 0; i < maxGridLength; i++) {
        for (var j = 0; j < maxGridLength; j++) {
            if(grid[i,j] > 1) {
                twoPlusCount++;
            }
        }
    }
    return twoPlusCount; //17604

});

app.MapGet("/day6-1", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day6.txt");
    var fishAges = File
        .ReadLines(filePath)
        .Select(row => row
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse))
        .First().ToList();

    for(var i = 0; i < 80; i++) {
        var newFishCount = fishAges.Count(fishAge => fishAge == 0);
        for (var fish = 0; fish < fishAges.Count; fish++) {
            fishAges[fish]--;
            if (fishAges[fish] < 0)  {
                fishAges[fish] = 6;
            }
        }
        var newFish = new List<int>();
        for(var k = 0; k < newFishCount; k++) {
            newFish.Add(8);
        }
        fishAges.AddRange(newFish);
    }

    return fishAges.Count; //360761

});

app.MapGet("/day6-2", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day6.txt");
    var fishAges = File
        .ReadLines(filePath)
        .Select(row => row
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse))
        .First().ToList();

    var fishCounts = new Dictionary<int,long>();
    for (var i = 0; i <= 8; i++) {
        fishCounts[i] = fishAges.Count(fa => fa == i);
    }

    for(var i = 0; i < 256; i++) {
        var newFishCount = fishCounts[0];
        for (var j = 0; j < 8; j++) {
            fishCounts[j] = fishCounts[j+1];
        }
        fishCounts[6] += newFishCount;
        fishCounts[8] = newFishCount;
    }
    return fishCounts.Values.Sum(); //1632779838045
});

app.MapGet("/day7-1", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day7.txt");
    var crabXPositions = File
        .ReadLines(filePath)
        .Select(row => row
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse))
        .First().OrderBy(i => i).ToList();
    
    var maxPosition = crabXPositions.Max();

    var bestPosition = 0;
    var bestFuel = long.MaxValue;
    var medianPosition = crabXPositions[(crabXPositions.Count)/2]; // Suspect this is the optimal position after sorting, but just brute force for now
    for(var i = 0; i < maxPosition; i++) {
        var fuelAmount = crabXPositions.Select(pos => Math.Abs(pos - i)).Sum();
        if(fuelAmount < bestFuel) {
            bestPosition = i;
            bestFuel = fuelAmount;
        }
    }
    
    return bestFuel; //345197
});

app.MapGet("/day7-2", () =>
{
    var filePath = System.IO.Path.Combine(builder.Environment.ContentRootPath, @"input\day7.txt");
    var crabXPositions = File
        .ReadLines(filePath)
        .Select(row => row
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse))
        .First().OrderBy(i => i).ToList();
    
    var maxPosition = crabXPositions.Max();

    var bestPosition = 0;
    var bestFuel = long.MaxValue;
    var medianPosition = (crabXPositions.Count)/2;
    for(var i = 0; i < maxPosition; i++) {
        var fuelAmount = crabXPositions.Select(pos => {
            // 1 + 2 + 3 + 4 + ... n = n(n+1)/2
            var distance = Math.Abs(pos - i);
            var fuelCost = distance*(distance+1)/2;
            return fuelCost;
            }).Sum();
        if(fuelAmount < bestFuel) {
            bestPosition = i;
            bestFuel = fuelAmount;
        }
    }
    
    return bestFuel; //96361606
});

app.Run();