[![Nuget](https://img.shields.io/nuget/v/AO.ConnectionStrings)](https://www.nuget.org/packages/AO.ConnectionStrings/)

Several times recently, I've needed to do stuff with connection strings, parsing individual tokens, and redacting sensitive info for display. I got tired of copying and pasting this code everywhere, so I decided to offer as a package. Examples:

```csharp
var redacted = ConnectionString.Redacted("Server=whatever;Database=thisdb;User Id=adamo;Password=*gkdZ7W1#QsN@EAH");
Assert.IsTrue(redacted.Equals("Server=whatever;Database=thisdb;User Id=&lt;redacted&gt;;Password=&lt;redacted&gt;"));

var server = ConnectionString.Server("Server=(localdb)\\mssqllocaldb;Database=AnotherDb;Integrated Security=true");
Assert.IsTrue(server.Equals("(localdb)\\mssqllocaldb"));
```
# AO.ConnectionStrings.ConnectionString [ConnectionString.cs](https://github.com/adamfoneil/ConnectionString.Library/blob/master/ConnectionString.Library/ConnectionString.cs#L7)
## Methods
- string [Token](https://github.com/adamfoneil/ConnectionString.Library/blob/master/ConnectionString.Library/ConnectionString.cs#L11)
 (string input, string token, [ string defaultValue ])
- string [Token](https://github.com/adamfoneil/ConnectionString.Library/blob/master/ConnectionString.Library/ConnectionString.cs#L18)
 (string input, IEnumerable<string> tokens, [ string defaultValue ])
- string [Server](https://github.com/adamfoneil/ConnectionString.Library/blob/master/ConnectionString.Library/ConnectionString.cs#L27)
 (string input)
- string [Database](https://github.com/adamfoneil/ConnectionString.Library/blob/master/ConnectionString.Library/ConnectionString.cs#L29)
 (string input)
- bool [IsWellFormed](https://github.com/adamfoneil/ConnectionString.Library/blob/master/ConnectionString.Library/ConnectionString.cs#L31)
 (string input)
- Dictionary\<string, string\> [ToDictionary](https://github.com/adamfoneil/ConnectionString.Library/blob/master/ConnectionString.Library/ConnectionString.cs#L44)
 (string input)
- string [Redact](https://github.com/adamfoneil/ConnectionString.Library/blob/master/ConnectionString.Library/ConnectionString.cs#L57)
 (string input, [ string replaceWith ])
- bool [IsSensitive](https://github.com/adamfoneil/ConnectionString.Library/blob/master/ConnectionString.Library/ConnectionString.cs#L64)
 (string input, Dictionary<string, string> redacted, [ string replaceWith ])
