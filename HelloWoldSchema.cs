using GraphQL.Types;
using GraphQLNet;

public class HelloWorldSchema : Schema
{
    public HelloWorldSchema(HelloWorldQuery query)
    {
        Query = query;
    }
}