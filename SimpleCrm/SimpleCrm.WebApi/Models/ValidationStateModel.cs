using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class ValidationStateModel
    {
        public string Messages { get; set; }
        public List<ValidationError> Errors { get; set; }
        public ValidationStateModel(ModelStateDictionary modelState)
        {
            var genericErrors = modelState.Keys
              .Where(key => string.IsNullOrWhiteSpace(key)) // model only errors have an empty key
              .Select(key => modelState[key].Errors.Select(x => x.ErrorMessage))
              .ToList();

            Messages = genericErrors.Count == 0 ? "Validation failed"
              : string.Join(".", genericErrors.Distinct());

            Errors = modelState.Keys
              .Where(key => !string.IsNullOrWhiteSpace(key))
              // some Linq magic ...
              .SelectMany(key => modelState[key].Errors
                  .Select(x => new ValidationError { Field = key, Message = x.ErrorMessage }))
              .ToList();
        }
    }


    
}

    