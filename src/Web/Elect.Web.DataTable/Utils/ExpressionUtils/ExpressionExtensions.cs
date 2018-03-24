#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ExpressionExtensions.cs </Name>
//         <Created> 23/03/2018 5:26:30 PM </Created>
//         <Key> ce6efb5b-885a-4227-8fb0-b797268c4dbc </Key>
//     </File>
//     <Summary>
//         ExpressionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Elect.Web.DataTable.Utils.ExpressionUtils
{
    public static class ExpressionExtensions
    {
        public static MethodInfo MethodInfo(this Expression method)
        {
            if (!(method is LambdaExpression lambda)) throw new ArgumentNullException(nameof(method));

            MethodCallExpression methodExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Call)
            {
                methodExpr = lambda.Body as MethodCallExpression;
            }

            if (methodExpr == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return methodExpr.Method;
        }
    }
}