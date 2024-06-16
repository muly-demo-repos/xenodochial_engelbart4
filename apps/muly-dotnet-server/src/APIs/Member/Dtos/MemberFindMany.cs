using Microsoft.AspNetCore.Mvc;
using MulyDotnet.APIs.Common;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class MemberFindMany : FindManyInput<Member, MemberWhereInput> { }
