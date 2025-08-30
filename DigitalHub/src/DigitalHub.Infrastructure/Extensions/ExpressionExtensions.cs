using System.Linq.Expressions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        // Combine the parameter
        var parameter = Expression.Parameter(typeof(T), "x");
        // Replace parameters in both expressions
        var left = new ReplaceParameterVisitor(expr1.Parameters[0], parameter).Visit(expr1.Body);
        var right = new ReplaceParameterVisitor(expr2.Parameters[0], parameter).Visit(expr2.Body);
        // Combine expressions with AndAlso
        var body = Expression.AndAlso(left, right);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        // Combine the parameter
        var parameter = Expression.Parameter(typeof(T), "x");
        // Replace parameters in both expressions
        var left = new ReplaceParameterVisitor(expr1.Parameters[0], parameter).Visit(expr1.Body);
        var right = new ReplaceParameterVisitor(expr2.Parameters[0], parameter).Visit(expr2.Body);
        // Combine expressions with OrElse
        var body = Expression.OrElse(left, right);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}

internal class ReplaceParameterVisitor : ExpressionVisitor
{
    private readonly ParameterExpression _oldParameter;
    private readonly ParameterExpression _newParameter;

    public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
    {
        _oldParameter = oldParameter;
        _newParameter = newParameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        // Replace old parameter with new one
        return node == _oldParameter ? _newParameter : base.VisitParameter(node);
    }
}