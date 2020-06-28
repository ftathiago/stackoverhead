// <copyright file="ResponseDefault.cs" company="BlogDoFT">
// Copyright (c) BlogDoFT. All rights reserved.
// </copyright>

namespace StackOverHead.Web.Models
{
    public class ResponseDefault<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }
    }
}
