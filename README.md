[![Build Status](https://travis-ci.org/josh-perry/Tracery.Net.svg?branch=master)](https://travis-ci.org/josh-perry/Tracery.Net)

# Tracery.Net
.NET port of Kate Compton's [Tracery](https://github.com/galaxykate/tracery).

## Minimal example
```cs
var grammar = new TraceryNet.Grammar(new FileInfo("grammar.json"));
var output = grammar.Flatten("#origin#");
Console.WriteLine(output);
```

Where grammar.json is:
```json
{
    "origin": "The #person# was feeling... #mood#",
    "person": ["girl", "dwarf", "cat", "dragon"],
    "mood": ["bashful", "dopey", "happy", "sleepy", "sneezy", "grumpy"]
}
```

Example outputs: 
```
The dwarf was feeling grumpy.
The girl was feeling sneezy.
The girl was feeling sleepy.
The dwarf was feeling grumpy.
The dragon was feeling dopey.
```

[See TraceryNetExample project for more](TraceryNetExample/Program.cs)

## Status
| Feature                           | Status                   |
|-----------------------------------|--------------------------|
| Capitalize All                    | :heavy_check_mark:       |
| Capitalize                        | :heavy_check_mark:       |
| In Quotes                         | :heavy_check_mark:       |
| Comma                             | :heavy_check_mark:       |
| :honeybee: Speak                  | :heavy_check_mark:       |
| Pluralize                         | :heavy_check_mark:       |
| Past-tensifiy                     | :heavy_check_mark:       |
| Custom modifiers                  | :heavy_check_mark:       |
| Saving data & actions             | :heavy_check_mark:       |
