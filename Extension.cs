using System;

public static class Extension 
{
    public static TOutput Pipe<TParam, TOutput>(this TParam input, Func<TParam, TOutput> func)
        => func(input);
}