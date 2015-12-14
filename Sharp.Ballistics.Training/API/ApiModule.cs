using Nancy;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sharp.Ballistics.Abstractions;

namespace Sharp.Ballistics.Training.API
{
    public class ApiModule : NancyModule
    {
        private readonly IDocumentSession session;
        public ApiModule(IDocumentSession session) : base("/api")
        {
            this.session = session;
            Get["/rifles/"] = _ => "Rifle list";
            Get["/rifles/{id*}"] = @params => "Rifle with id = " + @params.Id;
            Get["/ammo/"] = _ => "ammo list";
            Get["/ammo/{id*}"] = @params => "ammo with id = " + @params.Id;
        }
    }
}