using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Api.Contracts.Requests;
public class GetLastReleaseRequest
{
    public string Name { get; set; }
    public Domain.Models.FileRuntimeVersion? RuntimeVersion { get; set; }
}
