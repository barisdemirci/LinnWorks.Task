﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinnWorks.Task.FrontEnd.Services
{
    public interface IImportFileService
    {
        System.Threading.Tasks.Task UploadFile(IFormFileCollection files);
    }
}