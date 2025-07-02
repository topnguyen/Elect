namespace Elect.Web.DataTable.Utils.ExpressionUtils
{
    internal static class ExpressionExtensions
    {
        internal static MethodInfo MethodInfo(this Expression method)
        {
            if (!(method is LambdaExpression lambda)) throw new ArgumentNullException(nameof(method));
            MethodCallExpression methodExpr = null;
            if (lambda.Body.NodeType == ExpressionType.Call) methodExpr = lambda.Body as MethodCallExpression;
            if (methodExpr == null) throw new ArgumentNullException(nameof(method));
            return methodExpr.Method;
        }
    }
}
