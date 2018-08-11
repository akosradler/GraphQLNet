using System.IO;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQLNet;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class GraphQLMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly IDocumentWriter _writer;
    private readonly IDocumentExecuter _executer;
    private readonly ISchema _schema;
    public GraphQLMiddleWare(RequestDelegate next, IDocumentWriter writer, IDocumentExecuter executer, ISchema schema)
    {
        _next = next;
        _executer = executer;
        _schema = schema;
        _writer = writer;
    }

    public async Task InvokeAsync(HttpContext httpContext)
        {
            if(httpContext.Request.Path.StartsWithSegments("/api/graphql") && httpContext.Request.Method == "POST")
                {
                    using(var streamReader = new StreamReader(httpContext.Request.Body))
                    {
                        var body = await streamReader.ReadToEndAsync();

                        var request = JsonConvert.DeserializeObject<GraphQLRequest>(body);
                        var schema = new Schema()
                        {
                            Query = new HelloWorldQuery()
                        };

                        var result = await new DocumentExecuter().ExecuteAsync(
                            doc =>
                            {
                                doc.Schema = schema;
                                doc.Query = request.Query;
                            }   
                        ).ConfigureAwait(false);

                        var json = new DocumentWriter(indent: true).Write(result);
                        await httpContext.Response.WriteAsync(json);
                    }
                }
                else
                {
                    await _next(httpContext);
                }
        }
}