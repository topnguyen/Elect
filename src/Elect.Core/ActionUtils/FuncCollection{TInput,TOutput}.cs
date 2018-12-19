using System;
using System.Collections.Generic;
using System.Linq;
using Elect.Core.ActionUtils.Models;
using Elect.Core.LinqUtils;

namespace Elect.Core.ActionUtils
{
    public class FuncCollection<TInput, TOutput>
    {
        protected List<FuncModel<TInput, TOutput>> Funcs { get; set; } = new List<FuncModel<TInput, TOutput>>();

        public virtual List<FuncModel<TInput, TOutput>> Get()
        {
            return Funcs;
        }

        public virtual string Add(Func<TInput, TOutput> func)
        {
            var funcModel = new FuncModel<TInput, TOutput>(func);

            Funcs.Add(funcModel);

            return funcModel.Id;
        }

        public virtual void Remove(string funcId)
        {
            Funcs = Funcs.RemoveWhere(x => x.Id == funcId).ToList();
        }

        public virtual void Empty()
        {
            if (Funcs?.Any() != true)
            {
                return;
            }

            Funcs = Funcs.RemoveWhere(x => x.Func != null).ToList();
        }
    }
}