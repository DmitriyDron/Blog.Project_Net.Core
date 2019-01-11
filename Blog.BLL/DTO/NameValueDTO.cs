using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO
{
    public class NameValueDTO
    {
        public NameValueDTO(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}