using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.WebAdmin.Infrastructure
{
    public static class CustomHelper 
    {
        public static MvcHtmlString TextBoxNumericFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            TagBuilder tb = new TagBuilder("input");
            tb.Attributes.Add("type", "text");
            tb.Attributes.Add("name", name);
            tb.Attributes.Add("value", metadata.Model as string);
            tb.Attributes.Add("style", "color:red");
            tb.MergeAttribute("onkeypress", "return event.charCode >= 48 && event.charCode <= 57");
           return new MvcHtmlString(tb.ToString(TagRenderMode.Normal));
        }
    }
}