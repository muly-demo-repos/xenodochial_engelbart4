using Aaa.APIs.Common;
using Aaa.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
