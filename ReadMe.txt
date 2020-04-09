

    What is LINQ?

    LINQ (Language Integrated Query) is uniform query syntax in C# and VB.NET to retrieve data from different sources and formats.
It is integrated in C# or VB, thereby eliminating the mismatch between programming languages and databases, as well as providing
a single querying interface for different types of data sources.

    For example, SQL is a Structured Query Language used to save and retrieve data from a database. In the same way, LINQ is a structured
query syntax built in C# and VB.NET to retrieve data from different types of data sources such as collections, ADO.Net DataSet,
XML Docs, web service and MS SQL Server and other databases.


***EXAMPLE***

// Data source
string[] names = {"Bill", "Steve", "James", "Mohan" };

// LINQ Query 
var myLinqQuery = from name in names
                where name.Contains('a')
                select name;
    
// Query execution
foreach(var name in myLinqQuery)
    Console.Write(name + " ");