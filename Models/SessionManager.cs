
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using LinqKit;

namespace ShriVivah.Models
{
    public class SessionManager
    {
        private SessionManager()
        { 
            
        }

        static SessionManager _sessionmanager = null;
        public static SessionManager GetInstance
        {
            get
            {
                if (_sessionmanager == null)
                {
                    _sessionmanager = new SessionManager();
                }
                return _sessionmanager;
            }
        }

        public RegisterViewModel SearchUser
        {
            get
            {
                RegisterViewModel user = null;
                if (HttpContext.Current.Session["SearchUser"] != null)
                {
                    user = (RegisterViewModel)HttpContext.Current.Session["SearchUser"];
                }
                return user;
            }
            set { HttpContext.Current.Session["SearchUser"] = value; }
        }

        public int VendorTypeID
        {
            get
            {
                int user = 0;
                if (HttpContext.Current.Session["VendorTypeID"] != null)
                {
                    user = Convert.ToInt32(HttpContext.Current.Session["VendorTypeID"]);
                }
                return user;
            }
            set { HttpContext.Current.Session["VendorTypeID"] = value; }
        }

        //RegisterUser
        public RegisterViewModel RegisterUser
        {
            get
            {
                RegisterViewModel user = null;
                if (HttpContext.Current.Session["RegisterUser"] != null)
                {
                    user = (RegisterViewModel)HttpContext.Current.Session["RegisterUser"];
                }
                return user;
            }
            set { HttpContext.Current.Session["RegisterUser"] = value; }
        }

        public STP_GetUserDetail ActiveUser 
        {
            get
            {
                STP_GetUserDetail user = null;
                if (HttpContext.Current.Session["ActiveUser"] != null)
                {
                    user = (STP_GetUserDetail)HttpContext.Current.Session["ActiveUser"];
                }
                return user;
            }
            set { HttpContext.Current.Session["ActiveUser"]=value; }
        }

        public int? AdminUserId { get; internal set; }
    }


    public static class FilterHelper
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
    }

    public class ParameterRebinder : LinqKit.ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}