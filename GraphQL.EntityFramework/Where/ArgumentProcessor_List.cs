﻿using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;

static partial class ArgumentProcessor
{
    public static IEnumerable<TItem> ApplyGraphQlArguments<TItem, TSource>(this IEnumerable<TItem> items, ResolveFieldContext<TSource> context)
    {
        return ApplyToAll(items, (type, x) => context.GetArgument(type, x));
    }

    static IEnumerable<TItem> ApplyToAll<TItem>(this IEnumerable<TItem> items, Func<Type, string, object> getArguments)
    {
        foreach (var where in ExpressionContextExtractor.ReadWhere<TItem>(getArguments))
        {
            var predicate = FuncBuilder<TItem>.BuildPredicate(where);
            items = items.Where(predicate);
        }

        if (ExpressionContextExtractor.TryReadSkip(getArguments, out var skip))
        {
            items = items.Skip(skip);
        }

        if (ExpressionContextExtractor.TryReadTake(getArguments, out var take))
        {
            items = items.Take(take);
        }

        return items;
    }
}