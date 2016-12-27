﻿// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Testes
{
    using System;
    using System.Threading.Tasks;
    using Testing;
    using Testing.MessageObservers;
    using Util;


    public class BusTestConsumeObserver :
        IConsumeObserver
    {
        readonly ReceivedMessageList _messages;

        public BusTestConsumeObserver(TimeSpan timeout)
        {
            _messages = new ReceivedMessageList(timeout);
        }

        public IReceivedMessageList Messages => _messages;

        public Task PreConsume<T>(ConsumeContext<T> context) where T : class
        {
            return TaskUtil.Completed;
        }

        public Task PostConsume<T>(ConsumeContext<T> context) where T : class
        {
            _messages.Add(context);

            return TaskUtil.Completed;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
        {
            _messages.Add(context, exception);

            return TaskUtil.Completed;
        }
    }
}