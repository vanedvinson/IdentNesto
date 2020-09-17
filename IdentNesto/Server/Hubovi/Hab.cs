using IdentNesto.Server.Models;
using IdentNesto.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentNesto.Server.Hubovi
{
    public class Hab : Hub
    {
        private readonly ILogger<Hab> _log;
        private readonly UserManager<ApplicationUser> _um;
        public Hab(ILogger<Hab> log, UserManager<ApplicationUser> um)
        {
            _log = log;
            _um = um;

        }

        public async Task Reg(UserReg juzer)
        {
            ApplicationUser au = new ApplicationUser 
                     { Email = juzer.Mejl, UserName = juzer.Ime };

            var rez = await _um.CreateAsync(au, juzer.Sifra);

            if (rez.Succeeded)
            {
                List<string> l = new List<string>();
                l.Add("OK smo :)");
                Clients.Caller.SendAsync("Return", l);
            }else
            {
                Clients.Caller.SendAsync("Return", rez.Errors.Select(err => err.Description).ToList());
            }

        }
    }
}
