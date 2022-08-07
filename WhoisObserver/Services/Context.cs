﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhoisObserver.Services.Mapper;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.WhoisServersClients.Interfaces;

namespace WhoisObserver.Services
{
    public class Context
    {
        private IWhoisClient _strategy;

        public Context()
        { }

        public Context(IWhoisClient strategy)
        {
            this._strategy = strategy;
        }

        public void SetStrategy(IWhoisClient strategy)
        {
            this._strategy = strategy;
        }

        public async Task<string> GetResponseJsonAsync(string host)
        {
            var result = await this._strategy.ResponceJson(host);
       
            if (!String.IsNullOrWhiteSpace(result))
            {   
                return result;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public async Task<WhoisResponseModel> GetResponseModelAsync(string host)
        {
            WhoisResponseModel result = await this._strategy.ResponceObject(host);

            if (result == null)
            {
                throw new NullReferenceException();
            }
            return result;
        }
    }
}
