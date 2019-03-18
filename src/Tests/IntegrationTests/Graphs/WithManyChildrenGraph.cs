﻿using GraphQL.EntityFramework;
using Xunit;

public class WithManyChildrenGraph :
    EfObjectGraphType<WithManyChildrenEntity, MyDataContext>
{
    public WithManyChildrenGraph(IEfGraphQLService<MyDataContext> graphQlService) :
        base(graphQlService)
    {
        Field(x => x.Id);
        AddNavigationField(name: "child1",
            resolve: context =>
            {
                Assert.NotNull(context.Source.Child2);
                Assert.NotNull(context.Source.Child1);
                return context.Source.Child1;
            },
            includeNames: new []{ "Child2", "Child1" });
    }
}