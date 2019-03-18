﻿using System.Linq;
using GraphQL.EntityFramework;

public class WithMisNamedQueryParentGraph :
    EfObjectGraphType<WithMisNamedQueryParentEntity, MyDataContext>
{
    public WithMisNamedQueryParentGraph(IEfGraphQLService<MyDataContext> graphQlService) :
        base(graphQlService)
    {
        Field(x => x.Id);
        AddQueryField(
            name: "misNamedChildren",
            resolve: context =>
            {
                var dataContext = (MyDataContext)context.UserContext;
                var parentId = context.Source.Id;
                return dataContext.WithMisNamedQueryChildEntities
                    .Where(x=>x.ParentId == parentId);
            });
    }
}