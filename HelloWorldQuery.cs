using GraphQL.Types;
namespace GraphQLNet
{
    public class HelloWorldQuery : ObjectGraphType
    {
        public HelloWorldQuery()
        {
            Field<StringGraphType>(
                name:"hello",
                resolve: context => "world"
            );
        }
    }
}